using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class WeaponProperties : MonoBehaviour
{

    public string WeapName;
    public int Number;
    public int Damage;
    public int Condition;
    public int Skin;
    public int Bullets;
    public int Efficiency;
    public int Price;

    public bool isActive;
    public bool Bought;

    public Sprite[] NumSkin; // SKINS

    public Sprite[] BoughtSkin; // SKINS FOR BOUGHT

    public Material Default;
    public Material Additive;

    public TextAsset Data;

    public GameObject WeaponXRef;
    public GameObject Lighter;

    void Start()
    {

        string[] GetData = Data.text.Split('\n');

        if (WeapName == "") {
            this.WeapName = GetData[3 * (this.Skin - 1)];
            this.WeapName = this.WeapName.Substring(0, this.WeapName.Length - 1);
            this.Damage = int.Parse(GetData[3 * this.Skin - 2]);
            this.Price = this.Condition * int.Parse(GetData[3 * this.Skin - 1]);
        }

    }

    void OnDisable() {
        Efficiency = Damage * Condition;
    }
    void OnEnable() {
        Efficiency = Damage * Condition;    
    }

    void Update()
    {

        Efficiency = Damage * Condition;

        if (Skin != 0) {
            if (Bought == false) {
                this.GetComponent<SpriteRenderer>().sprite = NumSkin[Skin - 1];
                this.GetComponent<SpriteRenderer>().material = Default;
            } else {
                this.GetComponent<SpriteRenderer>().sprite = BoughtSkin[Skin - 1];
                this.GetComponent<SpriteRenderer>().material = Additive;
            }
        }

        if (isActive == true) {
            if (Bought == false) {
                Lighter.GetComponent<WeaponLighter>().Activate = true;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            } else {
                Lighter.GetComponent<WeaponLighter>().Activate = false;
                this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        } else {
            if (Bought == false) {
                Lighter.GetComponent<WeaponLighter>().Activate = false;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            } else {
                Lighter.GetComponent<WeaponLighter>().Activate = false;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }
}
