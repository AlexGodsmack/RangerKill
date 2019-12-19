using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenOnMap : MonoBehaviour
{

    public GameObject ContainerForStuff;
    public Text InfoScreen;

    private int WaterInfo = 0;

    void Start()
    {
        InfoScreen.text = "Water: " + ContainerForStuff.GetComponent<PackageOnMap>().Liters.ToString();
    }

    void Update()
    {
        if (Camera.main.GetComponent<MainMap>().GO == true) {
            InfoScreen.text = "Water: " + ContainerForStuff.GetComponent<PackageOnMap>().Liters.ToString();
        }
        
    }
}
