using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SlaveProperties : MonoBehaviour {

    [Header("Features")]
    public int Number;
    public int Health;
    public int FullHealth;
    public int Damage;
    public int Accuracy;
    public int Battles;
    public int Level;
    public int Skin;
    public int Price;
    public float PowerOfShot;
    public float Distance;
    public int Efficiency;
    //public string[] Package;
    [Space]
    public bool isActive;
    [Space]
    public bool Bought;
    [Space]
    public bool HaveGun;
    [Space]
    public bool ShowHealthbar;
    [Space]
    public bool FullPackage;
    [Space]
    public bool IN_RUSH;
    [Space]
    public int WeaponSkin;

    [Header("Objects")]
    public GameObject WeaponXRef;
    [Space]
    public GameObject Lighter;
    public GameObject InventoryPack;
    public GameObject SlaveXRef;
    public GameObject Goal;
    public GameObject Healthbar;
    public GameObject HealthLineProgress;
    //public GameObject LevelIndicator;
    public GameObject Fire;
    public GameObject ShellContainer;
    [Header("Sounds")]
    public AudioSource[] WeaponSounds;
    public AudioSource Death;
    [Header("Materials")]
    public Material Additive;
    public Material Default;

    //public Sprite[] SkinImage;

    void Start()
    {


        //FullHealth = FullHealth * Level;
        //Damage = Damage * Level;
        //Accuracy = Accuracy * Level;

        Price = Health + Damage * Accuracy;

        this.GetComponent<Animator>().SetInteger("Skin", Skin);
        
    }

    void OnEnable() {
        this.GetComponent<Animator>().SetInteger("Skin", Skin);        
    }

    void Update()
    {
        if (WeaponXRef != null) {
            Efficiency = WeaponXRef.GetComponent<WeaponProperties>().Efficiency;
            WeaponSkin = WeaponXRef.GetComponent<WeaponProperties>().Skin;
            if (Goal != null) {
                if (IN_RUSH == false) {
                    Distance = Vector3.Distance(this.transform.position, Goal.transform.position);
                    PowerOfShot = Mathf.RoundToInt((Efficiency * Damage * Accuracy) / (Distance * 100));
                } else {
                    Distance = Vector3.Distance(this.transform.position, Goal.transform.position);
                    PowerOfShot = Mathf.RoundToInt(3 * ((Efficiency * Damage * Accuracy) / (Distance * 100)));
                }
            }
        } else {
            Efficiency = 0;
            WeaponSkin = 0;
        }

        if (Bought == false) {
            this.GetComponent<Animator>().SetBool("ThisPersonBought", false);
            this.GetComponent<SpriteRenderer>().material = Default;
            this.GetComponent<Animator>().SetInteger("WithWeapon", WeaponSkin);
            if (isActive == true) {
                Lighter.GetComponent<SlaveLighter>().Activate = true;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            } else {
                Lighter.GetComponent<SlaveLighter>().Activate = false;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        } else {
            this.GetComponent<Animator>().SetBool("ThisPersonBought", true);
            this.GetComponent<SpriteRenderer>().material = Additive;
            this.GetComponent<Animator>().SetInteger("WithWeapon", 0);
            Lighter.GetComponent<SlaveLighter>().Activate = false;
            if (isActive == true) {
                this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            } else {
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }

        if (ShowHealthbar == true) {
            Healthbar.active = true;
            if (Health >= 0) {
                HealthLineProgress.transform.localScale = new Vector3((float)Health / FullHealth, 1, 1);
            } else {
                HealthLineProgress.transform.localScale = new Vector3(0, 1, 1);
            }
        } else {
            Healthbar.active = false;
        }

        //LevelIndicator.GetComponent<TextMesh>().text = Level.ToString();
        if (WeaponSkin != 0) {
            Fire.GetComponent<Fire>().Skin = WeaponSkin;
        }

        //if (IN_RUSH == true) {

        //}
        //if (Input.GetMouseButtonDown(0)) {
        //    GotDamage();
        //}
        //if (Input.GetMouseButtonDown(1)) {
        //    this.GetComponent<Animator>().SetBool("Fire", true);
        //}
    }

    public void UpgradeLevel() {
        if (Battles < 3) {
            Level = 1;
        }
        if (Battles >= 3 && Battles < 9) {
            Level = 2;
        }
        if (Battles >= 9 && Battles < 16) {
            Level = 3;
        }
        if (Battles >= 16 && Battles < 25) {
            Level = 4;
        }
        if (Battles >= 25) {
            Level = 5;
        }
    }

    public void BackIdleState() {
        this.GetComponent<Animator>().SetBool("Fire", false);
        this.GetComponent<Animator>().SetBool("Damaged", false);
        Fire.GetComponent<Fire>().OnFire = false;
    }
    public void OnFire() {
        //this.GetComponent<Animator>().SetBool("Fire", true);
        Fire.GetComponent<Fire>().OnFire = true;
        GameObject Shell = Instantiate(Resources.Load("Shell_" + WeaponSkin.ToString())) as GameObject;
        Shell.transform.position = this.transform.position;
        ShellContainer = Shell;
        ShellContainer.GetComponent<BulletShell>().Parent = this.gameObject;
        WeaponSounds[WeaponSkin - 1].Play();
    }

    public void GotDamage() {
        this.GetComponent<Animator>().SetBool("Damaged", true);
        GameObject Bleed = Instantiate(Resources.Load("BloodDoll")) as GameObject;
        Bleed.transform.SetParent(this.transform);
        Bleed.transform.position = this.transform.position + new Vector3(0, 0, 0.1f);
        Bleed.GetComponent<Animator>().SetInteger("Skin", Random.Range(1, 4));
    }

    public void OnDeath() {
        Death.Play();
    }
}
