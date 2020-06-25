using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditsDoll : MonoBehaviour
{

    public string Clan;
    public int Coverage;
    public int Population;
    public int Number;
    public int Attacks;

    public Collider2D[] Colliders;
    public Sprite[] Truman;
    public Sprite[] Knackers;
    public Sprite[] Horde;
    public Sprite WaterTower;

    void Start()
    {
        
        if(Clan == "Trumans'") {
            this.GetComponent<SpriteRenderer>().sprite = Truman[Coverage - 1];
        }
        if (Clan == "Knackers") {
            this.GetComponent<SpriteRenderer>().sprite = Knackers[Coverage - 1];
        }
        if (Clan == "Horde") {
            this.GetComponent<SpriteRenderer>().sprite = Horde[Coverage - 1];
        }
        if (Clan == "Water Tower") {
            this.GetComponent<SpriteRenderer>().sprite = WaterTower;
        }

        //if (Clan != "") {
        //    //this.Colliders[Coverage - 1].enabled = true;
        //} else {
        //    this.GetComponent<SpriteRenderer>().sprite = Truman[Coverage - 1];
        //    foreach (Collider2D col in this.Colliders) {
        //        col.enabled = false;
        //    }
        //}

    }

    void Update()
    {
        
    }
}
