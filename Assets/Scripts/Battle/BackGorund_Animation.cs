using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BackGorund_Animation : MonoBehaviour
{

    [Header("Source")]
    public GameObject Background;
    public GameObject Near;
    public GameObject Decoration;
    public GameObject Middle;
    public GameObject MiddleNear;
    public GameObject MiddleCenter;
    public GameObject MiddleFar;
    public GameObject Far;
    public GameObject BattlePanel;
    public GameObject Veil;
    public GameObject Veil2;

    [Header("Animation Tools")]
    [Tooltip("Начало откуда движется сцена с диорамой")]
    public GameObject Pos1;
    [Space]
    public GameObject FarGoal;
    public GameObject MidNearGoal;
    public GameObject MidCenterGoal;
    public GameObject MidFarGoal;
    public GameObject NearGoal;
    [Space]
    public Vector3 BattlePanelPos1;
    public Vector3 BattlePanelPos2;
    public GameObject TopCenter;
    //[Space]
    //[Tooltip("Суммарное время от старта и до конца полета камеры")]
    //public float FullTime;

    [Header("Timing")]
    [Tooltip("Камера пошла ко второму ракурсу")]
    public bool FirstStage;
    [Tooltip("Фиксируется время начала полета до места битвы")]
    public int CurrentFrame;
    public float StartRail;
    public float Position_1;

    [Header("Sounds")]
    public AudioSource BattlePanelSound;
    public AudioSource VeilSound;

    [Header("Reference Files")]
    public WORK_Battle GetBattleWork;

    void Start()
    {
        //FullTime = 0.0f;
        //StartRail = 0.0f;
        //Background.transform.position = Pos1.transform.position;   
        CurrentFrame = Time.frameCount;
        BattlePanelPos1 = TopCenter.transform.position + new Vector3(0, 0.6f, 0);
        BattlePanelPos2 = TopCenter.transform.position;
    }

    void Update()
    {

        if (Time.timeSinceLevelLoad < 3.0f) {
            //FullTime += 0.01f;
            Veil.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 2 * Time.timeSinceLevelLoad);
            Veil2.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 2 * Time.timeSinceLevelLoad);
            if (Time.frameCount - CurrentFrame == 3) {
                VeilSound.Play();
            }

            if (Time.frameCount - CurrentFrame == 60) {
                BattlePanelSound.Play();
                StartRail = Time.timeSinceLevelLoad;
                FirstStage = true;
                Debug.Log("ready");
            }
            if (FirstStage == true) {
                //StartRail += 0.005f;
                Position_1 = 0.1f * (Time.timeSinceLevelLoad - StartRail);
                Background.transform.position = Vector3.Lerp(Background.transform.position, Pos1.transform.position, Position_1);
                Far.transform.position = Vector3.Lerp(Far.transform.position, FarGoal.transform.position, Position_1);
                MiddleFar.transform.position = Vector3.Lerp(MiddleFar.transform.position, MidFarGoal.transform.position, Position_1);
                MiddleCenter.transform.position = Vector3.Lerp(MiddleCenter.transform.position, MidCenterGoal.transform.position, Position_1);
                MiddleNear.transform.position = Vector3.Lerp(MiddleNear.transform.position, MidNearGoal.transform.position, Position_1);
                Near.transform.position = Vector3.Lerp(Near.transform.position, NearGoal.transform.position, Position_1);
                BattlePanel.transform.position = Vector3.Lerp(BattlePanelPos1, BattlePanelPos2, 10 * Position_1);

                if (Time.time >= 2.0f) {
                    GetBattleWork.LetsStart = true;
                }
            }

            //if (FullTime >= 0.6f) {
            //        SecondStage = true;
            //        //Position = 0.0f;
            //}
        }

    }
}

