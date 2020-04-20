using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{

    public int Skin;
    //public Sprite[] FireSkin;
    //public Sprite[] FireFlareSkin;
    public Animator FireAnim;
    public Animator FlareAnim;
    public bool OnFire;

    void Start()
    {
        
    }

    void Update()
    {

        if (OnFire == true) {
            //this.GetComponent<SpriteRenderer>().enabled = true;
            //this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            //FireAnim.SetBool("Fire", true);
            FireAnim.SetInteger("Skin", Skin);
            FlareAnim.SetInteger("Skin", Skin);
            //this.GetComponent<SpriteRenderer>().sprite = FireSkin[Skin - 1];
            //this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = FireFlareSkin[Skin - 1];
        }
        
    }

    public void ResetSkin() {
        FireAnim.SetInteger("Skin", 0);
        FlareAnim.SetInteger("Skin", 0);
        OnFire = false;
    }

}
