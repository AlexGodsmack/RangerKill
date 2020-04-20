using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSample : MonoBehaviour
{

    public GameObject ButtonFree;
    public GameObject ButtonPressed;
    public AudioSource[] PlaySound;

    public bool isPressed;

    public bool isActive = true;

    void Start()
    {

        if (isActive == false) {
            ButtonFree.GetComponent<Collider2D>().enabled = false;
            ButtonFree.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.gameObject == ButtonFree) {
                ButtonFree.GetComponent<SpriteRenderer>().enabled = false;
                for (int i = 0; i < PlaySound.Length; i++) {
                    PlaySound[i].Play();
                }
                isPressed = true;
            }
        } else {
            isPressed = false;
        }

        if (Input.GetMouseButtonUp(0)) {
            if (isActive == true) {
                ButtonFree.GetComponent<Collider2D>().enabled = true;
                ButtonFree.GetComponent<SpriteRenderer>().enabled = true;
            } else {
                ButtonFree.GetComponent<Collider2D>().enabled = false;
                ButtonFree.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

    }
}
