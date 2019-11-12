using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class OtherStuff : MonoBehaviour
{

    public int Skin;
    public Material Additive;
    public Material Default;

    public Sprite Skin1;
    public Sprite Skin2;
    public Sprite Skin3;

    public Sprite SkinBought1;
    public Sprite SkinBought2;
    public Sprite SkinBought3;

    public int Price;

    public string Name;
    public string Description;

    public int NumberOfStuffInInv;
    public int PositionOnField;
    public int WhichPersUseIt;

    public bool Bought = false;

    public bool IsActive;

    void Start()
    {

        if (Skin == 1) {
            this.GetComponent<SpriteRenderer>().sprite = Skin1;
            Price = 250;
            Name = "Medicine";
            Description = "Medicine" + "\n\nHeals your slaves" + "\n\nUp health to 100%";
        }

        if (Skin == 2) {
            this.GetComponent<SpriteRenderer>().sprite = Skin2;
            Price = 100;
            Name = "Water";
            Description = "Water" + "\n\nNeed for move" + "\n\nGives 100 Steps";
        }

        if (Skin == 3) {
            this.GetComponent<SpriteRenderer>().sprite = Skin3;
            Price = 300;
            Name = "Buff";
            Description = "Buff" + "\n\nIncreases Damage" + "\n\nUp damage of your slave on x3 at current turn";
        }

    }

    void Update()
    {

        if (Bought == true)
        {
            if (Skin == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = SkinBought1;
            }
            if (Skin == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = SkinBought2;
            }
            if (Skin == 3)
            {
                this.GetComponent<SpriteRenderer>().sprite = SkinBought3;
            }
            this.GetComponent<SpriteRenderer>().material = Additive;
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
            this.GetComponent<SpriteRenderer>().material = Default;
        }

        if (IsActive == true)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

    }
}
