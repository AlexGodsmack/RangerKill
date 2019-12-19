using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExportRemains : MonoBehaviour
{

    public string[] Pers;
    public string[] Stuff;
    public string[] Weapon;

    public GameObject PersContainer;
    public GameObject StuffContainer;

    public GameObject Timer;

    void Start()
    {

        for (int i = 0; i < Pers.Length; i++) {
            Pers[i] = "None";
        }
        for (int i = 0; i < Stuff.Length; i++) {
            Stuff[i] = "None";
        }
        for (int i = 0; i < Weapon.Length; i++) {
            Weapon[i] = "None";
        }
        
    }

    void Update()
    {

        if (Timer.GetComponent<Timer>().TimerCount == 11) {

            for (int i = 0; i < Pers.Length; i++) {
                Pers[i] = "None";
            }
            for (int i = 0; i < Stuff.Length; i++) {
                Stuff[i] = "None";
            }
            for (int i = 0; i < Weapon.Length; i++) {
                Weapon[i] = "None";
            }

            for (int a = 0; a < PersContainer.transform.childCount; a++) {
                Pers[a] = PersContainer.transform.GetChild(a).gameObject.name;
            }

            int Weaps = 0;
            int Stuffs = 0;

            for (int a = 0; a < StuffContainer.transform.childCount; a++) {
                if (StuffContainer.transform.GetChild(a).gameObject.GetComponent<WeaponProperties>() != null) {
                    Weapon[Weaps] = StuffContainer.transform.GetChild(a).gameObject.name;
                    Weaps = Weaps + 1;
                }
                if (StuffContainer.transform.GetChild(a).gameObject.GetComponent<OtherStuff>() != null) {
                    Stuff[Stuffs] = StuffContainer.transform.GetChild(a).gameObject.name;
                    Stuffs = Stuffs + 1;
                }
            }
        }
        
    }
}
