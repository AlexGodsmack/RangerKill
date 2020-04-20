using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherStuff : MonoBehaviour
{

    public int Skin;
    public string Description;
    public string ShortDescription;
    public int Number;

    public bool isActive;
    public bool Bought;
    public int Price;

    public int Liters;
    public bool IfNewWater;

    public Sprite[] SkinNum;
    public Sprite[] SkinBoughtNum;

    public Material Default;
    public Material Additive;

    public GameObject StuffXRef;

    void Start()
    {
        if (Bought == true) {
            this.GetComponent<SpriteRenderer>().sprite = SkinBoughtNum[Skin - 1];
        } else {
            this.GetComponent<SpriteRenderer>().sprite = SkinNum[Skin - 1];
        }

        if (Skin == 1) {
            Description = "Medicine chest. \n\nHeals your slaves \nup to 100% of health";
            ShortDescription = "Medicine\nchest.\n\nHeals your\nslaves\nup to 100%\nof health";
            Price = 200;
        }
        if (Skin == 2) {
            if (IfNewWater == true) {
                Liters = 100;
            }
            Description = "Water. \n\nNeed for move on map \nLiters: " + Liters.ToString();
            ShortDescription = "Water.\n\nNeed for\nmove on map\nLiters: " + Liters.ToString();
            Price = 100;
        }
        if (Skin == 3) {
            Description = "Buff. \n\nIncreases power of shot \nto 300% due one pass";
            ShortDescription = "Buff.\n\nIncreases\npower\nof shot\nto 300% due\none pass";
            Price = 150;
        }
        if (Skin == 4) {
            Description = "Money";
            ShortDescription = "Money";
        }

    }
    void OnEnable() {
        if (Skin == 1) {
            Description = "Medicine chest. \n\nHeals your slaves \nup to 100% of health";
            ShortDescription = "Medicine\nchest.\n\nHeals your\nslaves\nup to 100%\nof health";
            Price = 200;
        }
        if (Skin == 2) {
            if (IfNewWater == true) {
                Liters = 100;
            }
            Description = "Water. \n\nNeed for move on map \nLiters: " + Liters.ToString();
            ShortDescription = "Water.\n\nNeed for\nmove on map\nLiters: " + Liters.ToString();
            Price = 100;
        }
        if (Skin == 3) {
            Description = "Buff. \n\nIncreases power of shot \nto 300% due one pass";
            ShortDescription = "Buff.\n\nIncreases\npower\nof shot\nto 300% due\none pass";
            Price = 150;
        }
        if (Skin == 4) {
            Description = "Money";
            ShortDescription = "Money";
        }
    }

    void Update()
    {

        if (Bought == true) {
            this.GetComponent<SpriteRenderer>().material = Additive;
            this.GetComponent<SpriteRenderer>().sprite = SkinBoughtNum[Skin - 1];
        } else {
            this.GetComponent<SpriteRenderer>().material = Default;
            this.GetComponent<SpriteRenderer>().sprite = SkinNum[Skin - 1];
        }

        if (isActive == true) {
            if (Bought == true) {
                if (this.transform.Find("Lighter") != null) {
                    this.transform.Find("Lighter").gameObject.active = false;
                }
                this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            } else {
                if (this.transform.Find("Lighter") != null) {
                    this.transform.Find("Lighter").gameObject.active = true;
                }
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        } else {
            if (Bought == true) {
                if (this.transform.Find("Lighter") != null) {
                    this.transform.Find("Lighter").gameObject.active = false;
                }
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            } else {
                if (this.transform.Find("Lighter") != null) {
                    this.transform.Find("Lighter").gameObject.active = false;
                }
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
        
    }
}
