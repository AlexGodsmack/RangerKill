using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class WORK_Battle : MonoBehaviour
{
    [Header("Participants")]
    public List<GameObject> Slaves;
    public List<GameObject> Enemies;

    [Header("Scene's Objects")]
    public GameObject SlavesContainer;
    public GameObject EnemiesContainer;
    public GameObject Trash;
    [Space]
    public GameObject FinalPanel;
    public GameObject Rotor;
    public GameObject Bullets;
    public GameObject InfoText;
    public GameObject LootContainer;
    public GameObject GameOverContainer;
    public GameObject TakeLootButton;
    public GameObject GameOverButton;
    public GameObject BackToMap;
    public GameObject TuningTable;
    [Space]
    public GameObject Chain_1;
    public GameObject Chain_2;

    [Header("Current turn")]
    public GameObject SlaveCurrent;
    public GameObject EnemyCurrent;
    public GameObject ShellCurrent;
    public GameObject Timer;

    [Header("Conditions")]
    public bool YourPass;
    public bool BattleIsOver;
    public int TimeCounter;
    public bool LetsStart;

    [Header("Layers")]
    public int SlavesLayer = 8;
    public int EnemyLayer = 15;
    public int StuffLayer = 11;

    [Header("Sounds")]
    public AudioSource Heal;
    public AudioSource Fury;
    public AudioSource TakeWeapon;
    public AudioSource TakeMoney;
    public AudioSource TakeWater;
    public AudioSource TakeMedicine;
    public AudioSource TakeBuff;
    public AudioSource PickMonitor;

    public GameObject PackContainer;
    private float Milsec;

    void Start()
    {
        Bullets.active = false;
        YourPass = true;
        Milsec = 0.0f;
        Application.targetFrameRate = 60;
        TimeCounter = 12;
        GameOverContainer.active = false;
        TuningTable.GetComponent<SpriteRenderer>().enabled = false;
        GetStart();
        GenerateLoot();
    }

    void ShowSlavePack() {
        InfoText.active = false;
        SlaveCurrent.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = true;

    }

    void GetStart() {
        if (SlaveCurrent != null) {
            SlaveCurrent.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
        }
        InfoText.active = true;
        InfoText.GetComponent<TextMesh>().text = "First - select slave\nSecond - select enemy\nor select stuff to use";
    }

    void EnemyPass() {
        if (SlaveCurrent != null) {
            SlaveCurrent.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
        }
        InfoText.active = true;
        InfoText.GetComponent<TextMesh>().text = "Enemy's turn now";
    }

    void ShowBullets() {
        if (SlaveCurrent.GetComponent<SlaveProperties>().WeaponXRef != null) {
            GameObject GetWeapon = SlaveCurrent.GetComponent<SlaveProperties>().WeaponXRef;
            int GetBullets = GetWeapon.GetComponent<WeaponProperties>().Bullets;
            Bullets.active = true;
            GameObject BulContainer = Bullets.transform.Find("Bullets").gameObject;

            if (GetBullets < BulContainer.transform.childCount) {
                foreach (Transform GetBul in BulContainer.transform) {
                    if (GetBul.GetSiblingIndex() <= GetBullets - 1) {
                        GetBul.gameObject.active = true;
                    } else {
                        GetBul.gameObject.active = false;
                    }
                }
            } else {
                foreach (Transform GetBul in Bullets.transform.Find("Bullets")) {
                    GetBul.gameObject.active = true;
                }
            }
        } else {
            Bullets.active = false;
        }
    }

    void GenerateLoot() {
        int RandomCount = Random.Range(1, 5);
        for (int c = 0; c < RandomCount; c++) {
            int randomKind = Random.Range(0, 2);
            if (randomKind == 0) {
                LootContainer.transform.GetChild(c).gameObject.active = true;
                GameObject newStuff = Instantiate(Resources.Load("OtherStuff")) as GameObject;
                newStuff.name = "Loot" + c;
                newStuff.GetComponent<OtherStuff>().Bought = true;
                newStuff.GetComponent<OtherStuff>().Skin = Random.Range(1, 5);
                if (newStuff.GetComponent<OtherStuff>().Skin == 4) {
                    newStuff.GetComponent<OtherStuff>().Price = Random.Range(150, 3500);
                }
                if (newStuff.GetComponent<OtherStuff>().Skin == 2) {
                    newStuff.GetComponent<OtherStuff>().Liters = 100;
                }
                newStuff.transform.SetParent(LootContainer.transform);
                newStuff.transform.position = LootContainer.transform.GetChild(c).transform.position;
                newStuff.transform.SetParent(LootContainer.transform.Find("LOOT").gameObject.transform);
            }
            if (randomKind == 1) {
                LootContainer.transform.GetChild(c).gameObject.active = true;
                GameObject newWeapon = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
                newWeapon.name = "Loot" + c;
                newWeapon.GetComponent<WeaponProperties>().Skin = Random.Range(1, 11);
                newWeapon.GetComponent<WeaponProperties>().Bought = true;
                newWeapon.GetComponent<WeaponProperties>().Condition = Random.Range(1, 11);
                newWeapon.transform.SetParent(LootContainer.transform);
                newWeapon.transform.position = LootContainer.transform.GetChild(c).transform.position;
                newWeapon.transform.SetParent(LootContainer.transform.Find("LOOT").gameObject.transform);
            }
        }
        LootContainer.active = false;
    }

    void ShowDescriptions() {
        LootContainer.active = true;
        for (int d = 0; d < LootContainer.transform.Find("LOOT").transform.childCount; d++) {
            GameObject GetDescription = LootContainer.transform.GetChild(d).transform.GetChild(0).gameObject;
            if (LootContainer.transform.Find("LOOT").GetChild(d).GetComponent<OtherStuff>() != null) {

                GameObject GetStuff = LootContainer.transform.Find("LOOT").GetChild(d).gameObject;
                int GetSkin = GetStuff.GetComponent<OtherStuff>().Skin;
                string[] GetDescrit = GetStuff.GetComponent<OtherStuff>().ShortDescription.Split('.');
                string GetName = GetDescrit[0];
                string GetPrice = GetStuff.GetComponent<OtherStuff>().Price.ToString();
                string GetLiters = GetStuff.GetComponent<OtherStuff>().Liters.ToString();

                if (GetSkin == 4) {
                    GetDescription.GetComponent<TextMesh>().text = GetName + "\n" + GetPrice;
                } else if (GetSkin == 2) {
                    GetDescription.GetComponent<TextMesh>().text = GetName + "\nLiters: " + GetLiters;
                } else {
                    GetDescription.GetComponent<TextMesh>().text = GetName;
                }
            }
            if (LootContainer.transform.Find("LOOT").GetChild(d).GetComponent<WeaponProperties>() != null) {
                string GetName = LootContainer.transform.Find("LOOT").GetChild(d).GetComponent<WeaponProperties>().WeapName;
                string GetCond = LootContainer.transform.Find("LOOT").GetChild(d).GetComponent<WeaponProperties>().Condition.ToString();
                GetDescription.GetComponent<TextMesh>().text = GetName + "\nCondition: " + GetCond;
            }
        }
    }

    void ShowFailBattle() {
        GameOverContainer.active = true;
    }

    void ActivateShakes() {
        Chain_1.GetComponent<Animator>().SetBool("Activated", true);
        Chain_2.GetComponent<Animator>().SetBool("Activated", true);
        this.GetComponent<ShakeCamera>().Activated = true;
    }

    void UpdateInventoryCounts() {
        int slaves = 0;
        int weapons = 0;
        int stuff = 0;
        foreach (GameObject Slv in this.GetComponent<PlayerInventory>().SlavePlace) {
            if (Slv != null) {
                slaves += 1;
            }
        }
        foreach (GameObject Wpn in this.GetComponent<PlayerInventory>().WeaponPlace) {
            if (Wpn != null) {
                weapons += 1;
            }
        }
        foreach (GameObject Stff in this.GetComponent<PlayerInventory>().StuffPlace) {
            if (Stff != null) {
                stuff += 1;
            }
        }
        foreach (SlavePackage Pack in this.GetComponent<PlayerInventory>().SlavesBag) {
            foreach (GameObject Stuff in Pack.Place) {
                if (Stuff != null) {
                    if (Stuff.GetComponent<WeaponProperties>() != null) {
                        weapons += 1;
                    }
                    if (Stuff.GetComponent<OtherStuff>() != null) {
                        stuff += 1;
                    }
                }
            }
        }

        this.GetComponent<PlayerInventory>().Slaves = slaves;
        this.GetComponent<PlayerInventory>().Weapons = weapons;
        this.GetComponent<PlayerInventory>().Stuff = stuff;
    }

    void Update()
    {

        if (LetsStart == true) {
            if (YourPass == true && BattleIsOver == false) {

                if (TimeCounter < 0) {
                    TimeCounter = 12;
                }

                Milsec += 1f / 60f;
                if (Milsec >= 1.0f) {
                    TimeCounter -= 1;
                    if (TimeCounter >= 0) {
                        Timer.transform.GetChild(TimeCounter).gameObject.active = false;
                    } else {
                        if (SlaveCurrent != null) {
                            SlaveCurrent.GetComponent<SlaveProperties>().isActive = false;
                            SlaveCurrent.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
                            EnemyPass();
                        }
                        EnemyCurrent = Enemies[Random.Range(0, Enemies.Count)].gameObject;
                        SlaveCurrent = Slaves[Random.Range(0, Slaves.Count)].gameObject;
                        Milsec = 0.0f;
                        YourPass = false;
                    }
                    Milsec = 0.0f;
                }

                if (Input.GetMouseButtonDown(0)) {

                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider.gameObject.layer == SlavesLayer) {
                        if (SlaveCurrent != null) {
                            SlaveCurrent.GetComponent<SlaveProperties>().isActive = false;
                            SlaveCurrent.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
                        }
                        SlaveCurrent = hit.collider.gameObject;
                        SlaveCurrent.GetComponent<SlaveProperties>().isActive = true;
                        SlaveCurrent.GetComponent<AudioSource>().Play();
                        SlaveCurrent.GetComponent<SlaveProperties>().gameObject.active = true;

                        ShowBullets();

                        ShowSlavePack();

                    }

                    if (hit.collider.gameObject.layer == StuffLayer) {
                        GameObject GetStuff = hit.collider.gameObject;
                        if (GetStuff.GetComponent<OtherStuff>().Skin != 2) {

                            GameObject GetPack = GetStuff.transform.parent.gameObject;
                            GameObject GetPlace = GetPack.transform.GetChild(GetStuff.transform.GetSiblingIndex() - 4).gameObject;

                            int numstff = 0;
                            if (SlaveCurrent.GetComponent<SlaveProperties>().Health != SlaveCurrent.GetComponent<SlaveProperties>().FullHealth) {
                                if (GetStuff.GetComponent<OtherStuff>().Skin == 1) {
                                    Destroy(GetStuff);
                                    SlaveCurrent.GetComponent<SlaveProperties>().Health = SlaveCurrent.GetComponent<SlaveProperties>().FullHealth;
                                    Heal.Play();
                                    GetPlace.active = true;
                                }
                            }
                            if (GetStuff.GetComponent<OtherStuff>().Skin == 3) {
                                Destroy(GetStuff);
                                SlaveCurrent.GetComponent<SlaveProperties>().IN_RUSH = true;
                                Fury.Play();
                                GetPlace.active = true;
                            }
                        }
                    }

                    if (hit.collider.gameObject.layer == EnemyLayer) {
                        if (SlaveCurrent != null) {
                            if (SlaveCurrent.GetComponent<SlaveProperties>().WeaponXRef != null) {

                                GameObject GetWeapon = SlaveCurrent.GetComponent<SlaveProperties>().WeaponXRef;
                                SlaveCurrent.GetComponent<SlaveProperties>().IN_RUSH = false;

                                if (GetWeapon.GetComponent<WeaponProperties>().Bullets > 0) {
                                    GetWeapon.GetComponent<WeaponProperties>().Bullets -= 1;
                                    ShowBullets();

                                    SlaveCurrent.GetComponent<Animator>().SetBool("Fire", true);

                                    EnemyCurrent = hit.collider.gameObject;
                                }
                            }
                        }
                    }
                }
                if (SlaveCurrent != null) {
                    if (SlaveCurrent.GetComponent<SlaveProperties>().ShellContainer != null) {
                        if (EnemyCurrent != null) {
                            ShellCurrent = SlaveCurrent.GetComponent<SlaveProperties>().ShellContainer;
                            ShellCurrent.GetComponent<BulletShell>().Target = EnemyCurrent;
                            if (ShellCurrent.GetComponent<BulletShell>().Gotcha == true) {

                                if (SlaveCurrent.GetComponent<SlaveProperties>().WeaponSkin == 9) {
                                    ActivateShakes();
                                }
                                if (SlaveCurrent.GetComponent<SlaveProperties>().WeaponSkin == 10) {
                                    ActivateShakes();
                                }

                                Destroy(ShellCurrent);

                                int GetPower = (int)SlaveCurrent.GetComponent<SlaveProperties>().PowerOfShot;
                                EnemyCurrent.GetComponent<EnemyProperties>().Damaged();
                                EnemyCurrent.GetComponent<EnemyProperties>().Health -= GetPower;
                                SlaveCurrent.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
                                SlaveCurrent.GetComponent<SlaveProperties>().isActive = false;
                                if (EnemyCurrent.GetComponent<EnemyProperties>().Health <= 0) {
                                    EnemyCurrent.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                    Enemies.Remove(EnemyCurrent);
                                    //Destroy(EnemyCurrent.gameObject);
                                    EnemyCurrent.transform.SetParent(Trash.transform);
                                    EnemyCurrent.GetComponent<Animator>().SetBool("Dead", true);
                                    if (Enemies.Count == 0) {
                                        Milsec = 0.0f;
                                        BattleIsOver = true;
                                    } else {
                                        EnemyCurrent = Enemies[Random.Range(0, Enemies.Count)].gameObject;
                                        SlaveCurrent = Slaves[Random.Range(0, Slaves.Count)].gameObject;
                                    }
                                }


                                EnemyPass();
                                Timer.active = false;
                                TimeCounter = -1;
                                YourPass = false;
                            }
                        }
                    }
                }
            } else if (YourPass == false && BattleIsOver == false){

                if (TimeCounter < 0) {
                    TimeCounter = 4;
                }

                Milsec += 1f / 60f;
                if (Milsec >= 1.0f) {
                    TimeCounter -= 1;
                    Milsec = 0.0f;
                }

                if (TimeCounter == 2) {
                    //if (EnemyCurrent != null) {
                    //    EnemyCurrent.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    //}

                    Bullets.active = false;
                    EnemyCurrent.GetComponent<Animator>().SetBool("Fire", true);
                }

                if (EnemyCurrent != null) {
                    if (SlaveCurrent != null) {
                        if (EnemyCurrent.GetComponent<EnemyProperties>().ShellContainer != null) {

                            ShellCurrent = EnemyCurrent.GetComponent<EnemyProperties>().ShellContainer;
                            ShellCurrent.GetComponent<BulletShell>().Target = SlaveCurrent;

                            if (ShellCurrent.GetComponent<BulletShell>().Gotcha == true) {
                                EnemyProperties EnmPrs = EnemyCurrent.GetComponent<EnemyProperties>();

                                if (EnmPrs.Skin == 1) {
                                    ActivateShakes();
                                }
                                if (EnmPrs.Skin == 3) {
                                    ActivateShakes();
                                }
                                if (EnmPrs.Skin == 4) {
                                    ActivateShakes();
                                }
                                if (EnmPrs.Skin == 5) {
                                    ActivateShakes();
                                }

                                Destroy(ShellCurrent);
                                SlaveCurrent.GetComponent<SlaveProperties>().Health -= EnemyCurrent.GetComponent<EnemyProperties>().PowerOfShot;
                                SlaveCurrent.GetComponent<SlaveProperties>().GotDamage();

                                GameObject GetWeapon;
                                if (SlaveCurrent.GetComponent<SlaveProperties>().WeaponXRef != null) {
                                    GetWeapon = SlaveCurrent.GetComponent<SlaveProperties>().WeaponXRef;
                                    int RandDamage = Random.Range(0, 2);
                                    if (RandDamage == 0) {
                                        if (GetWeapon.GetComponent<WeaponProperties>().Condition != 1) {
                                            GetWeapon.GetComponent<WeaponProperties>().Condition -= 1;
                                        }
                                    }
                                }
                                if (SlaveCurrent.GetComponent<SlaveProperties>().Health <= 0) {

                                    SlaveCurrent.GetComponent<Animator>().SetBool("Dead", true);
                                    int GetNum = SlaveCurrent.GetComponent<SlaveProperties>().Number;

                                    foreach (SlavePackage Pack in this.GetComponent<PlayerInventory>().SlavesBag) {
                                        if (Pack.NumberOfSlave == GetNum) {
                                            this.GetComponent<PlayerInventory>().SlavesBag.Remove(Pack);
                                            break;
                                        }
                                    }
                                    Slaves.Remove(SlaveCurrent);
                                    PlayerInventory PlayInv = this.GetComponent<PlayerInventory>();
                                    for (int a = 0; a < PlayInv.SlavePlace.Length; a++) {
                                        if (PlayInv.SlavePlace[a] == SlaveCurrent) {
                                            PlayInv.SlavePlace[a] = null;
                                        }
                                    }
                                    for (int a = 0; a < PlayInv.SlavesBag.Count; a++) {
                                        if (PlayInv.SlavesBag[a].NumberOfSlave == SlaveCurrent.GetComponent<SlaveProperties>().Number) {
                                            PlayInv.SlavesBag.Remove(PlayInv.SlavesBag[a]);
                                        }
                                    }
                                    SlaveCurrent.transform.SetParent(Trash.transform);
                                    SlaveCurrent.GetComponent<Collider2D>().enabled = false;
                                    SlaveCurrent.GetComponent<SlaveProperties>().ShowHealthbar = false;

                                    Destroy(SlaveCurrent.GetComponent<SlaveProperties>().InventoryPack.gameObject);

                                    SlaveCurrent.GetComponent<SlaveProperties>().isActive = false;
                                    SlaveCurrent = null;

                                    if (Slaves.Count == 0) {
                                        Milsec = 0.0f;
                                        BattleIsOver = true;
                                    }
                                } else {
                                    SlaveCurrent.GetComponent<SlaveProperties>().isActive = false;
                                }
                                EnemyCurrent.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                                Timer.active = true;
                                for (int Sec = 0; Sec < Timer.transform.childCount; Sec++) {
                                    Timer.transform.GetChild(Sec).gameObject.active = true;
                                }
                                GetStart();
                                TimeCounter = -1;
                                YourPass = true;
                            }
                        }
                    }
                }
            
                //if (TimeCounter == 2) {
                //    EnemyCurrent.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                //}

                //if (TimeCounter == 1) {
                //    SlaveCurrent.GetComponent<SlaveProperties>().isActive = true;
                //}

            }

            if (BattleIsOver == true && Milsec < 0.5f) {
                Milsec += 0.1f;
                if (SlavesContainer.transform.childCount == 0) {
                    ShowFailBattle();
                }
                if (EnemiesContainer.transform.childCount == 0) {
                    ShowDescriptions();
                }
                Timer.active = false;
                Rotor.GetComponent<Animator>().SetBool("Activated", true);
                FinalPanel.GetComponent<Animator>().SetBool("Activated", true);
                FinalPanel.GetComponent<AudioSource>().Play();

                UpdateInventoryCounts();
            }

            if (TakeLootButton.GetComponent<ButtonSample>().isPressed == true) {

                foreach (GameObject Slave in this.GetComponent<PlayerInventory>().SlavePlace) {
                    if (Slave != null) {
                        SlaveProperties SlvProp = Slave.GetComponent<SlaveProperties>();
                        SlvProp.Battles += 1;
                        int GetLvl = SlvProp.Level;
                        SlvProp.UpgradeLevel();
                        if (GetLvl != SlvProp.Level) {
                            SlvProp.FullHealth = (int)(SlvProp.FullHealth * 1.5f);
                            SlvProp.Damage = (int)(SlvProp.Damage * 1.5f);
                            SlvProp.Accuracy = (int)(SlvProp.Accuracy * 1.5f);
                            SlvProp.Health = SlvProp.FullHealth;
                        }
                    }
                }

                foreach (Transform Loot in LootContainer.transform.Find("LOOT")) {
                    if (Loot.GetComponent<WeaponProperties>() != null) {
                        if (this.GetComponent<PlayerInventory>().Weapons < 9) {
                            for (int wp = 0; wp < this.GetComponent<PlayerInventory>().WeaponPlace.Length; wp++) {
                                if (this.GetComponent<PlayerInventory>().WeaponPlace[wp] == null) {
                                    this.GetComponent<PlayerInventory>().WeaponPlace[wp] = Loot.gameObject;
                                    this.GetComponent<PlayerInventory>().Weapons += 1;
                                    Loot.gameObject.active = false;
                                    LootContainer.transform.GetChild(Loot.transform.GetSiblingIndex()).gameObject.active = false;

                                    TakeWeapon.Play();

                                    break;
                                }
                            }
                        } else {
                            Loot.gameObject.active = true;
                            LootContainer.transform.GetChild(Loot.transform.GetSiblingIndex()).gameObject.active = true;
                            GameObject SendMessage = LootContainer.transform.GetChild(Loot.transform.GetSiblingIndex()).transform.GetChild(0).gameObject;
                            SendMessage.GetComponent<TextMesh>().text = "Your bag is \noverload!!!";
                        }
                    }
                    if (Loot.GetComponent<OtherStuff>() != null) {
                        if (this.GetComponent<PlayerInventory>().Stuff < 9) {
                            for (int sp = 0; sp < this.GetComponent<PlayerInventory>().StuffPlace.Length; sp++) {
                                
                                if (Loot.GetComponent<OtherStuff>().Skin != 4 && this.GetComponent<PlayerInventory>().StuffPlace[sp] == null) {
                                    this.GetComponent<PlayerInventory>().StuffPlace[sp] = Loot.gameObject;
                                    this.GetComponent<PlayerInventory>().Stuff += 1;
                                    Loot.gameObject.active = false;
                                    LootContainer.transform.GetChild(Loot.transform.GetSiblingIndex()).gameObject.active = false;
                                    if (Loot.GetComponent<OtherStuff>().Skin == 1) {
                                        TakeMedicine.Play();
                                    }
                                    if (Loot.GetComponent<OtherStuff>().Skin == 2) {
                                        TakeWater.Play();
                                    }
                                    if (Loot.GetComponent<OtherStuff>().Skin == 3) {
                                        TakeBuff.Play();
                                    }
                                    break;
                                }
                            }
                        } else {
                            Loot.gameObject.active = true;
                            LootContainer.transform.GetChild(Loot.transform.GetSiblingIndex()).gameObject.active = true;
                            GameObject SendMessage = LootContainer.transform.GetChild(Loot.transform.GetSiblingIndex()).transform.GetChild(0).gameObject;
                            SendMessage.GetComponent<TextMesh>().text = "Your bag is \noverloaded!!!";
                        }
                        if (Loot.GetComponent<OtherStuff>().Skin == 4) {
                            this.GetComponent<PlayerInventory>().Money += Loot.GetComponent<OtherStuff>().Price;
                            Loot.gameObject.active = false;
                            TakeMoney.Play();
                            LootContainer.transform.GetChild(Loot.transform.GetSiblingIndex()).gameObject.active = false;
                        }
                    }
                }

                bool GotAll = true;
                foreach (Transform Loot in LootContainer.transform.Find("LOOT")) {
                    if (Loot.gameObject.active == true) {
                        GotAll = false;
                    }
                }

                if (GotAll == true) {
                    TuningTable.GetComponent<SpriteRenderer>().enabled = true;
                }
                TakeLootButton.GetComponent<ButtonSample>().isActive = false;
                TakeLootButton.transform.GetChild(0).gameObject.active = false;
                BackToMap.active = true;
            }

            if (BackToMap.GetComponent<ButtonSample>().isPressed == true) {
                SaveLoadData newSaveLoad = new SaveLoadData();
                newSaveLoad.PlayerInventory = this.GetComponent<PlayerInventory>();
                newSaveLoad.SaveAll();

                int GetBodies = this.GetComponent<DataLoaderInBattle>().Count;
                int GetClanArea = this.GetComponent<DataLoaderInBattle>().NumberOfArea;

                string json = File.ReadAllText(Application.persistentDataPath + "/MapData.json");
                MapData GetMap = JsonUtility.FromJson<MapData>(json);
                foreach (BanditArea Band in GetMap.GenerateIndexes.Bandits) {
                    if (Band.NumberOfArea == GetClanArea) {
                        Band.Population -= GetBodies;
                        if (Band.Population <= 0) {
                            GetMap.GenerateIndexes.Bandits.Remove(Band);
                        }
                        break;
                    }
                }

                string newMap = JsonUtility.ToJson(GetMap);
                StreamWriter WriteNewMap = new StreamWriter(Application.persistentDataPath + "/MapData.json");
                WriteNewMap.Write(newMap);
                WriteNewMap.Close();
            
                SceneManager.LoadScene(2);
            }

            if (GameOverButton.GetComponent<ButtonSample>().isPressed == true) {
                if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {
                    File.Delete(Application.persistentDataPath + "/PlayerData.json");
                }
                if (File.Exists(Application.persistentDataPath + "/MapData.json")) {
                    File.Delete(Application.persistentDataPath + "/MapData.json");
                }
                SceneManager.LoadScene(0);
            }
        }
    }
}
