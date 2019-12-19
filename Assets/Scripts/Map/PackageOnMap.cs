using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PackageOnMap : MonoBehaviour
{

    public GameObject IndexNumberForStuff;

    public int[] Skin;
    public int[] Place;
    public int[] WaterLiters;

    public int CoPers;
    public int CoWeap;
    public int CoBullet;
    public int CoStuff;

    private string[] CountOfAll;
    private string[] PlaySet;
    private string[] MapGen;
    private string[] InvSet;
    public int IndexNumber = 0;
    public int Container = 0;
    public int WaterInfo = 0;
    public int Liters = 0;

    private int NumPersParam = 8;
    private int NumWpnParam = 6;
    private int Columns = 10;
    private int Rows = 10;
    private float Decrease = 0.0f;

    private string MapGenPath;

    void Start()
    {

        for (int i = 0; i < Skin.Length; i++) {
            Skin[i] = 0;
        }

        for (int i = 0; i < Place.Length; i++) {
            Place[i] = 0;
        }

        for (int i = 0; i < WaterLiters.Length; i++) {
            WaterLiters[i] = 0;
        }

        string CountOfAllPath = Application.persistentDataPath + "/CountOfAll.txt";
        CountOfAll = File.ReadAllLines(CountOfAllPath);
        string PlaySetPath = Application.persistentDataPath + "/PlayerSource.txt";
        PlaySet = File.ReadAllLines(PlaySetPath);
        MapGenPath = Application.persistentDataPath + "/MapGen.txt";
        MapGen = File.ReadAllLines(MapGenPath);
        string InvSetPath = Application.persistentDataPath + "/InventorySettings.txt";
        InvSet = File.ReadAllLines(InvSetPath);

        CoPers = int.Parse(CountOfAll[0]);
        CoWeap = int.Parse(CountOfAll[1]);
        CoBullet = int.Parse(CountOfAll[2]);
        CoStuff = int.Parse(CountOfAll[3]);


        for (int i = 1; i < CoStuff + 1; i++)
        {
            Skin[i - 1] = int.Parse(PlaySet[2 + CoPers * NumPersParam + CoWeap * NumWpnParam + CoBullet * 3 + i * 2 - 1]);
            if (InvSet[48 + i] != "undefined") {
                Place[i - 1] = int.Parse(InvSet[48 + i]);
            } else if (InvSet[48 + i] == "undefined") {
                Place[i - 1] = 0;
            }
            if (Skin[i - 1] == 2)
            {
                WaterLiters[i - 1] = int.Parse(MapGen[2 * Columns * Rows + 12 + 10 + 6 + i].ToString());
                if (Place[i - 1] != 0) {
                    Container = Container + 1;
                }
            }
        }
        IndexNumber = WaterLiters.Length - 1;

        for (int i = 0; i < Skin.Length - 1; i++) {
            if (Skin[i] == 2 && Place[i] != 0) {
                Liters = Liters + WaterLiters[i];
            }
        }
    }

    void Update()
    {

        if (Camera.main.GetComponent<MainMap>().GO == true) {
            if (IndexNumber >= 0) {
                if (Skin[IndexNumber] == 2 && Place[IndexNumber] != 0) {
                    if (WaterLiters[IndexNumber] > 0) {
                        Decrease = Decrease + 0.3f;
                        if (Decrease >= 1.0f) {
                            Decrease = 0.0f;
                            WaterLiters[IndexNumber] = WaterLiters[IndexNumber] - 1;
                        }
                    } else if (WaterLiters[IndexNumber] <= 0) {
                        for (int a = IndexNumber; a < Skin.Length - 1; a++) {
                            Skin[a] = Skin[a + 1];
                            Place[a] = Place[a + 1];
                            WaterLiters[a] = WaterLiters[a + 1];
                        }
                        IndexNumber = IndexNumber - 1;
                        CoStuff = CoStuff - 1;
                        Container = Container - 1;
                    }
                } else {
                    IndexNumber = IndexNumber - 1;
                }
            } else if(IndexNumber <= 0){
                Camera.main.GetComponent<MainMap>().speed = 2.0f;
            }
        }
        int Sum = 0;
        for (int i = 0; i < Skin.Length - 1; i++ ) {
            if (Skin[i] == 2 && Place[i] != 0) {
                Sum = Sum + WaterLiters[i];
            }
        }

        Liters = Sum;
        
    }
}
