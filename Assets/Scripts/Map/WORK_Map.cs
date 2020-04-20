using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class WORK_Map : MonoBehaviour
{

    public GameObject MainCamera;
    public GameObject MapContainer;
    public GameObject MapField;
    public GameObject Player;
    public GameObject Goal;
    public GameObject MainCollider;
    public GameObject WaterPack;
    public GameObject BattleMessage;

    public GameObject MapPanel;
    public GameObject ArrowUp;
    public GameObject ArrowDown;
    public GameObject ArrowLeft;
    public GameObject ArrowRight;
    public GameObject GoToBattleButton;
    public GameObject BattleYes;
    public GameObject BattleNo;

    //public GameObject RightCenterAnchor;
    public GameObject CenterAnchor;

    public float CamBorderTop;
    public float CamBorderBottom;
    public float CamBorderRight;
    public float CamBorderLeft;

    public float MovingCounter = 0.0f;
    public Vector3 OldPlayerPos;
    public float Distance;
    public float Speed;
    public float CountOfWalk;
    public int Liters;
    public int RandomMeet;

    public List<GameObject> WaterContainer;

    public TextMesh InfoText;
    public string GetMessage;

    private int UILayer = 5;
    private float MinusLiter = 0;

    int NumOfField;
    int[] newFieldCover;

    void Start()
    {
        Application.targetFrameRate = 60;
        LoadMap();
        OldPlayerPos = Player.transform.localPosition;
    }

    void Update()
    {
        GetMessage = Player.GetComponent<PlayerChip>().Message;
        Liters = 0;
        foreach (GameObject Water in WaterContainer) {
            Liters += Water.GetComponent<OtherStuff>().Liters; 
        }

        InfoText.text = "Water: " + Liters + "\n" + GetMessage;

        if (Input.GetMouseButtonDown(0)) {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.gameObject.layer != UILayer) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Goal.transform.position = new Vector3(mousePos.x, mousePos.y, Goal.transform.position.z);
                MovingCounter = 0.0f;
                OldPlayerPos = Player.transform.position;
                Distance = Vector3.Distance(OldPlayerPos, Goal.transform.position);
                CountOfWalk = Distance;
            }

            if (hit.collider.gameObject == Player) {
                if (Player.GetComponent<PlayerChip>().ReadyGoToStore == true) {
                    SaveMapAndPlayerData();
                    SceneManager.LoadScene(1);
                }
            }

        }


        CountOfWalk -= Speed/1000;
        MovingCounter = (Distance - CountOfWalk) / Distance;
        Player.transform.position = Vector3.Lerp(OldPlayerPos, Goal.transform.position, MovingCounter);
        if (MovingCounter < 1.0f) {
            Vector3 OldPlayerPos = new Vector3(Player.transform.position.x, Player.transform.position.y, MainCamera.transform.position.z);
            if (Player.GetComponent<PlayerChip>().OnEnemyArea == false) {
                RandomMeet = Random.Range(1, 1000);
            } else {
                RandomMeet = Random.Range(300, 700);
            }
            if (RandomMeet == 500) {
                Goal.transform.position = Player.transform.position;
                BattleMessage.active = true;
                GoToBattleButton.active = false;
                BattleYes.active = false;
                BattleNo.active = false;
                GameObject GetTouch = Player.GetComponent<PlayerChip>().TouchObject;
                if (GetTouch != null && GetTouch.GetComponent<BanditsDoll>() != null) {
                    string BandName = Player.GetComponent<PlayerChip>().TouchObject.gameObject.GetComponent<BanditsDoll>().Clan;
                    BattleMessage.transform.Find("InfoText").GetComponent<TextMesh>().text =
                        "you have got\ninto the " + BandName + "\n area. You have to\nmake a stand";
                    GoToBattleButton.active = true;
                } else {
                    BattleMessage.transform.Find("InfoText").GetComponent<TextMesh>().text =
                        "Hmm. Looks like you\nmet some strangers.\n Will you take a fight?";
                    BattleYes.active = true;
                    BattleNo.active = true;
                }
            }

            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, OldPlayerPos, MovingCounter);

            MinusLiter += 0.1f;
            if (MinusLiter >= 1.0f) {
                if (WaterContainer.Count != 0) {
                    WaterContainer[WaterContainer.Count - 1].gameObject.GetComponent<OtherStuff>().Liters -= 1;
                    if (WaterContainer[WaterContainer.Count - 1].gameObject.GetComponent<OtherStuff>().Liters == 0) {
                        WaterContainer.Remove(WaterContainer[WaterContainer.Count - 1]);
                    }
                    MinusLiter = 0.0f;
                } else {
                    Speed = 5;
                }
            }
        } else if (MovingCounter >= 1.0f) {
            if (ArrowDown.GetComponent<ButtonSample>().isPressed == true) {
                if (MainCamera.transform.position.y > CamBorderBottom) {
                    MainCamera.transform.position -= new Vector3(0, 0.5f, 0);
                }
            }
            if (ArrowLeft.GetComponent<ButtonSample>().isPressed == true) {
                if (MainCamera.transform.position.x > CamBorderLeft) {
                    MainCamera.transform.position -= new Vector3(0.5f, 0, 0);
                }
            }
            if (ArrowRight.GetComponent<ButtonSample>().isPressed == true) {
                if (MainCamera.transform.position.x < CamBorderRight) {
                    MainCamera.transform.position += new Vector3(0.5f, 0, 0);
                }
            }
            if (ArrowUp.GetComponent<ButtonSample>().isPressed == true) {
                if (MainCamera.transform.position.y < CamBorderTop) {
                    MainCamera.transform.position += new Vector3(0, 0.5f, 0);
                }
            }
        }
        if (MainCamera.transform.position.y >= CamBorderTop) {
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, CamBorderTop, MainCamera.transform.position.z);
            ArrowUp.GetComponent<ButtonSample>().isActive = false;
        } else {
            ArrowUp.GetComponent<ButtonSample>().isActive = true;
        }
        if (MainCamera.transform.position.y <= CamBorderBottom) {
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, CamBorderBottom, MainCamera.transform.position.z);
            ArrowDown.GetComponent<ButtonSample>().isActive = false;
        } else {
            ArrowDown.GetComponent<ButtonSample>().isActive = true;
        }
        if (MainCamera.transform.position.x >= CamBorderRight) {
            MainCamera.transform.position = new Vector3(CamBorderRight, MainCamera.transform.position.y, MainCamera.transform.position.z);
            ArrowRight.GetComponent<ButtonSample>().isActive = false;
        } else {
            ArrowRight.GetComponent<ButtonSample>().isActive = true;
        }
        if (MainCamera.transform.position.x <= CamBorderLeft) {
            MainCamera.transform.position = new Vector3(CamBorderLeft, MainCamera.transform.position.y, MainCamera.transform.position.z);
            ArrowLeft.GetComponent<ButtonSample>().isActive = false;
        } else {
            ArrowLeft.GetComponent<ButtonSample>().isActive = true;
        }


        if (GoToBattleButton.GetComponent<ButtonSample>().isPressed == true) {
            SaveMapAndPlayerData();
            GenerateBanditTroop();
            SceneManager.LoadScene(3);
        }
        if (BattleYes.GetComponent<ButtonSample>().isPressed == true) {
            SaveMapAndPlayerData();
            GenerateBanditTroop();
            SceneManager.LoadScene(3);
        }
        if (BattleNo.GetComponent<ButtonSample>().isPressed == true) {
            BattleNo.GetComponent<ButtonSample>().isPressed = false;
            BattleMessage.active = false;
        }

    }

    void LoadMap() {
        if (File.Exists(Application.persistentDataPath + "/MapData.json")) {

            string MapData = File.ReadAllText(Application.persistentDataPath + "/MapData.json").ToString();

            MapData GetMap = new MapData();
            GetMap = JsonUtility.FromJson<MapData>(MapData);

            int GetMapTileNum = 0;
            foreach (MapTileSample TileMap in GetMap.Tiles) {
                GameObject Tile = Instantiate(Resources.Load("TileMapDoll")) as GameObject;
                Tile.GetComponent<MapTile>().Skin = GetMap.Tiles[GetMapTileNum].Skin;
                for (int s = 0; s < GetMap.Tiles[GetMapTileNum].Smokes.Length; s++) {
                    if (GetMap.Tiles[GetMapTileNum].Smokes[s] == false) {
                        Tile.GetComponent<MapTile>().SmokeOfWar[s].gameObject.active = false;
                    }
                }
                Tile.name = "Tile_" + GetMapTileNum;
                Tile.transform.SetParent(MapContainer.transform);
                Tile.transform.localPosition = GetMap.Tiles[GetMapTileNum].Coordinates;
                GetMapTileNum += 1;
            }


            int GetBanditNum = 0;
            foreach (BanditArea Band in GetMap.GenerateIndexes.Bandits) {
                GameObject newBand = Instantiate(Resources.Load("BanditsAreaDoll")) as GameObject;
                newBand.transform.SetParent(MapContainer.transform);
                newBand.transform.localPosition = MapContainer.transform.GetChild(GetMap.GenerateIndexes.Bandits[GetBanditNum].Coordinates).transform.localPosition + new Vector3(0, 0, -0.1f);
                newBand.GetComponent<BanditsDoll>().Clan = GetMap.GenerateIndexes.Bandits[GetBanditNum].Clan;
                newBand.GetComponent<BanditsDoll>().Coverage = GetMap.GenerateIndexes.Bandits[GetBanditNum].Coverage;
                newBand.GetComponent<BanditsDoll>().Population = GetMap.GenerateIndexes.Bandits[GetBanditNum].Population;
                newBand.GetComponent<BanditsDoll>().Number = GetMap.GenerateIndexes.Bandits[GetBanditNum].NumberOfArea;
                GetBanditNum += 1;
            }

            int GetStoreNum = 0;
            foreach (Store store in GetMap.GenerateIndexes.Stores) {
                GameObject newStore = Instantiate(Resources.Load("StoresDoll")) as GameObject;
                newStore.transform.SetParent(MapContainer.transform);
                newStore.transform.localPosition = GetMap.GenerateIndexes.Stores[GetStoreNum].Coordinates + new Vector3(0, 0, -0.1f);
                GetStoreNum += 1;
            }

            Player.transform.localPosition = new Vector3(GetMap.GenerateIndexes.PlayerCoords.x, GetMap.GenerateIndexes.PlayerCoords.y, InfoText.transform.position.z + 0.5f);

            CamBorderTop = MapContainer.transform.GetChild(0).transform.position.y - 0.32f * 2;
            CamBorderLeft = MapContainer.transform.GetChild(0).transform.position.x + 0.32f * 5;
            CamBorderBottom = MapContainer.transform.GetChild(GetMap.Tiles.Count - 1).transform.position.y + 0.32f * 2;
            CamBorderRight = MapContainer.transform.GetChild(GetMap.Tiles.Count - 1).transform.position.x - 0.32f * 2;

        }
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {

            string PlayerData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json").ToString();

            INVENTORY PlayerSource = new INVENTORY();
            PlayerSource = JsonUtility.FromJson<INVENTORY>(PlayerData);

            foreach (SaveStuff GetStuff in PlayerSource.AllStuff) {
                if (GetStuff.Skin == 2) {
                    foreach (SaveSlave GetSlave in PlayerSource.AllSlaves) {
                        for (int a = 0; a < GetSlave.Package.Length; a++) {
                            if (GetStuff.ID == GetSlave.Package[a]) {
                                GameObject Water = Instantiate(Resources.Load("OtherStuff")) as GameObject;
                                Water.name = "Water" + GetStuff.ID;
                                Water.GetComponent<OtherStuff>().Number = GetStuff.ID;
                                Water.GetComponent<OtherStuff>().Skin = GetStuff.Skin;
                                Water.GetComponent<OtherStuff>().Liters = GetStuff.Liters;
                                Water.GetComponent<SpriteRenderer>().enabled = false;
                                Water.transform.SetParent(WaterPack.transform);
                                Water.transform.localPosition = new Vector3(0, 0, 0);
                                WaterContainer.Add(Water);
                            }
                        }
                    }
                }
            }
        }
        MapContainer.transform.localPosition -= new Vector3(0, 0, Player.transform.localPosition.z) ;
        Goal.transform.localPosition = Player.transform.localPosition;
        MainCamera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, MainCamera.transform.position.z);
    }

    void SaveMapAndPlayerData() {

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {

            string PlayerData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");

            INVENTORY GetPlayerData = new INVENTORY();
            GetPlayerData = JsonUtility.FromJson<INVENTORY>(PlayerData);

            for (int w = 0; w < WaterPack.transform.childCount; w++) {
                GameObject GetWater = WaterPack.transform.GetChild(w).gameObject;
                if (GetWater.GetComponent<OtherStuff>().Liters == 0) {
                    for (int s = 0; s < GetPlayerData.AllStuff.Count; s++) {
                        SaveStuff GetStuff = GetPlayerData.AllStuff[s];
                        if (GetWater.GetComponent<OtherStuff>().Number == GetStuff.ID) {
                            GetPlayerData.PlayerSource.Stuff -= 1;
                            GetPlayerData.AllStuff.Remove(GetStuff);
                        }
                    }
                    foreach (SaveSlave GetSlave in GetPlayerData.AllSlaves) {
                        for (int s = 0; s < GetSlave.Package.Length; s++) {
                            if (GetSlave.Package[s] == GetWater.GetComponent<OtherStuff>().Number) {
                                GetSlave.Package[s] = 0;
                            }
                        }
                    }
                } else {
                    for (int s = 0; s < GetPlayerData.AllStuff.Count; s++) {
                        if (GetWater.GetComponent<OtherStuff>().Number == GetPlayerData.AllStuff[s].ID) {
                            GetPlayerData.AllStuff[s].Liters = GetWater.GetComponent<OtherStuff>().Liters;
                        }
                    }
                }
            }

            string NewPlayerData = JsonUtility.ToJson(GetPlayerData);
            StreamWriter UploadNewPlayerData = new StreamWriter(Application.persistentDataPath + "/PlayerData.json");
            UploadNewPlayerData.Write(NewPlayerData);
            UploadNewPlayerData.Close();

        }

        if (File.Exists(Application.persistentDataPath + "/MapData.json")) {

            string MapData = File.ReadAllText(Application.persistentDataPath + "/MapData.json");

            MapData GetMapData = new MapData();
            GetMapData = JsonUtility.FromJson<MapData>(MapData);

            GetMapData.GenerateIndexes.PlayerCoords = Player.transform.localPosition;
            for (int t = 0; t < MapContainer.transform.childCount; t++) {
                GameObject GetMapIssue = MapContainer.transform.GetChild(t).gameObject;
                if (GetMapIssue.GetComponent<MapTile>() != null) {
                    GetMapData.Tiles[t].Skin = GetMapIssue.GetComponent<MapTile>().Skin;
                    for (int a = 0; a < GetMapIssue.GetComponent<MapTile>().SmokeOfWar.Length; a++) {
                        if (GetMapIssue.GetComponent<MapTile>().SmokeOfWar[a].gameObject.active == false) {
                            GetMapData.Tiles[t].Smokes[a] = false;
                        } else {
                            GetMapData.Tiles[t].Smokes[a] = true;
                        }
                    }
                }
            }

            string NewMapData = JsonUtility.ToJson(GetMapData);
            StreamWriter UploadNewMapData = new StreamWriter(Application.persistentDataPath + "/MapData.json");
            UploadNewMapData.Write(NewMapData);
            UploadNewMapData.Close();

        }
    }

    public void GenerateBanditTroop() {
        string GetBandTroopData;

        if (File.Exists(Application.persistentDataPath + "/BanditTroop.json")) {
            GetBandTroopData = File.ReadAllText(Application.persistentDataPath + "/BanditTroop.json");
        } else {
            File.Create(Application.persistentDataPath + "/BanditTroop.json");
        }

        TroopData NewTroop = new TroopData();

        GameObject GetTouch = Player.GetComponent<PlayerChip>().TouchObject;
        if (GetTouch != null && GetTouch.gameObject.GetComponent<BanditsDoll>() != null) {
            NewTroop.Band = Player.GetComponent<PlayerChip>().TouchObject.GetComponent<BanditsDoll>().Clan;
            NewTroop.NumberOfArea = Player.GetComponent<PlayerChip>().TouchObject.GetComponent<BanditsDoll>().Number;
            NewTroop.TroopCount = Random.Range(1, 10);
            if (NewTroop.TroopCount > Player.GetComponent<PlayerChip>().TouchObject.GetComponent<BanditsDoll>().Population) {
                NewTroop.TroopCount = Player.GetComponent<PlayerChip>().TouchObject.GetComponent<BanditsDoll>().Population;
            }
        } else {
            NewTroop.Band = "Unknown";
            NewTroop.TroopCount = Random.Range(1, 10);
        }

        newFieldCover = new int[NewTroop.TroopCount];
        for (NumOfField = 0; NumOfField < newFieldCover.Length; NumOfField++) {
            RandomRepeat();
        }

        for (int d = 0; d < newFieldCover.Length; d++) {
            NewTroop.Places[newFieldCover[d]] = true;
        }

        for (int b = 0; b < NewTroop.TroopCount; b++) {
            EnemyData NewEnemy = new EnemyData();
            NewEnemy.Skin = Random.Range(1, 6);

            int healthCoof = Random.Range(1, 9);
            int damageCoof = Random.Range(1, 10 - healthCoof);
            int accuracyCoof = 10 - (healthCoof + damageCoof);

            NewEnemy.FullHealth = 15 * healthCoof;
            NewEnemy.Health = 15 * healthCoof;
            NewEnemy.Damage = 15 * damageCoof;
            NewEnemy.Accuracy = 15 * accuracyCoof;

            //NewEnemy.Weapon = Random.Range(1, 6);

            NewEnemy.Level = Random.Range(1, 5);
            NewTroop.Enemies.Add(NewEnemy);
        }

        string NewEnemyData = JsonUtility.ToJson(NewTroop);
        StreamWriter WriteEnemyData = new StreamWriter(Application.persistentDataPath + "/BanditTroop.json");
        WriteEnemyData.Write(NewEnemyData);
        WriteEnemyData.Close();

    }

    public void RandomRepeat() {
        int rand = Random.Range(0, 9);
        newFieldCover[NumOfField] = rand;
        for (int s = 0; s < NumOfField; s++) {
            if (newFieldCover[s] == newFieldCover[NumOfField]) {
                RandomRepeat();
            }
        }
    }
}

[System.Serializable]
public class TroopData {
    public int NumberOfArea;
    public string Band;
    public int TroopCount;
    public bool[] Places = new bool[9];
    public List<EnemyData> Enemies = new List<EnemyData>();
}

[System.Serializable]
public class EnemyData {
    public int Skin;
    public int FullHealth;
    public int Health;
    public int Damage;
    public int Accuracy;
    public int Level;
    //public int Weapon;
    //public int Place;
}
