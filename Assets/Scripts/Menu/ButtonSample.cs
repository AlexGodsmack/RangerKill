using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSample : MonoBehaviour
{

    public GameObject ButtonFree;
    public GameObject ButtonPressed;
    public AudioSource[] PlaySound;

    public bool isPressed;
    //public bool Show_Hide = true;
    public bool isActive = true;

    private bool checking;

    void Start()
    {

        if (isActive == false) {
            ButtonFree.GetComponent<Collider2D>().enabled = false;
            ButtonFree.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    void OnEnable() {
        if (isActive == true) {
            ButtonFree.GetComponent<Collider2D>().enabled = true;
            ButtonFree.GetComponent<SpriteRenderer>().enabled = true;
        } else {
            ButtonFree.GetComponent<Collider2D>().enabled = false;
            ButtonFree.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Update()
    {

        if (isActive == true) {
            ButtonFree.GetComponent<Collider2D>().enabled = true;
            ButtonFree.GetComponent<SpriteRenderer>().enabled = true;
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider.gameObject == ButtonFree) {
                    for (int i = 0; i < PlaySound.Length; i++) {
                        PlaySound[i].Play();
                    }
                    checking = true;
                    Debug.Log("pressed");
                    //isPressed = true;
                }
            } else {
                isPressed = false;
            }

            if (checking == true) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider.gameObject == ButtonFree) {
                    Debug.Log("over");
                    ButtonFree.GetComponent<SpriteRenderer>().enabled = false;
                } else {
                    ButtonFree.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            if (Input.GetMouseButtonUp(0)) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider.gameObject == ButtonFree) {
                    checking = false;
                    isPressed = true;
                    if (isActive == true) {
                        ButtonFree.GetComponent<SpriteRenderer>().enabled = true;
                    }
                } else {
                    checking = false;
                    isPressed = false;
                    if (isActive == true) {
                        ButtonFree.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        } else {
            isPressed = false;
            ButtonFree.GetComponent<Collider2D>().enabled = false;
            ButtonFree.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
