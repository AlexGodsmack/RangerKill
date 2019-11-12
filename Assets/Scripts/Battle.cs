using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour
{

    public string CountOfAll;
    public string PlayerSource;
    public string InventorySettings;
    public string EnemiesProps;

    public GameObject PersPanel;
    public GameObject TimerPanel;
    public GameObject DummySelector;
    public GameObject Enemy;
    public GameObject Pers;
    public GameObject GameOver;
    public GameObject YouAreWinText;
    public GameObject TestWeapon;

    public GameObject TrophyPanel;
    public Text Prize1;
    public Text Prize2;
    public Text Prize3;
    public Button TakeLoot;

    public bool YourPass = true;
    public bool GetPers = false;

    public int Timer;

    public string[] NumOfAllBullets;
    public string[] AllBullets;

    private string CountOfAllPath;
    private string PlayerSourcePath;
    private string InventorySettingsPath;
    private string EnemiesPropsPath;

    private string SelectedItem = "DummySelector";

    private int NumPersParam = 8;
    private int NumWpnParam = 6;
    private int NumOfPers;
    private int NumOfEnemy;
    private int CountOfEnemies = 6;

    private float Seconds = 0.0f;

    private int CountOfBoughtPers;
    private int CountOfBoughtWeapon;
    private int CountOfBoughtBullets;

    private int Prize1Num;
    private int Prize1Cond;

    private int Prize2Num;
    private int Prize2Cond;

    private int LootMoney;

    void Start()
    {

        TakeLoot.onClick.AddListener(TakeLootFunc);

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| IMPORT PERSON DATA |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        CountOfAllPath = Application.persistentDataPath + "/" + CountOfAll + ".txt";

        PlayerSourcePath = Application.persistentDataPath + "/" + PlayerSource + ".txt";

        InventorySettingsPath = Application.persistentDataPath + "/" + InventorySettings + ".txt";

        EnemiesPropsPath = Application.persistentDataPath + "/" + EnemiesProps + ".txt";

        CountOfBoughtPers = int.Parse(File.ReadAllLines(CountOfAllPath)[0]);
        CountOfBoughtWeapon = int.Parse(File.ReadAllLines(CountOfAllPath)[1]);
        CountOfBoughtBullets = int.Parse(File.ReadAllLines(CountOfAllPath)[2]);

        for (int i = 1; i < CountOfBoughtPers + 1; i++) {

            GameObject P = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
            P.name = "Pers" + i.ToString();
            P.transform.SetParent(PersPanel.transform);
            P.GetComponent<PersProperties>().Health = int.Parse(File.ReadAllLines(PlayerSourcePath)[i * NumPersParam - NumPersParam + 3]);
            P.GetComponent<PersProperties>().Damage = int.Parse(File.ReadAllLines(PlayerSourcePath)[i * NumPersParam - NumPersParam + 3 + 1]);
            P.GetComponent<PersProperties>().Accuracy = int.Parse(File.ReadAllLines(PlayerSourcePath)[i * NumPersParam - NumPersParam + 3 + 2]);
            P.GetComponent<PersProperties>().Level = int.Parse(File.ReadAllLines(PlayerSourcePath)[i * NumPersParam - NumPersParam + 3 + 3]);
            P.GetComponent<PersProperties>().CountOfBattles = int.Parse(File.ReadAllLines(PlayerSourcePath)[i * NumPersParam - NumPersParam + 3 + 4]);
            P.GetComponent<PersProperties>().Price = int.Parse(File.ReadAllLines(PlayerSourcePath)[i * NumPersParam - NumPersParam + 3 + 5]);
            P.GetComponent<PersProperties>().Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[i * NumPersParam - NumPersParam + 3 + 6]);
            P.GetComponent<PersProperties>().NumberOfPersInInventory = i;
            P.GetComponent<PersProperties>().ShowHealthBar = true;

            int PersPos = int.Parse(File.ReadAllLines(InventorySettingsPath)[10 + i]);
            P.GetComponent<PersProperties>().PositionOnField = PersPos;
            P.transform.localPosition = GameObject.Find(PersPanel.name + "/PosOnField" + PersPos).transform.localPosition + new Vector3(0, 0, -0.1f);
            if (File.ReadAllLines(InventorySettingsPath)[21 + i] != "NaN") {
                P.GetComponent<PersProperties>().WeaponInHands = int.Parse(File.ReadAllLines(InventorySettingsPath)[21 + i]);
            }

            GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(P.transform);
            L.transform.localPosition = new Vector3(0, 0, -0.1f);

        }

        for (int i = 1; i < 7; i++) {

            GameObject E = Instantiate(Resources.Load("EnemyDoll")) as GameObject;
            E.name = "Enemy" + i.ToString();
            E.transform.SetParent(PersPanel.transform);
            E.GetComponent<EnemyProperties>().Band = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 1]);
            E.GetComponent<EnemyProperties>().NumberOfSkin = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 2]);
            E.GetComponent<EnemyProperties>().Health = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 3]);
            E.GetComponent<EnemyProperties>().Damage = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 4]);
            E.GetComponent<EnemyProperties>().Accuracy = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 5]);
            E.GetComponent<EnemyProperties>().Level = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 6]);

            E.transform.localPosition = GameObject.Find(PersPanel.name + "/PosOnField" + (15 + i).ToString()).transform.localPosition + new Vector3(0, 0, -0.1f);
            GameObject.Find(E.name + "/Health").GetComponent<TextMesh>().text = E.GetComponent<EnemyProperties>().Health.ToString();
            GameObject.Find(E.name + "/Level").GetComponent<TextMesh>().text = E.GetComponent<EnemyProperties>().Level.ToString();

        }

        for (int i = 1; i < CountOfBoughtBullets + 1; i++) {
            NumOfAllBullets[i - 1] = File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + 3 * i - 2];
        }
        for (int i = 1; i < CountOfBoughtBullets + 1; i++) {
            AllBullets[i - 1] = File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + 3 * i - 1];
        }

        GameObject TestWeaponInst = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
        TestWeaponInst.GetComponent<SpriteRenderer>().enabled = false;
        TestWeaponInst.AddComponent<ForWeaponTester>();
        TestWeaponInst.name = "TestWeapon";
        TestWeapon = TestWeaponInst;

        Timer = 10;

    }

    void Update()
    {

        if (Input.touchCount > 0) {
            for (int a = 0; a < Input.touchCount; a++) {
                Touch touch = Input.GetTouch(a);
                if (touch.phase == TouchPhase.Began) {
                    if (YourPass == true)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                        if (hit.transform.gameObject.layer == 8)
                        {
                            GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                            hit.transform.gameObject.GetComponent<AudioSource>().Play();
                            SelectedItem = hit.transform.gameObject.name;
                            GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active = true;
                            GetPers = true;
                            int GetBullets = GameObject.Find(PersPanel.name + "/" + SelectedItem).GetComponent<PersProperties>().WeaponSkin;
                            TestWeapon.GetComponent<ForWeaponTester>().Skin = GetBullets;
                        }
                        if (hit.transform.gameObject.layer == 16)
                        {
                            if (GetPers == true)
                            {

                                GameObject AttackedPers = GameObject.Find(SelectedItem);

                                int GetBullets = GameObject.Find(PersPanel.name + "/" + SelectedItem).GetComponent<PersProperties>().WeaponSkin;

                                for (int W = 1; W < CountOfBoughtBullets + 1; W++)
                                {

                                    if (int.Parse(NumOfAllBullets[W - 1]) == GetBullets)
                                    {
                                        int CountOfSpentBullets = int.Parse(AllBullets[W - 1]);
                                        int Clip = TestWeapon.GetComponent<ForWeaponTester>().CountOfBullets;

                                        if (CountOfSpentBullets > 0)
                                        {

                                            AllBullets[W - 1] = (int.Parse(AllBullets[W - 1]) - Clip).ToString();

                                            int EnemyHealth = hit.transform.gameObject.GetComponent<EnemyProperties>().Health;
                                            EnemyHealth = EnemyHealth - (AttackedPers.GetComponent<PersProperties>().PowerOfShot + Random.Range(-10, 30));
                                            GameObject.Find(hit.transform.gameObject.name + "/Health").GetComponent<TextMesh>().text = EnemyHealth.ToString();
                                            hit.transform.gameObject.GetComponent<EnemyProperties>().Health = EnemyHealth;

                                            if (hit.transform.gameObject.GetComponent<EnemyProperties>().Health <= 0)
                                            {
                                                Destroy(hit.transform.gameObject);
                                                CountOfEnemies = CountOfEnemies - 1;
                                            }
                                            Timer = 10;
                                            Seconds = 0.0f;

                                            YourPass = false;

                                            for (int i = 1; i < 11; i++)
                                            {
                                                GameObject.Find("Canvas/" + TimerPanel.name + "/Sec" + i.ToString()).active = false;
                                            }

                                            SelectedItem = "DummySelector";
                                        }
                                        else
                                        {
                                            Debug.Log("This Person is empty");
                                        }

                                    }
                                }

                                GameObject.Find(AttackedPers.name + "/Lighter").active = false;

                            }
                            else
                            {
                                Debug.Log("Select Your Warrior");
                            }
                        }
                        if (hit.collider.name == "NullHit")
                        {
                            Debug.Log("Miss");
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0)) {

            if (YourPass == true)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.transform.gameObject.layer == 8)
                {
                    GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                    hit.transform.gameObject.GetComponent<AudioSource>().Play();
                    SelectedItem = hit.transform.gameObject.name;
                    GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active = true;
                    GetPers = true;
                    int GetBullets = GameObject.Find(PersPanel.name + "/" + SelectedItem).GetComponent<PersProperties>().WeaponSkin;
                    TestWeapon.GetComponent<ForWeaponTester>().Skin = GetBullets;
                }
                if (hit.transform.gameObject.layer == 16) {
                    if (GetPers == true)
                    {

                        GameObject AttackedPers = GameObject.Find(SelectedItem);

                        int GetBullets = GameObject.Find(PersPanel.name + "/" + SelectedItem).GetComponent<PersProperties>().WeaponSkin;

                        for (int W = 1; W < CountOfBoughtBullets + 1; W++)
                        {

                            if (int.Parse(NumOfAllBullets[W - 1]) == GetBullets)
                            {
                                int CountOfSpentBullets = int.Parse(AllBullets[W - 1]);
                                int Clip = TestWeapon.GetComponent<ForWeaponTester>().CountOfBullets;

                                if (CountOfSpentBullets > 0)
                                {

                                    AllBullets[W - 1] = (int.Parse(AllBullets[W - 1]) - Clip).ToString();

                                    int EnemyHealth = hit.transform.gameObject.GetComponent<EnemyProperties>().Health;
                                    EnemyHealth = EnemyHealth - (AttackedPers.GetComponent<PersProperties>().PowerOfShot + Random.Range(-10, 30));
                                    GameObject.Find(hit.transform.gameObject.name + "/Health").GetComponent<TextMesh>().text = EnemyHealth.ToString();
                                    hit.transform.gameObject.GetComponent<EnemyProperties>().Health = EnemyHealth;

                                    if (hit.transform.gameObject.GetComponent<EnemyProperties>().Health <= 0)
                                    {
                                        Destroy(hit.transform.gameObject);
                                        CountOfEnemies = CountOfEnemies - 1;
                                    }
                                    Timer = 10;
                                    Seconds = 0.0f;

                                    YourPass = false;

                                    for (int i = 1; i < 11; i++)
                                    {
                                        GameObject.Find("Canvas/" + TimerPanel.name + "/Sec" + i.ToString()).active = false;
                                    }

                                    SelectedItem = "DummySelector";
                                }
                                else {
                                    Debug.Log("This Person is empty");
                                }

                            }
                        }

                        GameObject.Find(AttackedPers.name + "/Lighter").active = false;

                    }
                    else {
                        Debug.Log("Select Your Warrior");
                    }
                }
                if (hit.collider.name == "NullHit") {
                    Debug.Log("Miss");
                }
            }
        }

        if (CountOfEnemies > 0) {
            if (YourPass == true)
            {

                TimerCounter();

                if (Timer == 10) {
                    for (int i = 1; i < 11; i++)
                    {
                        GameObject.Find("Canvas/" + TimerPanel.name + "/Sec" + i.ToString()).active = true;
                    }
                }
                GameObject.Find("Canvas/" + TimerPanel.name + "/Sec" + Timer.ToString()).active = false;

                if (Timer <= 1)
                {
                    YourPass = false;
                    Seconds = 0;
                }
            }
            if (YourPass == false){


                TimerCounter();

                if (Timer == 10) {

                    RandomPers();
                    RandomEnemy();

                    GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                    SelectedItem = "DummySelector";

                    if (GameObject.Find(PersPanel.name + "/Enemy" + NumOfEnemy.ToString()) != null) {
                    Enemy = GameObject.Find(PersPanel.name + "/Enemy" + NumOfEnemy.ToString());

                    }else {
                        for (int i = 1; i < 7; i++) {
                            if (GameObject.Find(PersPanel.name + "/Enemy" + i.ToString()) != null) {
                                Enemy = GameObject.Find(PersPanel.name + "/Enemy" + i.ToString());
                                break;
                            }
                        }
                    }
                    if (GameObject.Find(PersPanel.name + "/Pers" + NumOfPers.ToString()) != null)
                    {
                        Pers = GameObject.Find(PersPanel.name + "/Pers" + NumOfPers.ToString());
                    }
                    else {
                        for (int i = 1; i < 7; i++) {
                            if (GameObject.Find(PersPanel.name + "/Pers" + i.ToString()) != null) {
                                Pers = GameObject.Find(PersPanel.name + "/Pers" + i.ToString());
                                break;
                            }
                        }
                    }
                }

                if (Timer == 9) {
                    Enemy.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 1);
                }

                if(Timer == 8) {

                    if (Pers.GetComponent<SpriteRenderer>().color == new Color(1, 1, 1, 1)) {  
                        Pers.GetComponent<PersProperties>().Health = Pers.GetComponent<PersProperties>().Health - (Enemy.GetComponent<EnemyProperties>().Damage + Random.Range(-10, 10));
                        Pers.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 1);
                    }
                }

                if (Timer == 7) {
                    if (Pers != null ) {
                        if (Pers.GetComponent<PersProperties>().Health <= 0) {

                            if (Pers.GetComponent<PersProperties>().WeaponInHands != 0) {
                                CountOfBoughtWeapon = CountOfBoughtWeapon - 1;
                            }
                            Destroy(Pers);
                            CountOfBoughtPers = CountOfBoughtPers - 1;
                        }else {
                            Pers.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                        }
                    }
                    Enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

                    if (CountOfBoughtPers == 0) {
                        Debug.Log("GAME OVER");
                        GameOver.active = true;

                    }

                }
                if (Timer == 6 ) {
                    if (GameOver.active == true)
                    {
                        SceneManager.LoadScene(0);
                    }
                    else {
                        YourPass = true;

                        Timer = 10;
                        Seconds = 0.0f;
                    }
                }

            }
        }
        if (CountOfEnemies == 0) {

            Time.timeScale = 0;

            IfYouWon();

        }
        if (CountOfEnemies == -1) {

            //GameObject Wpn1 = GameObject.Find("wpn1");
            //GameObject Wpn2 = GameObject.Find("wpn2");

            //string nameofwpn = Wpn1.GetComponent<WeaponProperties>().Name;
            //int cond = Wpn1.GetComponent<WeaponProperties>().Condition;

            //string nameofwpn2 = Wpn2.GetComponent<WeaponProperties>().Name;
            //int cond2 = Wpn2.GetComponent<WeaponProperties>().Condition;

            //Prize1.text = nameofwpn.ToString() + " with condition " + cond.ToString();
            //Prize2.text = nameofwpn2.ToString() + " with condition " + cond2.ToString();

            //Prize1Num = Wpn1.GetComponent<WeaponProperties>().Skin;
            //Prize1Cond = Wpn1.GetComponent<WeaponProperties>().Condition;

            //Prize2Num = Wpn2.GetComponent<WeaponProperties>().Skin;
            //Prize2Cond = Wpn2.GetComponent<WeaponProperties>().Condition;

        }

    }

    void RandomPers() {
        NumOfPers = Random.Range(1, CountOfBoughtPers + 1);
    }

    void RandomEnemy() {
        NumOfEnemy = Random.Range(1, 7);
    }

    void TimerCounter() {
        Timer = 10;
        Seconds += Time.deltaTime;
        int OneSec = (int)(Seconds % 60);
        Timer = Timer - OneSec;
    }

    void IfYouWon() {

        YouAreWinText.active = true;

        TrophyPanel.active = true;

        ////////////////////////////////////////////////////////////////// Я ПОКА НЕ ЗНАЮ ЧТО С ЭТИМ ДЕЛАТЬ

        //int i = Random.Range(1, 11);
        //int c = Random.Range(7, 11);

        //GameObject NewWpn1 = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
        //NewWpn1.GetComponent<SpriteRenderer>().enabled = false;
        //NewWpn1.name = "wpn1";
        //NewWpn1.GetComponent<WeaponProperties>().Skin = i;
        //NewWpn1.GetComponent<WeaponProperties>().Condition = c;

        //int a = Random.Range(1, 11);
        //int d = Random.Range(7, 11);

        //GameObject NewWpn2 = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
        //NewWpn2.GetComponent<SpriteRenderer>().enabled = false;
        //NewWpn2.name = "wpn2";
        //NewWpn2.GetComponent<WeaponProperties>().Skin = a;
        //NewWpn2.GetComponent<WeaponProperties>().Condition = d;

        int Money = Random.Range(100, 3000);
        Prize3.text = "Money " + Money.ToString();
        LootMoney = Money;

        for (int q = 1; q < 7; q++){
            if (GameObject.Find(PersPanel.name + "/Pers" + q.ToString()) != null) {
                GameObject thispers = GameObject.Find(PersPanel.name + "/Pers" + q.ToString());
                thispers.GetComponent<PersProperties>().CountOfBattles = thispers.GetComponent<PersProperties>().CountOfBattles + 1;
            }
        }

        CountOfEnemies = -1;

    }

    void TakeLootFunc() {

        string[] CountAll = File.ReadAllLines(CountOfAllPath);
        string[] PlayerSrc = File.ReadAllLines(PlayerSourcePath);
        string[] InvSet = File.ReadAllLines(InventorySettingsPath);

        for (int w = 1; w < 7; w++)
        {
            if (GameObject.Find(PersPanel.name + "/Pers" + w.ToString()) == null)
            {
                InvSet[10 + w] = "NaN";
                InvSet[21 + w] = "NaN";
            }
        }

        int NewNameOfPers = 0;
        int NewNameOfWeap = 0;
        int NewNameOfBullets = 0;

        string NewPlaySrc = Application.persistentDataPath + "/TempPlayerSource.txt";
        StreamWriter TempPlayerSource = new StreamWriter(NewPlaySrc);

        TempPlayerSource.WriteLine(" === Your Money ===");
        TempPlayerSource.WriteLine((int.Parse(PlayerSrc[1]) + LootMoney).ToString());

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! COLLECT ALL SAVED PERS !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        for (int p = 1; p < 7; p++)
        {
            if (GameObject.Find(PersPanel.name + "/Pers" + p.ToString()) != null)
            {
                NewNameOfPers = NewNameOfPers + 1;
                TempPlayerSource.WriteLine("=== Pers " + NewNameOfPers.ToString() + " ===");
                TempPlayerSource.WriteLine(GameObject.Find(PersPanel.name + "/Pers" + p.ToString()).GetComponent<PersProperties>().Health.ToString());
                TempPlayerSource.WriteLine(GameObject.Find(PersPanel.name + "/Pers" + p.ToString()).GetComponent<PersProperties>().Damage.ToString());
                TempPlayerSource.WriteLine(GameObject.Find(PersPanel.name + "/Pers" + p.ToString()).GetComponent<PersProperties>().Accuracy.ToString());
                TempPlayerSource.WriteLine(GameObject.Find(PersPanel.name + "/Pers" + p.ToString()).GetComponent<PersProperties>().Level.ToString());
                TempPlayerSource.WriteLine(GameObject.Find(PersPanel.name + "/Pers" + p.ToString()).GetComponent<PersProperties>().CountOfBattles.ToString());
                TempPlayerSource.WriteLine(GameObject.Find(PersPanel.name + "/Pers" + p.ToString()).GetComponent<PersProperties>().Price.ToString());
                TempPlayerSource.WriteLine(GameObject.Find(PersPanel.name + "/Pers" + p.ToString()).GetComponent<PersProperties>().Skin.ToString());
                GameObject.Find(PersPanel.name + "/Pers" + p.ToString()).name = "Pers" + NewNameOfPers.ToString();
            }
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! UPLOAD ALL WEAPON IN HANDS !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        for (int w = 1; w < NewNameOfPers + 1; w++)
        {
            NewNameOfWeap = NewNameOfWeap + 1;

            int NumOfWeap = GameObject.Find(PersPanel.name + "/Pers" + w.ToString()).GetComponent<PersProperties>().WeaponInHands;
            TempPlayerSource.WriteLine("=== Weapon " + NewNameOfWeap.ToString() + " ===");
            TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NumOfWeap - 6 + 1]);
            TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NumOfWeap - 6 + 2]);
            TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NumOfWeap - 6 + 3]);
            TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NumOfWeap - 6 + 4]);
            TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NumOfWeap - 6 + 5]);
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! RELOAD UNUSED WEAPON IN INVENTORY !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        for (int NoW = 1; NoW < int.Parse(CountAll[1]) + 1; NoW++)
        {
            if (InvSet[37 + NoW] == "false")
            {

                NewNameOfWeap = NewNameOfWeap + 1;
                TempPlayerSource.WriteLine("=== Weapon " + NewNameOfWeap.ToString() + " ===");
                TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NoW - 6 + 1]);
                TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NoW - 6 + 2]);
                TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NoW - 6 + 3]);
                TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NoW - 6 + 4]);
                TempPlayerSource.WriteLine(PlayerSrc[2 + 8 * int.Parse(CountAll[0]) + 6 * NoW - 6 + 5]);

            }
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ADD WEAPON FROM TROPHY !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //GameObject wpn1 = GameObject.Find("wpn1");
        //GameObject wpn2 = GameObject.Find("wpn2");

        //NewNameOfWeap = NewNameOfWeap + 1;
        //TempPlayerSource.WriteLine("=== Weapon " + NewNameOfWeap.ToString() + " ===");
        //TempPlayerSource.WriteLine(wpn1.GetComponent<WeaponProperties>().Damage.ToString());
        //TempPlayerSource.WriteLine(wpn1.GetComponent<WeaponProperties>().Condition.ToString());
        //TempPlayerSource.WriteLine(wpn1.GetComponent<WeaponProperties>().CountOfBullets.ToString());
        //TempPlayerSource.WriteLine(wpn1.GetComponent<WeaponProperties>().Price.ToString());
        //TempPlayerSource.WriteLine(wpn1.GetComponent<WeaponProperties>().Skin.ToString());

        //NewNameOfWeap = NewNameOfWeap + 1;

        //TempPlayerSource.WriteLine("=== Weapon " + NewNameOfWeap.ToString() + " ===");
        //TempPlayerSource.WriteLine(wpn2.GetComponent<WeaponProperties>().Damage.ToString());
        //TempPlayerSource.WriteLine(wpn2.GetComponent<WeaponProperties>().Condition.ToString());
        //TempPlayerSource.WriteLine(wpn2.GetComponent<WeaponProperties>().CountOfBullets.ToString());
        //TempPlayerSource.WriteLine(wpn2.GetComponent<WeaponProperties>().Price.ToString());
        //TempPlayerSource.WriteLine(wpn2.GetComponent<WeaponProperties>().Skin.ToString());

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! RESAVE YOUR BULLETS !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        for (int b = 1; b < CountOfBoughtBullets + 1; b++) {
            if (int.Parse(AllBullets[b - 1]) != 0) {
                NewNameOfBullets = NewNameOfBullets + 1;
                TempPlayerSource.WriteLine("=== Bullets " + NewNameOfBullets.ToString() + " ===");
                TempPlayerSource.WriteLine(NumOfAllBullets[b - 1]);
                TempPlayerSource.WriteLine(AllBullets[b - 1]);
            }
        }

        TempPlayerSource.Close();

        CountAll[0] = NewNameOfPers.ToString();
        CountAll[1] = NewNameOfWeap.ToString();
        CountAll[2] = NewNameOfBullets.ToString();

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! REWRITE ALL POSITIONS FOR ALL IN INVENTORY !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        for (int p = 1; p < 11; p++)
        {
            if (GameObject.Find(PersPanel.name + "/Pers" + p.ToString()) != null)
            {
                InvSet[10 + p] = GameObject.Find(PersPanel.name + "/Pers" + p.ToString()).GetComponent<PersProperties>().PositionOnField.ToString();
            }
            else
            {
                InvSet[10 + p] = "NaN";
            }
        }

        for (int w = 1; w < 16; w++)
        {
            if (GameObject.Find(PersPanel.name + "/Pers" + w.ToString()) != null)
            {
                InvSet[21 + w] = w.ToString();
            }
            else
            {
                InvSet[21 + w] = "NaN";
            }
        }
        for (int t = 1; t < 11; t++)
        {
            if (GameObject.Find(PersPanel.name + "/Pers" + t.ToString()) != null)
            {
                InvSet[37 + t] = "true";
            }
            else
            {
                InvSet[37 + t] = "false";
            }
        }

        File.WriteAllLines(InventorySettingsPath, InvSet);
        File.WriteAllLines(CountOfAllPath, CountAll);

        StreamWriter NewDataPlayer = new StreamWriter(PlayerSourcePath);
        string[] getnewdata = File.ReadAllLines(NewPlaySrc);
        NewDataPlayer.Flush();
        for (int i = 0; i < getnewdata.Length; i++)
        {
            NewDataPlayer.WriteLine(getnewdata[i].ToString());
        }
        NewDataPlayer.Close();

        Time.timeScale = 1;

        SceneManager.LoadScene(2);

    }

}
