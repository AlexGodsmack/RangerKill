using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryWorking : MonoBehaviour
{

    public GameObject InvPers;
    public GameObject InvWeap;
    public GameObject InvStuff;
    public GameObject PersPackage;
    public GameObject InvProperties;
    public GameObject InvCanvasPanel;

    public Button RepairWeapon;
    public Button Menu;
    public Button Slaves;
    public Button Weapon;
    public Button GoToMap;

    public Text InvINFO;

    public Toggle StuffToWeapon;

    public GameObject PrevSelected;
    public GameObject NextSelected;

    public GameObject AdditionalObject;

    public int RepairPrice;
    public int Columns;
    public int Rows;

    private string[] PlayerSrc;
    private string[] InvSet;
    private string[] CountOfAll;
    private string[] MapGen;

    private string PlayerSourcePath;
    private string InvSetPath;
    private string CountOfAllPath;
    private string MapGenPath;

    private int CoBPerses;
    private int CoBWeapon;
    private int CoBBullets;
    private int CoBStuff;

    private int NumPersParam;
    private int NumWpnParam;
    private int Money;


    void OnEnable ()
    {

        PlayerSourcePath = Application.persistentDataPath + "/PlayerSource.txt";
        PlayerSrc = File.ReadAllLines(PlayerSourcePath);

        InvSetPath = Application.persistentDataPath + "/InventorySettings.txt";
        InvSet = File.ReadAllLines(InvSetPath);

        CountOfAllPath = Application.persistentDataPath + "/CountOfAll.txt";
        CountOfAll = File.ReadAllLines(CountOfAllPath);

        MapGenPath = Application.persistentDataPath + "/MapGen.txt";
        MapGen = File.ReadAllLines(MapGenPath);

        CoBPerses = int.Parse(CountOfAll[0]);
        CoBWeapon = int.Parse(CountOfAll[1]);
        CoBBullets = int.Parse(CountOfAll[2]);
        CoBStuff = int.Parse(CountOfAll[3]);

        NumPersParam = this.GetComponent<Store>().NumPersParam;
        NumWpnParam = this.GetComponent<Store>().NumWpnParam;

        Money = int.Parse(PlayerSrc[1]);

        RepairWeapon.onClick.AddListener(RepairClick);
        Slaves.onClick.AddListener(WriteNewData);
        Slaves.onClick.AddListener(BackToPurchaces);
        Weapon.onClick.AddListener(WriteNewData);
        Weapon.onClick.AddListener(BackToPurchaces);
        GoToMap.onClick.AddListener(WriteNewData);

        ResetInfoScreen();

        //====================================================================================================================================
        //======================================================== CLEAR INVENTORY ===========================================================
        //====================================================================================================================================

        InvPers.active = true;
        InvWeap.active = true;
        InvStuff.active = true;
        InvCanvasPanel.active = true;
        InvProperties.active = true;
        PersPackage.active = true;

        GoToMap.interactable = true;

        for (int p = 1; p < 10; p++) {
            if (GameObject.Find(InvPers.name + "/Pers" + p.ToString()) != null) {
                Destroy(GameObject.Find(InvPers.name + "/Pers" + p.ToString()));
            }
        }

        for (int w = 1; w < 10; w++) {
            if (GameObject.Find(InvWeap.name + "/Weapon" + w.ToString()) != null) {
                Destroy(GameObject.Find(InvWeap.name + "/Weapon" + w.ToString()));
            }
        }

        for (int s = 1; s < 10; s++) {
            if (GameObject.Find(InvStuff.name + "/Stuff" + s.ToString()) != null) {
                Destroy(GameObject.Find(InvStuff.name + "/Stuff" + s.ToString()));
            }
        }

        int PackCount = PersPackage.transform.childCount;

        for (int Pp = 1; Pp < PackCount; Pp++) {
            if (PersPackage.transform.GetChild(Pp).gameObject.layer == 18) {
                Destroy(PersPackage.transform.GetChild(Pp).gameObject);
            }
        }

        ///====================================================================================================================================
        ///======================================================= GENERATE INVENTORY =========================================================
        ///====================================================================================================================================

        ///********************************************************* Import Perses ************************************************************

        for (int b = 1; b < CoBPerses + 1; b++)
        {

            GameObject YourBoughtPers = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
            YourBoughtPers.name = "Pers" + b.ToString();
            YourBoughtPers.transform.SetParent(InvPers.transform);

            YourBoughtPers.GetComponent<PersProperties>().Health = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 1]);
            YourBoughtPers.GetComponent<PersProperties>().Damage = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 2]);
            YourBoughtPers.GetComponent<PersProperties>().Accuracy = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 3]);
            YourBoughtPers.GetComponent<PersProperties>().Level = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 4]);
            YourBoughtPers.GetComponent<PersProperties>().CountOfBattles = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 5]);
            YourBoughtPers.GetComponent<PersProperties>().Price = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 6]);
            YourBoughtPers.GetComponent<PersProperties>().Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + NumPersParam * b - NumPersParam + 7]);
            YourBoughtPers.GetComponent<PersProperties>().NumberOfPersInInventory = b;
            YourBoughtPers.GetComponent<PersProperties>().ShowHealthBar = true;

            GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(YourBoughtPers.transform);
            L.transform.localPosition = new Vector3(0, 0, -0.1f);

            for (int i = 0; i < 4; i++)
            {
                YourBoughtPers.GetComponent<PersProperties>().Package[i] = "None";
            }


            if (InvSet[10 + b] == "NaN") {
                for (int p = 1; p < 10; p++) {
                    GameObject NewPosItem = GameObject.Find(InvPers.name + "/PosOnField" + p.ToString());
                    if (NewPosItem.GetComponent<WarriorSettings>().Full == false) {
                        InvSet[10 + b] = p.ToString();
                        File.WriteAllLines(InvSetPath, InvSet);
                        YourBoughtPers.transform.localPosition = NewPosItem.transform.localPosition + new Vector3(0, 0, -0.1f);
                        YourBoughtPers.GetComponent<PersProperties>().PositionOnField = p;
                        NewPosItem.GetComponent<WarriorSettings>().Full = true;

                        break;
                    }
                }
            }
            else {

                GameObject NewPosItem = GameObject.Find(InvPers.name + "/PosOnField" + InvSet[10 + b].ToString());
                YourBoughtPers.transform.localPosition = NewPosItem.transform.localPosition + new Vector3(0, 0, -0.1f);
                NewPosItem.GetComponent<WarriorSettings>().Full = true;
                YourBoughtPers.GetComponent<PersProperties>().PositionOnField = int.Parse(InvSet[10 + b]);
            }

            int PlaceInPack = 0;

            if (InvSet[21 + b] != "NaN")
            {
                int NumWeap = int.Parse(InvSet[21 + b]);
                int Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + NumWeap * NumWpnParam - 1]);
                YourBoughtPers.GetComponent<PersProperties>().WeaponInHands = NumWeap;
                YourBoughtPers.GetComponent<PersProperties>().WeaponSkin = Skin;
                YourBoughtPers.GetComponent<PersProperties>().Package[0] = "Weapon" + NumWeap.ToString();
                PlaceInPack = 1;
            }

            for (int s = 1; s < 10; s++) {
                if (InvSet[48 + s] != "undefined") {
                    if (int.Parse(InvSet[48 + s]) == b) {
                        YourBoughtPers.GetComponent<PersProperties>().Package[PlaceInPack] = "Stuff" + s.ToString();
                        PlaceInPack = PlaceInPack + 1;
                    }
                }
            }

        }

        ///********************************************************* Import Weapon ************************************************************

        int PosOfUnusedItem = 0;

        for (int m = 1; m < CoBWeapon + 1; m++)
        {
            GameObject YourBoughtWeapon = Instantiate(Resources.Load("WeaponDoll")) as GameObject;

            if (InvSet[37 + m] == "false")
            {
                YourBoughtWeapon.name = "Weapon" + m.ToString();
                PosOfUnusedItem = PosOfUnusedItem + 1;
                YourBoughtWeapon.transform.SetParent(InvWeap.transform);
                YourBoughtWeapon.transform.localPosition = GameObject.Find(InvWeap.name + "/InvWpn" + PosOfUnusedItem.ToString()).transform.localPosition + new Vector3(0, 0, -0.1f);
                GameObject.Find(InvWeap.name + "/InvWpn" + PosOfUnusedItem.ToString()).GetComponent<WarriorSettings>().Full = true;
                YourBoughtWeapon.GetComponent<WeaponProperties>().Damage = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 1]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().Condition = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 2]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().Clip = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 3]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().Price = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 4]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 5]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().NumberOfWeaponInInventory = m;
                YourBoughtWeapon.GetComponent<WeaponProperties>().PositionOnField = PosOfUnusedItem;
            }
            else
            {
                YourBoughtWeapon.name = "Weapon" + m.ToString();
                YourBoughtWeapon.transform.SetParent(PersPackage.transform);
                YourBoughtWeapon.GetComponent<WeaponProperties>().Damage = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 1]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().Condition = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 2]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().Clip = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 3]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().Price = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 4]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().Skin = int.Parse(File.ReadAllLines(PlayerSourcePath)[2 + CoBPerses * NumPersParam + m * NumWpnParam - NumWpnParam + 5]);
                YourBoughtWeapon.GetComponent<WeaponProperties>().NumberOfWeaponInInventory = m;
                YourBoughtWeapon.GetComponent<WeaponProperties>().Bought = true;
                YourBoughtWeapon.GetComponent<SpriteRenderer>().enabled = false;
                YourBoughtWeapon.GetComponent<Collider2D>().enabled = false;
                YourBoughtWeapon.layer = 18;
            }

            GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(YourBoughtWeapon.transform);
            L.transform.localPosition = new Vector3(0, 0, -0.1f);

        }

        ///********************************************************* Import Stuff ************************************************************

        PosOfUnusedItem = 0;
        int Container = 0;

        for (int s = 1; s < CoBStuff + 1; s++)
        {
            GameObject YourBoughtStuff = Instantiate(Resources.Load("OtherStuffPrefab")) as GameObject;
            YourBoughtStuff.name = "Stuff" + s.ToString();

            if (InvSet[48 + s] == "undefined")
            {
                PosOfUnusedItem = PosOfUnusedItem + 1;
                YourBoughtStuff.transform.SetParent(InvStuff.transform);
                YourBoughtStuff.transform.localPosition = GameObject.Find(InvStuff.name + "/InvStuff" + PosOfUnusedItem.ToString()).transform.localPosition + new Vector3(0, 0, -0.1f);
                GameObject.Find(InvStuff.name + "/InvStuff" + PosOfUnusedItem.ToString()).GetComponent<WarriorSettings>().Full = true;
                YourBoughtStuff.GetComponent<OtherStuff>().Skin = int.Parse(PlayerSrc[2 + CoBPerses * NumPersParam + CoBWeapon * NumWpnParam + CoBBullets * 3 + s * 2 - 1]);
                YourBoughtStuff.GetComponent<OtherStuff>().NumberOfStuffInInv = s;
                YourBoughtStuff.GetComponent<OtherStuff>().PositionOnField = PosOfUnusedItem;
            }
            else
            {
                int NumberOfPers = int.Parse(InvSet[48 + s]);
                YourBoughtStuff.GetComponent<OtherStuff>().WhichPersUseIt = NumberOfPers;
                YourBoughtStuff.GetComponent<OtherStuff>().NumberOfStuffInInv = s;
                YourBoughtStuff.transform.SetParent(PersPackage.transform);

                YourBoughtStuff.GetComponent<OtherStuff>().Skin = int.Parse(PlayerSrc[2 + CoBPerses * NumPersParam + CoBWeapon * NumWpnParam + CoBBullets * 3 + s * 2 - 1]);
                YourBoughtStuff.GetComponent<OtherStuff>().Bought = true;
                YourBoughtStuff.layer = 18;
                YourBoughtStuff.GetComponent<SpriteRenderer>().enabled = false;
                YourBoughtStuff.GetComponent<Collider2D>().enabled = false;
            }

            if (YourBoughtStuff.GetComponent<OtherStuff>().Skin == 2) {
                YourBoughtStuff.GetComponent<OtherStuff>().WaterLiters = int.Parse(MapGen[2 * Columns * Rows + 12 + 10 + 6 + s]);
            }

            GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(YourBoughtStuff.transform);
            L.transform.localPosition = new Vector3(0, 0, -0.1f);
        }

        InvStuff.active = false;
        PersPackage.active = false;

        StuffToWeapon.isOn = true;

        PrevSelected = GameObject.Find(InvPers.name + "/DummySwitcherBought");
        NextSelected = GameObject.Find(InvPers.name + "/DummySwitcherBought"); 

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit) {

                PrevSelected = NextSelected;
                NextSelected = hit.collider.gameObject;

                if (NextSelected.layer == 8) {

                    if (PrevSelected.layer == 0) {
                        PersInfo();
                        OpenPersesFields();
                        CloseWeaponFields();
                    }
                    if (PrevSelected.layer == 8) {
                        GameObject.Find(InvPers.name + "/" + PrevSelected.name + "/Lighter").active = false;
                        PersInfo();
                        OpenPersesFields();
                        CloseWeaponFields();
                    }
                    if (PrevSelected.layer == 9) {
                        SetWeaponToPers();
                        ActivateAllPerses();
                    }
                    if (PrevSelected.layer == 10) {
                        PersInfo();
                        OpenPersesFields();
                        CloseWeaponFields();
                    }
                    if (PrevSelected.layer == 19) {
                        SetStuffToPers();
                        ActivateAllPerses();
                    }

                }

                if (InvStuff.active == true) {

                    if (NextSelected.layer == 19) {
                        CloseFullPers();
                        if (PrevSelected.layer == 0) {
                            StuffInfo();
                            ClosePersesFields();
                            OpenStuffFields();
                        }
                        if (PrevSelected.layer == 8) {
                            GameObject.Find(InvPers.name + "/" + PrevSelected.name + "/Lighter").active = false;
                            StuffInfo();
                            ClosePersesFields();
                            OpenStuffFields();
                        }
                        if (PrevSelected.layer == 19) {
                            GameObject.Find(InvStuff.name + "/" + PrevSelected.name + "/Lighter").active = false;
                            StuffInfo();
                            ClosePersesFields();
                            OpenStuffFields();
                        }
                        if (PrevSelected.layer == 10) {
                            StuffInfo();
                            ClosePersesFields();
                            OpenStuffFields();
                        }
                    }

                    if (NextSelected.layer == 18) {
                        SelectStuffInPackage();
                        ClosePersesFields();
                        LockAllPerses();
                        LockAllStuff();
                        BlockFieldsWithStuff();
                        StuffToWeapon.interactable = false;
                    }

                    if (NextSelected.layer == 10) {
                        StuffToWeapon.interactable = true;
                        if (PrevSelected.layer == 19) {
                            SetStuffInPlace();
                            CloseStuffFields();
                        }
                        if (PrevSelected.layer == 8) {
                            SetPersInPlace();
                            ClosePersesFields();
                        }
                        if (PrevSelected.layer == 18) {
                            PutStuffBack();
                            UnlockAllStuff();
                            UnlockAllPerses();
                            ClosePersesFields();
                            CloseStuffFields();
                        }
                    }
                }

                if (InvWeap.active == true) {

                    if (NextSelected.layer == 9) {
                        CloseFullPers();
                        CloseArmoredPers();
                        if (PrevSelected.layer == 0) {
                            WeaponInfo();
                            ClosePersesFields();
                            OpenWeaponFields();
                        }
                        if (PrevSelected.layer == 8) {
                            GameObject.Find(InvPers.name + "/" + PrevSelected.name + "/Lighter").active = false;
                            WeaponInfo();
                            ClosePersesFields();
                            OpenWeaponFields();
                        }
                        if (PrevSelected.layer == 9) {
                            GameObject.Find(InvWeap.name + "/" + PrevSelected.name + "/Lighter").active = false;
                            WeaponInfo();
                            ClosePersesFields();
                            OpenWeaponFields();
                        }
                        if (PrevSelected.layer == 10) {
                            WeaponInfo();
                            ClosePersesFields();
                            OpenWeaponFields();
                        }
                    }

                    if (NextSelected.layer == 18) {
                        StuffToWeapon.interactable = false;
                        SelectWeaponInPackage();
                        ClosePersesFields();
                        LockAllPerses();
                        LockAllWeapon();
                        BlockFieldsWithWpn();
                    }

                    if (NextSelected.layer == 10) {
                        StuffToWeapon.interactable = true;
                        if (PrevSelected.layer == 8) {
                            SetPersInPlace();
                            ClosePersesFields();
                        }
                        if (PrevSelected.layer == 9) {
                            SetWeaponInPlace();
                            CloseWeaponFields();
                            ActivateAllPerses();
                        }
                        if (PrevSelected.layer == 18) {
                            PutWeaponBack();
                            UnlockAllWeapon();
                            UnlockAllPerses();
                            ClosePersesFields();
                            CloseWeaponFields();
                        }
                    }
                }
            }

        }
    }
    //======================================================= General Functions =============================================================

    void ResetInfoScreen() {
        InvINFO.text = "1.Select your slave \n2.Tap on cell \nto drag your slave \n3.Select weapon \n4.Tap on slave \nto assign it";
        PersPackage.active = false;
        RepairWeapon.gameObject.active = false;
    }

    //====================================================================================================================================
    //======================================================= Perses Working =============================================================
    //====================================================================================================================================
    void PersInfo() {

        NextSelected.GetComponent<AudioSource>().Play();

        int Health = NextSelected.GetComponent<PersProperties>().Health;
        int Damage = NextSelected.GetComponent<PersProperties>().Damage;
        int Accuracy = NextSelected.GetComponent<PersProperties>().Accuracy;
        int Level = NextSelected.GetComponent<PersProperties>().Level;
        int Battles = NextSelected.GetComponent<PersProperties>().CountOfBattles;

        PersPackage.active = true;
        RepairWeapon.gameObject.active = false;

        int PersPackChildsCount = PersPackage.transform.childCount;
        for (int a = 0; a < PersPackChildsCount; a++) {
            if (PersPackage.transform.GetChild(a).gameObject.layer == 18) {
                PersPackage.transform.GetChild(a).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                PersPackage.transform.GetChild(a).gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }

        for (int inv = 0; inv < 4; inv++)
        {
            if (NextSelected.GetComponent<PersProperties>().Package[inv] != "None")
            {
            }
        }

        for (int i = 0; i < 4; i++) {
            if (NextSelected.GetComponent<PersProperties>().Package[i] == "None") {
                GameObject.Find(PersPackage.name + "/PersPack" + (i + 1).ToString()).GetComponent<SpriteRenderer>().enabled = true;
            }
            else {
                GameObject Item = GameObject.Find(PersPackage.name + "/" + NextSelected.GetComponent<PersProperties>().Package[i].ToString());
                Item.GetComponent<SpriteRenderer>().enabled = true;
                Item.transform.localPosition = PersPackage.transform.GetChild(i).gameObject.transform.localPosition;

                if (InvWeap.active == true) {

                    if (Item.GetComponent<WeaponProperties>() != null)
                    {
                        Item.GetComponent<Collider2D>().enabled = true;
                        Item.GetComponent<WeaponProperties>().IsActive = false;
                    }
                    else if (Item.GetComponent<OtherStuff>() != null) {
                        Item.GetComponent<Collider2D>().enabled = false;
                        Item.GetComponent<OtherStuff>().IsActive = true;
                    }
                }
                if (InvStuff.active == true) {

                    if (Item.GetComponent<WeaponProperties>() != null) {
                        Item.GetComponent<Collider2D>().enabled = false;
                        Item.GetComponent<WeaponProperties>().IsActive = true;
                    }
                    else if (Item.GetComponent<OtherStuff>() != null) {
                        Item.GetComponent<Collider2D>().enabled = true;
                        Item.GetComponent<OtherStuff>().IsActive = false;
                    }
                }
                GameObject.Find(PersPackage.name + "/PersPack" + (i + 1).ToString()).GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        InvINFO.text = NextSelected.name.ToString() + "\nHealth: " + Health.ToString() + "\nDamage: " + Damage.ToString() + "\nAccuracy: " + Accuracy.ToString() + "\nLevel: " + Level.ToString() + "\nBattles: " + Battles.ToString();

        GameObject.Find(InvPers.name + "/" + NextSelected.name + "/Lighter").active = true;
    }

    void SetPersInPlace() {

        GameObject.Find(InvPers.name + "/DummySwitcherBought").GetComponent<AudioSource>().Play();
        int OldPos = PrevSelected.GetComponent<PersProperties>().PositionOnField;
        GameObject.Find(InvPers.name + "/PosOnField" + OldPos.ToString()).GetComponent<WarriorSettings>().Full = false;
        PrevSelected.GetComponent<PersProperties>().PositionOnField = NextSelected.GetComponent<WarriorSettings>().NumberOfPos;
        NextSelected.GetComponent<WarriorSettings>().Full = true;
        PrevSelected.transform.localPosition = NextSelected.transform.localPosition + new Vector3(0, 0, -0.1f);
        GameObject.Find(PrevSelected.name + "/Lighter").active = false;
        ResetInfoScreen();
        PersPackage.active = false;
        NextSelected = PrevSelected;

    }

    void LockAllPerses() {
        for (int p = 1; p < CoBPerses + 1; p++) {
            GameObject.Find(InvPers.name + "/Pers" + p.ToString()).GetComponent<Collider2D>().enabled = false;
        }
    }

    void UnlockAllPerses() {
        for (int p = 1; p < CoBPerses + 1; p++) {
            GameObject.Find(InvPers.name + "/Pers" + p.ToString()).GetComponent<Collider2D>().enabled = true;
        }
    }

    //====================================================================================================================================
    //======================================================= Weapon Working =============================================================
    //====================================================================================================================================
    void WeaponInfo() {

        NextSelected.GetComponent<AudioSource>().Play();

        string Name = NextSelected.GetComponent<WeaponProperties>().Name;
        int Damage = NextSelected.GetComponent<WeaponProperties>().Damage;
        int Condition = NextSelected.GetComponent<WeaponProperties>().Condition;
        int COB = NextSelected.GetComponent<WeaponProperties>().Clip;
        int KindOfWeapon = NextSelected.GetComponent<WeaponProperties>().Skin;

        int Bullets = 0;

        for (int I = 1; I < CoBBullets + 1; I++) {
            if (int.Parse(PlayerSrc[2 + CoBPerses * NumPersParam + CoBWeapon * NumWpnParam + 3 * I - 2]) == KindOfWeapon)
            {
                Bullets = int.Parse(PlayerSrc[2 + CoBPerses * NumPersParam + CoBWeapon * NumWpnParam + 3 * I - 1]);
            }
        }

        RepairPrice = (10 - Condition) * Damage;

        InvINFO.text = Name + "\n\nDamage: " + Damage.ToString() + "\nCondition: " + Condition.ToString() + "\nClip: " + COB.ToString() + "\n\nBullets: "
        + Bullets.ToString() + "\n" + "\nRepair: " + RepairPrice.ToString() + "\nMoney: " + Money.ToString();

        GameObject.Find(InvWeap.name + "/" + NextSelected.name + "/Lighter").active = true;

        PersPackage.active = false;
        RepairWeapon.gameObject.active = true;
        if (NextSelected.GetComponent<WeaponProperties>().Condition != 10) {
            RepairWeapon.interactable = true;
        }
        else {
            RepairWeapon.interactable = false;
        }
    }

    void SetWeaponInPlace() {

        InvWeap.GetComponent<AudioSource>().Play();
        int OldPos = PrevSelected.GetComponent<WeaponProperties>().PositionOnField;
        GameObject.Find(InvWeap.name + "/InvWpn" + OldPos.ToString()).GetComponent<WarriorSettings>().Full = false;
        PrevSelected.transform.localPosition = NextSelected.transform.localPosition + new Vector3(0, 0, -0.1f);
        PrevSelected.GetComponent<WeaponProperties>().PositionOnField = NextSelected.GetComponent<WarriorSettings>().NumberOfPos;
        NextSelected.GetComponent<WarriorSettings>().Full = true;
        GameObject.Find(PrevSelected.name + "/Lighter").active = false;
        ResetInfoScreen();
    }

    void SetWeaponToPers() {

        InvPers.GetComponent<AudioSource>().Play();

        int OldPos = PrevSelected.GetComponent<WeaponProperties>().PositionOnField;
        GameObject.Find(InvWeap.name + "/InvWpn" + OldPos.ToString()).GetComponent<WarriorSettings>().Full = false;

        GameObject.Find(PrevSelected.name + "/Lighter").active = false;
        PrevSelected.layer = 18;
        PrevSelected.transform.SetParent(PersPackage.transform);
        PrevSelected.GetComponent<WeaponProperties>().Bought = true;

        for (int i = 0; i < 4; i++) {
            if (NextSelected.GetComponent<PersProperties>().Package[i] == "None") {
                NextSelected.GetComponent<PersProperties>().Package[i] = PrevSelected.name;
                PersPackage.active = true;
                PrevSelected.transform.localPosition = GameObject.Find(PersPackage.name + "/PersPack" + (i + 1).ToString()).transform.localPosition;
                PersPackage.active = false;
                break;
            }
        }
        int KindOfWeapon = PrevSelected.GetComponent<WeaponProperties>().Skin;
        int NumOfWeap = PrevSelected.GetComponent<WeaponProperties>().NumberOfWeaponInInventory;

        NextSelected.GetComponent<PersProperties>().WeaponSkin = KindOfWeapon;
        NextSelected.GetComponent<PersProperties>().WeaponInHands = NumOfWeap;

        ResetInfoScreen();
    }

    void SelectWeaponInPackage() {

        InvProperties.GetComponent<AudioSource>().Play();
        NextSelected.GetComponent<WeaponProperties>().IsActive = true;
        NextSelected.GetComponent<Collider2D>().enabled = false;
        AdditionalObject = PrevSelected;

    }

    void PutWeaponBack() {

        InvWeap.GetComponent<AudioSource>().Play();

        PrevSelected.GetComponent<WeaponProperties>().IsActive = false;
        PrevSelected.GetComponent<WeaponProperties>().Bought = false;
        PrevSelected.GetComponent<WeaponProperties>().PositionOnField = NextSelected.GetComponent<WarriorSettings>().NumberOfPos;
        PrevSelected.transform.SetParent(InvWeap.transform);
        PrevSelected.transform.localPosition = NextSelected.transform.localPosition + new Vector3(0, 0, -0.1f);
        PrevSelected.layer = 9;

        AdditionalObject.GetComponent<PersProperties>().WeaponInHands = 0;
        AdditionalObject.GetComponent<PersProperties>().WeaponSkin = 0;
        GameObject.Find(AdditionalObject.name + "/Lighter").active = false;

        int SubstractWeap = 0;

        for (int w = 0; w < 4; w++) {
            if (AdditionalObject.GetComponent<PersProperties>().Package[w] == PrevSelected.name) {
                SubstractWeap = w;
            }
        }

        for (int w = SubstractWeap; w < 3; w++) {
            AdditionalObject.GetComponent<PersProperties>().Package[w] = AdditionalObject.GetComponent<PersProperties>().Package[w + 1];
        }

        AdditionalObject.GetComponent<PersProperties>().Package[3] = "None";

        NextSelected.GetComponent<WarriorSettings>().Full = true;

        ResetInfoScreen();
    }

    void LockAllWeapon() {
        int Weapon = InvWeap.transform.childCount;
        for (int w = 0; w < Weapon; w++) {
            if (InvWeap.transform.GetChild(w).gameObject.layer == 9) {
                InvWeap.transform.GetChild(w).GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void UnlockAllWeapon() {
        int Weapon = InvWeap.transform.childCount;
        for (int w = 0; w < Weapon; w++) {
            if (InvWeap.transform.GetChild(w).gameObject.layer == 9) {
                InvWeap.transform.GetChild(w).GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    void RepairClick() {

        RepairWeapon.GetComponent<AudioSource>().Play();
        InvINFO.GetComponent<AudioSource>().Play();

        Money = Money - RepairPrice;
        NextSelected.GetComponent<WeaponProperties>().Condition = 10;

        WeaponInfo();

    }

    //====================================================================================================================================
    //======================================================= Stuff Working =============================================================
    //====================================================================================================================================

    void StuffInfo() {

        NextSelected.GetComponent<AudioSource>().Play();
        GameObject.Find(InvStuff.name + "/" + NextSelected.name + "/Lighter").active = true;
        InvINFO.text = NextSelected.GetComponent<OtherStuff>().Description;
        if (NextSelected.GetComponent<OtherStuff>().Skin == 2) {
            InvINFO.text = InvINFO.text + "\nLiters: " + NextSelected.GetComponent<OtherStuff>().WaterLiters.ToString();
        }
        PersPackage.active = false;

    }

    void SetStuffInPlace() {

        if (PrevSelected.GetComponent<OtherStuff>().Skin == 1) {
            GameObject.Find(InvStuff.name + "/Medicine").GetComponent<AudioSource>().Play();
        }
        if (PrevSelected.GetComponent<OtherStuff>().Skin == 2) {
            GameObject.Find(InvStuff.name + "/Water").GetComponent<AudioSource>().Play();
        }
        if (PrevSelected.GetComponent<OtherStuff>().Skin == 3) {
            GameObject.Find(InvStuff.name + "/Buff").GetComponent<AudioSource>().Play();
        }

        GameObject.Find(InvStuff.name + "/" + PrevSelected.name + "/Lighter").active = false;
        int OldPos = PrevSelected.GetComponent<OtherStuff>().PositionOnField;
        GameObject.Find(InvStuff.name + "/InvStuff" + OldPos.ToString()).GetComponent<WarriorSettings>().Full = false;
        NextSelected.GetComponent<WarriorSettings>().Full = true;
        PrevSelected.GetComponent<OtherStuff>().PositionOnField = NextSelected.GetComponent<WarriorSettings>().NumberOfPos;
        PrevSelected.transform.localPosition = NextSelected.transform.localPosition + new Vector3(0, 0, -0.1f);

        ResetInfoScreen();

    }

    void SetStuffToPers() {

        if (PrevSelected.GetComponent<OtherStuff>().Skin == 1) {
            GameObject.Find(InvStuff.name + "/Medicine").GetComponent<AudioSource>().Play();
        }
        if (PrevSelected.GetComponent<OtherStuff>().Skin == 2) {
            GameObject.Find(InvStuff.name + "/Water").GetComponent<AudioSource>().Play();
        }
        if (PrevSelected.GetComponent<OtherStuff>().Skin == 3) {
            GameObject.Find(InvStuff.name + "/Buff").GetComponent<AudioSource>().Play();
        }

        GameObject.Find(InvStuff.name + "/" + PrevSelected.name + "/Lighter").active = false;
        PrevSelected.GetComponent<OtherStuff>().WhichPersUseIt = NextSelected.GetComponent<PersProperties>().NumberOfPersInInventory;
        int OldPos = PrevSelected.GetComponent<OtherStuff>().PositionOnField;
        GameObject.Find(InvStuff.name + "/InvStuff" + OldPos.ToString()).GetComponent<WarriorSettings>().Full = false;
        PrevSelected.GetComponent<OtherStuff>().Bought = true;
        PrevSelected.transform.SetParent(PersPackage.transform);
        PrevSelected.layer = 18;

        for (int i = 0; i < 4; i++) {
            if (NextSelected.GetComponent<PersProperties>().Package[i] == "None") {
                NextSelected.GetComponent<PersProperties>().Package[i] = PrevSelected.name;
                PersPackage.active = true;
                PrevSelected.transform.localPosition = GameObject.Find(PersPackage.name + "/PersPack" + (i + 1).ToString()).transform.localPosition ;
                PersPackage.active = false;
                break;
            }
        }

        ResetInfoScreen();

    }

    void SelectStuffInPackage() {

        InvProperties.GetComponent<AudioSource>().Play();
        NextSelected.GetComponent<OtherStuff>().IsActive = true;
        NextSelected.GetComponent<Collider2D>().enabled = false;
        AdditionalObject = PrevSelected;

    }

    void PutStuffBack() {

        if (PrevSelected.GetComponent<OtherStuff>().Skin == 1)
        {
            GameObject.Find(InvStuff.name + "/Medicine").GetComponent<AudioSource>().Play();
        }
        if (PrevSelected.GetComponent<OtherStuff>().Skin == 2)
        {
            GameObject.Find(InvStuff.name + "/Water").GetComponent<AudioSource>().Play();
        }
        if (PrevSelected.GetComponent<OtherStuff>().Skin == 3)
        {
            GameObject.Find(InvStuff.name + "/Buff").GetComponent<AudioSource>().Play();
        }

        PrevSelected.GetComponent<OtherStuff>().WhichPersUseIt = 0;
        PrevSelected.GetComponent<OtherStuff>().IsActive = false;
        PrevSelected.GetComponent<OtherStuff>().Bought = false;
        PrevSelected.GetComponent<OtherStuff>().PositionOnField = NextSelected.GetComponent<WarriorSettings>().NumberOfPos;
        PrevSelected.transform.SetParent(InvStuff.transform);
        PrevSelected.transform.localPosition = NextSelected.transform.localPosition + new Vector3(0, 0, -0.1f);
        PrevSelected.layer = 19;

        GameObject.Find(AdditionalObject.name + "/Lighter").active = false;

        int SubstractStuff = 0;

        for (int s = 0; s < 4; s++)
        {
            if (AdditionalObject.GetComponent<PersProperties>().Package[s] == PrevSelected.name)
            {
                SubstractStuff = s;
            }
        }

        for (int s = SubstractStuff; s < 3; s++)
        {
            AdditionalObject.GetComponent<PersProperties>().Package[s] = AdditionalObject.GetComponent<PersProperties>().Package[s + 1];
        }

        AdditionalObject.GetComponent<PersProperties>().Package[3] = "None";

        NextSelected.GetComponent<WarriorSettings>().Full = true;
        ResetInfoScreen();

    }

    void LockAllStuff() {
        int Stuff = InvStuff.transform.childCount;
        for (int s = 0; s < Stuff; s++) {
            if (InvStuff.transform.GetChild(s).gameObject.layer == 19) {
                InvStuff.transform.GetChild(s).gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void UnlockAllStuff() {
        int Stuff = InvStuff.transform.childCount;
        for (int s = 0; s < Stuff; s++) {
            if (InvStuff.transform.GetChild(s).gameObject.layer == 19) {
                InvStuff.transform.GetChild(s).gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    //====================================================================================================================================
    //======================================================= Closing Fields =============================================================
    //====================================================================================================================================

    void ClosePersesFields(){
        for (int i = 1; i < 10; i++) {
            GameObject.Find(InvPers.name + "/PosOnField" + i.ToString()).GetComponent<Collider2D>().enabled = false;
        }
    }
    void CloseWeaponFields() {
        if (InvWeap.active == true) {
            for (int i = 1; i < 10; i++) {
                GameObject.Find(InvWeap.name + "/InvWpn" + i.ToString()).GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    void CloseStuffFields() {
        if (InvStuff.active == true) {
            for (int i = 1; i < 10; i++) {
                GameObject.Find(InvStuff.name + "/InvStuff" + i.ToString()).GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    void BlockFieldsWithWpn() {
        for (int i = 1; i < 10; i++) {
            if (GameObject.Find(InvWeap.name + "/InvWpn" + i.ToString()).GetComponent<WarriorSettings>().Full == true) {
                GameObject.Find(InvWeap.name + "/InvWpn" + i.ToString()).GetComponent<Collider2D>().enabled = false;
            }
            else {
                GameObject.Find(InvWeap.name + "/InvWpn" + i.ToString()).GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    void BlockFieldsWithStuff() {
        for (int i = 1; i < 10; i++) {
            if (GameObject.Find(InvStuff.name + "/InvStuff" + i.ToString()).GetComponent<WarriorSettings>().Full == true) {
                GameObject.Find(InvStuff.name + "/InvStuff" + i.ToString()).GetComponent<Collider2D>().enabled = false;
            }
            else {
                GameObject.Find(InvStuff.name + "/InvStuff" + i.ToString()).GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    void CloseArmoredPers() {
        for (int i = 1; i < CoBPerses + 1; i++) {
            if (GameObject.Find(InvPers.name + "/Pers" + i.ToString()).GetComponent<PersProperties>().WeaponInHands != 0) {
                GameObject.Find(InvPers.name + "/Pers" + i.ToString()).GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void CloseFullPers() {
        for (int i = 1; i < CoBPerses + 1; i++) {
            if (GameObject.Find(InvPers.name + "/Pers" + i.ToString()).GetComponent<PersProperties>().Package[3] != "None") {
                GameObject.Find(InvPers.name + "/Pers" + i.ToString()).GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    //====================================================================================================================================
    //======================================================= Opening Fields =============================================================
    //====================================================================================================================================

    void OpenPersesFields () {
        for (int i = 1; i < 10; i++) {
            GameObject.Find(InvPers.name + "/PosOnField" + i.ToString()).GetComponent<Collider2D>().enabled = true;
        }
    }
    void OpenWeaponFields () {
        if (InvWeap.active == true) {
            for (int i = 1; i < 10; i++) {
                GameObject.Find(InvWeap.name + "/InvWpn" + i.ToString()).GetComponent<Collider2D>().enabled = true;
            }
        }
    }
    void OpenStuffFields () {
        if (InvStuff.active == true) {
            for (int i = 1; i < 10; i++) {
                GameObject.Find(InvStuff.name + "/InvStuff" + i.ToString()).GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    void ActivateAllPerses() {
        for (int i = 1; i < CoBPerses + 1; i++) {
            GameObject.Find(InvPers.name + "/Pers" + i.ToString()).GetComponent<Collider2D>().enabled = true;
        }
    }

    //====================================================================================================================================
    //======================================================= Switch Inventory =============================================================
    //====================================================================================================================================

    public void InventorySwitcher(bool isOn)
    {
        Menu.GetComponent<AudioSource>().Play();
        if (isOn == true)
        {
            isOn = false;
            if (GameObject.Find(NextSelected.name + "/Lighter") != null) {
                GameObject.Find(NextSelected.name + "/Lighter").active = false;
            }
            InvWeap.active = true;
            InvStuff.active = false;
            PrevSelected = GameObject.Find(InvPers.name + "/DummySwitcherBought");
            NextSelected = GameObject.Find(InvPers.name + "/DummySwitcherBought");
            ActivateAllPerses();
            ResetInfoScreen();
        }
        else if(isOn == false)
        {
            isOn = true;
            if (GameObject.Find(NextSelected.name + "/Lighter") != null) {
                GameObject.Find(NextSelected.name + "/Lighter").active = false;
            }
            InvWeap.active = false;
            InvStuff.active = true;
            PrevSelected = GameObject.Find(InvPers.name + "/DummySwitcherBought");
            NextSelected = GameObject.Find(InvPers.name + "/DummySwitcherBought");
            ActivateAllPerses();
            ResetInfoScreen();
        }
    }

    //====================================================================================================================================
    //======================================================= Write Info Data =============================================================
    //====================================================================================================================================

    void WriteNewData() {
        InvWeap.active = true;
        InvStuff.active = true;
        InvPers.active = true;
        InvProperties.active = true;
        PersPackage.active = true;

        string[] NewInvSet = File.ReadAllLines(InvSetPath);

        for (int p = 1; p < CoBPerses + 1; p++) {
            GameObject Pers = GameObject.Find(InvPers.name + "/Pers" + p.ToString());
            NewInvSet[10 + p] = Pers.GetComponent<PersProperties>().PositionOnField.ToString();
            if (Pers.GetComponent<PersProperties>().WeaponInHands != 0) {
                NewInvSet[21 + p] = Pers.GetComponent<PersProperties>().WeaponInHands.ToString();
                NewInvSet[37 + Pers.GetComponent<PersProperties>().WeaponInHands] = "true";
            } else if(Pers.GetComponent<PersProperties>().WeaponInHands == 0){
                NewInvSet[21 + p] = "NaN";
            }
        }

        int FindWeap = InvWeap.transform.childCount;
        for (int w = 0; w < FindWeap; w++) {
            if (InvWeap.transform.GetChild(w).gameObject.layer == 9) {
                GameObject ThisWeap = InvWeap.transform.GetChild(w).gameObject;
                NewInvSet[37 + ThisWeap.GetComponent<WeaponProperties>().NumberOfWeaponInInventory] = "false";
            }
        }

        string[] MapGenNew = MapGen;

        int StuffInPack = PersPackage.transform.childCount;
        for (int s = 0; s < StuffInPack; s++) {
            if (PersPackage.transform.GetChild(s).gameObject.GetComponent<OtherStuff>() != null) {
                int NumOfPers = PersPackage.transform.GetChild(s).gameObject.GetComponent<OtherStuff>().WhichPersUseIt;
                int NumOfStuff = PersPackage.transform.GetChild(s).gameObject.GetComponent<OtherStuff>().NumberOfStuffInInv;
                NewInvSet[48 + NumOfStuff] = NumOfPers.ToString();
                if (PersPackage.transform.GetChild(s).gameObject.GetComponent<OtherStuff>().Skin == 2) {
                    MapGenNew[2 * Columns * Rows + 12 + 10 + 6 + PersPackage.transform.GetChild(s).gameObject.GetComponent<OtherStuff>().NumberOfStuffInInv] = PersPackage.transform.GetChild(s).gameObject.GetComponent<OtherStuff>().WaterLiters.ToString();
                }
            }
        }

        int FindStuff = InvStuff.transform.childCount;
        for (int s = 0; s < FindStuff; s++) {
            if (InvStuff.transform.GetChild(s).gameObject.layer == 19) {
                GameObject ThisStuff = InvStuff.transform.GetChild(s).gameObject;
                NewInvSet[48 + ThisStuff.GetComponent<OtherStuff>().NumberOfStuffInInv] = "undefined";
                if (ThisStuff.GetComponent<OtherStuff>().Skin == 2) {
                    MapGenNew[2 * Columns * Rows + 12 + 10 + 6 + ThisStuff.GetComponent<OtherStuff>().NumberOfStuffInInv] = ThisStuff.GetComponent<OtherStuff>().WaterLiters.ToString();
                }
            }
        }

        File.WriteAllLines(MapGenPath, MapGenNew);
        File.WriteAllLines(InvSetPath, NewInvSet);

        InvWeap.active = false;
        InvStuff.active = false;
        InvPers.active = false;
        InvProperties.active = false;
        PersPackage.active = false;

        GoToMap.interactable = false;

    }

    void BackToPurchaces() {
        this.GetComponent<Store>().enabled = true;
        this.GetComponent<InventoryWorking>().enabled = false;
    }

}
