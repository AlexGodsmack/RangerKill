using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveLighter : MonoBehaviour {

    public bool Activate;
    public GameObject Info;
    public GameObject Multiply;
    public TextMesh Lvl;
    public TextMesh Pow;
    public TextMesh Bul;
    public AudioSource Open;
    public AudioSource Close;
    public GameObject HealthBar;

    public void ShowInfo() {
        Info.active = true;
        //Multiply.active = true;
        HealthBar.transform.localPosition = new Vector3(HealthBar.transform.localPosition.x, HealthBar.transform.localPosition.y, -1.1f);
    }

    public void PlaySound() {
        Open.Play();
    }

    public void CloseSound() {
        Close.Play();
    }

    public void CloseInfo() {
        Info.active = false;
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (Activate == true) {
            this.GetComponent<Animator>().SetBool("Activation", true);
            Multiply.GetComponent<Animator>().SetBool("Activation", true);
            Lvl.text = this.transform.parent.GetComponent<SlaveProperties>().Level.ToString();
            Pow.text = this.transform.parent.GetComponent<SlaveProperties>().PowerOfShot.ToString();
            if (this.transform.parent.GetComponent<SlaveProperties>().WeaponXRef != null) {
                WeaponProperties wpn = this.transform.parent.GetComponent<SlaveProperties>().WeaponXRef.gameObject.GetComponent<WeaponProperties>();
                if (wpn.Bullets >= 10) {
                    Bul.text = wpn.Bullets.ToString();
                } else if (wpn.Bullets < 10) {
                    Bul.text = "0" + wpn.Bullets.ToString();
                }
            } else {
                Bul.text = "00";
            }
            //this.transform.parent.GetComponent<SlaveProperties>().ShowHealthbar = true;
        } else {
            this.GetComponent<Animator>().SetBool("Activation", false);
            Multiply.GetComponent<Animator>().SetBool("Activation", false);
            HealthBar.transform.localPosition = new Vector3(HealthBar.transform.localPosition.x, HealthBar.transform.localPosition.y, -0.1f);
            //this.transform.parent.GetComponent<SlaveProperties>().ShowHealthbar = false;
        }

    }
}
