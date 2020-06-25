using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsLighter : MonoBehaviour
{

    public GameObject Info;
    public GameObject Multiply;
    public TextMesh Name;
    public TextMesh Count;
    public AudioSource Activate;
    public AudioSource Deactivate;

    void Update()
    {
        if (this.transform.parent.gameObject.GetComponent<BulletsProperties>() != null) {
            if (this.transform.parent.GetComponent<BulletsProperties>().isActive == true) {
                this.GetComponent<Animator>().SetBool("Activation", true);
                if (Multiply != null) {
                    Multiply.GetComponent<Animator>().SetBool("Activation", true);
                }
            } else {
                this.GetComponent<Animator>().SetBool("Activation", false);
                if (Multiply != null) {
                    Multiply.GetComponent<Animator>().SetBool("Activation", false);
                }
            }
        } else if(this.transform.parent.gameObject.GetComponent<OtherStuff>() != null){
            if (this.transform.parent.GetComponent<OtherStuff>().Bought == false) {
                if (this.transform.parent.GetComponent<OtherStuff>().isActive == true) {
                    this.GetComponent<Animator>().SetBool("Activation", true);
                    if (Multiply != null) {
                        Multiply.GetComponent<Animator>().SetBool("Activation", true);
                    }
                } else {
                    this.GetComponent<Animator>().SetBool("Activation", false);
                    if (Multiply != null) {
                        Multiply.GetComponent<Animator>().SetBool("Activation", false);
                    }
                }
            } else {
                this.GetComponent<Animator>().SetBool("Activation", false);
                if (Multiply != null) {
                    Multiply.GetComponent<Animator>().SetBool("Activation", false);
                }
            }
        }

    }

    public void ShowSound() {
        Activate.Play();
    }
    public void CloseSound() {
        Deactivate.Play();
    }

    public void ShowInfo() {
        Info.active = true;
        if (this.transform.parent.GetComponent<BulletsProperties>() != null) {
            Name.text = this.transform.parent.GetComponent<BulletsProperties>().Name;
            int cnt = this.transform.parent.GetComponent<BulletsProperties>().Count;
            Count.text = "";
            for (int a = 0; a < cnt; a++) {
                Count.text += "i";
            }
        } else if (this.transform.parent.GetComponent<OtherStuff>() != null) {
            Name.text = this.transform.parent.GetComponent<OtherStuff>().Name;
            int cnt = this.transform.parent.GetComponent<OtherStuff>().Liters;
            if (this.transform.parent.GetComponent<OtherStuff>().Skin == 2) {
                Count.text = "Liters: " + cnt.ToString();
            } else {
                Count.text = "";
            }
        }
    }
    public void CloseInfo() {
        Info.active = false;
    }
}
