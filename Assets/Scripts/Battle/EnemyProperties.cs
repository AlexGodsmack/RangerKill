using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour {

    public int Skin;
    public int FullHealth;
    public int Health;
    public int Damage;
    public int Accuracy;
    public int Level;
    public int PowerOfShot;
    public int WeaponInHand;

    public int PlaceOnField;

    public GameObject HealthBarContainer;
    public GameObject HealthBar;
    public GameObject ShellContainer;
    public GameObject Lighter;
    public AudioSource[] WeaponSound;
    public AudioSource Death;

    public bool isActive;

    void Start() {

        FullHealth = Health = Health * Level;
        Damage = Damage * Level;
        Accuracy = Accuracy * Level;
        PowerOfShot = Damage + Accuracy;
        //LevelIndicator.GetComponent<TextMesh>().text = Level.ToString();
        this.GetComponent<Animator>().SetInteger("Clan", 1);
        this.GetComponent<Animator>().SetInteger("Skin", Skin);
        this.GetComponent<Animator>().SetInteger("Weapon", WeaponInHand);

    }

    void OnEnable() {


    }

    void Update() {

        if (Health >= 0) {
            HealthBar.transform.localScale = new Vector3((float)Health / (float)FullHealth, 1, 1);
            HealthBarContainer.active = true;
            if (isActive == true) {
                Lighter.GetComponent<Animator>().SetBool("Activation", true);
                this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            } else {
                Lighter.GetComponent<Animator>().SetBool("Activation", false);
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        } else {
            HealthBar.transform.localScale = new Vector3(0, 1, 1);
            HealthBarContainer.active = false;
        }

    }

    public void Damaged() {
        GameObject Bleed = Instantiate(Resources.Load("BloodDoll")) as GameObject;
        Bleed.transform.SetParent(this.transform);
        Bleed.transform.position = this.transform.position + new Vector3(0, 0, 0.1f);
        Bleed.transform.localScale = new Vector3(-1, 1, 1);
        Bleed.GetComponent<Animator>().SetInteger("Skin", Random.Range(1, 4));
        this.GetComponent<Animator>().SetBool("Damaged", true);
    }

    public void BackToIdle() {
        this.GetComponent<Animator>().SetBool("Fire", false);
        this.GetComponent<Animator>().SetBool("Damaged", false);
    }

    public void OnFire() {
        GameObject Shell = Instantiate(Resources.Load("Shell_" + Skin.ToString())) as GameObject;
        Shell.transform.position = this.transform.position;
        ShellContainer = Shell;
        ShellContainer.GetComponent<BulletShell>().Parent = this.gameObject;
        WeaponSound[Skin - 1].Play();
    }

    public void OnDeath() {
        Death.Play();
    }

}
