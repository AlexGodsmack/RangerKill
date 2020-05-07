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

    void Start() {
        //public static GenerateStores Instance;
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
        PlayerDataChanger CreateNewPlayer = new PlayerDataChanger();
        CreateNewPlayer.CreateNewPlayerData();
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

    public BanditArea CreateNewBand(MapData NewMapData, int BandID) {

        BanditArea newBand = new BanditArea();

        newBand.Coverage = Random.Range(1, 4);

        CreateInCircle Borders = new CreateInCircle();
        Borders.MapColumn = NewMapData.GenerateIndexes.Columns;
        Borders.MapRow = NewMapData.GenerateIndexes.Row;

        if (newBand.Coverage == 1) {
            int GetRandomTile = Random.Range(0, NewMapData.GenerateIndexes.Columns * NewMapData.GenerateIndexes.Row - 1);
            newBand.Coordinates = NewMapData.Tiles[GetRandomTile].TileID;
        }
        if (newBand.Coverage == 2) {
            int GetRandomTile = Random.Range(0, NewMapData.GenerateIndexes.Columns * NewMapData.GenerateIndexes.Row - 1);
            newBand.Coordinates = NewMapData.Tiles[GetRandomTile].TileID;
            Borders.TakeCell = newBand.Coordinates;
            Borders.GetBorders();
            if (Borders.IdentifyRow == 1) {
                newBand.Coordinates += NewMapData.GenerateIndexes.Row;
            }
            if (Borders.IdentifyRow == NewMapData.GenerateIndexes.Row) {
                newBand.Coordinates -= NewMapData.GenerateIndexes.Row;
            }
            if (Borders.IdentifyColumn == 1) {
                newBand.Coordinates += 1;
            }
            if (Borders.IdentifyColumn == NewMapData.GenerateIndexes.Columns) {
                newBand.Coordinates -= 1;
            }
        }
        if (newBand.Coverage == 3) {
            int GetRandomTile = Random.Range(0, NewMapData.GenerateIndexes.Columns * NewMapData.GenerateIndexes.Row - 1);
            newBand.Coordinates = NewMapData.Tiles[GetRandomTile].TileID;
            Borders.TakeCell = newBand.Coordinates;
            Borders.GetBorders();
            if (Borders.IdentifyRow < 3) {
                newBand.Coordinates += 2 * NewMapData.GenerateIndexes.Row;
            }
            if (Borders.IdentifyRow > NewMapData.GenerateIndexes.Row - 3) {
                newBand.Coordinates -= 2 * NewMapData.GenerateIndexes.Row;
            }
            if (Borders.IdentifyColumn < 3) {
                newBand.Coordinates += 2;
            }
            if (Borders.IdentifyColumn > NewMapData.GenerateIndexes.Columns - 3) {
                newBand.Coordinates -= 2;
            }
        }

        CreateInCircle circ = new CreateInCircle();
        circ.TakeCell = newBand.Coordinates;
        circ.Radius = newBand.Coverage;
        circ.MapRow = NewMapData.GenerateIndexes.Row;
        circ.MapColumn = NewMapData.GenerateIndexes.Columns;
        circ.TakeRadius();

        bool CheckCollision = false;

        foreach (int cell in circ.FullTiles) {
            if (NewMapData.Tiles[cell - 1].Empty == true) {
                CheckCollision = true;
            } else {
                CheckCollision = false;
                break;
            }
        }

        if (CheckCollision == true) {
            foreach (MapTileSample Tile in NewMapData.Tiles) {
                foreach (int Cell in circ.FullTiles) {
                    if (Tile.TileID == Cell) {
                        Tile.Empty = false;
                    }
                }
            }
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
            int Populate = 0;
            if (newBand.Coverage == 1) {
                Populate = Random.Range(1, 5);
                newBand.Population = Populate;
            }
            if (newBand.Coverage == 2) {
                Populate = Random.Range(5, 10);
                newBand.Population = Populate;
            }
            if (newBand.Coverage == 3) {
                Populate = Random.Range(10, 15);
                newBand.Population = Populate;
            }
            newBand.NumberOfArea = BandID;
            return newBand;
        } else {
            return null;
        }
    }

    public Store CreateNewStore(MapData NewMapData, StoreStack StoresStack, string Type, int StoreID) {
        if (Type == "Slaves") {
            int RandomCell = NewMapData.Tiles[Random.Range(0, NewMapData.GenerateIndexes.Columns * NewMapData.GenerateIndexes.Row - 1)].TileID;

            if (NewMapData.Tiles[RandomCell].Empty == true) {

                MapTileSample Tile = NewMapData.Tiles[RandomCell];

                Store newStore = new Store();
                StorePoint AddStore = new StorePoint();
                newStore.TileID = Tile.TileID;
                newStore.StoreID = StoreID;
                newStore.Type = Type;

                AddStore.TypeOfStore = newStore.Type;
                AddStore.StoreID = newStore.StoreID;
                AddStore.CountOfItem = Random.Range(10, 15);
                for (int Slv = 0; Slv < AddStore.CountOfItem; Slv++) {
                    AddStore.SlaveRandomize();
                }
                StoresStack.storePoint.Add(AddStore);

                if (StoreID == 0) {
                    NewMapData.GenerateIndexes.PlayerCoords = Tile.Coordinates + new Vector3(0, 0, -5);
                }

                Tile.Empty = false;
                return newStore;
            } else {
                return null;
            }
        }
        if (Type == "Guns") {
            CreateInCircle newCirc = new CreateInCircle();
            newCirc.TakeCell = NewMapData.GenerateIndexes.Stores[StoreID - 1].TileID;
            newCirc.MapColumn = NewMapData.GenerateIndexes.Columns;
            newCirc.MapRow = NewMapData.GenerateIndexes.Row;
            newCirc.Radius = 3;
            newCirc.GenerateCell();
            //Debug.Log(newCirc.TargetCell);
            if (NewMapData.Tiles[newCirc.TargetCell - 1].Empty == true) {
                MapTileSample Tile = NewMapData.Tiles[newCirc.TargetCell - 1];
                Store newStore = new Store();
                StorePoint AddStore = new StorePoint();
                newStore.TileID = Tile.TileID;
                newStore.StoreID = StoreID;
                newStore.Type = Type;

                AddStore.TypeOfStore = newStore.Type;
                AddStore.StoreID = newStore.StoreID;
                AddStore.CountOfItem = Random.Range(10, 15);
                for (int Wpn = 0; Wpn < AddStore.CountOfItem; Wpn++) {
                    AddStore.WeaponRandomize();
                }
                StoresStack.storePoint.Add(AddStore);

                Tile.Empty = false;
                return newStore;
            } else {
                return null;
            }
        }
        if (Type == "Bullets") {
            CreateInCircle newCirc = new CreateInCircle();
            newCirc.TakeCell = NewMapData.GenerateIndexes.Stores[StoreID - 1].TileID;
            newCirc.MapColumn = NewMapData.GenerateIndexes.Columns;
            newCirc.MapRow = NewMapData.GenerateIndexes.Row;
            newCirc.Radius = 3;
            newCirc.GenerateCell();
            //Debug.Log(newCirc.TargetCell);
            if (NewMapData.Tiles[newCirc.TargetCell - 1].Empty == true) {
                MapTileSample Tile = NewMapData.Tiles[newCirc.TargetCell - 1];
                Store newStore = new Store();
                StorePoint AddStore = new StorePoint();
                newStore.TileID = Tile.TileID;
                newStore.StoreID = StoreID;
                newStore.Type = Type;

                AddStore.TypeOfStore = newStore.Type;
                AddStore.StoreID = newStore.StoreID;
                AddStore.CountOfItem = Random.Range(10, 15);
                for (int Bul = 0; Bul < AddStore.CountOfItem; Bul++) {
                    AddStore.BulletRandomize();
                }
                StoresStack.storePoint.Add(AddStore);

                Tile.Empty = false;
                return newStore;
            } else {
                return null;
            }
        }
        if (Type == "Stuff") {
            CreateInCircle newCirc = new CreateInCircle();
            newCirc.TakeCell = NewMapData.GenerateIndexes.Stores[StoreID - 1].TileID;
            newCirc.MapColumn = NewMapData.GenerateIndexes.Columns;
            newCirc.MapRow = NewMapData.GenerateIndexes.Row;
            newCirc.Radius = 3;
            newCirc.GenerateCell();
            //Debug.Log(newCirc.TargetCell);
            if (NewMapData.Tiles[newCirc.TargetCell - 1].Empty == true) {
                MapTileSample Tile = NewMapData.Tiles[newCirc.TargetCell - 1];
                Store newStore = new Store();
                StorePoint AddStore = new StorePoint();
                newStore.TileID = Tile.TileID;
                newStore.StoreID = StoreID;
                newStore.Type = Type;

                AddStore.TypeOfStore = newStore.Type;
                AddStore.StoreID = newStore.StoreID;
                AddStore.CountOfItem = Random.Range(10, 15);
                for (int Stf = 0; Stf < AddStore.CountOfItem; Stf++) {
                    AddStore.StuffRandomize();
                }
                StoresStack.storePoint.Add(AddStore);

                Tile.Empty = false;
                return newStore;
            } else {
                return null;
            }
        }
        if (Type == "Recycling") {
            //CreateInCircle newCirc = new CreateInCircle();
            //newCirc.TakeCell = NewMapData.GenerateIndexes.Stores[StoreID - 1].TileID;
            //newCirc.MapColumn = NewMapData.GenerateIndexes.Columns;
            //newCirc.MapRow = NewMapData.GenerateIndexes.Row;
            //newCirc.Radius = 3;
            //newCirc.GenerateCell();
            int RandomCell = NewMapData.Tiles[Random.Range(0, NewMapData.GenerateIndexes.Columns * NewMapData.GenerateIndexes.Row - 1)].TileID;

            Debug.Log(RandomCell);
            if (NewMapData.Tiles[RandomCell].Empty == true) {
                MapTileSample Tile = NewMapData.Tiles[RandomCell];
                Store newStore = new Store();
                StorePoint AddStore = new StorePoint();
                newStore.TileID = Tile.TileID;
                newStore.StoreID = StoreID;
                newStore.Type = Type;

                AddStore.TypeOfStore = newStore.Type;
                AddStore.StoreID = newStore.StoreID;
                StoresStack.storePoint.Add(AddStore);

                Tile.Empty = false;
                return newStore;
            } else {
                return null;
            }
        } else {
            return null;
        }

    }

    public void GenerateMap() {

        MapData NewMapData = new MapData();
        NewMapData.GenerateIndexes.Columns = 20;
        NewMapData.GenerateIndexes.Row = 20;

        StoreStack StoresStack = new StoreStack();

        int TileID = 1;
        float Xcoord = - 0.64f * (NewMapData.GenerateIndexes.Columns/2) + 0.32f;
        float Ycoord = 0.64f * (NewMapData.GenerateIndexes.Row/2) - 0.32f;
        for (int c = 1; c < NewMapData.GenerateIndexes.Columns + 1; c++) {
            for (int r = 1; r < NewMapData.GenerateIndexes.Row + 1; r++) {
                MapTileSample newTile = new MapTileSample();
                newTile.TileID = TileID;
                newTile.Skin = Random.Range(0, 6);
                for (int a = 0; a < newTile.Smokes.Length; a++) {
                    newTile.Smokes[a] = true;
                }
                newTile.Coordinates = new Vector3(Xcoord, Ycoord, 0.0f);
                newTile.Empty = true;
                NewMapData.Tiles.Add(newTile);
                Xcoord += 0.64f;
                TileID += 1;
            }
            Xcoord = -0.64f * (NewMapData.GenerateIndexes.Columns / 2) + 0.32f;
            Ycoord -= 0.64f;
        }

        int StoresNum = Random.Range(5, 10);
        StoresNum = 10;
        for (int s = 0; s < StoresNum; s++) {
            string Type = "";
            if (s == 0) {
                Type = "Slaves";
                Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                if (newstore != null) {
                    NewMapData.GenerateIndexes.Stores.Add(newstore);
                } else {
                    s -= 1;
                }
            }
            if (s == 1) {
                Type = "Guns";
                Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                if (newstore != null) {
                    NewMapData.GenerateIndexes.Stores.Add(newstore);
                } else {
                    s -= 1;
                }
            }
            if (s == 2) {
                Type = "Bullets";
                Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                if (newstore != null) {
                    NewMapData.GenerateIndexes.Stores.Add(newstore);
                } else {
                    s -= 1;
                }
            }
            if (s == 3) {
                Type = "Stuff";
                Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                if (newstore != null) {
                    NewMapData.GenerateIndexes.Stores.Add(newstore);
                } else {
                    s -= 1;
                }
            }
            if (s == 4) {
                Type = "Recycling";
                Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                if (newstore != null) {
                    NewMapData.GenerateIndexes.Stores.Add(newstore);
                } else {
                    s -= 1;
                }
            }
            if (s >= 5) {
                int randNum = Random.Range(1, 6);
                if (randNum == 1) {
                    Type = "Slaves";
                    Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                    if (newstore != null) {
                        NewMapData.GenerateIndexes.Stores.Add(newstore);
                    } else {
                        s -= 1;
                    }
                }
                if (randNum == 2) {
                    Type = "Guns";
                    Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                    if (newstore != null) {
                        NewMapData.GenerateIndexes.Stores.Add(newstore);
                    } else {
                        s -= 1;
                    }
                }
                if (randNum == 3) {
                    Type = "Bullets";
                    Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                    if (newstore != null) {
                        NewMapData.GenerateIndexes.Stores.Add(newstore);
                    } else {
                        s -= 1;
                    }
                }
                if (randNum == 4) {
                    Type = "Stuff";
                    Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                    if (newstore != null) {
                        NewMapData.GenerateIndexes.Stores.Add(newstore);
                    } else {
                        s -= 1;
                    }
                }
                if (randNum == 5) {
                    Type = "Recycling";
                    Store newstore = CreateNewStore(NewMapData, StoresStack, Type, s);
                    if (newstore != null) {
                        NewMapData.GenerateIndexes.Stores.Add(newstore);
                    } else {
                        s -= 1;
                    }
                }
            }

        }

        for (int a = 0; a < 10; a++) {

            BanditArea NewBand = CreateNewBand(NewMapData, a);
            if (NewBand != null) {
                NewMapData.GenerateIndexes.Bandits.Add(NewBand);
            } else {
                a -= 1;
            }

        }

        string SaveData = JsonUtility.ToJson(NewMapData);
        StreamWriter WriteData = new StreamWriter(Application.persistentDataPath + "/MapData.json");
        WriteData.Write(SaveData);
        WriteData.Close();

        string SaveStores = JsonUtility.ToJson(StoresStack);
        StreamWriter WriteStores = new StreamWriter(Application.persistentDataPath + "/StoresStack.json");
        WriteStores.Write(SaveStores);
        WriteStores.Close();

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
    public string Type;
    public int StoreID;
    public int TileID;
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
    public int TileID;
    public int Skin;
    public bool[] Smokes = new bool[4];
    public bool Empty;
    public Vector3 Coordinates;
}

[System.Serializable]
public class MapData {
    public MapIndexes GenerateIndexes = new MapIndexes();
    public List<MapTileSample> Tiles = new List<MapTileSample>();
}

[System.Serializable]
public class CreateInCircle {
    public int TakeCell;
    public int Radius;
    public int MapColumn;
    public int MapRow;
    public int TargetCell;
    public int IdentifyRow;
    public int IdentifyColumn;
    public List<int> FullTiles = new List<int>();

    public void GetBorders() {
        IdentifyRow = Mathf.CeilToInt((float)TakeCell / MapRow);
        IdentifyColumn = TakeCell - (IdentifyRow - 1) * MapColumn;
    }

    public void GenerateCell() {
        int IdentifyRow = Mathf.CeilToInt((float)TakeCell / MapRow);
        int IdentifyColumn = TakeCell - (IdentifyRow - 1) * MapColumn;

        int RandomColumn = Random.Range(IdentifyColumn - Radius, IdentifyColumn + Radius);
        if (RandomColumn <= 0) {
            RandomColumn = 1;
        }
        if (RandomColumn > MapColumn) {
            RandomColumn = MapColumn;
        }

        int RandomRow = Random.Range(IdentifyRow - Radius, IdentifyRow + Radius);
        if (RandomRow <= 0) {
            RandomRow = 1;
        }
        if (RandomRow > MapRow) {
            RandomRow = MapRow;
        }
        TargetCell = (RandomRow - 1) * MapColumn + RandomColumn;
    }

    public void TakeRadius() {
        if (Radius == 1) {
            FullTiles.Add(TakeCell);
        }
        if (Radius == 2) {
            int IdentifyRow = Mathf.CeilToInt((float)TakeCell / MapRow);
            int IdentifyColumn = TakeCell - (IdentifyRow - 1)* MapColumn;
            for (int r = IdentifyRow - 2; r < IdentifyRow + 1; r++) {
                int RowSum = r * MapRow;
                for (int c = IdentifyColumn - 1; c <= IdentifyColumn + 1; c++) {
                    FullTiles.Add(RowSum + c);
                }
            }
        }
        if (Radius == 3) {
            int IdentifyRow = Mathf.CeilToInt((float)TakeCell / MapRow);
            int IdentifyColumn = TakeCell - (IdentifyRow - 1) * MapColumn;
            for (int r = IdentifyRow - 3; r < IdentifyRow + 2; r++) {
                int RowSum = r * MapRow;
                for (int c = IdentifyColumn - 2; c <= IdentifyColumn + 2; c++) {
                    int NewCell = RowSum + c;

                    int LeftTop = (IdentifyRow - 3) * MapRow + IdentifyColumn - 2;
                    int RightTop = (IdentifyRow - 3) * MapRow + IdentifyColumn + 2;
                    int LeftBottom = (IdentifyRow + 1) * MapRow + IdentifyColumn - 2;
                    int RightBottom = (IdentifyRow + 1) * MapRow + IdentifyColumn + 2;
                    if (NewCell != LeftTop) {
                        if (NewCell != RightBottom) {
                            if (NewCell != RightTop) {
                                if (NewCell != LeftBottom) {
                                    FullTiles.Add(NewCell);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}