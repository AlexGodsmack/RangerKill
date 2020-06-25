using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveEngine : MonoBehaviour
{
    [Header("Diorama Elemets")]
    public Transform Foreground;
    public Transform Midground;
    public Transform Farground;
    public Transform NearFront;

    public GameObject Background;
    public GameObject Center;
    public GameObject RightAnchor;
    [Header("Active Objects")]
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject SlaveSource;
    public GameObject ItemSource;
    public GameObject BoughtPanel;
    [Header("Values")]
    public double FarPos;
    public double MidPos;
    public double ForePos;
    public double NearPos;
    public int SpeedOfRoll;
    public float MyTime;
    public int LengthOfSlaves;
    [Header("Classes")]
    public MainPlayerControl PlayInv;
    public WORK_STORE_HEAD GetMetods;

    public void OnEnable() {

        foreach (Transform slaves in SlaveSource.transform) {
            slaves.transform.localPosition = new Vector3(0, 0, 0);
        }

        foreach (GameObject Slave in PlayInv.SlavePlace) {
            if (Slave != null) {
                Slave.transform.position = BoughtPanel.transform.GetChild(Slave.transform.GetSiblingIndex()).transform.position;
                BoughtPanel.transform.GetChild(Slave.transform.GetSiblingIndex()).gameObject.active = false;
                Slave.GetComponent<SlaveProperties>().Bought = true;
                Slave.GetComponent<SlaveProperties>().ShowHealthbar = false;
            }
        }

        foreach (Transform Item in ItemSource.transform) {
            Item.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    void Start()
    {

        Background.transform.position = Center.transform.position;
        FarPos = 5.3d;
        MidPos = 7.25d;
        ForePos = 15.9d;
        NearPos = 21.2d;
        Farground.localPosition = new Vector3((float)FarPos, Farground.localPosition.y, Farground.localPosition.z);
        Midground.localPosition = new Vector3((float)MidPos, Midground.localPosition.y, Midground.localPosition.z);
        Foreground.localPosition = new Vector3((float)ForePos, Foreground.localPosition.y, Foreground.localPosition.z);
        NearFront.localPosition = new Vector3((float)NearPos, NearFront.localPosition.y, NearFront.localPosition.z);

    }

    void Update()
    {
        if (LeftButton.GetComponent<ButtonSample>().isPressed == true) {
            FarPos += SpeedOfRoll * 0.1d;
            MidPos += SpeedOfRoll * 0.15d;
            ForePos += SpeedOfRoll * 0.3d;
            NearPos += SpeedOfRoll * 0.4d;
            Farground.localPosition = new Vector3((float)FarPos, Farground.localPosition.y, Farground.localPosition.z);
            Midground.localPosition = new Vector3((float)MidPos, Midground.localPosition.y, Midground.localPosition.z);
            Foreground.localPosition = new Vector3((float)ForePos, Foreground.localPosition.y, Foreground.localPosition.z);
            NearFront.localPosition = new Vector3((float)NearPos, NearFront.localPosition.y, NearFront.localPosition.z);
            if (FarPos >= 5.5d) {
                LeftButton.GetComponent<ButtonSample>().isActive = false;
                RightButton.GetComponent<ButtonSample>().isActive = true;
            } else {
                LeftButton.GetComponent<ButtonSample>().isActive = true;
                RightButton.GetComponent<ButtonSample>().isActive = true;
            }
        }

        if (RightButton.GetComponent<ButtonSample>().isPressed == true)
        {
            FarPos -= SpeedOfRoll * 0.1d;
            MidPos -= SpeedOfRoll * 0.15d;
            ForePos -= SpeedOfRoll * 0.3d;
            NearPos -= SpeedOfRoll * 0.4d;
            Farground.localPosition = new Vector3((float)FarPos, Farground.localPosition.y, Farground.localPosition.z);
            Midground.localPosition = new Vector3((float)MidPos, Midground.localPosition.y, Midground.localPosition.z);
            Foreground.localPosition = new Vector3((float)ForePos, Foreground.localPosition.y, Foreground.localPosition.z);
            NearFront.localPosition = new Vector3((float)NearPos, NearFront.localPosition.y, NearFront.localPosition.z);
            if (MidPos <= 7.95f - LengthOfSlaves * 0.5f + 1) {
                RightButton.GetComponent<ButtonSample>().isActive = false;
                LeftButton.GetComponent<ButtonSample>().isActive = true;
            } else {
                RightButton.GetComponent<ButtonSample>().isActive = true;
                LeftButton.GetComponent<ButtonSample>().isActive = true;
            }
        }
    }
}
