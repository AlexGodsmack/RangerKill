using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WeaponProperties : MonoBehaviour
{

    public int Damage;
    public int Condition;
    public int CountOfBullets;
    public int Price;
    public int Skin;
    public bool Bought = false;
    public Material Additive;
    public Material Default;
    public string Name;
    public int NumberOfWeaponInInventory;
    public int PositionOnField;
    public bool InHands = false;
    public bool IsActive = false;

    public Sprite Skin1;
    public Sprite Skin2;
    public Sprite Skin3;
    public Sprite Skin4;
    public Sprite Skin5;
    public Sprite Skin6;
    public Sprite Skin7;
    public Sprite Skin8;
    public Sprite Skin9;
    public Sprite Skin10;

    public Sprite BoughtSkin1;
    public Sprite BoughtSkin2;
    public Sprite BoughtSkin3;
    public Sprite BoughtSkin4;
    public Sprite BoughtSkin5;
    public Sprite BoughtSkin6;
    public Sprite BoughtSkin7;
    public Sprite BoughtSkin8;
    public Sprite BoughtSkin9;
    public Sprite BoughtSkin10;

    // Start is called before the first frame update
    void Start()
    {

        if (Skin == 1) {
            Damage = 10;
            CountOfBullets = 15;
            Price = Damage * Condition + 15;
            this.GetComponent<SpriteRenderer>().sprite = Skin1;
            Name = "AK-45";
        }
        if (Skin == 2) {
            Damage = 30;
            CountOfBullets = 20;
            Price = Damage * Condition + 25;
            this.GetComponent<SpriteRenderer>().sprite = Skin2;
            Name = "M-16";
        }
        if (Skin == 3) {
            Damage = 40;
            CountOfBullets = 12;
            Price = Damage * Condition + 35;
            this.GetComponent<SpriteRenderer>().sprite = Skin3;
            Name = "Desert-25";
        }
        if (Skin == 4) {
            Damage = 25;
            CountOfBullets = 25;
            Price = Damage * Condition + 20;
            this.GetComponent<SpriteRenderer>().sprite = Skin4;
            Name = "G-61";
        }
        if (Skin == 5) {
            Damage = 20;
            CountOfBullets = 12;
            Price = Damage * Condition + 15;
            this.GetComponent<SpriteRenderer>().sprite = Skin5;
            Name = "P-11";
        }
        if (Skin == 6) {
            Damage = 15;
            CountOfBullets = 24;
            Price = Damage * Condition + 10;
            this.GetComponent<SpriteRenderer>().sprite = Skin6;
            Name = "K-100";
        }
        if (Skin == 7) {
            Damage = 35;
            CountOfBullets = 9;
            Price = Damage * Condition + 20;
            this.GetComponent<SpriteRenderer>().sprite = Skin7;
            Name = "2-barreled gun";
        }
        if (Skin == 8) {
            Damage = 45;
            CountOfBullets = 5;
            Price = Damage * Condition + 40;
            this.GetComponent<SpriteRenderer>().sprite = Skin8;
            Name = "Riffle";
        }
        if (Skin == 9) {
            Damage = 50;
            CountOfBullets = 2;
            Price = Damage * Condition + 60;
            this.GetComponent<SpriteRenderer>().sprite = Skin9;
            Name = "PMG-1";
        }
        if (Skin == 10) {
            Damage = 65;
            CountOfBullets = 1;
            Price = Damage * Condition + 80;
            this.GetComponent<SpriteRenderer>().sprite = Skin10;
            Name = "RPG-3";
        }

        //Debug.Log(Name);

    }

    // Update is called once per frame
    void Update()
    {
        if (Bought == true)
        {

            if (Skin == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin1;
            }
            if (Skin == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin2;
            }
            if (Skin == 3)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin3;
            }
            if (Skin == 4)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin4;
            }
            if (Skin == 5)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin5;
            }
            if (Skin == 6)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin6;
            }
            if (Skin == 7)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin7;
            }
            if (Skin == 8)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin8;
            }
            if (Skin == 9)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin9;
            }
            if (Skin == 10)
            {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin10;
            }
            this.GetComponent<SpriteRenderer>().sharedMaterial = Additive;

        }
        else {
            if (Skin == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin1;
            }
            if (Skin == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin2;
            }
            if (Skin == 3)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin3;
            }
            if (Skin == 4)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin4;
            }
            if (Skin == 5)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin5;
            }
            if (Skin == 6)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin6;
            }
            if (Skin == 7)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin7;
            }
            if (Skin == 8)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin8;
            }
            if (Skin == 9)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin9;
            }
            if (Skin == 10)
            {
                this.GetComponent<SpriteRenderer>().sprite = Skin10;
            }
            this.GetComponent<SpriteRenderer>().sharedMaterial = Default;
        }
        if (IsActive == true)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        else {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        if (this.transform.parent.name == "PersPackCage")
        {
            this.gameObject.layer = 18;
        }
        else if (this.transform.parent.name == "WpnBoughtPanel")
        {
            this.gameObject.layer = 17;
        }
        else {
            this.gameObject.layer = 9;
        }
    }
}
