using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

    public int NumberOfTile;

    public bool HaveSmoke;

    public GameObject Smoke;

    public Sprite Tile1;
    public Sprite Tile2;
    public Sprite Tile3;
    public Sprite Tile4;
    public Sprite Tile5;

    // Start is called before the first frame update
    void Start()
    {

        if (NumberOfTile == 1) {
            this.GetComponent<SpriteRenderer>().sprite = Tile1;
        }
        if (NumberOfTile == 2) {
            this.GetComponent<SpriteRenderer>().sprite = Tile2;
        }
        if (NumberOfTile == 3) {
            this.GetComponent<SpriteRenderer>().sprite = Tile3;
        }
        if (NumberOfTile == 4) {
            this.GetComponent<SpriteRenderer>().sprite = Tile4;
        }
        if (NumberOfTile == 5) {
            this.GetComponent<SpriteRenderer>().sprite = Tile5;
        }

        if (HaveSmoke == false)
        {
            Destroy(Smoke);
        }

        
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
