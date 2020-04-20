using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructMonitor : MonoBehaviour
{

    public GameObject LeftBottomAnchor;
    public GameObject LeftTopAnchor;
    public GameObject RightTopAnchor;
    public GameObject RightBottomAnchor;

    public GameObject LeftBottomMon;
    public GameObject LeftTopMon;
    public GameObject RightTopMon;
    public GameObject RightBottomMon;

    public GameObject MidBottomMon;
    public GameObject MidRightMon;
    public GameObject MidLeftMon;
    public GameObject MidTopMon;

    public GameObject Monitor;
    public GameObject Vignete;
    public GameObject MapPanelBack;
    public GameObject BattleMessageWindow;
    public GameObject BtlMsgShadow;
    public GameObject InfoField;

    void Start()
    {

        LeftBottomMon.transform.position = LeftBottomAnchor.transform.position;
        LeftTopMon.transform.position = LeftTopAnchor.transform.position;
        RightBottomMon.transform.position = RightBottomAnchor.transform.position;
        RightTopMon.transform.position = RightTopAnchor.transform.position;

        float Width = Mathf.Abs(LeftBottomMon.transform.position.x) + Mathf.Abs(RightBottomMon.transform.position.x);
        float Height = Mathf.Abs(LeftBottomMon.transform.position.y) + Mathf.Abs(LeftTopMon.transform.position.y);

        MidBottomMon.transform.localScale = new Vector3(Width / 0.32f, 1, 1);
        MidTopMon.transform.localScale = new Vector3(Width / 0.32f, 1, 1);
        MidLeftMon.transform.localScale = new Vector3(1, Height / 0.32f, 1);
        MidRightMon.transform.localScale = new Vector3(1, Height / 0.32f, 1);
        Monitor.transform.localScale = new Vector3(Width / 0.32f, 1, 1);

        MidBottomMon.transform.position = new Vector3((LeftBottomMon.transform.position.x + RightBottomMon.transform.position.x)/2F, LeftBottomAnchor.transform.position.y, LeftBottomAnchor.transform.position.z + 0.1f);
        MidTopMon.transform.position = new Vector3((LeftTopMon.transform.position.x + RightBottomMon.transform.position.x) / 2F, LeftTopMon.transform.position.y, LeftTopMon.transform.position.z + 0.1f);
        MidRightMon.transform.position = new Vector3(RightBottomMon.transform.position.x, (RightTopMon.transform.position.y + RightBottomMon.transform.position.y) / 2F, RightBottomMon.transform.position.z + 0.1f);
        MidLeftMon.transform.position = new Vector3(LeftBottomMon.transform.position.x, (LeftTopMon.transform.position.y + LeftBottomMon.transform.position.y) / 2F, LeftBottomMon.transform.position.z + 0.1f);
        Monitor.transform.position = new Vector3(MidBottomMon.transform.position.x, MidLeftMon.transform.position.y, LeftBottomMon.transform.position.z + 0.5f);

        Vignete.transform.position = Monitor.transform.position + new Vector3(0, 0, -0.1f);
        Vignete.transform.localScale = new Vector3(Width / 0.32f, Height / 0.32f, 1);

        MapPanelBack.transform.position = MidRightMon.transform.position + new Vector3(0, 0, 0.1f);

        InfoField.transform.position = LeftBottomAnchor.transform.position + new Vector3(0.15f, 0.15f, 0);
        InfoField.transform.localPosition = new Vector3(InfoField.transform.localPosition.x, InfoField.transform.localPosition.y, Monitor.transform.localPosition.z + 0.2f);

        BattleMessageWindow.transform.position = new Vector3 (MidTopMon.transform.position.x, MidLeftMon.transform.position.y, InfoField.transform.position.z + 0.1f);
        BtlMsgShadow.transform.localScale = new Vector3(MidTopMon.transform.localScale.x, MidLeftMon.transform.localScale.y, 1);
        BattleMessageWindow.active = false;

    }

    void Update()
    {
        
    }
}
