using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{

    public string CountOfAll;
    public string PlayerSource;
    public string InventorySettings;
    public string EnemiesProps;
    public string StartHealthOfPers;
    public string MapGen;

    public GameObject PersPanel;
    public GameObject TimerPanel;
    public GameObject DummySelector;
    public GameObject PersContainer;
    public GameObject EnemyContainer;
    public GameObject BulletsContainer;
    public GameObject StuffContainer;
    public GameObject InfoText;
    public GameObject LootPanel;

    public int RandomPrize;

    private string CountOfAllPath;
    private string PlayerSourcePath;
    private string InventorySettingsPath;
    private string StartHealthOfPersPath;
    private string EnemiesPropsPath;
    private string MapGenPath;

    private string[] CountAll;
    private string[] PlaySrc;
    private string[] InvSet;
    private string[] EnemiesProp;
    private string[] MapGenGet;

    private int NumPersParam = 8;
    private int NumWpnParam = 6;
    private int NumOfPers;
    private int NumOfEnemy;
    private int CountOfEnemies = 6;

    public int Columns;
    public int Rows;

    private int CoPers;
    private int CoWeap;
    private int CoBullets;
    private int CoStuff;

    void Start()
    {

        StuffContainer.transform.position = InfoText.transform.position;
        for (int i = 0; i < StuffContainer.transform.childCount; i++) {
            StuffContainer.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }

        ///|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ///|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| IMPORT PERSON DATA |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ///|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        
        CountOfAllPath = Application.persistentDataPath + "/" + CountOfAll + ".txt";
        PlayerSourcePath = Application.persistentDataPath + "/" + PlayerSource + ".txt";
        InventorySettingsPath = Application.persistentDataPath + "/" + InventorySettings + ".txt";
        EnemiesPropsPath = Application.persistentDataPath + "/" + EnemiesProps + ".txt";
        StartHealthOfPersPath = Application.persistentDataPath + "/" + StartHealthOfPers + ".txt";
        MapGenPath = Application.persistentDataPath + "/" + MapGen + ".txt";

        CountAll = File.ReadAllLines(CountOfAllPath);
        PlaySrc = File.ReadAllLines(PlayerSourcePath);
        InvSet = File.ReadAllLines(InventorySettingsPath);
        EnemiesProp = File.ReadAllLines(EnemiesPropsPath);
        MapGenGet = File.ReadAllLines(MapGenPath);

        CoPers = int.Parse(File.ReadAllLines(CountOfAllPath)[0]);
        CoWeap = int.Parse(File.ReadAllLines(CountOfAllPath)[1]);
        CoBullets = int.Parse(File.ReadAllLines(CountOfAllPath)[2]);
        CoStuff = int.Parse(File.ReadAllLines(CountOfAllPath)[3]);

        ///000000000000000000000000000000000000000000000000000000000000000000<<< IMPORT PERS >>>00000000000000000000000000000000000000000000000000000000000000000
        
        for (int i = 1; i < CoPers + 1; i++) {

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

            P.GetComponent<PersProperties>().FullHealth = int.Parse(File.ReadAllLines(StartHealthOfPersPath)[i - 1]);

            P.GetComponent<PersProperties>().NumberOfPersInInventory = i;
            P.GetComponent<PersProperties>().ShowHealthBar = true;

            int PersPos = int.Parse(File.ReadAllLines(InventorySettingsPath)[10 + i]);
            P.GetComponent<PersProperties>().PositionOnField = PersPos;
            P.transform.localPosition = GameObject.Find(PersPanel.name + "/PosOnField" + PersPos).transform.localPosition + new Vector3(0, 0, -0.1f);
            if (File.ReadAllLines(InventorySettingsPath)[21 + i] != "NaN") {
                P.GetComponent<PersProperties>().WeaponInHands = int.Parse(File.ReadAllLines(InventorySettingsPath)[21 + i]);
            }

            P.transform.SetParent(PersContainer.transform);
            GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(P.transform);
            L.transform.localPosition = new Vector3(0, 0, -0.1f);

        }

        ///000000000000000000000000000000000000000000000000000000000000000000<<< IMPORT ENEMIES >>>00000000000000000000000000000000000000000000000000000000000000000

        for (int i = 1; i < 7; i++) {

            GameObject E = Instantiate(Resources.Load("EnemyDoll")) as GameObject;
            E.name = "Enemy" + i.ToString();
            E.transform.SetParent(PersPanel.transform);
            E.GetComponent<EnemyProperties>().Band = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 1]);
            E.GetComponent<EnemyProperties>().NumberOfSkin = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 2]);
            E.GetComponent<EnemyProperties>().StartHealth = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 3]);
            E.GetComponent<EnemyProperties>().Health = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 3]);
            E.GetComponent<EnemyProperties>().Damage = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 4]);
            E.GetComponent<EnemyProperties>().Accuracy = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 5]);
            E.GetComponent<EnemyProperties>().Level = int.Parse(File.ReadAllLines(EnemiesPropsPath)[7 * i - 7 + 6]);

            E.transform.localPosition = GameObject.Find(PersPanel.name + "/PosOnField" + (15 + i).ToString()).transform.localPosition + new Vector3(0, 0, -0.1f);
            E.transform.SetParent(EnemyContainer.transform);
            GameObject.Find(E.name + "/Level").GetComponent<TextMesh>().text = E.GetComponent<EnemyProperties>().Level.ToString();

        }

        //for (int i = 1; i < CoBullets + 1; i++) {
        //    NumOfAllBullets[i - 1] = File.ReadAllLines(PlayerSourcePath)[2 + CoPers * NumPersParam + CoWeap * NumWpnParam + 3 * i - 2];
        //}
        //for (int i = 1; i < CoBullets + 1; i++) {
        //    AllBullets[i - 1] = File.ReadAllLines(PlayerSourcePath)[2 + CoPers * NumPersParam + CoWeap * NumWpnParam + 3 * i - 1];
        //}

        ///000000000000000000000000000000000000000000000000000000000000000000<<< IMPORT WEAPON >>>00000000000000000000000000000000000000000000000000000000000000000

        for (int w = 1; w < CoWeap + 1; w++) {
            GameObject W = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
            W.name = "Weapon" + w.ToString();
            W.GetComponent<WeaponProperties>().Damage = int.Parse(PlaySrc[2 + CoPers * NumPersParam + w * NumWpnParam - NumWpnParam + 1]);
            W.GetComponent<WeaponProperties>().Condition = int.Parse(PlaySrc[2 + CoPers * NumPersParam + w * NumWpnParam - NumWpnParam + 2]);
            W.GetComponent<WeaponProperties>().Clip = int.Parse(PlaySrc[2 + CoPers * NumPersParam + w * NumWpnParam - NumWpnParam + 3]);
            W.GetComponent<WeaponProperties>().Skin = int.Parse(PlaySrc[2 + CoPers * NumPersParam + w * NumWpnParam - NumWpnParam + 5]);
            W.GetComponent<WeaponProperties>().Bought = true;
            W.layer = 18;

            W.transform.SetParent(StuffContainer.transform);

            if (InvSet[37 + w] != "false") {
                W.GetComponent<WeaponProperties>().InHands = true;
                for (int p = 1; p < CoPers + 1; p++) {
                    if (InvSet[21 + p] == w.ToString()) {
                        GameObject Pers = GameObject.Find(PersContainer.name + "/Pers" + p.ToString());
                        for (int i = 0; i < 4; i++) {
                            if (Pers.GetComponent<PersProperties>().Package[i] == "None") {
                                Pers.GetComponent<PersProperties>().Package[i] = W.name;
                                W.transform.localPosition = StuffContainer.transform.Find("Pos" + (i + 1).ToString()).transform.localPosition;
                                W.GetComponent<WeaponProperties>().NumberOfWeaponInInventory = p;
                                break;
                            }
                        }
                    }
                }
            }
            W.GetComponent<SpriteRenderer>().enabled = false;
            W.GetComponent<Collider2D>().enabled = false;
        }

        ///000000000000000000000000000000000000000000000000000000000000000000<<< IMPORT BULLETS >>>00000000000000000000000000000000000000000000000000000000000000000

        for (int b = 1; b < CoBullets + 1; b++) {
            GameObject B = Instantiate(Resources.Load("BulletsDoll")) as GameObject;
            B.name = "Bullets" + b.ToString();
            Destroy(B.GetComponent<Collider2D>());
            B.GetComponent<Collider2D>().enabled = false;
            B.GetComponent<Bullets>().BulletsSkin = int.Parse(PlaySrc[2 + CoPers * NumPersParam + CoWeap * NumWpnParam + 3 * b - 2]);
            B.GetComponent<Bullets>().CountOfBullets = int.Parse(PlaySrc[2 + CoPers * NumPersParam + CoWeap * NumWpnParam + 3 * b - 1]);
            B.transform.SetParent(BulletsContainer.transform);
        }

        ///000000000000000000000000000000000000000000000000000000000000000000<<< IMPORT STUFF >>>00000000000000000000000000000000000000000000000000000000000000000

        for (int s = 1; s < CoStuff + 1; s++) {
            GameObject S = Instantiate(Resources.Load("OtherStuffPrefab")) as GameObject;
            S.name = "Stuff" + s.ToString();
            S.GetComponent<OtherStuff>().Skin = int.Parse(PlaySrc[2 + CoPers * NumPersParam + CoWeap * NumWpnParam + 3 * CoBullets + s * 2 - 1]);
            S.transform.SetParent(StuffContainer.transform);
            S.layer = 18;
            if (InvSet[48 + s] != "undefined") {
                int PersNum = int.Parse(InvSet[48 + s]);
                S.GetComponent<OtherStuff>().WhichPersUseIt = PersNum;
                S.GetComponent<OtherStuff>().Bought = true;
                GameObject Pers = GameObject.Find(PersContainer.name + "/Pers" + PersNum);
                for (int i = 0; i < 4; i++) {
                    if (Pers.GetComponent<PersProperties>().Package[i] == "None") {
                        Pers.GetComponent<PersProperties>().Package[i] = "Stuff" + s.ToString();
                        S.transform.localPosition = StuffContainer.transform.Find("Pos" + (i + 1).ToString()).transform.localPosition;
                        S.GetComponent<OtherStuff>().NumberOfStuffInInv = i + 1;
                        break;
                    }
                }
            }
            S.GetComponent<SpriteRenderer>().enabled = false;
            S.GetComponent<Collider2D>().enabled = false;
            if (S.GetComponent<OtherStuff>().Skin == 2) {
                S.GetComponent<OtherStuff>().IsActive = true;
                S.GetComponent<OtherStuff>().WaterLiters = int.Parse(MapGenGet[2 * Columns * Rows + 12 + 10 + 6 + s]);
                //S.GetComponent<Collider2D>().enabled = false;
            }
        }
        ///000000000000000000000000000000000000000000000000000000000000000000<<< GENERATE LOOT >>>00000000000000000000000000000000000000000000000000000000000000000
        for (int i = 1; i < 4; i++)
        {
            RandomPrize = Random.Range(0, 3);
            if (RandomPrize == 0)
            {
                GameObject B = Instantiate(Resources.Load("BulletsDoll")) as GameObject;
                B.name = "Lot" + i.ToString();
                B.GetComponent<Bullets>().BulletsSkin = Random.Range(1, 11);
                B.GetComponent<Bullets>().CountOfBullets = Random.Range(10, 30);
                B.transform.SetParent(LootPanel.transform);
                B.transform.localPosition = new Vector3(0, 0, 0);
            }

            if (RandomPrize == 1)
            {
                GameObject W = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
                W.name = "Lot" + i.ToString();
                W.GetComponent<WeaponProperties>().Skin = Random.Range(1, 11);
                W.GetComponent<WeaponProperties>().Condition = Random.Range(1, 11);
                W.transform.SetParent(LootPanel.transform);
                W.transform.localPosition = new Vector3(0, 0, 0);
            }

            if (RandomPrize == 2)
            {
                GameObject S = Instantiate(Resources.Load("OtherStuffPrefab")) as GameObject;
                S.name = "Lot" + i.ToString();
                S.GetComponent<OtherStuff>().Skin = Random.Range(1, 4);
                if (S.GetComponent<OtherStuff>().Skin == 2) {
                    S.GetComponent<OtherStuff>().WaterLiters = 100;
                }
                S.transform.SetParent(LootPanel.transform);
                S.transform.localPosition = new Vector3(0, 0, 0);
            }
        }

    }

    void Update() {
        
    }
}
