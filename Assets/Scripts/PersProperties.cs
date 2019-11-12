using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PersProperties : MonoBehaviour
{

    public int Health;
    public int Damage;
    public int Level;
    public int Accuracy;
    public int CountOfBattles;
    public int Price;
    public int Skin;
    public int PositionOnField;
    public int NumberOfPersInInventory = 0;
    public bool Bought = false;
    public bool ShowHealthBar = false;
    public int WeaponInHands = 0;
    public int WeaponSkin;
    //public int CountStuffInPack = 0;
    public string[] Package;

    public GameObject HealthProgressBar;
    public GameObject HealthLine;

    public int HealthBar;

    public GameObject PersPack;

    public int PowerOfShot;

    //public Material Additive;

    public Sprite SkinSprite1;
    public Sprite SkinSprite2;
    public Sprite SkinSprite3;
    public Sprite SkinSprite4;
    public Sprite SkinSprite5;
    public Sprite SkinSprite6;

    public string StartHealthOfPers;

    private string StartHealthOfPersPath;

    private int NumPersParam = 8;
    private int NumWpnParam = 6;

    //Image MySkin;

    // Start is called before the first frame update
    void Start()
    {

        StartHealthOfPersPath = Application.persistentDataPath + "/" + StartHealthOfPers + ".txt";

        //MySkin = GetComponent<Image>();

        //if (Skin == 1) {
        //    MySkin.sprite = SkinSprite1;
        //}
        //if (Skin == 2) {
        //    MySkin.sprite = SkinSprite2;
        //}
        //if (Skin == 3) {
        //    MySkin.sprite = SkinSprite3;
        //}
        //if (Skin == 4) {
        //    MySkin.sprite = SkinSprite4;
        //}
        //if (Skin == 5) {
        //    MySkin.sprite = SkinSprite5;
        //}
        //if (Skin == 6) {
        //    MySkin.sprite = SkinSprite6;
        //}

        if (WeaponInHands > 0) {
            string[] InvSet = File.ReadAllLines(Application.persistentDataPath + "/InventorySettings.txt");
            string[] PlayerSet = File.ReadAllLines(Application.persistentDataPath + "/PlayerSource.txt");
            string[] CountOfAll = File.ReadAllLines(Application.persistentDataPath + "/CountOfAll.txt");

            int CountOfBoughtPers = int.Parse(CountOfAll[0]);
            int CountOfBoughtWeapon = int.Parse(CountOfAll[1]);

            int DamageOfWeapon  = int.Parse(PlayerSet[2 + CountOfBoughtPers * NumPersParam + WeaponInHands * NumWpnParam - NumWpnParam + 1]);
            int ConditionOfWeapon = int.Parse(PlayerSet[2 + CountOfBoughtPers * NumPersParam + WeaponInHands * NumWpnParam - NumWpnParam + 2]);

            WeaponSkin = int.Parse(PlayerSet[2 + CountOfBoughtPers * NumPersParam + WeaponInHands * NumWpnParam - NumWpnParam + 5]);

            PowerOfShot = Mathf.FloorToInt( 0.1f * Damage * Accuracy + 0.5f * ConditionOfWeapon * DamageOfWeapon + 300);
            PersPack.active = true;
            GameObject GetWeapon = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
            GetWeapon.transform.SetParent(PersPack.transform);
            GetWeapon.transform.localPosition = new Vector3(0, 0, -0.1f);
            //int WpnInInv = int.Parse(InvSet[21 + WeaponInHands]);
            GetWeapon.name = "Weapon" + WeaponInHands.ToString();
            GetWeapon.layer = 18;
            GetWeapon.GetComponent<WeaponProperties>().Damage = int.Parse(PlayerSet[2 + CountOfBoughtPers * NumPersParam + WeaponInHands * NumWpnParam - NumWpnParam + 1]);
            GetWeapon.GetComponent<WeaponProperties>().Condition = int.Parse(PlayerSet[2 + CountOfBoughtPers * NumPersParam + WeaponInHands * NumWpnParam - NumWpnParam + 2]);
            GetWeapon.GetComponent<WeaponProperties>().CountOfBullets = int.Parse(PlayerSet[2 + CountOfBoughtPers * NumPersParam + WeaponInHands * NumWpnParam - NumWpnParam + 3]);
            GetWeapon.GetComponent<WeaponProperties>().Price = int.Parse(PlayerSet[2 + CountOfBoughtPers * NumPersParam + WeaponInHands * NumWpnParam - NumWpnParam + 4]);
            GetWeapon.GetComponent<WeaponProperties>().Skin = int.Parse(PlayerSet[2 + CountOfBoughtPers * NumPersParam + WeaponInHands * NumWpnParam - NumWpnParam + 5]);
            GetWeapon.GetComponent<WeaponProperties>().NumberOfWeaponInInventory = WeaponInHands;
            PersPack.active = false;

        }

    }

        // Update is called once per frame
    void Update()
    {

        if (this.Bought == true) {
            this.GetComponent<Animator>().SetBool("ThisPersonBought", true);
            //this.GetComponent<SpriteRenderer>().material = Additive;
        }

        //if (WeaponSkin > 0)
        //{
        //    this.GetComponent<Animator>().SetInteger("WithWeapon", WeaponSkin);
        //}
        //else {
        this.GetComponent<Animator>().SetInteger("WithWeapon", WeaponSkin);
        //}

        if (ShowHealthBar == true) {
            HealthProgressBar.active = true;
            int GetFull = int.Parse(File.ReadAllLines(StartHealthOfPersPath)[NumberOfPersInInventory - 1]);
            float Progess = 1.0f * Health / GetFull;
            HealthLine.transform.localScale = new Vector3(Progess, 1, 1);
            if (Health <= 0) {
                Destroy(HealthLine);
                ShowHealthBar = false;
            }
        }

    }
}

