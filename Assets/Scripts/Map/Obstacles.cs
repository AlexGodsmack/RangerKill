using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [Header("Numbers")]
    public int Skin;
    [Header("Sprites")]
    public Sprite[] Skin_Img;
    [Header("Colliders")]
    public Collider2D[] Coll;

    void Start()
    {

        this.GetComponent<SpriteRenderer>().sprite = Skin_Img[Skin - 1];
        Coll[Skin - 1].enabled = true;
        
    }

    void Update()
    {
        
    }
}
