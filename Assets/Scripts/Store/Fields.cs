using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fields : MonoBehaviour
{

    public bool isActive;
    public int Number;

    void Start()
    {
        
    }

    void Update()
    {

        if (isActive == true) {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            this.GetComponent<Collider2D>().enabled = true;
        } else {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            this.GetComponent<Collider2D>().enabled = false;
        }
        
    }
}
