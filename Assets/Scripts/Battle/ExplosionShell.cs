using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionShell : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EndThisExplosion() {
        Destroy(this.gameObject); 
    }
}
