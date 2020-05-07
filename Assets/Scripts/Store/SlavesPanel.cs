using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlavesPanel : MonoBehaviour
{

    public Transform Foreground;
    public Transform Midground;
    public Transform Farground;
    public Transform NearFront;

    public GameObject Background;
    public GameObject Center;

    public GameObject LeftButton;
    public GameObject RightButton;

    public double FarPos;
    public double MidPos;
    public double ForePos;
    public double NearPos;
    public int SpeedOfRoll;
    public float MyTime;
    public int LengthOfSlaves;

    //public TextMesh Health;
    //public TextMesh Damage;
    //public TextMesh Accuracy;
    //public TextMesh Level;
    //public TextMesh Price;

    //public int HealthGrade;
    //public int DamageGrade;
    //public int AccuracyGrade;
    //public GameObject HG;
    //public GameObject DG;
    //public GameObject AG;

    void Start()
    {

        Background.transform.position = Center.transform.position;
        FarPos = 5.3d;
        MidPos = 7.95d;
        ForePos = 15.9d;
        NearPos = 21.2d;

    }

    void Update()
    {


        if (LeftButton.GetComponent<ButtonSample>().isPressed == true) {
            FarPos += SpeedOfRoll * 0.1d;
            MidPos += SpeedOfRoll * 0.15d;
            ForePos += SpeedOfRoll * 0.3d;
            NearPos += SpeedOfRoll * 0.4d;
            MyTime = 0.0f;
        }

        if (RightButton.GetComponent<ButtonSample>().isPressed == true)
        {
            FarPos -= SpeedOfRoll * 0.1d;
            MidPos -= SpeedOfRoll * 0.15d;
            ForePos -= SpeedOfRoll * 0.3d;
            NearPos -= SpeedOfRoll * 0.4d;
            MyTime = 0.0f;
        }


        if (MyTime <= 1.0f) {
            MyTime += 0.01f;
        }

        Farground.localPosition = new Vector3(Mathf.Lerp(Farground.localPosition.x, (float)FarPos, MyTime), Farground.localPosition.y, Farground.localPosition.z);
        Midground.localPosition = new Vector3(Mathf.Lerp(Midground.localPosition.x, (float)MidPos, MyTime), Midground.localPosition.y, Midground.localPosition.z);
        Foreground.localPosition = new Vector3(Mathf.Lerp(Foreground.localPosition.x, (float)ForePos, MyTime), Foreground.localPosition.y, Foreground.localPosition.z);
        NearFront.localPosition = new Vector3(Mathf.Lerp(NearFront.localPosition.x, (float)NearPos, MyTime), NearFront.localPosition.y, NearFront.localPosition.z);

        if (FarPos >= 5.5d) {
            LeftButton.GetComponent<ButtonSample>().isActive = false;
        } else {
            LeftButton.GetComponent<ButtonSample>().isActive = true;
        }
        if (MidPos <= 7.95f - LengthOfSlaves * 0.5f + 1) {
            RightButton.GetComponent<ButtonSample>().isActive = false;
        } else {
            RightButton.GetComponent<ButtonSample>().isActive = true;
        }

        //HG.GetComponent<GradeStore>().GetGrade = HealthGrade;
        //DG.GetComponent<GradeStore>().GetGrade = DamageGrade;
        //AG.GetComponent<GradeStore>().GetGrade = AccuracyGrade;

    }
}
