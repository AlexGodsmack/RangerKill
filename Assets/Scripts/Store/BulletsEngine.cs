using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsEngine : MonoBehaviour
{

    public Transform BackGround;
    public GameObject BackAll;
    [Header("Anchors")]
    public GameObject CenterAnchor;
    public GameObject MidUpAnchor;
    public GameObject LeftTopAnchor;
    public GameObject RightUpAnchor;
    [Header("Objects")]
    public GameObject SlavesSource;
    public GameObject ItemSource;
    public GameObject BoughtItems;
    public GameObject LeftButton;
    public GameObject RightButton;
    [Space]
    public List<GameObject> Weapons = new List<GameObject>();
    [Header("Features")]
    public double BackPos;
    public double ForePos;
    public int SpeedOfRoll;
    public int LenghtOfItems;

    public float MyTime;

    [Header("Classes")]
    public WORK_STORE_HEAD GetMetods;
    public MainPlayerControl GetPackage;
    public Tutorial Tutor;
    //public SaveLoadData Loader;

    void Start()
    {

        BackAll.transform.position = CenterAnchor.transform.position;
        BackPos = LeftTopAnchor.transform.position.x;
        BackGround.localPosition = new Vector3(LeftTopAnchor.transform.position.x, RightUpAnchor.transform.position.y, BackGround.localPosition.z);
        //Loader.FindWeapons(this.GetComponent<BulletsEngine>());
        if (Tutor != null) {
            if (GetPackage.If_Tutorial == true) {
                if (GetPackage.Step_Of_Tutorial == 38) {
                    Tutor.enabled = true;
                    Tutor.First_Launch = true;
                }
            }
        }
    }

    public void LoadWeapons() {
        int Plc = 0;
        foreach (GameObject Wpn in Weapons) {
            GameObject WeapRef = Instantiate(Wpn);
            WeapRef.transform.SetParent(BoughtItems.transform);
            Wpn.active = true;
            Wpn.GetComponent<WeaponProperties>().WeaponXRef = WeapRef;
            WeapRef.GetComponent<WeaponProperties>().WeaponXRef = Wpn;
            WeapRef.transform.position = BoughtItems.transform.GetChild(Plc).transform.position;
            BoughtItems.transform.GetChild(Plc).gameObject.active = false;
            WeapRef.GetComponent<WeaponProperties>().Bought = true;
            Plc++;
        }
    }

    public void ShowMatchWeapons(GameObject ActiveBullets) {
        foreach (GameObject Wpn in Weapons) {
            if (Wpn.GetComponent<WeaponProperties>().Skin == ActiveBullets.GetComponent<BulletsProperties>().Skin) {
                GameObject GetRef = Wpn.GetComponent<WeaponProperties>().WeaponXRef.gameObject;
                GetRef.GetComponent<WeaponProperties>().isActive = false;
                GetRef.GetComponent<Collider2D>().enabled = true;
            } else {
                GameObject GetRef = Wpn.GetComponent<WeaponProperties>().WeaponXRef.gameObject;
                GetRef.GetComponent<WeaponProperties>().isActive = true;
                GetRef.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void Deselect() {
        foreach (GameObject wpn in Weapons) {
            GameObject GetRef = wpn.GetComponent<WeaponProperties>().WeaponXRef.gameObject;
            GetRef.GetComponent<WeaponProperties>().isActive = true;
            GetRef.GetComponent<Collider2D>().enabled = false;
        }
    }


    void OnEnable() {
        foreach (Transform Slaves in SlavesSource.transform) {
            Slaves.transform.localPosition = new Vector3(0, 0, 0);
        }
        foreach (Transform Item in ItemSource.transform) {
            Item.transform.localPosition = new Vector3(0, 0, 0);
        }

        // int Plc = 0;
        //foreach (GameObject Item in GetPackage.Package) {
        //    if (Item == null) {
        //        BoughtItems.transform.GetChild(Plc).gameObject.active = true;
        //    } else {
        //        if (Item.GetComponent<WeaponProperties>() != null) {
        //            Item.GetComponent<WeaponProperties>().Bought = true;
        //        }
        //        if (Item.GetComponent<OtherStuff>() != null) {
        //            Item.GetComponent<OtherStuff>().Bought = true;
        //        }
        //        Item.transform.position = BoughtItems.transform.GetChild(Plc).gameObject.transform.position;
        //        BoughtItems.transform.GetChild(Plc).gameObject.active = false;
        //    }
        //    Plc++;
        //}
        //Loader.FindWeapons(this.GetComponent<BulletsEngine>());
        GetMetods.ShowYourItems();
    }

    void Update()
    {

        if (LeftButton.GetComponent<ButtonSample>().isPressed == true) {
            BackPos += SpeedOfRoll * 0.15d;
            BackGround.localPosition = new Vector3((float)BackPos, BackGround.localPosition.y, BackGround.localPosition.z);
            if (BackPos >= LeftTopAnchor.transform.position.x - 0.15d) {
                LeftButton.GetComponent<ButtonSample>().isActive = false;
                RightButton.GetComponent<ButtonSample>().isActive = true;
            } else {
                LeftButton.GetComponent<ButtonSample>().isActive = true;
                RightButton.GetComponent<ButtonSample>().isActive = true;
            }

        }

        if (RightButton.GetComponent<ButtonSample>().isPressed == true) {
            BackPos -= SpeedOfRoll * 0.15d;
            BackGround.localPosition = new Vector3((float)BackPos, BackGround.localPosition.y, BackGround.localPosition.z);
            Debug.Log(BackPos);
            if (BackPos <= LeftTopAnchor.transform.position.x - 0.5f * LenghtOfItems + 3) {
                RightButton.GetComponent<ButtonSample>().isActive = false;
                LeftButton.GetComponent<ButtonSample>().isActive = true;
                if (Tutor != null) {
                    if (Tutor.Steps == 38) {
                        Tutor.Steps += 1;
                        Tutor.enabled = false;
                        Tutor.enabled = true;
                        Tutor.PickMonitor.Play();
                    }
                }
            } else {
                RightButton.GetComponent<ButtonSample>().isActive = true;
                LeftButton.GetComponent<ButtonSample>().isActive = true;
            }
        }
    }
}
