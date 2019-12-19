using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{

    public GameObject HealthBarProgress;

    public int Band;
    public int NumberOfSkin;
    public int StartHealth;
    public int Health;
    public int Damage;
    public int Accuracy;
    public int Level;

    public int PowerOfShot;

    void Start()
    {

        PowerOfShot = (int)(Level * (1.2f * Damage + 0.7f * Accuracy));

    }

    void Update()
    {
        if (Health > 0) {
            HealthBarProgress.transform.localScale = new Vector3(1.0f * Health / StartHealth, 1, 1);
        } else if(Health <= 0){
            HealthBarProgress.transform.localScale = new Vector3(0, 1, 1);
        }

    }
}
