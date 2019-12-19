using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LootScreen : MonoBehaviour
{

    public int KindOfLoot;
    public int Skin;

    public Sprite Weap1;
    public Sprite Weap2;
    public Sprite Weap3;
    public Sprite Weap4;
    public Sprite Weap5;
    public Sprite Weap6;
    public Sprite Weap7;
    public Sprite Weap8;
    public Sprite Weap9;
    public Sprite Weap10;

    public Sprite Bullet1;
    public Sprite Bullet2;
    public Sprite Bullet3;
    public Sprite Bullet4;
    public Sprite Bullet5;
    public Sprite Bullet6;
    public Sprite Bullet7;
    public Sprite Bullet8;
    public Sprite Bullet9;
    public Sprite Bullet10;

    public Sprite Stuff1;
    public Sprite Stuff2;
    public Sprite Stuff3;

    public Sprite Money;
    public int MoneyLoot;

    void Start()
    {
        if (KindOfLoot == 0) {
            if (Skin == 1) {
                this.GetComponent<Image>().sprite = Bullet1;
            }
            if (Skin == 2) {
                this.GetComponent<Image>().sprite = Bullet2;
            }
            if (Skin == 3) {
                this.GetComponent<Image>().sprite = Bullet3;
            }
            if (Skin == 4) {
                this.GetComponent<Image>().sprite = Bullet4;
            }
            if (Skin == 5) {
                this.GetComponent<Image>().sprite = Bullet5;
            }
            if (Skin == 6) {
                this.GetComponent<Image>().sprite = Bullet6;
            }
            if (Skin == 7) {
                this.GetComponent<Image>().sprite = Bullet7;
            }
            if (Skin == 8) {
                this.GetComponent<Image>().sprite = Bullet8;
            }
            if (Skin == 9) {
                this.GetComponent<Image>().sprite = Bullet9;
            }
            if (Skin == 10) {
                this.GetComponent<Image>().sprite = Bullet10;
            }
        }

        if (KindOfLoot == 1) {
            if (Skin == 1) {
                this.GetComponent<Image>().sprite = Weap1;
            }
            if (Skin == 2) {
                this.GetComponent<Image>().sprite = Weap2;
            }
            if (Skin == 3) {
                this.GetComponent<Image>().sprite = Weap3;
            }
            if (Skin == 4) {
                this.GetComponent<Image>().sprite = Weap4;
            }
            if (Skin == 5) {
                this.GetComponent<Image>().sprite = Weap5;
            }
            if (Skin == 6) {
                this.GetComponent<Image>().sprite = Weap6;
            }
            if (Skin == 7) {
                this.GetComponent<Image>().sprite = Weap7;
            }
            if (Skin == 8) {
                this.GetComponent<Image>().sprite = Weap8;
            }
            if (Skin == 9) {
                this.GetComponent<Image>().sprite = Weap9;
            }
            if (Skin == 10) {
                this.GetComponent<Image>().sprite = Weap10;
            }
        }

        if (KindOfLoot == 2) {
            if (Skin == 1) {
                this.GetComponent<Image>().sprite = Stuff1;
            }
            if (Skin == 2) {
                this.GetComponent<Image>().sprite = Stuff2;
            }
            if (Skin == 3) {
                this.GetComponent<Image>().sprite = Stuff3;
            }
        }

        if (KindOfLoot == 3) {
            this.GetComponent<Image>().sprite = Money;
        }

    }

    void Update()
    {
        
    }
}
