using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreChip : MonoBehaviour
{

    public string TypeOfStore;
    public int StoreID;
    public Sprite[] Skin;

    void Start()
    {
        if (TypeOfStore == "Slaves") {
            this.GetComponent<SpriteRenderer>().sprite = Skin[0];
        }
        if (TypeOfStore == "Guns") {
            this.GetComponent<SpriteRenderer>().sprite = Skin[1];
        }
        if (TypeOfStore == "Bullets") {
            this.GetComponent<SpriteRenderer>().sprite = Skin[2];
        }
        if (TypeOfStore == "Stuff") {
            this.GetComponent<SpriteRenderer>().sprite = Skin[3];
        }
        if (TypeOfStore == "Recycling") {
            this.GetComponent<SpriteRenderer>().sprite = Skin[4];
        }
    }

    void Update()
    {
        
    }

    void OnEnable() {
    }
}
