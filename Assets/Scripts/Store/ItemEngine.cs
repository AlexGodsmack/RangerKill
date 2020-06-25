using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEngine : MonoBehaviour
{

    public Transform BackGround;

    //public GameObject WeapInfo;
    //public GameObject WeapInvInfo;
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

    public double BackPos;
    public double ForePos;
    public int SpeedOfRoll;
    public int LenghtOfItems;

    public float MyTime;

    [Header("Classes")]
    public WORK_STORE_HEAD GetMetods;
    public MainPlayerControl GetPackage;
    public Tutorial Tutor;

    void Start()
    {

        BackAll.transform.position = CenterAnchor.transform.position;
        BackGround.localPosition = new Vector3(LeftTopAnchor.transform.position.x, RightUpAnchor.transform.position.y, BackGround.localPosition.z);
        BackPos = LeftTopAnchor.transform.position.x;
        if (Tutor != null) {
            Tutor.enabled = true;
            Tutor.First_Launch = true;
        }
    }

    void OnEnable() {
        foreach (Transform Slaves in SlavesSource.transform) {
            Slaves.transform.localPosition = new Vector3(0, 0, 0);
        }
        //int Plc = 0;
        //foreach (Transform Items in ItemSource.transform) {
        //    Items.transform.position = BoughtItems.transform.GetChild(Plc).transform.position;
        //    Plc++;
        //}

        int Plc = 0;
        foreach (GameObject Item in GetPackage.Package) {
            if (Item == null) {
                BoughtItems.transform.GetChild(Plc).gameObject.active = true;
            } else {
                if (Item.GetComponent<WeaponProperties>() != null) {
                    Item.GetComponent<WeaponProperties>().Bought = true;
                }
                if (Item.GetComponent<OtherStuff>() != null) {
                    Item.GetComponent<OtherStuff>().Bought = true;
                }
                Item.transform.position = BoughtItems.transform.GetChild(Plc).gameObject.transform.position;
                BoughtItems.transform.GetChild(Plc).gameObject.active = false;
            }
            Plc++;
        }
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
            if (BackPos <= LeftTopAnchor.transform.position.x - 0.5f * LenghtOfItems + 3) {
                RightButton.GetComponent<ButtonSample>().isActive = false;
                LeftButton.GetComponent<ButtonSample>().isActive = true;
            } else {
                RightButton.GetComponent<ButtonSample>().isActive = true;
                LeftButton.GetComponent<ButtonSample>().isActive = true;
            }
        }
    }
}
