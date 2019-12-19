using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{

    public int BulletsSkin;
    public int CountOfBullets;
    public int Price;
    public int ClipOfWeapon;
    public string Name;
    public bool Bought = false;
    public Material Default;
    public Material Additive;

    public Sprite skin1;
    public Sprite skin2;
    public Sprite skin3;
    public Sprite skin4;
    public Sprite skin5;
    public Sprite skin6;
    public Sprite skin7;
    public Sprite skin8;
    public Sprite skin9;
    public Sprite skin10;

    public Sprite Boughtskin1;
    public Sprite Boughtskin2;
    public Sprite Boughtskin3;
    public Sprite Boughtskin4;
    public Sprite Boughtskin5;
    public Sprite Boughtskin6;
    public Sprite Boughtskin7;
    public Sprite Boughtskin8;
    public Sprite Boughtskin9;
    public Sprite Boughtskin10;

    // Start is called before the first frame update
    void Start()
    {

        if (BulletsSkin == 1) {
            this.GetComponent<SpriteRenderer>().sprite = skin1;
            Price = 10;
            Name = "AK-45";
            ClipOfWeapon = 15;

        }
        if (BulletsSkin == 2) {
            this.GetComponent<SpriteRenderer>().sprite = skin2;
            Price = 7;
            Name = "M-16";
            ClipOfWeapon = 20;

        }
        if (BulletsSkin == 3) {
            this.GetComponent<SpriteRenderer>().sprite = skin3;
            Price = 3;
            Name = "DE-25";
            ClipOfWeapon = 12;

        }
        if (BulletsSkin == 4) {
            this.GetComponent<SpriteRenderer>().sprite = skin4;
            Price = 12;
            Name = "G-61";
            ClipOfWeapon = 25;

        }
        if (BulletsSkin == 5) {
            this.GetComponent<SpriteRenderer>().sprite = skin5;
            Price = 15;
            Name = "P-11";
            ClipOfWeapon = 12;

        }
        if (BulletsSkin == 6) {
            this.GetComponent<SpriteRenderer>().sprite = skin6;
            Price = 16;
            Name = "K-100";
            ClipOfWeapon = 24;

        }
        if (BulletsSkin == 7) {
            this.GetComponent<SpriteRenderer>().sprite = skin7;
            Price = 8;
            Name = "D-BG";
            ClipOfWeapon = 9;

        }
        if (BulletsSkin == 8) {
            this.GetComponent<SpriteRenderer>().sprite = skin8;
            Price = 20;
            Name = "Riffle";
            ClipOfWeapon = 5;

        }
        if (BulletsSkin == 9) {
            this.GetComponent<SpriteRenderer>().sprite = skin9;
            Price = 18;
            Name = "PMG-1";
            ClipOfWeapon = 2;

        }
        if (BulletsSkin == 10) {
            this.GetComponent<SpriteRenderer>().sprite = skin10;
            Price = 30;
            Name = "RPG-3";
            ClipOfWeapon = 1;

        }

    }

    void Update()
    {
        if (Bought == true) {
            if (BulletsSkin == 1) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin1;
            }
            if (BulletsSkin == 2) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin2;
            }
            if (BulletsSkin == 3) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin3;
            }
            if (BulletsSkin == 4) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin4;
            }
            if (BulletsSkin == 5) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin5;
            }
            if (BulletsSkin == 6) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin6;
            }
            if (BulletsSkin == 7) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin7;
            }
            if (BulletsSkin == 8) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin8;
            }
            if (BulletsSkin == 9) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin9;
            }
            if (BulletsSkin == 10) {
                this.GetComponent<SpriteRenderer>().sprite = Boughtskin10;
            }

            this.GetComponent<SpriteRenderer>().material = Additive;

        } else {
            if (BulletsSkin == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin1;
            }
            if (BulletsSkin == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin2;
            }
            if (BulletsSkin == 3)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin3;
            }
            if (BulletsSkin == 4)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin4;
            }
            if (BulletsSkin == 5)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin5;
            }
            if (BulletsSkin == 6)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin6;
            }
            if (BulletsSkin == 7)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin7;
            }
            if (BulletsSkin == 8)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin8;
            }
            if (BulletsSkin == 9)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin9;
            }
            if (BulletsSkin == 10)
            {
                this.GetComponent<SpriteRenderer>().sprite = skin10;
            }

            this.GetComponent<SpriteRenderer>().material = Default;

        }
    }
}
