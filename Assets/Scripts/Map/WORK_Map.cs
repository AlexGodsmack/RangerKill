using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class WORK_Map : MonoBehaviour
{
    //========= Значения в инспекторе ==========
    [Header("Objects")]
    public GameObject MainCamera;
    public GameObject MapContainer;
    public GameObject MapField;
    public GameObject Player;
    public ButtonSample PlayerIndicator;
    public GameObject Goal;
    public GameObject MainCollider;
    public GameObject WaterPack;
    public GameObject BattleMessage;
    [Header("Menu Panel")]
    public GameObject MapPanel;
    public GameObject ArrowUp;
    public GameObject ArrowDown;
    public GameObject ArrowLeft;
    public GameObject ArrowRight;
    public GameObject GoToBattleButton;
    public GameObject BattleYes;
    public GameObject BattleNo;
    public GameObject GoToWaterTower;
    public GameObject OkButton;
    [Header("Anchors")]
    public GameObject RightCenterAnchor;
    public GameObject CenterAnchor;
    [Header("Map borders")]
    public float CamBorderTop;
    public float CamBorderBottom;
    public float CamBorderRight;
    public float CamBorderLeft;
    [Header("Active Numbers")]
    public float MovingCounter = 0.0f;
    public bool Go;
    public Vector3 OldPlayerPos;
    public float Distance;
    public float Speed;
    public float CountOfWalk;
    public int Liters;
    public int RandomMeet;
    public int Minimal;
    public int Maximal;
    [Header("Classes")]
    public SaveLoadData Loader;
    public MainPlayerControl PlayInv;
    public List<GameObject> WaterContainer;
    public Tutorial Tutor;
    [Header("Texts")]
    public TextMesh InfoText;
    public string GetMessage;
    [Header("Layers")]
    public int UILayer = 5;
    public float MinusLiter = 0;
    [Header("Sounds")]
    public AudioSource Walk;

    int NumOfField;
    int[] newFieldCover;

    //============== Старте ==============
    void Start()
    {
        Application.targetFrameRate = 60;
        LoadMap();
        OldPlayerPos = Player.transform.localPosition;
        Minimal = 0;
        Maximal = 2000;
        if (PlayInv.If_Tutorial == true) {
            Tutor.Steps = PlayInv.Step_Of_Tutorial;
            Tutor.enabled = true;
        } else {
            Tutor.gameObject.active = false;
        }
    }

    //============== Действия на карте ===============
    void Update()
    {
        GetMessage = Player.GetComponent<PlayerChip>().Message;
        Liters = 0;
        foreach (GameObject Water in WaterContainer) {
            Liters += Water.GetComponent<OtherStuff>().Liters;
        }

        InfoText.text = "Water: " + Liters + "\n" + GetMessage;

        if (PlayerIndicator.isPressed == true) {
            if (Player.GetComponent<PlayerChip>().ReadyGoToStore == true) {
                if (Tutor != null) {
                    if (Tutor.Steps == 22 || Tutor.Steps == 37) {
                        Tutor.Steps += 1;
                        Tutor.PickMonitor.Play();
                    }
                }
                SaveMapAndPlayerData();
                if (Player.GetComponent<PlayerChip>().TouchObject != null) {
                    GameObject GetStore = Player.GetComponent<PlayerChip>().TouchObject.gameObject;
                    if (GetStore.GetComponent<StoreChip>().TypeOfStore == "Slaves") {
                        SceneManager.LoadScene(1);
                    } else if (GetStore.GetComponent<StoreChip>().TypeOfStore == "Guns") {
                        SceneManager.LoadScene(2);
                    } else if (GetStore.GetComponent<StoreChip>().TypeOfStore == "Bullets") {
                        SceneManager.LoadScene(3);
                    } else if (GetStore.GetComponent<StoreChip>().TypeOfStore == "Stuff") {
                        SceneManager.LoadScene(4);
                    } else if (GetStore.GetComponent<StoreChip>().TypeOfStore == "Recycling") {
                        SceneManager.LoadScene(7);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0)) {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.gameObject.layer != UILayer) {
                if (hit.collider.gameObject == MainCollider) {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Goal.transform.position = new Vector3(mousePos.x, mousePos.y, Goal.transform.position.z);
                    MovingCounter = 0.0f;
                    OldPlayerPos = Player.transform.position;
                    Distance = Vector3.Distance(OldPlayerPos, Goal.transform.position);
                    CountOfWalk = Distance;
                    Go = true;
                }
            }

        }

        if (Go == true) {
            CountOfWalk -= Speed / 1000;
            MovingCounter = (Distance - CountOfWalk) / Distance;
            Player.transform.position = Vector3.Lerp(OldPlayerPos, Goal.transform.position, MovingCounter);
            if (MovingCounter < 1.0f) {
                if (!Walk.isPlaying) {
                    Walk.Play();
                }
                Vector3 OldPlayerPos = new Vector3(Player.transform.position.x, Player.transform.position.y, MainCamera.transform.position.z);
                if (Player.GetComponent<PlayerChip>().OnEnemyArea == false) {
                    Minimal = 0;
                    Maximal = 2000;
                    //RandomMeet = Random.Range(Minimal, Maximal);
                } else {
                    if (Minimal != 1000) {
                        Minimal += 10;
                    }
                    if (Maximal != 1000) {
                        Maximal -= 10;
                    }
                    RandomMeet = Random.Range(Minimal, Maximal);
                }
                if (RandomMeet == 1000) {
                    Go = false;
                    if (Walk.isPlaying) {
                        Walk.Stop();
                    }
                    BattleMessage.active = true;
                    GoToBattleButton.active = false;
                    BattleYes.active = false;
                    BattleNo.active = false;
                    GameObject GetTouch = Player.GetComponent<PlayerChip>().TouchObject;
                    if (GetTouch != null && GetTouch.GetComponent<BanditsDoll>() != null) {
                        if (GetTouch.GetComponent<BanditsDoll>().Clan != "") {
                            string BandName = Player.GetComponent<PlayerChip>().TouchObject.gameObject.GetComponent<BanditsDoll>().Clan;
                            BattleMessage.transform.Find("InfoText").GetComponent<TextMesh>().text =
                                "you have got\ninto the " + BandName + "\n area. You have to\nmake a stand";
                            GoToBattleButton.active = true;
                        }
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
                        if (WaterContainer[WaterContainer.Count - 1].gameObject.GetComponent<OtherStuff>().Liters <= 0) {
                            GameObject Bottle = WaterContainer[WaterContainer.Count - 1].gameObject;
                            WaterContainer.Remove(WaterContainer[WaterContainer.Count - 1]);
                            Destroy(Bottle);
                        }
                        MinusLiter = 0.0f;
                    } else {
                        Speed = 5;
                    }
                }
            } else if (MovingCounter >= 1.0f) {
                if (Walk.isPlaying) {
                    Walk.Stop();
                }
                Go = false;
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
        } else {
            //Go = false;
            if (Walk.isPlaying) {
                Walk.Stop();
            }
        }

        if (MainCamera.transform.position.y >= CamBorderTop) {
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, CamBorderTop, MainCamera.transform.position.z);
            if (Tutor.Steps == 45) {
                Tutor.Main.transform.position = new Vector3(Tutor.Main.transform.position.x, CamBorderTop, Tutor.transform.position.z);
            }
            ArrowUp.GetComponent<ButtonSample>().isActive = false;
        } else {
            ArrowUp.GetComponent<ButtonSample>().isActive = true;
        }
        if (MainCamera.transform.position.y <= CamBorderBottom) {
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, CamBorderBottom, MainCamera.transform.position.z);
            if (Tutor.Steps == 45) {
                Tutor.Main.transform.position = new Vector3(Tutor.Main.transform.position.x, CamBorderBottom, Tutor.transform.position.z);
            }
            ArrowDown.GetComponent<ButtonSample>().isActive = false;
        } else {
            ArrowDown.GetComponent<ButtonSample>().isActive = true;
        }
        if (MainCamera.transform.position.x >= CamBorderRight) {
            MainCamera.transform.position = new Vector3(CamBorderRight, MainCamera.transform.position.y, MainCamera.transform.position.z);
            if (Tutor.Steps == 45) {
                Tutor.Main.transform.position = new Vector3(CamBorderRight, Tutor.Main.transform.position.y, Tutor.transform.position.z);
            }
            ArrowRight.GetComponent<ButtonSample>().isActive = false;
        } else {
            ArrowRight.GetComponent<ButtonSample>().isActive = true;
        }
        if (MainCamera.transform.position.x <= CamBorderLeft) {
            MainCamera.transform.position = new Vector3(CamBorderLeft, MainCamera.transform.position.y, MainCamera.transform.position.z);
            if (Tutor.Steps == 45) {
                Tutor.Main.transform.position = new Vector3(CamBorderLeft, Tutor.Main.transform.position.y, Tutor.transform.position.z);
            }
            ArrowLeft.GetComponent<ButtonSample>().isActive = false;
        } else {
            ArrowLeft.GetComponent<ButtonSample>().isActive = true;
        }


        if (GoToBattleButton.GetComponent<ButtonSample>().isPressed == true) {
            SaveMapAndPlayerData();
            GenerateBanditTroop();
            SceneManager.LoadScene(6);
        }
        if (BattleYes.GetComponent<ButtonSample>().isPressed == true) {
            SaveMapAndPlayerData();
            GenerateBanditTroop();
            SceneManager.LoadScene(6);
        }
        if (BattleNo.GetComponent<ButtonSample>().isPressed == true) {
            BattleNo.GetComponent<ButtonSample>().isPressed = false;
            BattleMessage.active = false;
            Go = true;
        }
        if (OkButton.GetComponent<ButtonSample>().isPressed == true) {
            OkButton.GetComponent<ButtonSample>().isPressed = false;
            GoToWaterTower.active = false;
        }

    }

    //=========================== Загрузка карты и ресурсов игрока =============================
    void LoadMap() {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {

            Loader.LoadAll();
            foreach(GameObject Slv in PlayInv.SlavePlace) {
                if (Slv != null) {
                    Slv.transform.localPosition = new Vector3(0, 0, 0);
                    GameObject GetPack = Slv.GetComponent<SlaveProperties>().InventoryPack.gameObject;
                    foreach (Transform place in GetPack.transform) {
                        if (place.transform.childCount != 0) {
                            GameObject getItem = place.transform.GetChild(0).gameObject;
                            if (getItem.GetComponent<OtherStuff>() != null) {
                                if (getItem.GetComponent<OtherStuff>().Skin == 2) {
                                    WaterContainer.Add(getItem);
                                }
                            }
                        }
                    }
                }
            }
            
        }
        if (File.Exists(Application.persistentDataPath + "/MapData.json")) {

            string MapData = File.ReadAllText(Application.persistentDataPath + "/MapData.json").ToString();
            
            MapData GetMap = new MapData();
            GetMap = JsonUtility.FromJson<MapData>(MapData);

            int GetMapTileNum = 0;
            foreach (MapTileSample TileMap in GetMap.Tiles) {
                GameObject Tile = Instantiate(Resources.Load("TileMapDoll")) as GameObject;
                Tile.GetComponent<MapTile>().TileID = GetMap.Tiles[GetMapTileNum].TileID;
                Tile.GetComponent<MapTile>().Skin = GetMap.Tiles[GetMapTileNum].Skin;
                for (int s = 0; s < GetMap.Tiles[GetMapTileNum].Smokes.Length; s++) {
                    if (GetMap.Tiles[GetMapTileNum].Smokes[s] == false) {
                        Tile.GetComponent<MapTile>().SmokeOfWar[s].gameObject.active = false;
                    }
                }

                Tile.name = "Tile_" + Tile.GetComponent<MapTile>().TileID;
                Tile.transform.SetParent(MapContainer.transform);
                Tile.transform.localPosition = GetMap.Tiles[GetMapTileNum].Coordinates;
                //if (TileMap.Empty == false) {
                //    Tile.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                //}
                GetMapTileNum += 1;
            }


            int GetBanditNum = 0;
            foreach (BanditArea Band in GetMap.GenerateIndexes.Bandits) {
                GameObject newBand = Instantiate(Resources.Load("BanditsAreaDoll")) as GameObject;
                newBand.transform.SetParent(MapContainer.transform);
                newBand.transform.localPosition = MapContainer.transform.GetChild(GetMap.GenerateIndexes.Bandits[GetBanditNum].Coordinates - 1).transform.localPosition + new Vector3(0, 0, -0.1f);
                newBand.GetComponent<BanditsDoll>().Clan = GetMap.GenerateIndexes.Bandits[GetBanditNum].Clan;
                newBand.GetComponent<BanditsDoll>().Coverage = GetMap.GenerateIndexes.Bandits[GetBanditNum].Coverage;
                newBand.GetComponent<BanditsDoll>().Population = GetMap.GenerateIndexes.Bandits[GetBanditNum].Population;
                newBand.GetComponent<BanditsDoll>().Number = GetMap.GenerateIndexes.Bandits[GetBanditNum].NumberOfArea;
                newBand.GetComponent<BanditsDoll>().Attacks = GetMap.GenerateIndexes.Bandits[GetBanditNum].Attacks;
                if (PlayInv.If_Tutorial == true) {
                    foreach (Collider2D col in newBand.GetComponent<BanditsDoll>().Colliders) {
                        col.enabled = false;
                    }
                } else {
                    if (newBand.GetComponent<BanditsDoll>().Clan != "") {
                        newBand.GetComponent<BanditsDoll>().Colliders[newBand.GetComponent<BanditsDoll>().Coverage - 1].enabled = true;
                    }
                }
                GetBanditNum += 1;
            }

            int GetStoreNum = 0;
            foreach (Store store in GetMap.GenerateIndexes.Stores) {
                GameObject newStore = Instantiate(Resources.Load("StoresDoll")) as GameObject;
                newStore.transform.SetParent(MapContainer.transform);
                newStore.GetComponent<StoreChip>().TypeOfStore = GetMap.GenerateIndexes.Stores[GetStoreNum].Type;
                newStore.GetComponent<StoreChip>().StoreID = GetMap.GenerateIndexes.Stores[GetStoreNum].StoreID;
                Vector3 GetCoords = GetMap.Tiles[GetMap.GenerateIndexes.Stores[GetStoreNum].TileID - 1].Coordinates;
                newStore.transform.localPosition = GetCoords + new Vector3(0, 0, -0.1f);
                GetStoreNum += 1;
            }
            int GetObstNum = 0;
            foreach (Obstacle Obst in GetMap.GenerateIndexes.Obstacles) {
                GameObject newObst = Instantiate(Resources.Load("Obstacles")) as GameObject;
                newObst.transform.SetParent(MapContainer.transform);
                newObst.GetComponent<Obstacles>().Skin = Obst.Skin;
                Vector3 GetCoords = GetMap.Tiles[Obst.Place].Coordinates;
                newObst.transform.localPosition = GetCoords + new Vector3(0, 0, -0.1f);
                GetObstNum += 1;
            }

            Player.transform.localPosition = new Vector3(GetMap.GenerateIndexes.PlayerCoords.x, GetMap.GenerateIndexes.PlayerCoords.y, InfoText.transform.position.z + 0.5f);

            CamBorderTop = MapContainer.transform.GetChild(0).transform.position.y - 0.32f * 2;
            CamBorderLeft = MapContainer.transform.GetChild(0).transform.position.x + 0.32f * 5;
            CamBorderBottom = MapContainer.transform.GetChild(GetMap.Tiles.Count - 1).transform.position.y + 0.32f * 2;
            CamBorderRight = MapContainer.transform.GetChild(GetMap.Tiles.Count - 1).transform.position.x - 0.32f * 2;

            foreach (BanditArea band in GetMap.GenerateIndexes.Bandits) {
                if (band.Clan == "") {
                    GoToWaterTower.active = true;
                } else {
                    GoToWaterTower.active = false;
                    break;
                }
            }

        }
        MapContainer.transform.localPosition -= new Vector3(0, 0, Player.transform.localPosition.z);
        Goal.transform.localPosition = Player.transform.localPosition;
        MainCamera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, MainCamera.transform.position.z);
    }
    
    //=========================== Включение врагов =============================
    public void Turn_on_Clans() {
        foreach (Transform band in MapContainer.transform) {
            if (band.GetComponent<BanditsDoll>() != null) {
                band.GetComponent<BanditsDoll>().Colliders[band.GetComponent<BanditsDoll>().Coverage - 1].enabled = true;
            }
        }
    }

    //=========================== Сохранение карты и ресурсов игрока =============================
    void SaveMapAndPlayerData() {

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {

            if (Player.GetComponent<PlayerChip>().TouchObject != null) {
                if (Player.GetComponent<PlayerChip>().TouchObject.gameObject.layer == 16) {
                    GameObject Store = Player.GetComponent<PlayerChip>().TouchObject.gameObject;
                    PlayInv.StoreID = Store.GetComponent<StoreChip>().StoreID;
                    PlayInv.TypeOfStore = Store.GetComponent<StoreChip>().TypeOfStore;
                }
            }
            Loader.SaveAll();

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

    //================================= Генерация врагов ===================================
    public void GenerateBanditTroop() {
        string GetBandTroopData;

        //if (File.Exists(Application.persistentDataPath + "/BanditTroop.json")) {
        //    GetBandTroopData = File.ReadAllText(Application.persistentDataPath + "/BanditTroop.json");
        //} else {
        //    File.Create(Application.persistentDataPath + "/BanditTroop.json");
        //}

        TroopData NewTroop = new TroopData();

        GameObject GetTouch = Player.GetComponent<PlayerChip>().TouchObject;
        if (GetTouch != null && GetTouch.gameObject.GetComponent<BanditsDoll>() != null) {
            BanditsDoll Band = GetTouch.gameObject.GetComponent<BanditsDoll>();
            NewTroop.Band = Band.Clan;
            NewTroop.NumberOfArea = Band.Number;
            if (Band.Attacks == 0) {
                NewTroop.TroopCount = Random.Range(1, 3);
                if (NewTroop.TroopCount > Band.Population) {
                    NewTroop.TroopCount = Band.Population;
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
                    NewEnemy.Skin = Random.Range(1, 4);

                    int healthCoof = Random.Range(1, 9);
                    int damageCoof = Random.Range(1, 10 - healthCoof);
                    int accuracyCoof = 10 - (healthCoof + damageCoof);

                    NewEnemy.FullHealth = 15 * healthCoof;
                    NewEnemy.Health = 15 * healthCoof;
                    NewEnemy.Damage = 15 * damageCoof;
                    NewEnemy.Accuracy = 15 * accuracyCoof;

                    //NewEnemy.Weapon = Random.Range(1, 6);

                    NewEnemy.Level = Random.Range(1, 3);
                    NewTroop.Enemies.Add(NewEnemy);
                }
            } else if (Band.Attacks == 1) {
                NewTroop.TroopCount = Random.Range(3, 6);
                if (NewTroop.TroopCount > Band.Population) {
                    NewTroop.TroopCount = Band.Population;
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

                    NewEnemy.Level = Random.Range(2, 5);
                    NewTroop.Enemies.Add(NewEnemy);
                }
            } else if (Band.Attacks >= 2) {
                NewTroop.TroopCount = Random.Range(6, 10);
                if (NewTroop.TroopCount > Band.Population) {
                    NewTroop.TroopCount = Band.Population;
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

                    NewEnemy.Level = Random.Range(4, 10);
                    NewTroop.Enemies.Add(NewEnemy);
                }
            }
        } else {
            NewTroop.Band = "Unknown";
            NewTroop.TroopCount = Random.Range(1, 7);

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

                NewEnemy.Level = Random.Range(1, 5);
                NewTroop.Enemies.Add(NewEnemy);
            }
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
