using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlavePackage {
    public int NumberOfSlave;
    public GameObject[] Place = new GameObject[4];
}

public class PlayerInventory : MonoBehaviour {

    public int Money;

    public int Slaves;
    public int Weapons;
    public int Stuff;
    //public string TypeOfStore;
    public int StoreID;

    public GameObject[] SlavePlace;
    public GameObject[] WeaponPlace;
    public GameObject[] StuffPlace;
    public List<SlavePackage> SlavesBag;

    void Start()
    {

    }

    void Update()
    {

    }


}
