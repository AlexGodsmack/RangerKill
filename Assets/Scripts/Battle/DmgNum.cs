using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgNum : MonoBehaviour
{

    public void RemoveThis() {
        Destroy(this.transform.parent.gameObject);
    }
    
}
