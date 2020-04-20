using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WORK_Menu : MonoBehaviour
{

    public Button Continue;
    public Button StartNewGame;
    public Button Quit;

    void Start()
    {
        StartNewGame.onClick.AddListener(NewGame);
        Quit.onClick.AddListener(Exit);
        Continue.onClick.AddListener(ContinueGame);
    }

    void NewGame() {
        StartNewGame.GetComponent<AudioSource>().Play();
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {
            File.Delete(Application.persistentDataPath + "/PlayerData.json");
        }
        GenerateMap();
        SceneManager.LoadScene(1);
    }

    void Exit() {
        Quit.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    void ContinueGame() {
        if (File.Exists(Application.persistentDataPath + "/MapData.json") && File.Exists(Application.persistentDataPath + "/PlayerData.json")) {
            SceneManager.LoadScene(2);
        }
    }

    void Update()
    {
        
    }

    public void GenerateMap() {

        MapData NewMapData = new MapData();
        NewMapData.GenerateIndexes.Columns = 16;
        NewMapData.GenerateIndexes.Row = 16;
        NewMapData.GenerateIndexes.Row = 16;

        int NumOfArea = 0;
        for (int a = 0; a < Random.Range(3, 6); a++) {
            BanditArea newBand = new BanditArea();
            int band = Random.Range(1, 4);
            if (band == 1) {
                newBand.Clan = "Trumans'";
            }
            if (band == 2) {
                newBand.Clan = "Knackers";
            }
            if (band == 3) {
                newBand.Clan = "Horde";
            }
            newBand.Coverage = Random.Range(1, 4);
            newBand.Coordinates = Random.Range(1, NewMapData.GenerateIndexes.Columns * NewMapData.GenerateIndexes.Row);
            int Populate = 0;
            if (newBand.Coverage == 1) {
                Populate = Random.Range(1, 5);
            }
            if (newBand.Coverage == 2) {
                Populate = Random.Range(5, 10);
            }
            if (newBand.Coverage == 3) {
                Populate = Random.Range(10, 15);
            }
            newBand.Population = Populate;
            NumOfArea += 1;
            newBand.NumberOfArea = NumOfArea;
            NewMapData.GenerateIndexes.Bandits.Add(newBand);

        }


        float Xcoord = - 0.64f * (NewMapData.GenerateIndexes.Columns/2) + 0.32f;
        float Ycoord = 0.64f * (NewMapData.GenerateIndexes.Row/2) - 0.32f;
        for (int c = 1; c < NewMapData.GenerateIndexes.Columns + 1; c++) {
            for (int r = 1; r < NewMapData.GenerateIndexes.Row + 1; r++) {
                MapTileSample newTile = new MapTileSample();
                newTile.Skin = Random.Range(0, 6);
                for (int a = 0; a < newTile.Smokes.Length; a++) {
                    newTile.Smokes[a] = true;
                }
                newTile.Coordinates = new Vector3(Xcoord, Ycoord, 0.0f);
                NewMapData.Tiles.Add(newTile);
                Xcoord += 0.64f;
            }
            Xcoord = -0.64f * (NewMapData.GenerateIndexes.Columns / 2) + 0.32f;
            Ycoord -= 0.64f;
        }

        int StoresNum = Random.Range(3, 10);
        for (int s = 0; s < StoresNum; s++) {
            Store newStore = new Store();
            newStore.Coordinates = NewMapData.Tiles[Random.Range(1, NewMapData.GenerateIndexes.Columns * NewMapData.GenerateIndexes.Row)].Coordinates;
            NewMapData.GenerateIndexes.Stores.Add(newStore);
        }

        int GetPlayerCoord = Random.Range(0, NewMapData.GenerateIndexes.Stores.Count);
        Debug.Log(GetPlayerCoord);
        NewMapData.GenerateIndexes.PlayerCoords = NewMapData.GenerateIndexes.Stores[GetPlayerCoord].Coordinates + new Vector3(0, 0, -5);

        string SaveData = JsonUtility.ToJson(NewMapData);
        StreamWriter WriteData = new StreamWriter(Application.persistentDataPath + "/MapData.json");
        WriteData.Write(SaveData);
        WriteData.Close();
        return;
    }
}

[System.Serializable]
public class BanditArea {

    public string Clan;
    public int Coverage;
    public int Coordinates;
    public int Population;
    public int NumberOfArea;
}

[System.Serializable]
public class Store {
    public Vector3 Coordinates;
}

[System.Serializable]
public class MapIndexes {

    public int Columns;
    public int Row;
    public List<BanditArea> Bandits = new List<BanditArea>();
    public List<Store> Stores = new List<Store>();
    public Vector3 PlayerCoords;
}

[System.Serializable]
public class MapTileSample {
    public int Skin;
    public bool[] Smokes = new bool[4];
    public Vector3 Coordinates;
}

[System.Serializable]
public class MapData {
    public MapIndexes GenerateIndexes = new MapIndexes();
    public List<MapTileSample> Tiles = new List<MapTileSample>();
}
