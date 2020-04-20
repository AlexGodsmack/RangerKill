using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{

    [Header("GameObjects")]
    public GameObject ShakingObject;

    [Header("Parameters")]
    public float Amplitude = 0.5f;
    public float Duration = 1.0f;
    public bool Activated;
    public bool ShakingRun;
    public Vector3 StartPos;

    private float CurrentTime;
    private float CurrentPos;
    private float CurrentAmplitude;
    private bool Revert;

    void Start()
    {

    }

    void Update()
    {
        //Debug.Log(Time.fixedDeltaTime);
        if (Activated == true && ShakingRun != true) {
            StartPos = ShakingObject.transform.position;
            CurrentTime = Duration;
            CurrentAmplitude = Amplitude;
            ShakingRun = true;
        }

        if (ShakingRun == true) {
            if (CurrentTime > 0.0f) {
                CurrentTime -= Time.fixedDeltaTime;
                if (Revert == true) {
                    int RandDirect = Random.Range(1, 5);
                    if (RandDirect == 1) {
                        ShakingObject.transform.position = new Vector3(CurrentAmplitude, CurrentAmplitude, ShakingObject.transform.position.z);
                    }
                    if (RandDirect == 2) {
                        ShakingObject.transform.position = new Vector3(-CurrentAmplitude, CurrentAmplitude, ShakingObject.transform.position.z);
                    }
                    if (RandDirect == 3) {
                        ShakingObject.transform.position = new Vector3(-CurrentAmplitude, -CurrentAmplitude, ShakingObject.transform.position.z);
                    }
                    if (RandDirect == 4) {
                        ShakingObject.transform.position = new Vector3(CurrentAmplitude, -CurrentAmplitude, ShakingObject.transform.position.z);
                    }
                    Revert = false;
                } else {
                    ShakingObject.transform.position = -ShakingObject.transform.position;
                    CurrentAmplitude -= 0.2f * CurrentAmplitude;
                    Revert = true;
                }
                //ShakingObject.transform.position = StartPos;
                //ShakingObject.transform.position = StartPos;
                Debug.Log(CurrentAmplitude);
            } else {
                ShakingObject.transform.position = StartPos;
                ShakingRun = false;
                Activated = false;
            }
        }

    }
}
