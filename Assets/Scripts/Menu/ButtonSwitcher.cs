using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitcher : MonoBehaviour
{

    public GameObject Head;
    public GameObject[] Buttons;
    public AudioSource[] PlaySound;
    public int PrevNumber;
    public int Number;
    public bool isPressed;

    public GameObject[] SwitchingObjects_1;
    public GameObject[] SwitchingObjects_2;
    //public GameObject[] SwitchingObjects_3;

    void Start()
    {

        if (Number != 0) {
            Buttons[Number - 1].GetComponent<SpriteRenderer>().enabled = false;
            Buttons[Number - 1].GetComponent<Collider2D>().enabled = false;
        }
        
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.gameObject.transform.GetComponentInParent<ButtonSwitcher>() != null) {
                if (hit.collider.gameObject.transform.GetComponentInParent<ButtonSwitcher>().Head == Head) {
                    isPressed = true;
                    PrevNumber = Number;
                    for (int i = 0; i < Buttons.Length; i++) {
                        if (hit.collider.gameObject == Buttons[i]) {
                            Buttons[i].GetComponent<SpriteRenderer>().enabled = false;
                            Buttons[i].GetComponent<Collider2D>().enabled = false;
                            if (SwitchingObjects_1[i] != null) {
                                SwitchingObjects_1[i].active = true;
                            }
                            if (SwitchingObjects_2[i] != null) {
                                SwitchingObjects_2[i].active = true;
                            }
                            //SwitchingObjects_3[i].active = true;
                            for (int a = 0; a < PlaySound.Length; a++) {
                                PlaySound[a].Play();
                            }
                            Number = i + 1;
                        } else {
                            Buttons[i].GetComponent<SpriteRenderer>().enabled = true;
                            Buttons[i].GetComponent<Collider2D>().enabled = true;
                            if (SwitchingObjects_1[i] != null) {
                                SwitchingObjects_1[i].active = false;
                            }
                            if (SwitchingObjects_2[i] != null) {
                                SwitchingObjects_2[i].active = false;
                            }
                            //SwitchingObjects_3[i].active = false;
                        }
                    }
                }
            }
        } else {
            isPressed = false;
        }
    }
}
