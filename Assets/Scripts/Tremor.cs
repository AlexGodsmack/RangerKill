using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tremor : MonoBehaviour
{

    public GameObject Scene;
    public Vector3 StartPos;
    public float Frequency;
    public float Duration = 0.0f;
    public float Amplitude;
    public bool Activated = false;
    public float Delay = 0.0f;

    private float OldAmplitude;

    void Start()
    {

        StartPos = Scene.transform.localPosition;
        OldAmplitude = Amplitude;

    }

    void Update()
    {

        if (Activated == true) {
            Delay = Delay - 0.1f;
            if (Delay <= 0.0f) {
                Delay = 0.0f;
                Duration = Duration + Frequency;
                if (Duration >= 0.2f) {
                    Scene.transform.localPosition = StartPos;
                }
                if (Duration >= 0.5f) {
                    Amplitude = Amplitude - 0.02f;
                    Vector3 NewPos = new Vector3(Random.Range(-Amplitude, Amplitude), Random.Range(-Amplitude, Amplitude), 0);
                    Scene.transform.localPosition = Scene.transform.localPosition + NewPos;
                    Duration = 0.0f;
                    if (Amplitude <= 0.0f) {
                        Activated = false;
                        Scene.transform.localPosition = StartPos;
                        Amplitude = 0.1f;
                        Duration = 0.0f;
                    }
                }
            }
        }

    }
}
