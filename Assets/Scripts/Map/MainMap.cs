using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMap : MonoBehaviour {

    public GameObject MainHero;
    public GameObject MsgBattle;
    public GameObject MenuSet;
    public GameObject MapSprites;
    public GameObject HeroGoal;
    public GameObject UIField;
    public GameObject ContainerForStuff;
    public Button GoToBattle, Cancel, Menu, Back, Quit, Left, Up, Down, Right;
    public Text InfoScreen;
    public string EnemiesProps;

    //public string MapGen;

    public int Columns;
    public int Rows;
    public float WidthOfCell;
    public float HeightOfCell;
    public float speed = 5.0f;
    public int battlevar = 10000;
    public int RandomInt;
    public int NumberOFActiveBand = 0;
    public bool StayOnStore = false;
    public bool GO = false;

    private float dist;
    private Vector3 newHeroPos;
    private Vector3 mousePos;
    private bool PauseTrig = false;

    private string MapGenPath;
    private string EnemiesPropsPath;
    private string PlaySetPath;
    private string CountOfAllPath;
    private string InvSetPath;

    private Vector3 OldPosHero;
    private Vector3 Hypotenuse;
    private float MotionCounter;
    private float Seconds = 0.0f;
    private float WaterDecrease = 0.0f;

    private float MovingMapStep = 0.2f;

    private string[] CountOfAll;
    private string[] PlaySet;
    private string[] MapGen;
    private string[] InvSet;

    private int CoPers;
    private int CoWeap;
    private int CoBullet;
    private int CoStuff;
    private int NumPersParam = 8;
    private int NumWpnParam = 6;

    private int WaterLiters;

    //===================================================================================================================================================

    void Start () {

        //================================================================ Import Data =====================================================================

        CountOfAllPath = Application.persistentDataPath + "/CountOfAll.txt";
        CountOfAll = File.ReadAllLines(CountOfAllPath);
        PlaySetPath = Application.persistentDataPath + "/PlayerSource.txt";
        PlaySet = File.ReadAllLines(PlaySetPath);
        MapGenPath = Application.persistentDataPath + "/MapGen.txt";
        MapGen = File.ReadAllLines(MapGenPath);
        InvSetPath = Application.persistentDataPath + "/InventorySettings.txt";
        InvSet = File.ReadAllLines(InvSetPath);

        CoPers = int.Parse(CountOfAll[0]);
        CoWeap = int.Parse(CountOfAll[1]);
        CoBullet = int.Parse(CountOfAll[2]);
        CoStuff = int.Parse(CountOfAll[3]);

        WaterLiters = 0;

        for (int i = 1; i < 11; i++) {
            if (MapGen[2 * Columns * Rows + 12 + 10 + 6 + i] != "Empty") {
                WaterLiters = WaterLiters + int.Parse(MapGen[2 * Columns * Rows + 12 + 10 + 6 + i]);
            }
        }

        //================================================================ Generate Map =====================================================================

        UIField.transform.position = Down.transform.position;

        float StartPosX = WidthOfCell / 2 - (Columns * WidthOfCell) / 2;
        float StartPosY = Rows * HeightOfCell / 2 - HeightOfCell / 2;

        float NewX = StartPosX;
        float NewY = StartPosY;

        int countColumn = 1;

        for (int c = 1; c < Rows * Columns + 1; c++) {

            GameObject NewTile = Instantiate(Resources.Load("TileMapDoll")) as GameObject;
            NewTile.GetComponent<Tile>().NumberOfTile = int.Parse(MapGen[2 * c - 2]);
            NewTile.GetComponent<Tile>().HaveSmoke = bool.Parse(MapGen[2 * c - 1]);
            NewTile.name = "Tile" + (c).ToString();
            NewTile.transform.SetParent(MapSprites.transform);
            NewTile.transform.localPosition = new Vector3(NewX, NewY, 0);

            countColumn = countColumn + 1;

            NewX = NewX + WidthOfCell;

            if (countColumn == Columns + 1)
            {
                NewX = StartPosX;
                NewY = NewY - HeightOfCell;
                countColumn = 1;
            }

        }

        for (int f = 1; f < 4; f++) {

            GameObject NewBand = Instantiate(Resources.Load("BanditsAreaDoll")) as GameObject;
            NewBand.name = "BAND_" + f.ToString();
            NewBand.GetComponent<BanditsAreaDoll>().NumberOfBand = int.Parse(MapGen[2 * Columns * Rows + f * 4 - 4 + 1]);
            NewBand.GetComponent<BanditsAreaDoll>().SizeOfArea = int.Parse(MapGen[2 * Columns * Rows + f * 4 - 4 + 2]);
            NewBand.transform.SetParent(MapSprites.transform);
            Vector3 NewPosition = GameObject.Find(MapSprites.name + "/Tile" + int.Parse(MapGen[2 * Columns * Rows + f * 4 - 4 + 3].ToString())).transform.localPosition;
            NewBand.transform.localPosition = new Vector3(NewPosition.x, NewPosition.y, NewPosition.z - 1);

        }

        for (int s = 1; s < 6; s++) {
            GameObject NewStore = Instantiate(Resources.Load("StoresDoll")) as GameObject;
            NewStore.name = "Store" + s.ToString();
            NewStore.transform.SetParent(MapSprites.transform);
            Vector3 NewPosition = GameObject.Find(MapSprites.name + "/Tile" + int.Parse(MapGen[2 * Columns * Rows + 12 + 2 * s - 2 + 1]).ToString()).transform.localPosition;
            NewStore.transform.localPosition = new Vector3(NewPosition.x, NewPosition.y, NewPosition.z - 1);
        }
        float MapX = float.Parse(MapGen[2 * Columns * Rows + 12 + 10 + 1]);
        float MapY = float.Parse(MapGen[2 * Columns * Rows + 12 + 10 + 2]);
        float HeroX = float.Parse(MapGen[2 * Columns * Rows + 12 + 10 + 4]);
        float HeroY = float.Parse(MapGen[2 * Columns * Rows + 12 + 10 + 5]);
        MapSprites.transform.localPosition = new Vector3( MapX, MapY, MapSprites.transform.localPosition.z);
        MainHero.transform.localPosition = new Vector3(HeroX, HeroY, MainHero.transform.localPosition.z);

        GoToBattle.onClick.AddListener(StartBattle);
        Cancel.onClick.AddListener(BackToRide);
        Menu.onClick.AddListener(OpenMenu);
        Quit.onClick.AddListener(ExitGame);

        Left.onClick.AddListener(LeftClick);
        Up.onClick.AddListener(UpClick);
        Down.onClick.AddListener(DownClick);
        Right.onClick.AddListener(RightClick);

        HeroGoal.transform.localPosition = MainHero.transform.localPosition + new Vector3(0, 0, 1);

        //ScreenMonitoring();

	}

    //======================================================================== Navigation ===========================================================================

    void LeftClick() {
        Left.GetComponent<AudioSource>().Play();
        if (MapSprites.transform.localPosition.x <= 1.4f)
        {
            MapSprites.transform.localPosition = new Vector3(MapSprites.transform.localPosition.x + MovingMapStep, MapSprites.transform.localPosition.y, MapSprites.transform.localPosition.z);
            Right.interactable = true;
        }
        else {
            Left.interactable = false;
        }
    }

    void UpClick() {
        Up.GetComponent<AudioSource>().Play();
        if (MapSprites.transform.localPosition.y >= -2.2f)
        {
            MapSprites.transform.localPosition = new Vector3(MapSprites.transform.localPosition.x, MapSprites.transform.localPosition.y - MovingMapStep, MapSprites.transform.localPosition.z);
            Down.interactable = true;
        }
        else {
            Up.interactable = false;
        }
    }

    void DownClick() {
        Down.GetComponent<AudioSource>().Play();
        if (MapSprites.transform.localPosition.y <= 2.2f)
        {
            MapSprites.transform.localPosition = new Vector3(MapSprites.transform.localPosition.x, MapSprites.transform.localPosition.y + MovingMapStep, MapSprites.transform.localPosition.z);
            Up.interactable = true;
        }
        else {
            Down.interactable = false;
        }
    }

    void RightClick() {
        Right.GetComponent<AudioSource>().Play();
        if (MapSprites.transform.localPosition.x >= -1.4)
        {
            MapSprites.transform.localPosition = new Vector3(MapSprites.transform.localPosition.x - MovingMapStep, MapSprites.transform.localPosition.y, MapSprites.transform.localPosition.z);
            Left.interactable = true;
        }
        else {
            Right.interactable = false;
        }
    }

    //======================================================================== Navigation ===========================================================================

    void StartBattle() {

        SaveAndEscape();

        SceneManager.LoadScene(3);

    }

    void BackToRide() {

        MsgBattle.active = false;

    }

    void OpenMenu() {

        MenuSet.active = true;
        Back.onClick.AddListener(CloseMenu);
        PauseTrig = true;

    }

    void CloseMenu() {

        MenuSet.active = false;
        PauseTrig = false;

    }

    void ExitGame() {

        //Application.Quit();
        SceneManager.LoadScene(0);
    }

    public void SaveAndEscape() {

        CoStuff = ContainerForStuff.GetComponent<PackageOnMap>().CoStuff;
        string[] NewInvSet = InvSet;
        string[] NewPlaySet = PlaySet;
        string[] NewMapGen = MapGen;

        StreamWriter NewCountWrite = new StreamWriter(CountOfAllPath);
        NewCountWrite.WriteLine(CoPers);
        NewCountWrite.WriteLine(CoWeap);
        NewCountWrite.WriteLine(CoBullet);
        NewCountWrite.WriteLine(CoStuff);
        NewCountWrite.Close();

        StreamWriter PlaySetWrite = new StreamWriter(PlaySetPath);

        for (int i = 0; i < 2 + CoPers * NumPersParam + CoWeap * NumWpnParam + CoBullet * 3; i++) {
            PlaySetWrite.WriteLine(NewPlaySet[i]);
        }

        for (int i = 0; i < CoStuff; i++) {
            PlaySetWrite.WriteLine("=== Stuff " + (i + 1).ToString() + " ===");
            PlaySetWrite.WriteLine(ContainerForStuff.GetComponent<PackageOnMap>().Skin[i]);
        }

        PlaySetWrite.Close();

        for (int i = 1; i < 11; i++) {
            if (ContainerForStuff.GetComponent<PackageOnMap>().Place[i - 1] != 0) {
                NewInvSet[48 + i] = ContainerForStuff.GetComponent<PackageOnMap>().Place[i - 1].ToString();
            } else {
                NewInvSet[48 + i] = "undefined";
            }
        }

        File.WriteAllLines(InvSetPath, NewInvSet);

        string[] GetMap = File.ReadAllLines(MapGenPath);


        for (int c = 1; c < Rows * Columns + 1; c++)
        {
            GetMap[2 * c - 1] = GameObject.Find(MapSprites.name + "/Tile" + c).GetComponent<Tile>().HaveSmoke.ToString();
        }

        GetMap[2 * Columns * Rows + 12 + 10 + 1] = MapSprites.transform.localPosition.x.ToString();
        GetMap[2 * Columns * Rows + 12 + 10 + 2] = MapSprites.transform.localPosition.y.ToString();

        GetMap[2 * Columns * Rows + 12 + 10 + 4] = MainHero.transform.localPosition.x.ToString();
        GetMap[2 * Columns * Rows + 12 + 10 + 5] = MainHero.transform.localPosition.y.ToString();

        //int UsedContainer = 0;

        for (int i = 0; i < ContainerForStuff.GetComponent<PackageOnMap>().WaterLiters.Length; i++) {
            if (ContainerForStuff.GetComponent<PackageOnMap>().WaterLiters[i] != 0) {
                GetMap[2 * Columns * Rows + 12 + 10 + 7 + i] = ContainerForStuff.GetComponent<PackageOnMap>().WaterLiters[i].ToString();
            } else {
                GetMap[2 * Columns * Rows + 12 + 10 + 7 + i] = "Empty";
            }
            //if (ContainerForStuff.GetComponent<PackageOnMap>().Skin[i] == 2) {
            //    if (ContainerForStuff.GetComponent<PackageOnMap>().Place[i] != 0) {
            //        GetMap[2 * Columns * Rows + 12 + 10 + 7 + i] = ContainerForStuff.GetComponent<PackageOnMap>().WaterLiters[i].ToString();
            //    }
            //} else {
            //    GetMap[2 * Columns * Rows + 12 + 10 + 7 + i] = "Empty";
            //}
            //if (ContainerForStuff.GetComponent<PackageOnMap>().WaterLiters[i] == 0) {
            //    GetMap[2 * Columns * Rows + 12 + 10 + 7 + i] = "Empty";
            //} else {
            //    GetMap[2 * Columns * Rows + 12 + 10 + 7 + i] = ContainerForStuff.GetComponent<PackageOnMap>().WaterLiters[i].ToString();
            //}
            //if (ContainerForStuff.GetComponent<PackageOnMap>().Skin[i] == 2) {
            //    Container = Container + 1;
            //if (ContainerForStuff.GetComponent<PackageOnMap>().WaterLiters[i] == 0) {
            //    if (ContainerForStuff.GetComponent<PackageOnMap>().Skin[i] != 2) {
            //        GetMap[2 * Columns * Rows + 12 + 10 + 7 + i] = "Empty";
            //    }
            //}
            //else {
            //    GetMap[2 * Columns * Rows + 12 + 10 + 7 + i] = ContainerForStuff.GetComponent<PackageOnMap>().WaterLiters[i].ToString();
            //}
        }

        File.WriteAllLines(MapGenPath, GetMap);

    }

//===================================================================================================================================================

    void ifBattle(){


        RandomInt = Random.Range(0, battlevar);

        if (RandomInt == battlevar / 2){

            GO = false;
            MsgBattle.active = true;
            
            //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
            //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| GENERATION OF ENEMIES ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
            //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

            EnemiesPropsPath = Application.persistentDataPath + "/" + EnemiesProps + ".txt";
            StreamWriter EnemySet = new StreamWriter(EnemiesPropsPath);
            for (int e = 1; e < 7; e++)
            {
                EnemySet.WriteLine("=== Enemy " + e.ToString() + " ===");
                int SkinOfEnemy = Random.Range(1, 10);
                int Health;
                int Damage = Random.Range(30, 90);
                int Accuracy = 100 - Damage;
                Health = Mathf.FloorToInt(100 - Accuracy + 2 * Damage);
                int Level = Random.Range( 1, 4 );
                Health = Health * Level;
                Damage = Damage * Level;
                Accuracy = Accuracy * Level;

                EnemySet.WriteLine(NumberOFActiveBand.ToString());
                EnemySet.WriteLine(SkinOfEnemy.ToString());
                EnemySet.WriteLine(Health.ToString());
                EnemySet.WriteLine(Damage.ToString());
                EnemySet.WriteLine(Accuracy.ToString());
                EnemySet.WriteLine(Level.ToString());
            }
            EnemySet.Close();

        }

    }

    void Update() {

        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider.gameObject.layer == 10)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    HeroGoal.transform.position = new Vector3(mousePos.x, mousePos.y, HeroGoal.transform.position.z);
                    OldPosHero = MainHero.transform.position;
                    Hypotenuse = HeroGoal.transform.position - OldPosHero;
                    dist = Mathf.Sqrt(Hypotenuse.x * Hypotenuse.x + Hypotenuse.y * Hypotenuse.y);
                    Seconds = dist;
                    GO = true;
                }
                if (hit.collider.gameObject.layer == 8)
                {
                    if (StayOnStore == true)
                    {
                        SaveAndEscape();
                        SceneManager.LoadScene(1);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.gameObject.layer == 10)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                HeroGoal.transform.position = new Vector3(mousePos.x, mousePos.y, HeroGoal.transform.position.z);
                OldPosHero = MainHero.transform.position;
                Hypotenuse = HeroGoal.transform.position - OldPosHero;
                dist = Mathf.Sqrt(Hypotenuse.x * Hypotenuse.x + Hypotenuse.y * Hypotenuse.y);
                WaterDecrease = 0.0f;
                Seconds = dist;
                GO = true;
            }
            if (hit.collider.gameObject.layer == 8) {
                if (StayOnStore == true) {
                    SaveAndEscape();
                    SceneManager.LoadScene(1);
                }
            }
        }
        if (GO == true) {
            Seconds = Seconds - 0.001f * speed;
            MotionCounter = (dist - Seconds) / dist;
            MainHero.transform.position = new Vector3(Mathf.Lerp(OldPosHero.x, HeroGoal.transform.position.x, MotionCounter), Mathf.Lerp(OldPosHero.y, HeroGoal.transform.position.y, MotionCounter), MainHero.transform.position.z);;
            if (MotionCounter > 1) {
                GO = false;
            }
            ifBattle();
        }
        
    }

}
