using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLighter : MonoBehaviour
{

    public GameObject Multiply;
    public GameObject Info;
    public TextMesh Damage;
    public TextMesh Condition;
    public TextMesh Name;
    public TextMesh Bullets;

    public bool Activate;

    void Start()
    {
        
    }

    void Update()
    {

        if (Activate == true) {
            this.GetComponent<Animator>().SetBool("Activation", true);
            Multiply.GetComponent<Animator>().SetBool("Activation", true);
        } else {
            this.GetComponent<Animator>().SetBool("Activation", false);
            Multiply.GetComponent<Animator>().SetBool("Activation", false);
        }
        
    }

    public void OpenInfo() {
        Info.active = true;
        WeaponProperties GetInfo = this.transform.parent.GetComponent<WeaponProperties>();

        Damage.text = GetInfo.Damage.ToString();
        Name.text = GetInfo.WeapName;
        Bullets.text = GetInfo.Bullets.ToString();
        Condition.text = "";
        for (int a = 1; a < GetInfo.Condition + 1; a++) {
            Condition.text += "0";
        }
    }

    public void CloseInfo() {
        Info.active = false;
    }

}
