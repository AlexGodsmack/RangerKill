using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{

    public GameObject Head;
    public GameObject PushOn;
    public GameObject PushOff;

    public bool OnOff;

    public AudioSource Sound;

    void Start()
    {

        //if (OnOff == false) {
        //    OnOff = true;
        //    PushOn.GetComponent<SpriteRenderer>().enabled = false;
        //    PushOff.GetComponent<SpriteRenderer>().enabled = true;
        //}
        //if (OnOff == true) {
        //    OnOff = false;
        //    PushOn.GetComponent<SpriteRenderer>().enabled = true;
        //    PushOff.GetComponent<SpriteRenderer>().enabled = false;
        //}

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.GetComponent<ButtonToggle>() != null) {
                if (hit.collider.GetComponent<ButtonToggle>().Head == Head) {
                    Sound.Play();
                    if (OnOff == false) {
                        OnOff = true;
                        PushOn.GetComponent<SpriteRenderer>().enabled = true;
                        PushOff.GetComponent<SpriteRenderer>().enabled = false;
                    } else {
                        OnOff = false;
                        PushOn.GetComponent<SpriteRenderer>().enabled = false;
                        PushOff.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }   
            }
        }
    }
}
