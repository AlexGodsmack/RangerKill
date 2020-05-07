using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{

    public int TileID;
    public int Skin;
    public Sprite[] TileNumber;
    public GameObject[] SmokeOfWar = new GameObject[4];
    public Vector3 Coordinates;

    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = TileNumber[Skin];
    }

    void Update()
    {
        
    }
}
