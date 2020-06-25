using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPlace : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

        if (this.transform.childCount == 0) {
            this.GetComponent<SpriteRenderer>().enabled = true;
        } else {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
}
