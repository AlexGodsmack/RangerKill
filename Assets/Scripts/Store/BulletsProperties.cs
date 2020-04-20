using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsProperties : MonoBehaviour
{

    public int Skin;
    public string Name;
    public int Count;
    public int Price;

    public Sprite[] NumSkin;
    public Sprite[] NumBoughtSkin;

    public bool Bought;
    public bool isActive;

    public Material Default;
    public Material Additive;

    public TextAsset Data;

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

        Price = int.Parse(GetData[5 * Skin - 1]);

        for (int i = 1; i < 11; i++) {
            if (Skin == i) {
                Name = GetData[5 * (Skin - 1)] + " " + GetData[5 * Skin - 2];
            }
        }
        
    }

    void Update()
    {

        if (isActive == false) {
            if (Bought == false) {
                this.transform.GetChild(0).gameObject.active = false;
            } else {
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        } else {
            if (Bought == false) {
                this.transform.GetChild(0).gameObject.active = true;
            } else {
                this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }

    }
}
