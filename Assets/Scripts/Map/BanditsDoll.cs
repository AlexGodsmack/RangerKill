using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditsDoll : MonoBehaviour
{

    public string Clan;
    public int Coverage;
    public int Population;
    public int Number;

    public Collider2D[] Colliders;
    public Sprite[] Truman;
    public Sprite[] Knackers;
    public Sprite[] Horde;

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

        this.Colliders[Coverage - 1].enabled = true;

    }

    void Update()
    {
        
    }
}
