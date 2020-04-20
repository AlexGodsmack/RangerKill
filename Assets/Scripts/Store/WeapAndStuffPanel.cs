using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapAndStuffPanel : MonoBehaviour
{

    public Transform BackGround;
    public Transform ForeGround;

    public GameObject WeapInfo;
    public GameObject WeapInvInfo;
    public GameObject StuffInvInfo;
    public GameObject StuffInfo;
    public GameObject BulletInfo;
    public GameObject BackAll;
    public TextMesh GeneralText;

    public GameObject CenterAnchor;
    public GameObject MidUpAnchor;
    public GameObject LeftTopAnchor;

    public GameObject LeftButton;
    public GameObject RightButton;

    public double BackPos;
    public double ForePos;
    public int SpeedOfRoll;
    public int LenghtOfWeapons;

    public float MyTime;

    void Start()
    {

        WeapInfo.active = false;
        WeapInvInfo.active = false;
        StuffInvInfo.active = false;
        StuffInfo.active = false;
        BulletInfo.active = false;
        GeneralText.text = "Please choose weapon \nto get info about it\nor switch \nto other panel...";
        BackAll.transform.position = CenterAnchor.transform.position;
        BackGround.localPosition = new Vector3(MidUpAnchor.transform.position.x, MidUpAnchor.transform.position.y, BackGround.localPosition.z);
        BackPos = LeftTopAnchor.transform.position.x;


    }

    void Update()
    {

        if (LeftButton.GetComponent<ButtonSample>().isPressed == true) {
            BackPos += SpeedOfRoll * 0.15d;
            MyTime = 0.0f;
        }

        if (RightButton.GetComponent<ButtonSample>().isPressed == true) {
            BackPos -= SpeedOfRoll * 0.15d;
            MyTime = 0.0f;
        }

        if (MyTime <= 1.0f) {
            MyTime += 0.01f;
        }

        BackGround.localPosition = new Vector3(Mathf.Lerp(BackGround.localPosition.x, (float)BackPos, MyTime), BackGround.localPosition.y, BackGround.localPosition.z);

        if (BackPos >= LeftTopAnchor.transform.position.x) {
            LeftButton.GetComponent<ButtonSample>().isActive = false;
        } else {
            LeftButton.GetComponent<ButtonSample>().isActive = true;
        }

        if (BackPos <= LeftTopAnchor.transform.position.x - 0.15f * LenghtOfWeapons - 1) {
            RightButton.GetComponent<ButtonSample>().isActive = false;
        } else {
            RightButton.GetComponent<ButtonSample>().isActive = true;
        }

    }
}
