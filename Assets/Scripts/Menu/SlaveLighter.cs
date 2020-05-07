using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveLighter : MonoBehaviour
{

    public bool Activate;
    public GameObject Info;
    public GameObject Multiply;
    public TextMesh Lvl;
    public TextMesh Pow;

    public void ShowInfo() {
        Info.active = true;
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
            this.transform.parent.GetComponent<SlaveProperties>().ShowHealthbar = true;
        } else {
            this.GetComponent<Animator>().SetBool("Activation", false);
            Multiply.GetComponent<Animator>().SetBool("Activation", false);
            this.transform.parent.GetComponent<SlaveProperties>().ShowHealthbar = false;
        }

    }
}
