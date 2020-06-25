using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsProperties : MonoBehaviour
{
    [Header("Features")]
    public int Skin;
    public string Name;
    public int Count;
    public int Price;
    [Header("Skins")]
    public Sprite[] NumSkin;
    public Sprite[] NumBoughtSkin;
    [Header("Conditions")]
    public bool Bought;
    public bool isActive;
    [Header("Materials")]
    public Material Default;
    public Material Additive;
    [Header("Info source")]
    public TextAsset Data;
    [Header("Objects")]
    public GameObject Lighter;

    void Start()
    {

        string[] GetData = Data.text.Split('\n');

        if (Bought == false) {
            this.GetComponent<SpriteRenderer>().sprite = NumSkin[Skin - 1];
            this.GetComponent<SpriteRenderer>().material = Default;
        } else {
            this.GetComponent<SpriteRenderer>().sprite = NumBoughtSkin[Skin - 1];
            this.GetComponent<SpriteRenderer>().material = Additive;
        }

        //Price = int.Parse(GetData[5 * Skin - 1]);

        //for (int i = 1; i < 11; i++) {
        //    if (Skin == i) {
        //        Name = GetData[5 * (Skin - 1)] + " " + GetData[5 * Skin - 2];
        //    }
        //}
        
    }

    void Update()
    {

        if (isActive == false) {
            if (Bought == false) {
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            } else {
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        } else {
            if (Bought == false) {
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            } else {
                this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }

    }
}
