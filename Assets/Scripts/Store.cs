using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{

    public GameObject PersPanel;
    public GameObject BoughtPersonsPanel;
    public GameObject WeaponPanel;
    public GameObject WpnBoughtPanel;
    public GameObject MenuPanel;
    public GameObject PersonsStorePanel;
    public GameObject WeaponStorePanel;
    public GameObject InventoryPanel;
    public GameObject InventoryPersons;
    public GameObject InventoryWeapon;
    public GameObject InventoryStuff;
    public GameObject WeaponProperties;
    public GameObject BuyPanel;
    public GameObject Frontyard;
    public GameObject LastBack;
    public GameObject NearFront;
    public GameObject BoughtPersUI;
    public GameObject WeaponBoughtUI;
    public GameObject WeaponChanger;
    public GameObject BulletsPanel;
    public GameObject BoughtBulletsPanel;
    public GameObject OtherPanel;
    public GameObject BoughtOtherPanel;
    public GameObject ArmorPanel;
    public GameObject InventoryProperties;
    public GameObject PersPackCage;

    public Button Next;
    public Button Prev;
    public Button PrevWpn;
    public Button NextWpn;
    public Button Buy;
    public Button Menu;
    public Button Back;
    public Button Resume;
    public Button Slaves;
    public Button Weapon;
    public Button Inventory;
    public Button WpnSwitch;
    public Button BulletsSwitch;
    public Button OtherSwitch;
    public Button GoToMapButton;
    public Button UpCountBullets;
    public Button DownCountBullets;
    public Button RepairWeapon;

    public Text Health;
    public Text Damage;
    public Text Accuracy;
    public Text Level;
    public Text Price;
    public Text Money;
    public Text InventoryText;

    public Text WeaponOfOtherProperties;

    public int MoneyCount;
    public int CountOfBoughtPers;
    public int CountOfBoughtWeapon;
    public int CountOfBoughtBullets;
    public int CountOfBoughtStuff;
    public int PersPosOnField;
    public int RepairPrice;

    public string SelectedItem;
    public string PersSettings;
    public string PlayerSource;
    public string WeaponBundle;
    public string InventorySettings;
    public string BulletsInStore;
    public string CountOfAll;
    public string StartHealthOfPers;
    public string OtherStuff;

    public int NumPersParam = 8;
    public int NumWpnParam = 6;

    public int PanelActivated;

    private int CountOfPers = 10;
    private int CountOfWeapon = 15;
    private int CountOfBullets = 10;
    private int CountOfOtherStuff = 10;

    private int CountSelectedBullets;

    private string PersSetPath;
    private string PlayerSourcePath;
    private string WeaponBundlePath;
    private string InvSetPath;
    private string BulletsInStorePath;
    private string CountOfAllPath;
    private string StartHealthOfPersPath;
    private string OtherStuffPath;

    private GameObject SelectedObj;
    private GameObject AssignPosForPers;
    private Vector3 OldPosOfPers;
    private string NameOfSelected = "DummySwitcher";
    private string SelectedInPanel = "DummySwitcherBought";

    private string NextSelectedInv = "DummySwitcherBought";
    private string PrevSelectedInv;
    private string FrstLvlSelected;
    private string GetPersPack;

    private Vector3 BuyButtonPos;
    private Vector3 MoneyPos;
    private bool AppointPos = false;
    private bool SelectedWeapon = false;
    private bool SelectedInPack = false;
    private float WeaponPanelPos = -2.0f;
    private Vector3 NewWpnPanPos = new Vector3(-2.0f, 0.4f, -3.0f);

    // Start is called before the first frame update
    void Start() {

        Input.simulateMouseWithTouches = false;

        BuyButtonPos = Buy.transform.localPosition;
        MoneyPos = Money.transform.localPosition;

        PanelActivated = 1;

        Next.onClick.AddListener(NextPers);
        Prev.onClick.AddListener(PrevPers);
        Buy.onClick.AddListener(BuyIt);
        Menu.onClick.AddListener(OpenMenu);
        Slaves.onClick.AddListener(BuyPerses);
        Weapon.onClick.AddListener(BuyWeapon);
        Inventory.onClick.AddListener(GoToInventory);
        PrevWpn.onClick.AddListener(PrevWeapon);
        NextWpn.onClick.AddListener(NextWeapon);
        WpnSwitch.onClick.AddListener(SwitchToWeapon);
        BulletsSwitch.onClick.AddListener(SwitchToBullets);
        OtherSwitch.onClick.AddListener(SwitchToOther);
        GoToMapButton.onClick.AddListener(GoToMap);
        UpCountBullets.onClick.AddListener(UpperCountBullets);
        DownCountBullets.onClick.AddListener(DownerCountBullets);

        PlayerSourcePath = Application.persistentDataPath.ToString() + "/" + PlayerSource + ".txt";
        CountOfAllPath = Application.persistentDataPath + "/" + CountOfAll + ".txt";
        InvSetPath = Application.persistentDataPath + "/" + InventorySettings + ".txt";
        StartHealthOfPersPath = Application.persistentDataPath + "/" + StartHealthOfPers + ".txt";

        CountOfBoughtPers = int.Parse(File.ReadAllLines(CountOfAllPath)[0]);
        CountOfBoughtWeapon = int.Parse(File.ReadAllLines(CountOfAllPath)[1]);
        CountOfBoughtBullets = int.Parse(File.ReadAllLines(CountOfAllPath)[2]);
        CountOfBoughtStuff = int.Parse(File.ReadAllLines(CountOfAllPath)[3]);

        MoneyCount = int.Parse(File.ReadAllLines(PlayerSourcePath)[1].ToString());
        Money.text = MoneyCount.ToString();


        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| GENERATE STORE |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Generation Of Persons to File ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        PersSetPath = Application.persistentDataPath + "/" + PersSettings.ToString() + ".txt"; 
        StreamWriter WritePersPrefs = new StreamWriter(PersSetPath);
        for (int i = 1; i < CountOfPers + 1; i++)
        {

            int Health;
            int Damage = Random.Range(30, 90);
            int Accuracy = 100 - Damage;
            Health = Mathf.FloorToInt(100 - Accuracy + 2 * Damage);
            int Level = 1;
            int CountOfBattles = 0;
            int Price = Mathf.FloorToInt(Level * (2 * Accuracy + Damage + Health));
            int Skin = Random.Range(1, 6);

            WritePersPrefs.WriteLine("=== Person " + i.ToString() + "===");
            WritePersPrefs.WriteLine(Health);
            WritePersPrefs.WriteLine(Damage);
            WritePersPrefs.WriteLine(Accuracy);
            WritePersPrefs.WriteLine(Level);
            WritePersPrefs.WriteLine(CountOfBattles);
            WritePersPrefs.WriteLine(Price);
            WritePersPrefs.WriteLine(Skin);

        }
        WritePersPrefs.Close();

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Generation Of Weapon to File ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        WeaponBundlePath = Application.persistentDataPath + "/" + WeaponBundle + ".txt";
        StreamWriter WriteWeapon = new StreamWriter(WeaponBundlePath);
        for (int i = 1; i < CountOfWeapon + 1; i++)
        {

            int WpnCount = Random.Range(1, 11);
            int Condition = Random.Range(0, 10);
            WriteWeapon.WriteLine("=== Weapon " + i.ToString() + " ===");
            WriteWeapon.WriteLine(WpnCount.ToString());
            WriteWeapon.WriteLine(Condition.ToString());

        }
        WriteWeapon.Close();

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Generation Of Bullets to File ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        BulletsInStorePath = Application.persistentDataPath + "/" + BulletsInStore + ".txt";
        StreamWriter WriteBullets = new StreamWriter(BulletsInStorePath);
        for (int i = 1; i < CountOfBullets + 1; i++)
        {

            int BulletSkin = i;
            int Count = Random.Range(1, 100);
            WriteBullets.WriteLine("=== Bullets " + i.ToString() + "===");
            WriteBullets.WriteLine(BulletSkin.ToString());
            WriteBullets.WriteLine(Count.ToString());

        }

        WriteBullets.Close();

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Generation Of Other Stuff ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        OtherStuffPath = Application.persistentDataPath + "/" + OtherStuff + ".txt";
        StreamWriter WriteOtherStuff = new StreamWriter(OtherStuffPath);
        for (int i = 1; i < CountOfOtherStuff + 1; i++) {

            int OtherStuffSkin = Random.Range(1, 4);
            WriteOtherStuff.WriteLine("=== Other Stuff " + i.ToString() + "===");
            WriteOtherStuff.WriteLine(OtherStuffSkin.ToString());

        }

        WriteOtherStuff.Close();

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Import Pers From File ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        //PersSetPath = Application.persistentDataPath.ToString() + "/" + PersSettings + ".txt";

        for (int i = 1; i < CountOfPers + 1; i++) {
            GameObject P = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
            P.name = "Pers" + i.ToString();
            P.transform.SetParent(PersPanel.transform);
            P.transform.localPosition = new Vector3(0.5f * i, 0.15f, -0.2f);

            P.GetComponent<PersProperties>().Health = int.Parse(File.ReadAllLines(PersSetPath)[NumPersParam * i - NumPersParam + 1]);
            P.GetComponent<PersProperties>().Damage = int.Parse(File.ReadAllLines(PersSetPath)[NumPersParam * i - NumPersParam + 2]);
            P.GetComponent<PersProperties>().Accuracy = int.Parse(File.ReadAllLines(PersSetPath)[NumPersParam * i - NumPersParam + 3]);
            P.GetComponent<PersProperties>().Level = int.Parse(File.ReadAllLines(PersSetPath)[NumPersParam * i - NumPersParam + 4]);
            P.GetComponent<PersProperties>().CountOfBattles = int.Parse(File.ReadAllLines(PersSetPath)[NumPersParam * i - NumPersParam + 5]);
            P.GetComponent<PersProperties>().Price = int.Parse(File.ReadAllLines(PersSetPath)[NumPersParam * i - NumPersParam + 6]);

            GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(P.transform);
            L.transform.localPosition = new Vector3(0, 0, -0.1f);
        }

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Import Weapon From File ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        //WeaponBundlePath = Application.persistentDataPath + "/" + WeaponBundle + ".txt";

        for (int i = 1; i < CountOfWeapon + 1; i++) {
            GameObject W = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
            W.name = "Weapon" + i.ToString();
            W.transform.SetParent(WeaponPanel.transform);
            W.transform.localPosition = new Vector3(0.5f * i, -0.1f, -1);

            W.GetComponent<WeaponProperties>().Skin = int.Parse(File.ReadAllLines(WeaponBundlePath)[3 * i - 3 + 1]);
            W.GetComponent<WeaponProperties>().Condition = int.Parse(File.ReadAllLines(WeaponBundlePath)[3 * i - 3 + 2]);

            GameObject L = Instantiate(Resources.Load("Lighter_01")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(W.transform);
            L.transform.localPosition = new Vector3( 0, 0.02f, 0.1f);

        }

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Import Bullets From File ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        //BulletsInStorePath = Application.persistentDataPath + "/" + BulletsInStore + ".txt";

        for (int i = 1; i < CountOfBullets + 1; i++) {
            GameObject B = Instantiate(Resources.Load("BulletsDoll")) as GameObject;
            B.name = "Bullets" + i.ToString();
            B.transform.SetParent(BulletsPanel.transform);
            B.transform.localPosition = new Vector3(0.5f * i, -0.1f, -1);

            B.GetComponent<Bullets>().BulletsSkin = int.Parse(File.ReadAllLines(BulletsInStorePath)[3 * i - 3 + 1]);
            B.GetComponent<Bullets>().CountOfBullets = int.Parse(File.ReadAllLines(BulletsInStorePath)[3 * i - 3 + 2]);

            GameObject L = Instantiate(Resources.Load("Lighter_01")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(B.transform);
            L.transform.localPosition = new Vector3( 0, 0.02f, 0.1f);

        }
        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Import Other Stuff From File ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        for (int i = 1; i < CountOfOtherStuff + 1; i++) {
            GameObject S = Instantiate(Resources.Load("OtherStuffPrefab")) as GameObject;
            S.name = "Stuff" + i.ToString();
            S.transform.SetParent(OtherPanel.transform);
            S.transform.localPosition = new Vector3(0.5f * i, -0.1f, -1);

            S.GetComponent<OtherStuff>().Skin = int.Parse(File.ReadAllLines(OtherStuffPath)[i * 2 - 1]);

            GameObject L = Instantiate(Resources.Load("Lighter_01")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(S.transform);
            L.transform.localPosition = new Vector3(0, 0.02f, 0.1f);

        }

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Import Bought Pers ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        for (int Pp = 1; Pp < CountOfBoughtPers + 1; Pp++) {
            GameObject P = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
            P.name = "BoughtPers" + Pp.ToString();
            P.transform.SetParent(BoughtPersonsPanel.transform);
            P.GetComponent<PersProperties>().Bought = true;
            P.layer = 17;
            P.transform.localPosition = GameObject.Find(BoughtPersonsPanel.name + "/PosPers" + Pp.ToString()).transform.localPosition;
            P.GetComponent<PersProperties>().Health = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + Pp * NumPersParam - NumPersParam + 1]); 
            P.GetComponent<PersProperties>().Damage = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + Pp * NumPersParam - NumPersParam + 2]); 
            P.GetComponent<PersProperties>().Accuracy = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + Pp * NumPersParam - NumPersParam + 3]); 
            P.GetComponent<PersProperties>().Level = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + Pp * NumPersParam - NumPersParam + 4]); 
            P.GetComponent<PersProperties>().CountOfBattles = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + Pp * NumPersParam - NumPersParam + 5]); 
            P.GetComponent<PersProperties>().Price = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + Pp * NumPersParam - NumPersParam + 6]); 
            Destroy(GameObject.Find(BoughtPersonsPanel.name + "/PosPers" + Pp.ToString()));
        }

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Import Bought Weapon ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        for (int Ww = 1; Ww < CountOfBoughtWeapon + 1; Ww++) {
            GameObject W = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
            W.name = "BoughtWeapon" + Ww.ToString();
            W.transform.SetParent(WpnBoughtPanel.transform);
            W.GetComponent<WeaponProperties>().Bought = true;
            W.layer = 17;
            W.transform.localPosition = GameObject.Find(WpnBoughtPanel.name + "/PosWpn" + Ww.ToString()).transform.localPosition;
            W.GetComponent<WeaponProperties>().Damage = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + Ww * NumWpnParam - NumWpnParam + 1]);
            W.GetComponent<WeaponProperties>().Condition = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + Ww * NumWpnParam - NumWpnParam + 2]);
            W.GetComponent<WeaponProperties>().CountOfBullets = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + Ww * NumWpnParam - NumWpnParam + 3]);
            W.GetComponent<WeaponProperties>().Price = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + Ww * NumWpnParam - NumWpnParam + 4]);
            W.GetComponent<WeaponProperties>().Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + Ww * NumWpnParam - NumWpnParam + 5]);
            Destroy(GameObject.Find(WpnBoughtPanel.name + "/PosWpn" + Ww.ToString()));

        }

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Import Bought Bullets ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        for (int Bb = 1; Bb < CountOfBoughtBullets + 1; Bb++) {
            GameObject B = Instantiate(Resources.Load("BulletsDoll")) as GameObject;
            B.name = "BoughtBullets" + Bb.ToString();
            B.transform.SetParent(BoughtBulletsPanel.transform);
            B.GetComponent<Bullets>().Bought = true;
            B.layer = 17;
            B.transform.localPosition = GameObject.Find(BoughtBulletsPanel.name + "/PosBullets" + Bb.ToString()).transform.localPosition;
            B.GetComponent<Bullets>().BulletsSkin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + 3 * Bb - 2]);
            B.GetComponent<Bullets>().CountOfBullets = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + 3 * Bb - 1]);
            Destroy(GameObject.Find(BoughtBulletsPanel.name + "/PosBullets" + Bb.ToString()));
        }

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Import Bought Stuff ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        for (int Bo = 1; Bo < CountOfBoughtStuff + 1; Bo++) {
            GameObject O = Instantiate(Resources.Load("OtherStuffPrefab")) as GameObject;
            O.name = "BoughtStuff" + Bo.ToString();
            O.transform.SetParent(BoughtOtherPanel.transform);
            O.GetComponent<OtherStuff>().Bought = true;
            O.layer = 17;
            O.transform.localPosition = GameObject.Find(BoughtOtherPanel.name + "/PosOther" + Bo.ToString()).transform.localPosition;
            O.GetComponent<OtherStuff>().Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + CountOfBoughtBullets * 3 + Bo * 2 - 1]);
            Destroy(GameObject.Find(BoughtOtherPanel.name + "/PosOther" + Bo.ToString()));
        }

        PersPanel.transform.position = new Vector3(Camera.main.transform.position.x - 0.5f, Camera.main.transform.position.y + 0.1f, -1.5f);
        BoughtPersonsPanel.transform.localPosition = new Vector3(0 , Camera.main.transform.position.y - 0.7f, 6);
        BoughtPersonsPanel.transform.position = BoughtPersUI.transform.position - new Vector3(0, 0, 0.5f);
        WeaponPanel.transform.position = new Vector3(Camera.main.transform.position.x - 2, Camera.main.transform.position.y + 0.4f, WeaponPanel.transform.position.z);
        BulletsPanel.transform.position = WeaponPanel.transform.position;
        OtherPanel.transform.position = WeaponPanel.transform.position;
        WpnBoughtPanel.transform.position = WeaponBoughtUI.transform.position;
        BoughtBulletsPanel.transform.position = WeaponBoughtUI.transform.position;
        BoughtOtherPanel.transform.position = WeaponBoughtUI.transform.position;
        InventoryProperties.transform.position = GameObject.Find(InventoryPanel.name + "/InventoryProperties").transform.position;

        UpCountBullets.interactable = false;
        DownCountBullets.interactable = false;
        OtherPanel.active = false;
        BoughtOtherPanel.active = false;
        BoughtBulletsPanel.active = false;
        WpnSwitch.interactable = false;
        Slaves.interactable = false;

        GoToMapButton.interactable = false;

        SelectedItem = "DummySwitcher";

        Buy.interactable = false;

        PersonsStorePanel.active = true;
        PersPanel.active = true;
        BoughtPersonsPanel.active = true;
        WeaponStorePanel.active = false;
        ArmorPanel.active = false;
        InventoryPanel.active = false;
        InventoryPersons.active = false;
        InventoryWeapon.active = false;
        BulletsPanel.active = false;
        InventoryStuff.active = false;
        InventoryProperties.active = false;

    }


    void NextPers() {

        Prev.GetComponent<AudioSource>().Play();
        PersPanel.transform.localPosition = new Vector3(PersPanel.transform.localPosition.x - 0.25f, PersPanel.transform.localPosition.y, PersPanel.transform.localPosition.z);
        NearFront.transform.localPosition = new Vector3(NearFront.transform.localPosition.x - 0.4f, NearFront.transform.localPosition.y, NearFront.transform.localPosition.z);
        Frontyard.transform.localPosition = new Vector3(Frontyard.transform.localPosition.x - 0.2f, Frontyard.transform.localPosition.y, Frontyard.transform.localPosition.z);
        LastBack.transform.localPosition = new Vector3(LastBack.transform.localPosition.x + 0.2f, LastBack.transform.localPosition.y, LastBack.transform.localPosition.z);
        if (PersPanel.transform.localPosition.x >= 0.0f)
        {
            Prev.interactable = false;
        }
        else
        {
            Prev.interactable = true;
        }
        if (PersPanel.transform.localPosition.x <= -5.0f)
        {
            Next.interactable = false;
        }
        else
        {
            Next.interactable = true;
        }
    }

    void PrevPers() {

        Next.GetComponent<AudioSource>().Play();
        PersPanel.transform.localPosition = new Vector3(PersPanel.transform.localPosition.x + 0.25f, PersPanel.transform.localPosition.y, PersPanel.transform.localPosition.z);
        NearFront.transform.localPosition = new Vector3(NearFront.transform.localPosition.x + 0.4f, NearFront.transform.localPosition.y, NearFront.transform.localPosition.z);
        Frontyard.transform.localPosition = new Vector3(Frontyard.transform.localPosition.x + 0.2f, Frontyard.transform.localPosition.y, Frontyard.transform.localPosition.z);
        LastBack.transform.localPosition = new Vector3(LastBack.transform.localPosition.x - 0.2f, LastBack.transform.localPosition.y, LastBack.transform.localPosition.z);
        if (PersPanel.transform.localPosition.x >= -1.5f)
        {
            Prev.interactable = false;
        }
        else
        {
            Prev.interactable = true;
        }
        if (PersPanel.transform.localPosition.x <= -7.0f)
        {
            Next.interactable = false;
        }
        else
        {
            Next.interactable = true;
        }
    }

    void PrevWeapon() {

        NextWpn.GetComponent<AudioSource>().Play();
        WeaponPanelPos = WeaponPanelPos + 0.25f;
        NewWpnPanPos = new Vector3(WeaponPanelPos, NewWpnPanPos.y, NewWpnPanPos.z);
        WeaponPanel.transform.position = NewWpnPanPos;
        BulletsPanel.transform.position = NewWpnPanPos;
        OtherPanel.transform.position = NewWpnPanPos;
        if (WeaponPanelPos >= -1.5f)
        {
            PrevWpn.interactable = false;
        }
        else
        {
            PrevWpn.interactable = true;
        }
        if (WeaponPanelPos <= -7.0f)
        {
            NextWpn.interactable = false;
        }
        else
        {
            NextWpn.interactable = true;
        }

    }

    void NextWeapon() {

        PrevWpn.GetComponent<AudioSource>().Play();
        WeaponPanelPos = WeaponPanelPos - 0.25f;
        NewWpnPanPos = new Vector3(WeaponPanelPos, NewWpnPanPos.y, NewWpnPanPos.z);
        WeaponPanel.transform.position = NewWpnPanPos;
        BulletsPanel.transform.position = NewWpnPanPos;
        OtherPanel.transform.position = NewWpnPanPos;
        if (WeaponPanelPos >= 1.0f)
        {
            PrevWpn.interactable = false;
        }
        else
        {
            PrevWpn.interactable = true;
        }
        if (WeaponPanelPos <= -5.0f)
        {
            NextWpn.interactable = false;
        }
        else
        {
            NextWpn.interactable = true;
        }

    }

    void OpenMenu() {

        Menu.GetComponent<AudioSource>().Play();

        MenuPanel.active = true;
        Prev.enabled = false;
        Next.enabled = false;
        PrevWpn.enabled = false;
        NextWpn.enabled = false;
        Slaves.enabled = false;
        Weapon.enabled = false;
        Inventory.enabled = false;
        Buy.enabled = false;

        Back.onClick.AddListener(GotoMainMenu);
        Resume.onClick.AddListener(BackToSell);

    }

    void GotoMainMenu() {

        SceneManager.LoadScene(0);

    }

    void BackToSell() {


        MenuPanel.active = false;
        Prev.enabled = true;
        Next.enabled = true;
        PrevWpn.enabled = true;
        NextWpn.enabled = true;
        Slaves.enabled = true;
        Weapon.enabled = true;
        Inventory.enabled = true;
        Buy.enabled = true;

    }

    void BuyPerses() {

        for (int i = 1; i < 10; i++) {
            if (GameObject.Find("Lighter") != null) {
                GameObject.Find("Lighter").active = false;
            }
        }
        SelectedItem = "DummySwitcher";
        if (GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>() != null) {
            GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
        }
        else {
            GameObject.Find(SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        SelectedInPanel = "DummySwitcherBought";

        Slaves.GetComponent<AudioSource>().Play();
        Slaves.interactable = false;
        Weapon.interactable = true;
        Inventory.interactable = true;
        Buy.interactable = false;
        PanelActivated = 1;

        Buy.transform.gameObject.active = true;

        Buy.transform.SetParent(BuyPanel.transform);
        Money.transform.SetParent(BuyPanel.transform);
        Buy.transform.localPosition = BuyButtonPos;
        Money.transform.localPosition = MoneyPos;

        PersonsStorePanel.active = true;
        PersPanel.active = true;
        BoughtPersonsPanel.active = true;

        WeaponStorePanel.active = false;
        ArmorPanel.active = false;

        InventoryPanel.active = false;
        InventoryPersons.active = false;
        InventoryWeapon.active = false;
        InventoryStuff.active = false;
        InventoryProperties.active = false;

        GoToMapButton.interactable = false;

    }

    void BuyWeapon() {

        for (int i = 1; i < 10; i++) {
            if (GameObject.Find("Lighter") != null) {
                GameObject.Find("Lighter").active = false;
            }
        }
        SelectedItem = "DummySwitcher";
        Weapon.GetComponent<AudioSource>().Play();
        if (GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>() != null) {
            GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
        }
        else {
            GameObject.Find(SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        SelectedInPanel = "DummySwitcherBought";
        Weapon.interactable = false;
        Slaves.interactable = true;
        Inventory.interactable = true;
        Buy.interactable = false;

        PanelActivated = 2;

        Buy.transform.gameObject.active = true;

        PersonsStorePanel.active = false;
        PersPanel.active = false;
        BoughtPersonsPanel.active = false;

        WeaponStorePanel.active = true;
        ArmorPanel.active = true;
        if (WpnSwitch.interactable == false) {
            WeaponPanel.active = true;
            WpnBoughtPanel.active = true;
            BoughtBulletsPanel.active = false;
            BulletsPanel.active = false;
            OtherPanel.active = false;
            BoughtOtherPanel.active = false;
        }

        if (BulletsSwitch.interactable == false) {
            WeaponPanel.active = false;
            WpnBoughtPanel.active = false;
            BoughtBulletsPanel.active = true;
            BulletsPanel.active = true;
            OtherPanel.active = false;
            BoughtOtherPanel.active = false;
        }

        if (OtherSwitch.interactable == false) {
            WeaponPanel.active = false;
            WpnBoughtPanel.active = false;
            BoughtBulletsPanel.active = false;
            BulletsPanel.active = false;
            OtherPanel.active = true;
            BoughtOtherPanel.active = true;
        }

        InventoryPanel.active = false;
        InventoryPersons.active = false;
        InventoryWeapon.active = false;
        InventoryStuff.active = false;
        InventoryProperties.active = false;

        Buy.transform.SetParent(WeaponProperties.transform);
        Buy.transform.localPosition = new Vector3(0, 35, Buy.transform.localPosition.z);

        WeaponOfOtherProperties.text = "Select any item \n\n\nYour Money: " + MoneyCount.ToString();
        GoToMapButton.interactable = false;

    }

    void GoToInventory() {

        //for (int i = 1; i < 10; i++)
        //{
        //    if (GameObject.Find("Lighter") != null)
        //    {
        //        GameObject.Find("Lighter").active = false;
        //    }
        //}
        //if (GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>() != null)
        //{
        //    GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
        //}
        //else
        //{
        //    GameObject.Find(SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        //}

        Inventory.GetComponent<AudioSource>().Play();
        Inventory.interactable = false;
        Weapon.interactable = true;
        Slaves.interactable = true;


        Buy.transform.gameObject.active = false;
        //Money.transform.localPosition = new Vector3(15, 0, Money.transform.localPosition.z);

        PersonsStorePanel.active = false;
        PersPanel.active = false;
        BoughtPersonsPanel.active = false;

        WeaponStorePanel.active = false;
        ArmorPanel.active = false;

        //InventoryPanel.active = true;
        //InventoryPersons.active = true;
        //InventoryWeapon.active = true;
        //InventoryProperties.active = true;
        GenerateYourInventory();

        SelectedInPanel = "DummySwitcherBought";
        NameOfSelected = "DummySwitcherBought";
        NextSelectedInv = "DummySwitcherBought";
        PrevSelectedInv = "DummySwitcherBought";
        FrstLvlSelected = "DummySwitcherBought";

        //this.GetComponent<InventoryWorking>().PrevSelected = GameObject.Find(InventoryPersons.name + "/DummySwitcherBought");
        //this.GetComponent<InventoryWorking>().NextSelected = GameObject.Find(InventoryPersons.name + "/DummySwitcherBought");

        //ShowGuide();
        //AppointPos = false;
        //SelectedWeapon = false;
        //SelectedInPack = false;

        PanelActivated = 3;
        Debug.Log("End");
        this.GetComponent<Store>().enabled = false;
        this.GetComponent<InventoryWorking>().enabled = true;
        //GoToMapButton.interactable = true;
        //RepairOff();

    }

    //void ShowGuide() {
    //    PersPackCage.active = false;
    //    InventoryText.text = "1.Select your slave \n2.Tap on cell \nto drag your slave \n3.Select weapon \n4.Tap on slave \nto assign it";
    //}

    void SwitchToWeapon() {

        UpCountBullets.interactable = false;
        DownCountBullets.interactable = false;
        WeaponOfOtherProperties.text = "Select any item \n\n\nYour Money: " + MoneyCount.ToString();

        if (GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>() != null)
        {
            GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
        }
        else
        {
            GameObject.Find(SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        SelectedInPanel = "DummySwitcherBought";

        WpnSwitch.GetComponent<AudioSource>().Play();
        WpnSwitch.transform.SetAsFirstSibling();
        BulletsSwitch.transform.SetAsLastSibling();
        OtherSwitch.transform.SetAsLastSibling();
        WpnSwitch.interactable = false;
        BulletsSwitch.interactable = true;
        OtherSwitch.interactable = true;
        WeaponPanel.active = true;
        WpnBoughtPanel.active = true;
        BoughtBulletsPanel.active = false;
        BulletsPanel.active = false;
        OtherPanel.active = false;
        BoughtOtherPanel.active = false;
        Buy.interactable = false;
        for (int i = 1; i < 10; i++)
        {
            if (GameObject.Find("Lighter") != null)
            {
                GameObject.Find("Lighter").active = false;
            }
        }
        SelectedItem = "DummySwitcher";

    }

    void SwitchToBullets() {

        WeaponOfOtherProperties.text = "Select any item \n\n\nYour Money: " + MoneyCount.ToString();

        if (GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>() != null)
        {
            GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
        }
        else
        {
            GameObject.Find(SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        SelectedInPanel = "DummySwitcherBought";

        BulletsSwitch.GetComponent<AudioSource>().Play();
        BulletsSwitch.transform.SetAsFirstSibling();
        WpnSwitch.transform.SetAsLastSibling();
        OtherSwitch.transform.SetAsLastSibling();
        BulletsSwitch.interactable = false;
        OtherSwitch.interactable = true;
        WpnSwitch.interactable = true;
        WeaponPanel.active = false;
        WpnBoughtPanel.active = false;
        BoughtBulletsPanel.active = true;
        BulletsPanel.active = true;
        OtherPanel.active = false;
        BoughtOtherPanel.active = false;
        Buy.interactable = false;
        for (int i = 1; i < 10; i++)
        {
            if (GameObject.Find("Lighter") != null)
            {
                GameObject.Find("Lighter").active = false;
            }
        }
        SelectedItem = "DummySwitcher";

    }
    void SwitchToOther() {
        UpCountBullets.interactable = false;
        DownCountBullets.interactable = false;
        WeaponOfOtherProperties.text = "Select any item \n\n\nYour Money: " + MoneyCount.ToString();

        if (GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>() != null) {
            GameObject.Find(SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
        }
        else {
            GameObject.Find(SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        SelectedInPanel = "DummySwitcherBought";

        OtherSwitch.GetComponent<AudioSource>().Play();
        OtherSwitch.transform.SetAsFirstSibling();
        WpnSwitch.transform.SetAsLastSibling();
        BulletsSwitch.transform.SetAsLastSibling();
        OtherSwitch.interactable = false;
        WpnSwitch.interactable = true;
        BulletsSwitch.interactable = true;
        WeaponPanel.active = false;
        WpnBoughtPanel.active = false;
        BoughtBulletsPanel.active = false;
        BulletsPanel.active = false;
        OtherPanel.active = true;
        BoughtOtherPanel.active = true;
        Buy.interactable = false;
        for (int i = 1; i < 10; i++)
        {
            if (GameObject.Find("Lighter") != null)
            {
                GameObject.Find("Lighter").active = false;
            }
        }
        SelectedItem = "DummySwitcher";

    }

    void GoToMap() {
        GoToMapButton.GetComponent<AudioSource>().Play();

        StreamWriter NewData = new StreamWriter(CountOfAllPath);
        NewData.WriteLine(CountOfBoughtPers);
        NewData.WriteLine(CountOfBoughtWeapon);
        NewData.WriteLine(CountOfBoughtBullets);
        NewData.WriteLine(CountOfBoughtStuff);
        NewData.Close();

        SceneManager.LoadScene(2);
    }

    void UpperCountBullets() {

        UpCountBullets.GetComponent<AudioSource>().Play();
        GameObject SelectedBullets = GameObject.Find(BulletsPanel.name + "/" + SelectedItem);
        CountSelectedBullets = CountSelectedBullets + 1;
        int Clip = SelectedBullets.GetComponent<Bullets>().ClipOfWeapon;
        int CountOfBullets = SelectedBullets.GetComponent<Bullets>().CountOfBullets;
        int Price = SelectedBullets.GetComponent<Bullets>().Price;
        int InClip = CountSelectedBullets * Clip;

        if ((CountSelectedBullets + 1) * Clip > CountOfBullets)
        {
            UpCountBullets.interactable = false;
            string Name = SelectedBullets.GetComponent<Bullets>().Name.ToString();
            WeaponOfOtherProperties.text = "How much would You buy?:" + "\nfor " + Name + "\n" + InClip.ToString() + "\nPrice: " + (InClip * Price).ToString() + "\nCount in store: " + CountOfBullets + "\nYour Money: " + MoneyCount.ToString();
        }
        else {
            string Name = SelectedBullets.GetComponent<Bullets>().Name.ToString();
            WeaponOfOtherProperties.text = "How much would You buy?:" + "\nfor " + Name + "\n" + InClip.ToString() + "\nPrice: " + (InClip * Price).ToString() + "\nCount in store: " + CountOfBullets + "\nYour Money: " + MoneyCount.ToString();
        }
        DownCountBullets.interactable = true;

    }

    void DownerCountBullets() {

        DownCountBullets.GetComponent<AudioSource>().Play();
        GameObject SelectedBullets = GameObject.Find(BulletsPanel.name + "/" + SelectedItem);
        CountSelectedBullets = CountSelectedBullets - 1;
        int Clip = SelectedBullets.GetComponent<Bullets>().ClipOfWeapon;
        int CountOfBullets = SelectedBullets.GetComponent<Bullets>().CountOfBullets;
        int Price = SelectedBullets.GetComponent<Bullets>().Price;
        int InClip = CountSelectedBullets * Clip;

        if ((CountSelectedBullets - 1) * Clip < 0)
        {
            DownCountBullets.interactable = false;
            string Name = SelectedBullets.GetComponent<Bullets>().Name.ToString();
            WeaponOfOtherProperties.text = "How much would You buy?:" + "\nfor " + Name + "\n" + InClip.ToString() + "\nPrice: " + (InClip * Price).ToString() + "\nCount in store: " + CountOfBullets + "\nYour Money: " + MoneyCount.ToString();
        }
        else
        {
            string Name = SelectedBullets.GetComponent<Bullets>().Name.ToString();
            WeaponOfOtherProperties.text = "How much would You buy?:" + "\nfor " + Name + "\n" + InClip.ToString() + "\nPrice: " + (InClip * Price).ToString() + "\nCount in store: " + CountOfBullets + "\nYour Money: " + MoneyCount.ToString();
        }
        UpCountBullets.interactable = true;

    }

    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| UPDATE FUNCTION ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

    void Update()
    {

        //if (PanelActivated == 3)
        //{
        //    this.GetComponent<InventoryWorking>().enabled = true;
        //    this.GetComponent<Store>().enabled = false;
        //}
        //else {
        //    this.GetComponent<InventoryWorking>().enabled = false;
        //    this.GetComponent<Store>().enabled = true;
        //}

        if (MenuPanel.active == false) {
            if (Input.touchCount > 0)
            {
                for (int q = 0; q < Input.touchCount; ++q)
                {
                    Touch touch = Input.GetTouch(q);
                    if (touch.phase == TouchPhase.Began)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                        if (hit)
                        {
                            if (PanelActivated == 1)
                            {

                                if (hit.collider.gameObject.layer == 8)
                                {
                                    Health.text = hit.transform.gameObject.GetComponent<PersProperties>().Health.ToString();
                                    Damage.text = hit.transform.gameObject.GetComponent<PersProperties>().Damage.ToString();
                                    Accuracy.text = hit.transform.gameObject.GetComponent<PersProperties>().Accuracy.ToString();
                                    Level.text = hit.transform.gameObject.GetComponent<PersProperties>().Level.ToString();
                                    Price.text = hit.transform.gameObject.GetComponent<PersProperties>().Price.ToString();
                                    hit.transform.gameObject.GetComponent<AudioSource>().Play();
                                    if (hit.transform.gameObject.GetComponent<PersProperties>().Bought == false)
                                    {
                                        GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                                        GameObject.Find(PersPanel.name + "/" + hit.transform.gameObject.name + "/Lighter").active = true;
                                        SelectedItem = hit.transform.gameObject.name;
                                        Buy.interactable = true;
                                    }
                                    GameObject.Find(BoughtPersonsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                    SelectedInPanel = "DummySwitcherBought";
                                }

                                if (hit.collider.gameObject.layer == 17)
                                {
                                    Health.text = hit.transform.gameObject.GetComponent<PersProperties>().Health.ToString();
                                    Damage.text = hit.transform.gameObject.GetComponent<PersProperties>().Damage.ToString();
                                    Accuracy.text = hit.transform.gameObject.GetComponent<PersProperties>().Accuracy.ToString();
                                    Level.text = hit.transform.gameObject.GetComponent<PersProperties>().Level.ToString();
                                    Price.text = hit.transform.gameObject.GetComponent<PersProperties>().Price.ToString();
                                    BoughtPersonsPanel.GetComponent<AudioSource>().Play();
                                    GameObject.Find(BoughtPersonsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                    SelectedInPanel = hit.collider.gameObject.name;
                                    GameObject.Find(BoughtPersonsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                                    if (GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active != null)
                                    {
                                        GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                                        Buy.interactable = false;
                                    }
                                }

                            }

                            if (PanelActivated == 2)
                            {

                                if (hit.collider.gameObject.layer == 9)
                                {
                                    WeaponProperties.GetComponent<AudioSource>().Play();
                                    string Name = hit.transform.gameObject.GetComponent<WeaponProperties>().Name.ToString();
                                    string Damage = hit.transform.gameObject.GetComponent<WeaponProperties>().Damage.ToString();
                                    int Condition = hit.transform.gameObject.GetComponent<WeaponProperties>().Condition;
                                    int CountOfBullets = hit.transform.gameObject.GetComponent<WeaponProperties>().CountOfBullets;
                                    string Price = hit.transform.gameObject.GetComponent<WeaponProperties>().Price.ToString();
                                    string Con = "";
                                    string COB = "";
                                    for (int a = 1; a < Condition + 1; a++)
                                    {
                                        Con = Con + "|";
                                    }
                                    for (int b = 1; b < CountOfBullets + 1; b++)
                                    {
                                        COB = COB + "|";
                                    }

                                    WeaponOfOtherProperties.text = Name + "\n\nDamage: " + Damage + "\nCondition: " + Con + Condition.ToString() + "\nClip: " + COB + CountOfBullets.ToString() + "\nPrice: " + Price + "\nYour Money: " + MoneyCount.ToString();
                                    hit.transform.gameObject.GetComponent<AudioSource>().Play();

                                    GameObject.Find(WeaponPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                                    GameObject.Find(WeaponPanel.name + "/" + hit.transform.gameObject.name + "/Lighter").active = true;
                                    Buy.interactable = true;
                                    SelectedItem = hit.transform.gameObject.name;
                                    GameObject.Find(WpnBoughtPanel.name + "/" + SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
                                    SelectedInPanel = "DummySwitcherBought";
                                }

                                if (hit.collider.gameObject.layer == 11)
                                {

                                    WeaponProperties.GetComponent<AudioSource>().Play();
                                    string Name = hit.transform.gameObject.GetComponent<Bullets>().Name.ToString();
                                    string CountOfBullets = hit.transform.gameObject.GetComponent<Bullets>().CountOfBullets.ToString();
                                    int Price = hit.transform.gameObject.GetComponent<Bullets>().Price;
                                    int Clip = hit.transform.gameObject.GetComponent<Bullets>().ClipOfWeapon;
                                    CountSelectedBullets = 1;
                                    int InClip = CountSelectedBullets * Clip;

                                    WeaponOfOtherProperties.text = "How much would You buy?:" + "\nfor " + Name + "\n" + InClip.ToString() + "\nPrice: " + (InClip * Price).ToString() + "\nCount in store: " + CountOfBullets + "\nYour Money: " + MoneyCount.ToString();

                                    hit.transform.gameObject.GetComponent<AudioSource>().Play();

                                    if (InClip <= hit.transform.gameObject.GetComponent<Bullets>().CountOfBullets)
                                    {
                                        if (InClip + hit.transform.gameObject.GetComponent<Bullets>().ClipOfWeapon <= hit.transform.gameObject.GetComponent<Bullets>().CountOfBullets)
                                        {
                                            UpCountBullets.interactable = true;
                                        }
                                        else
                                        {
                                            UpCountBullets.interactable = false;
                                        }
                                        Buy.interactable = true;
                                    }
                                    else
                                    {
                                        UpCountBullets.interactable = false;
                                        Buy.interactable = false;
                                    }

                                    GameObject.Find(BulletsPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                                    GameObject.Find(BulletsPanel.name + "/" + hit.transform.gameObject.name + "/Lighter").active = true;
                                    SelectedItem = hit.transform.gameObject.name;

                                    GameObject.Find(BoughtBulletsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                    SelectedInPanel = "DummySwitcherBought";
                                }

                                if (hit.collider.gameObject.layer == 19)
                                {

                                    WeaponProperties.GetComponent<AudioSource>().Play();
                                    hit.transform.gameObject.GetComponent<AudioSource>().Play();

                                    string Name = hit.transform.gameObject.GetComponent<OtherStuff>().Name.ToString();
                                    int Skin = hit.transform.gameObject.GetComponent<OtherStuff>().Skin;
                                    int Price = hit.transform.gameObject.GetComponent<OtherStuff>().Price;

                                    Buy.interactable = true;

                                    string Description = "";

                                    if (Skin == 1)
                                    {
                                        Description = "Heals your slave";
                                    }
                                    if (Skin == 2)
                                    {
                                        Description = "Need for move";
                                    }
                                    if (Skin == 3)
                                    {
                                        Description = "Increases damage";
                                    }

                                    WeaponOfOtherProperties.text = Name + "\n" + Description + "\nPrice: " + Price.ToString();

                                    GameObject.Find(OtherPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                                    GameObject.Find(OtherPanel.name + "/" + hit.transform.gameObject.name + "/Lighter").active = true;
                                    SelectedItem = hit.transform.gameObject.name;

                                    GameObject.Find(BoughtOtherPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                    SelectedInPanel = "DummySwitcherBought";
                                }

                                if (hit.collider.gameObject.layer == 17)
                                {
                                    WeaponProperties.GetComponent<AudioSource>().Play();
                                    if (hit.collider.gameObject.GetComponent<WeaponProperties>() != null)
                                    {
                                        string Name = hit.transform.gameObject.GetComponent<WeaponProperties>().Name.ToString();
                                        string Damage = hit.transform.gameObject.GetComponent<WeaponProperties>().Damage.ToString();
                                        int Condition = hit.transform.gameObject.GetComponent<WeaponProperties>().Condition;
                                        int CountOfBullets = hit.transform.gameObject.GetComponent<WeaponProperties>().CountOfBullets;
                                        string Price = hit.transform.gameObject.GetComponent<WeaponProperties>().Price.ToString();
                                        string Con = "";
                                        string COB = "";
                                        for (int a = 1; a < Condition + 1; a++)
                                        {
                                            Con = Con + "|";
                                        }
                                        for (int b = 1; b < CountOfBullets + 1; b++)
                                        {
                                            COB = COB + "|";
                                        }
                                        WeaponOfOtherProperties.text = Name + "\n\nDamage: " + Damage + "\nCondition: " + Con + Condition.ToString() + "\nClip: " + COB + CountOfBullets.ToString() + "\nPrice: " + Price;
                                        GameObject.Find(WpnBoughtPanel.name + "/" + SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
                                        SelectedInPanel = hit.collider.gameObject.name;
                                        GameObject.Find(WpnBoughtPanel.name + "/" + SelectedInPanel).GetComponent<WeaponProperties>().IsActive = true;
                                    }
                                    if (hit.collider.gameObject.GetComponent<Bullets>() != null)
                                    {
                                        string Name = hit.transform.gameObject.GetComponent<Bullets>().Name.ToString();
                                        string CountOfBullets = hit.transform.gameObject.GetComponent<Bullets>().CountOfBullets.ToString();

                                        WeaponOfOtherProperties.text = "Bullets for " + Name + "\n\nIn inventory: " + CountOfBullets.ToString();
                                        GameObject.Find(BoughtBulletsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                        SelectedInPanel = hit.collider.gameObject.name;
                                        GameObject.Find(BoughtBulletsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                                    }

                                }


                            }

                            if (PanelActivated == 3)
                            {

                            }
                        }
                    }
                }
                
            }

            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit)
                {
                    if (PanelActivated == 1) {

                        if (hit.collider.gameObject.layer == 8) {
                            Health.text = hit.transform.gameObject.GetComponent<PersProperties>().Health.ToString();
                            Damage.text = hit.transform.gameObject.GetComponent<PersProperties>().Damage.ToString();
                            Accuracy.text = hit.transform.gameObject.GetComponent<PersProperties>().Accuracy.ToString();
                            Level.text = hit.transform.gameObject.GetComponent<PersProperties>().Level.ToString();
                            Price.text = hit.transform.gameObject.GetComponent<PersProperties>().Price.ToString();
                            hit.transform.gameObject.GetComponent<AudioSource>().Play();
                            if (hit.transform.gameObject.GetComponent<PersProperties>().Bought == false)
                            {
                                GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                                GameObject.Find(PersPanel.name + "/" + hit.transform.gameObject.name + "/Lighter").active = true;
                                SelectedItem = hit.transform.gameObject.name;
                                Buy.interactable = true;
                            }
                            GameObject.Find(BoughtPersonsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                            SelectedInPanel = "DummySwitcherBought";
                        }

                        if (hit.collider.gameObject.layer == 17) {
                            Health.text = hit.transform.gameObject.GetComponent<PersProperties>().Health.ToString();
                            Damage.text = hit.transform.gameObject.GetComponent<PersProperties>().Damage.ToString();
                            Accuracy.text = hit.transform.gameObject.GetComponent<PersProperties>().Accuracy.ToString();
                            Level.text = hit.transform.gameObject.GetComponent<PersProperties>().Level.ToString();
                            Price.text = hit.transform.gameObject.GetComponent<PersProperties>().Price.ToString();
                            BoughtPersonsPanel.GetComponent<AudioSource>().Play();
                            GameObject.Find(BoughtPersonsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                            SelectedInPanel = hit.collider.gameObject.name;
                            GameObject.Find(BoughtPersonsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                            if (GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active != null) {
                                GameObject.Find(PersPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                                Buy.interactable = false;
                            }
                        }

                    }

                    if (PanelActivated == 2) {

                        if (hit.collider.gameObject.layer == 9) {
                            WeaponProperties.GetComponent<AudioSource>().Play();
                            string Name = hit.transform.gameObject.GetComponent<WeaponProperties>().Name.ToString();
                            string Damage = hit.transform.gameObject.GetComponent<WeaponProperties>().Damage.ToString();
                            int Condition = hit.transform.gameObject.GetComponent<WeaponProperties>().Condition;
                            int CountOfBullets = hit.transform.gameObject.GetComponent<WeaponProperties>().CountOfBullets;
                            string Price = hit.transform.gameObject.GetComponent<WeaponProperties>().Price.ToString();
                            string Con = "";
                            string COB = "";
                            for (int a = 1; a < Condition + 1; a++) {
                                Con = Con + "|";
                            }
                            for (int b = 1; b < CountOfBullets + 1; b++) {
                                COB = COB + "|";
                            }

                            WeaponOfOtherProperties.text = Name + "\n\nDamage: " + Damage + "\nCondition: " + Con + Condition.ToString() + "\nClip: " + COB + CountOfBullets.ToString() + "\nPrice: " + Price + "\nYour Money: " + MoneyCount.ToString();
                            hit.transform.gameObject.GetComponent<AudioSource>().Play();

                            GameObject.Find(WeaponPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                            GameObject.Find(WeaponPanel.name + "/" + hit.transform.gameObject.name + "/Lighter").active = true;
                            Buy.interactable = true;
                            SelectedItem = hit.transform.gameObject.name;
                            GameObject.Find(WpnBoughtPanel.name + "/" + SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
                            SelectedInPanel = "DummySwitcherBought";
                        }

                        if (hit.collider.gameObject.layer == 11) {

                            WeaponProperties.GetComponent<AudioSource>().Play();
                            string Name = hit.transform.gameObject.GetComponent<Bullets>().Name.ToString();
                            string CountOfBullets = hit.transform.gameObject.GetComponent<Bullets>().CountOfBullets.ToString();
                            int Price = hit.transform.gameObject.GetComponent<Bullets>().Price;
                            int Clip = hit.transform.gameObject.GetComponent<Bullets>().ClipOfWeapon;
                            CountSelectedBullets = 1;
                            int InClip = CountSelectedBullets * Clip;

                            WeaponOfOtherProperties.text = "How much would You buy?:" + "\nfor " + Name + "\n" + InClip.ToString() + "\nPrice: " + (InClip * Price).ToString() + "\nCount in store: " + CountOfBullets + "\nYour Money: " + MoneyCount.ToString();

                            hit.transform.gameObject.GetComponent<AudioSource>().Play();

                            if (InClip <= hit.transform.gameObject.GetComponent<Bullets>().CountOfBullets)
                            {
                                if (InClip + hit.transform.gameObject.GetComponent<Bullets>().ClipOfWeapon <= hit.transform.gameObject.GetComponent<Bullets>().CountOfBullets)
                                {
                                    UpCountBullets.interactable = true;
                                }
                                else {
                                    UpCountBullets.interactable = false;
                                }
                                Buy.interactable = true;
                            }
                            else {
                                UpCountBullets.interactable = false;
                                Buy.interactable = false;
                            }

                            GameObject.Find(BulletsPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                            GameObject.Find(BulletsPanel.name + "/" + hit.transform.gameObject.name + "/Lighter").active = true;
                            SelectedItem = hit.transform.gameObject.name;

                            GameObject.Find(BoughtBulletsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                            SelectedInPanel = "DummySwitcherBought";
                        }

                        if (hit.collider.gameObject.layer == 19) {

                            WeaponProperties.GetComponent<AudioSource>().Play();
                            hit.transform.gameObject.GetComponent<AudioSource>().Play();

                            string Name = hit.transform.gameObject.GetComponent<OtherStuff>().Name.ToString();
                            int Skin = hit.transform.gameObject.GetComponent<OtherStuff>().Skin;
                            int Price = hit.transform.gameObject.GetComponent<OtherStuff>().Price;

                            Buy.interactable = true;

                            string Description = "";

                            if (Skin == 1) {
                                Description = "Heals your slave";
                            }
                            if (Skin == 2) {
                                Description = "Need for move";
                            }
                            if (Skin == 3) {
                                Description = "Increases damage";
                            }

                            WeaponOfOtherProperties.text = Name + "\n" + Description + "\nPrice: " + Price.ToString();

                            GameObject.Find(OtherPanel.name + "/" + SelectedItem + "/Lighter").active = false;
                            GameObject.Find(OtherPanel.name + "/" + hit.transform.gameObject.name + "/Lighter").active = true;
                            SelectedItem = hit.transform.gameObject.name;

                            GameObject.Find(BoughtOtherPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                            SelectedInPanel = "DummySwitcherBought";
                        }

                        if (hit.collider.gameObject.layer == 17) {
                            WeaponProperties.GetComponent<AudioSource>().Play();
                            if (hit.collider.gameObject.GetComponent<WeaponProperties>() != null) {
                                string Name = hit.transform.gameObject.GetComponent<WeaponProperties>().Name.ToString();
                                string Damage = hit.transform.gameObject.GetComponent<WeaponProperties>().Damage.ToString();
                                int Condition = hit.transform.gameObject.GetComponent<WeaponProperties>().Condition;
                                int CountOfBullets = hit.transform.gameObject.GetComponent<WeaponProperties>().CountOfBullets;
                                string Price = hit.transform.gameObject.GetComponent<WeaponProperties>().Price.ToString();
                                string Con = "";
                                string COB = "";
                                for (int a = 1; a < Condition + 1; a++)
                                {
                                    Con = Con + "|";
                                }
                                for (int b = 1; b < CountOfBullets + 1; b++)
                                {
                                    COB = COB + "|";
                                }
                                WeaponOfOtherProperties.text = Name + "\n\nDamage: " + Damage + "\nCondition: " + Con + Condition.ToString() + "\nClip: " + COB + CountOfBullets.ToString() + "\nPrice: " + Price;
                                GameObject.Find(WpnBoughtPanel.name + "/" + SelectedInPanel).GetComponent<WeaponProperties>().IsActive = false;
                                SelectedInPanel = hit.collider.gameObject.name;
                                GameObject.Find(WpnBoughtPanel.name + "/" + SelectedInPanel).GetComponent<WeaponProperties>().IsActive = true;
                            }
                            if (hit.collider.gameObject.GetComponent<Bullets>() != null) {
                                string Name = hit.transform.gameObject.GetComponent<Bullets>().Name.ToString();
                                string CountOfBullets = hit.transform.gameObject.GetComponent<Bullets>().CountOfBullets.ToString();

                                WeaponOfOtherProperties.text = "Bullets for " + Name + "\n\nIn inventory: " + CountOfBullets.ToString();
                                GameObject.Find(BoughtBulletsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                SelectedInPanel = hit.collider.gameObject.name;
                                GameObject.Find(BoughtBulletsPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                            }
                            if (hit.collider.gameObject.GetComponent<OtherStuff>() != null) {
                                string Name = hit.transform.gameObject.GetComponent<OtherStuff>().Name.ToString();
                                int Skin = hit.transform.gameObject.GetComponent<OtherStuff>().Skin;

                                string Description = "";

                                if (Skin == 1)
                                {
                                    Description = "Heals your slave";
                                }
                                if (Skin == 2)
                                {
                                    Description = "Need for move";
                                }
                                if (Skin == 3)
                                {
                                    Description = "Increases damage";
                                }

                                WeaponOfOtherProperties.text = Name + "\n" + Description;
                                GameObject.Find(BoughtOtherPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                SelectedInPanel = hit.collider.gameObject.name;
                                GameObject.Find(BoughtOtherPanel.name + "/" + SelectedInPanel).GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                            }

                        }


                    }

                    if (PanelActivated == 3) {
                        
                    }
                }
            }
        }
    }

    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| BUY SOME STUFF |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

    void BuyIt() {
        if (MenuPanel.active == false) {

            Buy.GetComponent<AudioSource>().Play();
            GameObject SelectedObject = GameObject.Find(SelectedItem.ToString());

            if (PanelActivated == 1)
            {
                if (CountOfBoughtPers < 6)
                {
                    GameObject Bought = GameObject.Find(SelectedItem);
                    int NewMoney;

                    NewMoney = MoneyCount - Bought.GetComponent<PersProperties>().Price;
                    MoneyCount = NewMoney;
                    Money.text = NewMoney.ToString();
                    CountOfBoughtPers = CountOfBoughtPers + 1;
                    GameObject YourPers = Instantiate(Bought);
                    Destroy(GameObject.Find(YourPers.name + "/Lighter"));
                    YourPers.GetComponent<PersProperties>().Bought = true;
                    YourPers.transform.SetParent(BoughtPersonsPanel.transform);
                    YourPers.transform.localPosition = GameObject.Find(BoughtPersonsPanel.name + "PosPers" + CountOfBoughtPers.ToString()).transform.localPosition;
                    Destroy(GameObject.Find(BoughtPersonsPanel.name + "PosPers" + CountOfBoughtPers.ToString()));
                    YourPers.name = "BoughtPers" + CountOfBoughtPers.ToString();
                    YourPers.layer = 17;
                    string[] StarterHealth = File.ReadAllLines(StartHealthOfPersPath);
                    StarterHealth[CountOfBoughtPers - 1] = YourPers.GetComponent<PersProperties>().Health.ToString();
                    File.WriteAllLines(StartHealthOfPersPath, StarterHealth);

                    YourPers.GetComponent<PersProperties>().PositionOnField = 1;

                    Destroy(Bought);
                    SelectedItem = "DummySwitcher";
                    Buy.interactable = false;
                }
                else {
                    GameObject.Find(SelectedItem + "/Lighter").active = false;
                    Buy.interactable = false;
                    Debug.Log("Storage is full");
                }
            }
            if (PanelActivated == 2)
            {
                GameObject Bought = GameObject.Find(SelectedItem);

                if (Bought.gameObject.layer == 9) {

                    if (CountOfBoughtWeapon < 9)
                    {
                        int NewMoney;

                        NewMoney = MoneyCount - Bought.GetComponent<WeaponProperties>().Price;
                        MoneyCount = NewMoney;
                        Money.text = NewMoney.ToString();
                        CountOfBoughtWeapon = CountOfBoughtWeapon + 1;
                        GameObject YourWeapon = Instantiate(Bought);
                        Destroy(GameObject.Find(YourWeapon.name + "/Lighter"));
                        YourWeapon.GetComponent<WeaponProperties>().Bought = true;
                        YourWeapon.transform.SetParent(WpnBoughtPanel.transform);
                        YourWeapon.transform.localPosition = GameObject.Find(WpnBoughtPanel.name + "/PosWpn" + CountOfBoughtWeapon.ToString()).transform.localPosition;
                        Destroy(GameObject.Find(WpnBoughtPanel.name + "/PosWpn" + CountOfBoughtWeapon.ToString()));
                        YourWeapon.name = "BoughtWeapon" + CountOfBoughtWeapon.ToString();
                        YourWeapon.layer = 17;
                        Destroy(Bought);
                        Debug.Log(SelectedItem);
                    }
                    else
                    {
                        GameObject.Find(SelectedItem + "/Lighter").active = false;
                        Buy.interactable = false;
                        Debug.Log("Storage is full");
                        WeaponOfOtherProperties.text = "Storage is full!";
                    }

                    SelectedItem = "DummySwitcher";
                    Buy.interactable = false;
                }
                if (Bought.gameObject.layer == 11) {

                    if (CountOfBoughtBullets < 9) {

                        UpCountBullets.interactable = false;
                        DownCountBullets.interactable = false;


                        for (int bb = 1; bb < 10; bb++) {
                            if (GameObject.Find(BoughtBulletsPanel.name + "/BoughtBullets" + bb.ToString()) != null) {
                                if (Bought.GetComponent<Bullets>().BulletsSkin == GameObject.Find(BoughtBulletsPanel.name + "/BoughtBullets" + bb.ToString()).GetComponent<Bullets>().BulletsSkin)
                                {

                                    MoneyCount = MoneyCount - Bought.GetComponent<Bullets>().Price * CountSelectedBullets;

                                    GameObject NowYouHave = GameObject.Find(BoughtBulletsPanel.name + "/BoughtBullets" + bb.ToString());
                                    int CSB = CountSelectedBullets * Bought.GetComponent<Bullets>().ClipOfWeapon;
                                    NowYouHave.GetComponent<Bullets>().CountOfBullets = NowYouHave.GetComponent<Bullets>().CountOfBullets + CSB;
                                    Bought.GetComponent<Bullets>().CountOfBullets = Bought.GetComponent<Bullets>().CountOfBullets - CountSelectedBullets * Bought.GetComponent<Bullets>().ClipOfWeapon;
                                    string Name = Bought.GetComponent<Bullets>().Name;
                                    int InClip = Bought.GetComponent<Bullets>().ClipOfWeapon;
                                    int Price = Bought.GetComponent<Bullets>().Price;
                                    int COB = Bought.GetComponent<Bullets>().CountOfBullets;
                                    WeaponOfOtherProperties.text = "How much would You buy?:" + "\nfor " + Name + "\n" + InClip.ToString() + "\nPrice: " + (InClip * Price).ToString() + "\nCount in store: " + COB + "\nYour Money: " + MoneyCount.ToString();
                                    GameObject.Find(SelectedItem + "/Lighter").active = false;

                                    SelectedItem = "DummySwitcher";

                                    break;
                                }
                            }
                            else {

                                int NewMoney;
                                NewMoney = MoneyCount - Bought.GetComponent<Bullets>().Price * CountSelectedBullets;
                                CountOfBoughtBullets = CountOfBoughtBullets + 1;
                                int CSB = CountSelectedBullets * Bought.GetComponent<Bullets>().ClipOfWeapon;

                                Bought.GetComponent<Bullets>().CountOfBullets = Bought.GetComponent<Bullets>().CountOfBullets - CSB;

                                GameObject YourBullets = Instantiate(Bought);

                                Destroy(GameObject.Find(YourBullets.name + "/Lighter"));
                                YourBullets.GetComponent<Bullets>().Bought = true;

                                YourBullets.GetComponent<Bullets>().CountOfBullets = CSB;
                                YourBullets.layer = 17;

                                YourBullets.transform.SetParent(BoughtBulletsPanel.transform);
                                YourBullets.transform.localPosition = GameObject.Find(BoughtBulletsPanel.name + "PosBullets" + CountOfBoughtBullets.ToString()).transform.localPosition;

                                Destroy(GameObject.Find(BoughtBulletsPanel.name + "PosBullets" + CountOfBoughtBullets.ToString()));

                                YourBullets.name = "BoughtBullets" + CountOfBoughtBullets.ToString();

                                string Name = Bought.GetComponent<Bullets>().Name;
                                int InClip = Bought.GetComponent<Bullets>().ClipOfWeapon;
                                int Price = Bought.GetComponent<Bullets>().Price;
                                int COB = Bought.GetComponent<Bullets>().CountOfBullets;
                                WeaponOfOtherProperties.text = "How much would You buy?:" + "\nfor " + Name + "\n" + InClip.ToString() + "\nPrice: " + (InClip * Price).ToString() + "\nCount in store: " + COB + "\nYour Money: " + MoneyCount.ToString();
                                GameObject.Find(SelectedItem + "/Lighter").active = false;

                                SelectedItem = "DummySwitcher";

                                break;
                                
                            }
                        }

                    }
                    else
                    {
                        GameObject.Find(SelectedItem + "/Lighter").active = false;
                        Buy.interactable = false;
                        Debug.Log("Storage is full");
                    }

                    SelectedItem = "DummySwitcher";
                    Buy.interactable = false;
                }
                if (Bought.gameObject.layer == 19) {
                    if (CountOfBoughtStuff < 9)
                    {
                        int NewMoney;
                        NewMoney = MoneyCount - Bought.GetComponent<OtherStuff>().Price;
                        MoneyCount = NewMoney;
                        Money.text = NewMoney.ToString();
                        CountOfBoughtStuff = CountOfBoughtStuff + 1;
                        Destroy(GameObject.Find(Bought.name + "/Lighter"));
                        Bought.GetComponent<OtherStuff>().Bought = true;
                        Bought.transform.SetParent(BoughtOtherPanel.transform);
                        Bought.transform.localPosition = GameObject.Find(BoughtOtherPanel.name + "/PosOther" + CountOfBoughtStuff.ToString()).transform.localPosition;
                        Destroy(GameObject.Find(BoughtOtherPanel.name + "/PosOther" + CountOfBoughtStuff.ToString()));
                        Bought.name = "BoughtStuff" + CountOfBoughtStuff.ToString();
                        Bought.layer = 17;
                    }
                    else {
                        GameObject.Find(SelectedItem + "/Lighter").active = false;
                        Debug.Log("Storage is full");
                        WeaponOfOtherProperties.text = "Storage is full!";
                    }

                    SelectedItem = "DummySwitcher";
                    Buy.interactable = false;
                }
            }
        }
    }
    
    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| GENERATE INVENTORY |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    void GenerateYourInventory() {

        //************************************************************************ CLEAR OF OLD STUFF **********************************************************************************

        //int OldCountOfBoughtPers = int.Parse(File.ReadAllLines(CountOfAllPath)[0]);
        //int OldCountOfBoughtWeapon = int.Parse(File.ReadAllLines(CountOfAllPath)[1]);
        //int OldCountOfBoughtBullets = int.Parse(File.ReadAllLines(CountOfAllPath)[2]);
        //int OldCountOfBoughtOther = int.Parse(File.ReadAllLines(CountOfAllPath)[3]);

        //for (int p = 1; p < OldCountOfBoughtPers + 1; p++) {

        //    Destroy(GameObject.Find(InventoryPersons.name + "/Pers" + p.ToString()));

        //}
        //for (int w = 1; w < OldCountOfBoughtWeapon + 1; w++) {

        //    Destroy(GameObject.Find(InventoryWeapon.name + "/Weapon" + w.ToString()));

        //}

        //InventoryStuff.active = true;
        //for (int o = 1; o < OldCountOfBoughtOther + 1; o++) {

        //    Destroy(GameObject.Find(InventoryStuff.name + "/Stuff" + o.ToString()));

        //}
        //InventoryStuff.active = false;

        //PersPackCage.active = true;
        //for (int iw = 1; iw < 10; iw++) {

        //    if (GameObject.Find(PersPackCage.name + "/Weapon" + iw.ToString()) != null) {
        //        Destroy(GameObject.Find(PersPackCage.name + "/Weapon" + iw.ToString()));
        //    }
        //    if (GameObject.Find(PersPackCage.name + "/Stuff" + iw.ToString()) != null) {
        //        Destroy(GameObject.Find(PersPackCage.name + "/Stuff" + iw.ToString()));
        //    }

        //}
        //PersPackCage.active = false;

        //************************************************************************ WRITE NEW INFO ABOUT BOUGHT **********************************************************************************

        string[] CountAll = File.ReadAllLines(CountOfAllPath);

        string[] GetPlayerData = new string[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + CountOfBoughtBullets * 3 + CountOfBoughtStuff * 2];

        GetPlayerData[0] = "=== Your Money ===";
        GetPlayerData[1] = MoneyCount.ToString();

        BoughtPersonsPanel.active = true;
        ArmorPanel.active = true;
        WpnBoughtPanel.active = true;
        BoughtBulletsPanel.active = true;
        BoughtOtherPanel.active = true;

        for (int p = 1; p < CountOfBoughtPers + 1; p++) {
            
            GameObject BoughtPerson = GameObject.Find(BoughtPersonsPanel.name + "/BoughtPers" + p.ToString());
            GetPlayerData[2 + p * NumPersParam - NumPersParam] = "=== Person " + p.ToString() + " ===";
            GetPlayerData[2 + p * NumPersParam - NumPersParam + 1] = BoughtPerson.GetComponent<PersProperties>().Health.ToString();
            GetPlayerData[2 + p * NumPersParam - NumPersParam + 2] = BoughtPerson.GetComponent<PersProperties>().Damage.ToString();
            GetPlayerData[2 + p * NumPersParam - NumPersParam + 3] = BoughtPerson.GetComponent<PersProperties>().Accuracy.ToString();
            GetPlayerData[2 + p * NumPersParam - NumPersParam + 4] = BoughtPerson.GetComponent<PersProperties>().Level.ToString();
            GetPlayerData[2 + p * NumPersParam - NumPersParam + 5] = BoughtPerson.GetComponent<PersProperties>().CountOfBattles.ToString();
            GetPlayerData[2 + p * NumPersParam - NumPersParam + 6] = BoughtPerson.GetComponent<PersProperties>().Price.ToString();
            GetPlayerData[2 + p * NumPersParam - NumPersParam + 7] = BoughtPerson.GetComponent<PersProperties>().Skin.ToString();

        }

        for (int w = 1; w < CountOfBoughtWeapon + 1; w++)
        {
            GameObject BoughtWeapon = GameObject.Find(WpnBoughtPanel.name + "/BoughtWeapon" + w.ToString());
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + w * NumWpnParam - NumWpnParam] = "=== Weapon " + w.ToString() + " ===";
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + w * NumWpnParam - NumWpnParam + 1] = BoughtWeapon.GetComponent<WeaponProperties>().Damage.ToString();
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + w * NumWpnParam - NumWpnParam + 2] = BoughtWeapon.GetComponent<WeaponProperties>().Condition.ToString();
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + w * NumWpnParam - NumWpnParam + 3] = BoughtWeapon.GetComponent<WeaponProperties>().CountOfBullets.ToString();
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + w * NumWpnParam - NumWpnParam + 4] = BoughtWeapon.GetComponent<WeaponProperties>().Price.ToString();
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + w * NumWpnParam - NumWpnParam + 5] = BoughtWeapon.GetComponent<WeaponProperties>().Skin.ToString();
        }

        for (int b = 1; b < CountOfBoughtBullets + 1; b++)
        {
            GameObject BoughtBullets = GameObject.Find(BoughtBulletsPanel.name + "/BoughtBullets" + b.ToString());
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + b * 3 - 3] = "=== Bullets " + b.ToString() + " ===";
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + b * 3 - 2] = BoughtBullets.GetComponent<Bullets>().BulletsSkin.ToString();
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + b * 3 - 1] = BoughtBullets.GetComponent<Bullets>().CountOfBullets.ToString();

        }

        for (int o = 1; o < CountOfBoughtStuff + 1; o++)
        {
            GameObject BoughtStuff = GameObject.Find(BoughtOtherPanel.name + "/BoughtStuff" + o.ToString());
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + CountOfBoughtBullets * 3 + o * 2 - 2] = "=== Stuff " + o.ToString() + " ===";
            GetPlayerData[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + CountOfBoughtBullets * 3 + o * 2 - 1] = BoughtStuff.GetComponent<OtherStuff>().Skin.ToString();
        }

        BoughtPersonsPanel.active = false;
        ArmorPanel.active = false;
        WpnBoughtPanel.active = false;
        BoughtBulletsPanel.active = false;
        BoughtOtherPanel.active = false;

        File.WriteAllLines(PlayerSourcePath, GetPlayerData);

        CountAll[0] = CountOfBoughtPers.ToString();
        CountAll[1] = CountOfBoughtWeapon.ToString();
        CountAll[2] = CountOfBoughtBullets.ToString();
        CountAll[3] = CountOfBoughtStuff.ToString();
        File.WriteAllLines(CountOfAllPath, CountAll);

        //************************************************************************ GET NUMBERS OF FIELDS **********************************************************************************

        InventoryPersons.active = true;
        InventoryWeapon.active = true;
        InventoryStuff.active = true;
        for (int a = 1; a < 10; a++) {
            int NumberOfField = int.Parse(File.ReadAllLines(InvSetPath)[a]);
            GameObject.Find(InventoryPersons.name + "/PosOnField" + a.ToString()).GetComponent<WarriorSettings>().NumberOfPos = NumberOfField;
            GameObject.Find(InventoryWeapon.name + "/InvWpn" + a.ToString()).GetComponent<WarriorSettings>().NumberOfPos = NumberOfField;
            GameObject.Find(InventoryStuff.name + "/InvStuff" + a.ToString()).GetComponent<WarriorSettings>().NumberOfPos = NumberOfField;
        }
        InventoryPersons.active = false;
        InventoryWeapon.active = false;
        InventoryStuff.active = false;

        //************************************************************************ SET NEW STUFF INTO POSITION **********************************************************************************

        //string[] InvSet = File.ReadAllLines(InvSetPath);

        //for (int b = 1; b < CountOfBoughtPers + 1; b++) {

        //    GameObject YourBoughtPers = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
        //    YourBoughtPers.name = "Pers" + b.ToString();
        //    YourBoughtPers.transform.SetParent(InventoryPersons.transform);

        //    YourBoughtPers.GetComponent<PersProperties>().Health = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 1]);
        //    YourBoughtPers.GetComponent<PersProperties>().Damage = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 2]);
        //    YourBoughtPers.GetComponent<PersProperties>().Accuracy = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 3]);
        //    YourBoughtPers.GetComponent<PersProperties>().Level = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 4]);
        //    YourBoughtPers.GetComponent<PersProperties>().CountOfBattles = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 5]);
        //    YourBoughtPers.GetComponent<PersProperties>().Price = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 6]);
        //    YourBoughtPers.GetComponent<PersProperties>().Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 7]);
        //    YourBoughtPers.GetComponent<PersProperties>().NumberOfPersInInventory = b;
        //    YourBoughtPers.GetComponent<PersProperties>().ShowHealthBar = true;

        //    GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
        //    L.name = "Lighter";
        //    L.transform.SetParent(YourBoughtPers.transform);
        //    L.transform.localPosition = new Vector3(0, 0, -0.1f);

        //    for (int i = 0; i < 4; i++) {
        //        YourBoughtPers.GetComponent<PersProperties>().Package[i] = "None";
        //    }


        //    if (InvSet[10 + b] == "NaN")
        //    {
        //        for (int p = 1; p < 10; p++) {
        //            GameObject NewPosItem = GameObject.Find(InventoryPersons.name + "/PosOnField" + p.ToString());
        //            if (NewPosItem.GetComponent<WarriorSettings>().Full == false)
        //            {
        //                InvSet[10 + b] = p.ToString();
        //                File.WriteAllLines(InvSetPath, InvSet);
        //                YourBoughtPers.transform.localPosition = NewPosItem.transform.localPosition + new Vector3(0, 0, -0.1f);
        //                YourBoughtPers.GetComponent<PersProperties>().PositionOnField = p;
        //                NewPosItem.GetComponent<WarriorSettings>().Full = true;

        //                break;
        //            }
        //        }
        //    }
        //    else {

        //        GameObject NewPosItem = GameObject.Find(InventoryPersons.name + "/PosOnField" + InvSet[10 + b].ToString());
        //        YourBoughtPers.transform.localPosition = NewPosItem.transform.localPosition + new Vector3(0, 0, -0.1f);
        //        NewPosItem.GetComponent<WarriorSettings>().Full = true;
        //        YourBoughtPers.GetComponent<PersProperties>().PositionOnField = int.Parse(InvSet[10 + b]);
        //    }

        //    int CountStuff = 0;

        //    if (InvSet[21 + b] != "NaN") {
        //        int NumWeap = int.Parse(InvSet[21 + b]);
        //        int Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + NumWeap * NumWpnParam - 1]);
        //        YourBoughtPers.GetComponent<PersProperties>().WeaponInHands = NumWeap;
        //        YourBoughtPers.GetComponent<PersProperties>().WeaponSkin = Skin;
        //        YourBoughtPers.GetComponent<PersProperties>().Package[0] = "Weapon" + NumWeap.ToString();
        //        CountStuff = CountStuff + 1;
        //    }

        //}

        //int PositionOfWeaponInInv = 0;

        //for (int m = 1; m < CountOfBoughtWeapon + 1; m++)
        //{
        //    //string[] InvSet = File.ReadAllLines(InvSetPath);
        //    GameObject YourBoughtWeapon = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
        //    PersPackCage.active = true;
        //    if (InvSet[37 + m] == "false")
        //    {
        //        YourBoughtWeapon.name = "Weapon" + m.ToString();
        //        PositionOfWeaponInInv = PositionOfWeaponInInv + 1;
        //        YourBoughtWeapon.transform.SetParent(InventoryWeapon.transform);
        //        YourBoughtWeapon.transform.localPosition = GameObject.Find(InventoryWeapon.name + "/InvWpn" + PositionOfWeaponInInv.ToString()).transform.localPosition + new Vector3(0, 0, -0.1f);
        //        GameObject.Find(InventoryWeapon.name + "/InvWpn" + PositionOfWeaponInInv.ToString()).GetComponent<WarriorSettings>().Full = true;
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().Damage = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 1]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().Condition = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 2]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().CountOfBullets = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 3]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().Price = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 4]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 5]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().NumberOfWeaponInInventory = m;
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().PositionOnField = PositionOfWeaponInInv;
        //        GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
        //        L.name = "Lighter";
        //        L.transform.SetParent(YourBoughtWeapon.transform);
        //        L.transform.localPosition = new Vector3(0, 0, -0.1f);
        //    }
        //    else {
        //        YourBoughtWeapon.name = "Weapon" + m.ToString();
        //        YourBoughtWeapon.transform.SetParent(PersPackCage.transform);
        //        YourBoughtWeapon.transform.localPosition = GameObject.Find("PersPack1").transform.localPosition;
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().Damage = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 1]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().Condition = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 2]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().CountOfBullets = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 3]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().Price = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 4]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CountOfBoughtPers * NumPersParam + m * NumWpnParam - NumWpnParam + 5]);
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().NumberOfWeaponInInventory = m;
        //        YourBoughtWeapon.GetComponent<WeaponProperties>().Bought = true;
        //        YourBoughtWeapon.GetComponent<SpriteRenderer>().enabled = false;
        //        YourBoughtWeapon.GetComponent<Collider2D>().enabled = false;
        //        YourBoughtWeapon.layer = 18;
        //    }
        //    PersPackCage.active = false;

        //}

        //PositionOfWeaponInInv = 0;

        //string[] PlaySrc = File.ReadAllLines(PlayerSourcePath);

        //for (int s = 1; s < CountOfBoughtStuff + 1; s++) {
        //    //string[] InvSet = File.ReadAllLines(InvSetPath);
        //    GameObject YourBoughtStuff = Instantiate(Resources.Load("OtherStuffPrefab")) as GameObject;
        //    YourBoughtStuff.name = "Stuff" + s.ToString();

        //    if (InvSet[48 + s] == "undefined")
        //    {
        //        PositionOfWeaponInInv = PositionOfWeaponInInv + 1;
        //        InventoryStuff.active = true;
        //        YourBoughtStuff.transform.SetParent(InventoryStuff.transform);
        //        YourBoughtStuff.transform.localPosition = GameObject.Find(InventoryStuff.name + "/InvStuff" + PositionOfWeaponInInv.ToString()).transform.localPosition + new Vector3(0, 0, -0.1f);
        //        GameObject.Find(InventoryStuff.name + "/InvStuff" + PositionOfWeaponInInv.ToString()).GetComponent<WarriorSettings>().Full = true;
        //        YourBoughtStuff.GetComponent<OtherStuff>().Skin = int.Parse(PlaySrc[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + CountOfBoughtBullets * 3 + s * 2 - 1]);
        //        YourBoughtStuff.GetComponent<OtherStuff>().NumberOfStuffInInv = s;
        //        YourBoughtStuff.GetComponent<OtherStuff>().PositionOnField = PositionOfWeaponInInv;
        //        GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
        //        L.name = "Lighter";
        //        L.transform.SetParent(YourBoughtStuff.transform);
        //        L.transform.localPosition = new Vector3(0, 0, -0.1f);
        //        InventoryStuff.active = false;
        //    }
        //    else{
        //        int NumberOfPers = int.Parse(InvSet[48 + s]);
        //        //YourBoughtStuff.GetComponent<OtherStuff>().WhichPersUseIt = NumberOfPers;
        //        YourBoughtStuff.GetComponent<OtherStuff>().NumberOfStuffInInv = s;
        //        PersPackCage.active = true;
        //        YourBoughtStuff.transform.SetParent(PersPackCage.transform);

        //        GameObject Pers = GameObject.Find(InventoryPersons.name + "/Pers" + NumberOfPers);

        //        for (int i = 0; i < 4; i++) {
        //            if (Pers.GetComponent<PersProperties>().Package[i] == "None") {
        //                Pers.GetComponent<PersProperties>().Package[i] = YourBoughtStuff.name;
        //                YourBoughtStuff.transform.localPosition = GameObject.Find(PersPackCage.name + "/PersPack" + (i + 1).ToString()).transform.localPosition;
        //                break;
        //            }
        //        }

        //        YourBoughtStuff.GetComponent<OtherStuff>().Skin = int.Parse(PlaySrc[2 + CountOfBoughtPers * NumPersParam + CountOfBoughtWeapon * NumWpnParam + CountOfBoughtBullets * 3 + s * 2 - 1]);
        //        YourBoughtStuff.GetComponent<OtherStuff>().Bought = true;
        //        YourBoughtStuff.layer = 18;
        //        YourBoughtStuff.GetComponent<SpriteRenderer>().enabled = false;
        //        YourBoughtStuff.GetComponent<Collider2D>().enabled = false;
        //        PersPackCage.active = false;
        //    }
        //}

    }
}
