using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerControl : MonoBehaviour {

    [Header("Player Resources")]
    public int Money;
    public GameObject[] SlavePlace = new GameObject[9];
    public GameObject[] Package = new GameObject[9];
    [Header("Store Items")]
    public int StoreID;
    public string TypeOfStore;
    public List<GameObject> Items;
    [Space]
    public bool If_Tutorial;
    public int Step_Of_Tutorial;

    void Start()
    {

    }

    void Update()
    {

    }


}
