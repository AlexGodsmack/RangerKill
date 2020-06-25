using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class WORK_Battle : MonoBehaviour {
    //=============== Значения в инспекторе ================
    [Header("Participants")]
    public List<GameObject> Slaves;
    public List<GameObject> Enemies;
    [Space]
    public List<GameObject> Loots;

    [Header("Scene's Objects")]
    public GameObject SlavesContainer;
    public GameObject EnemiesContainer;
    public GameObject Trash;
    [Space]
    public GameObject FinalPanel;
    public GameObject Rotor;
    //public GameObject Bullets;
    public GameObject InfoText;
    public GameObject LootContainer;
    public GameObject LOOT;
    public GameObject GameOverContainer;
    public GameObject TakeLootButton;
    public GameObject GameOverButton;
    public GameObject OverLoad;
    public GameObject BackToMap;
    public GameObject TuningTable;
    [Space]
    public GameObject Chain_1;
    public GameObject Chain_2;
    [Space]
    public ButtonSample SkipPass;
    [Space]
    public TextMesh[] Names;

    [Header("Current turn")]
    public GameObject SlaveCurrent;
    public GameObject EnemyCurrent;
    public GameObject StuffCurrent;
    public GameObject ShellCurrent;
    public SlaveProperties isActiveSlave;
    public EnemyProperties isActiveEnemy;
    public OtherStuff isActiveStuff;
    public GameObject Timer;

    [Header("Conditions")]
    public string Clan;
    public int TimeCounter = 12;
    public bool YourPass;
    public bool BattleIsOver;
    public bool LetsStart;
    public bool Wait_A_Second;
    public int Final_Power_of_Shot;

    [Header("Layers")]
    public int SlavesLayer = 8;
    public int EnemyLayer = 15;
    public int StuffLayer = 11;
    public int ItemLayer = 18;

    [Header("Sounds")]
    public AudioSource Heal;
    public AudioSource Fury;
    public AudioSource TakeWeapon;
    public AudioSource TakeMoney;
    public AudioSource TakeWater;
    public AudioSource TakeMedicine;
    public AudioSource TakeBuff;
    public AudioSource PickMonitor;
    public AudioSource SecondsSound;
    public AudioSource Misfire;

    public GameObject PackContainer;
    private float Milsec;

    [Header("Classes")]
    public MainPlayerControl PlayInv;
    public SaveLoadData Loader;
    public DataLoaderInBattle Enm_Data;

    float elapsed = 0f;

    //============================ На Старте ===============================

    void Start() {
        YourPass = true;
        Milsec = 0.0f;
        Application.targetFrameRate = 60;
        TimeCounter = 12;
        GameOverContainer.active = false;
        //TuningTable.GetComponent<SpriteRenderer>().enabled = false;
        GenerateLoot();
        //foreach (GameObject slv in PlayInv.SlavePlace) {
        //    slv.GetComponent<SlaveProperties>().ShowHealthbar = true;
        //}
    }

    void EnemyPass() {
        SkipPass.gameObject.active = false;
        InfoText.active = true;
        InfoText.GetComponent<TextMesh>().text = "Enemy's turn now";
    }

    void PlayerPass() {
        SkipPass.gameObject.active = true;
        SkipPass.isPressed = false;
        SkipPass.isActive = true;
        InfoText.active = false;
    }

    void Info_On_End_Battle(bool Condition) {
        SkipPass.gameObject.active = false;
        //InfoText.active = true;
        if (Condition == false) {
            InfoText.GetComponent<TextMesh>().text = "!!!You lose!!!";
            GameOverContainer.active = true;
        } else {
            InfoText.GetComponent<TextMesh>().text = "YOU WIN! TAKE A LOOT";
            LootContainer.active = true;
        }
    }

    void Rename_Loot(GameObject Item, int c) {
        if (Item.GetComponent<OtherStuff>() != null) {
            OtherStuff ItemProp = Item.GetComponent<OtherStuff>();
            Names[c].text = ItemProp.Name;
            if (ItemProp.Skin == 2) {
                Names[c].text += "\nLiters: " + ItemProp.Liters.ToString();
            }
            if (ItemProp.Skin == 4) {
                Names[c].text += "\n" + ItemProp.Price.ToString() + " $";
            }
        } else if (Item.GetComponent<WeaponProperties>() != null) {
            WeaponProperties ItemProp = Item.GetComponent<WeaponProperties>();
            Names[c].text = ItemProp.WeapName;
            Names[c].text += "\nCondition: " + ItemProp.Condition;
        }
        Loots.Add(Item);
    }

    //=================================== Показываю количество пуль в индикаторе =======================================

    //void ShowBullets() {
    //    if (SlaveCurrent.GetComponent<SlaveProperties>().WeaponXRef != null) {
    //        GameObject GetWeapon = SlaveCurrent.GetComponent<SlaveProperties>().WeaponXRef;
    //        int GetBullets = GetWeapon.GetComponent<WeaponProperties>().Bullets;
    //        Bullets.active = true;
    //        GameObject BulContainer = Bullets.transform.Find("Bullets").gameObject;

    //        if (GetBullets < BulContainer.transform.childCount) {
    //            foreach (Transform GetBul in BulContainer.transform) {
    //                if (GetBul.GetSiblingIndex() <= GetBullets - 1) {
    //                    GetBul.gameObject.active = true;
    //                } else {
    //                    GetBul.gameObject.active = false;
    //                }
    //            }
    //        } else {
    //            foreach (Transform GetBul in Bullets.transform.Find("Bullets")) {
    //                GetBul.gameObject.active = true;
    //            }
    //        }
    //    } else {
    //        Bullets.active = false;
    //    }
    //}

    //===================================== Генерирую луты =========================================

    void GenerateLoot() {
        LootContainer.active = true;
        for (int c = 0; c < 4; c++) {
            //=================== Если клан ТРУМАНА, то больше генерируется денег ====================
            if (Clan == "Trumans'") {
                int RandomItem = Random.Range(1, 11);
                if (RandomItem == 3) {

                    Create_Stuff(c);

                } else if (RandomItem == 6) {

                    Create_Weapon(c);

                } else {

                    Create_Money(c);

                }
            }
            //=================== Если клан СКОТОДЕРОВ, то больше генерируется оружия ====================
            if (Clan == "Knackers") {
                int RandomItem = Random.Range(1, 11);
                if (RandomItem == 3) {

                    Create_Money(c);

                } else if (RandomItem == 6) {

                    Create_Stuff(c);

                } else {

                    Create_Weapon(c);

                }
            }
            //=================== Если клан ОРДЫ, то больше генерируется шмоток ====================
            if (Clan == "Horde") {
                int RandomItem = Random.Range(1, 11);
                if (RandomItem == 3) {

                    Create_Weapon(c);

                } else if (RandomItem == 6) {

                    Create_Money(c);

                } else {

                    Create_Stuff(c);

                }
            }
            //=================== Если клан НЕИЗВЕСТЕН, то генерируется все что угодно ====================
            if (Clan == "Unknown") {
                int RandomItem = Random.Range(0, 3);
                if (RandomItem == 0) {
                    Create_Stuff(c);
                } else if (RandomItem == 1) {
                    Create_Money(c);
                } else if (RandomItem == 2) {
                    Create_Weapon(c);
                }
            }
        }
        LootContainer.active = false;
    }

    void Create_Weapon(int c) {
        //LootContainer.transform.GetChild(c).gameObject.active = true;
        GameObject newWeapon = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
        WeaponProperties getWpn = newWeapon.GetComponent<WeaponProperties>();
        newWeapon.name = c.ToString();
        getWpn.Skin = Random.Range(1, 11);
        getWpn.Bought = true;
        getWpn.Condition = Random.Range(5, 11);
        newWeapon.transform.SetParent(LootContainer.transform);
        newWeapon.transform.position = LootContainer.transform.GetChild(c).transform.position;
        newWeapon.transform.SetParent(LOOT.transform);
    }

    void Create_Stuff(int c) {
        //LootContainer.transform.GetChild(c).gameObject.active = true;
        GameObject newStuff = Instantiate(Resources.Load("OtherStuff")) as GameObject;
        OtherStuff GetStf = newStuff.GetComponent<OtherStuff>();
        newStuff.name = c.ToString();
        GetStf.Bought = true;
        GetStf.Skin = Random.Range(1, 4);

        if (GetStf.Skin == 1) {
            GetStf.Price = 200;
        }
        if (GetStf.Skin == 2) {
            GetStf.Liters = 100;
            GetStf.Price = 100;
        }
        if (GetStf.Skin == 3) {
            GetStf.Price = 150;
        }
        newStuff.transform.SetParent(LootContainer.transform);
        newStuff.transform.position = LootContainer.transform.GetChild(c).transform.position;
        newStuff.transform.SetParent(LOOT.transform);
    }

    void Create_Money(int c) {
        GameObject newStuff = Instantiate(Resources.Load("OtherStuff")) as GameObject;
        OtherStuff GetStf = newStuff.GetComponent<OtherStuff>();
        newStuff.name = c.ToString();
        GetStf.Name = "Money";
        GetStf.Bought = true;
        GetStf.Skin = 4;
        GetStf.Price = Random.Range(15, 300);
        newStuff.transform.SetParent(LootContainer.transform);
        newStuff.transform.position = LootContainer.transform.GetChild(c).transform.position;
        newStuff.transform.SetParent(LOOT.transform);
    }

    //================================ Активировал тряску камеры =====================================
    void ActivateShakes() {
        Chain_1.GetComponent<Animator>().SetBool("Activated", true);
        Chain_2.GetComponent<Animator>().SetBool("Activated", true);
        this.GetComponent<ShakeCamera>().Activated = true;
    }

    void Nullify_Timer() {
        TimeCounter = 12;
        foreach (Transform sec in Timer.transform) {
            sec.gameObject.active = true;
        }
        if (isActiveEnemy != null) {
            isActiveEnemy.isActive = false;
        }
        if (isActiveSlave != null) {
            isActiveSlave.isActive = false;
        }
        if (isActiveStuff != null) {
            isActiveStuff.isActive = false;
        }
        isActiveSlave = null;
        isActiveEnemy = null;
        isActiveStuff = null;
        SlaveCurrent = null;
        EnemyCurrent = null;
        StuffCurrent = null;
    }

    void Shot_Timer_Minus() {
        int CurrentTime = TimeCounter;
        while (TimeCounter > CurrentTime - 5) {
            Timer.transform.GetChild(TimeCounter - 1).gameObject.active = false;
            TimeCounter--;
            SecondsSound.Play();
        }
    }

    void Heal_Timer_Minus() {
        int CurrentTime = TimeCounter;
        while (TimeCounter > CurrentTime - 3) {
            Timer.transform.GetChild(TimeCounter - 1).gameObject.active = false;
            TimeCounter--;
            SecondsSound.Play();
        }
    }

    void Update() {

        //================================ Начало гринда =====================================
        if (LetsStart == true) {
            if (YourPass == true && BattleIsOver == false) {

                //elapsed += Time.deltaTime;
                //if (elapsed >= 1f) {
                //    TimeCounter -= 1;
                //    SecondsSound.Play();
                //    Debug.Log(Timer.transform.GetChild(TimeCounter).gameObject.name); 
                //    Timer.transform.GetChild(TimeCounter).gameObject.active = false;
                //    elapsed = 0;
                //}

                //if (TimeCounter <= 0) {
                //    Nullify_Timer();
                //    EnemyPass();
                //    YourPass = false;
                //}

                if (Input.GetMouseButtonDown(0)) {

                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider.gameObject.layer == SlavesLayer) {
                        if (EnemyCurrent != null) {
                            isActiveEnemy.isActive = false;
                            isActiveEnemy = null;
                            EnemyCurrent = null;
                        }
                        if (isActiveSlave != null) {
                            if (isActiveSlave == hit.collider.gameObject.GetComponent<SlaveProperties>()) {
                                isActiveSlave.isActive = false;
                                SlaveCurrent = null;
                                isActiveSlave = null;
                            } else {
                                isActiveSlave.isActive = false;
                                isActiveSlave = hit.collider.gameObject.GetComponent<SlaveProperties>();
                                SlaveCurrent = hit.collider.gameObject;
                                isActiveSlave.isActive = true;
                            }
                        } else {
                            isActiveSlave = hit.collider.gameObject.GetComponent<SlaveProperties>();
                            SlaveCurrent = hit.collider.gameObject;
                            isActiveSlave.isActive = true;
                        }
                    }
                    if (hit.collider.gameObject.layer == ItemLayer) {
                        if (hit.collider.gameObject.layer == ItemLayer) {
                            if (isActiveStuff != null) {
                                if (isActiveStuff.GetComponent<OtherStuff>() != null) {
                                    isActiveStuff.isActive = false;
                                    SlaveCurrent = null;
                                    isActiveStuff = null;
                                }
                            }
                        }
                        if (hit.collider.gameObject.GetComponent<OtherStuff>() != null) {
                            isActiveStuff = hit.collider.gameObject.GetComponent<OtherStuff>();
                            StuffCurrent = hit.collider.gameObject;
                            isActiveStuff.isActive = true;
                            if (isActiveSlave.Health != isActiveSlave.FullHealth) {
                                if (isActiveStuff.Skin == 1) {
                                    if (TimeCounter >= 3) {
                                        Heal_Timer_Minus();
                                        Heal.Play();
                                        isActiveSlave.Health = isActiveSlave.FullHealth;
                                        Destroy(StuffCurrent);
                                    }
                                }
                            }
                        }
                    }
                    if (hit.collider.gameObject.layer == EnemyLayer) {
                        if (SlaveCurrent != null && isActiveSlave != null) {
                            isActiveEnemy = hit.collider.gameObject.GetComponent<EnemyProperties>();
                            EnemyCurrent = hit.collider.gameObject;
                            if (TimeCounter >= 5) {
                                if (isActiveSlave.WeaponXRef != null) {
                                    WeaponProperties SlvWeapon = isActiveSlave.WeaponXRef.gameObject.GetComponent<WeaponProperties>();
                                    if (SlvWeapon.Bullets != 0) {
                                        if (isActiveSlave != null) {
                                            Wait_A_Second = true;
                                            isActiveSlave.isActive = false;
                                            if (Wait_A_Second == true) {
                                                isActiveSlave.OnFire();
                                                Wait_A_Second = false;
                                                Final_Power_of_Shot = (int)isActiveSlave.PowerOfShot + Random.Range(-30, 30);
                                                SlvWeapon.Bullets--;
                                            }
                                            Shot_Timer_Minus();
                                        }
                                    } else {
                                        Misfire.Play();
                                    }
                                }
                            }
                        } else {
                            if (isActiveEnemy != null) {
                                if (EnemyCurrent == hit.collider.gameObject) {
                                    isActiveEnemy.isActive = false;
                                    isActiveEnemy = null;
                                    EnemyCurrent = null;
                                } else {
                                    isActiveEnemy.isActive = false;
                                    isActiveEnemy = null;
                                    EnemyCurrent = null;
                                    isActiveEnemy = hit.collider.gameObject.GetComponent<EnemyProperties>();
                                    EnemyCurrent = hit.collider.gameObject;
                                    isActiveEnemy.isActive = true;
                                }
                            } else {
                                isActiveEnemy = hit.collider.gameObject.GetComponent<EnemyProperties>();
                                EnemyCurrent = hit.collider.gameObject;
                                isActiveEnemy.isActive = true;
                            }
                        }
                    }
                }


                //============================================ Ход врага ===============================================
            } else if (YourPass == false && BattleIsOver == false) {

                //if (TimeCounter <= 2) {

                //}
                if (Wait_A_Second == false) {

                    elapsed += Time.deltaTime;

                    if (EnemyCurrent != null) {
                        isActiveEnemy.isActive = false;
                        isActiveEnemy = null;
                        EnemyCurrent = null;
                    }
                    if (SlaveCurrent != null) {
                        isActiveSlave = null;
                        SlaveCurrent = null;
                    }

                    if (elapsed >= 1f) {
                        SecondsSound.Play();

                        Wait_A_Second = true;

                        int randEnm = Random.Range(0, Enemies.Count);
                        EnemyCurrent = Enemies[randEnm].gameObject;
                        isActiveEnemy = EnemyCurrent.GetComponent<EnemyProperties>();
                        isActiveEnemy.isActive = true;



                        //int rand = Random.Range(0, 2);
                        //if (rand == 0) {
                        //    if (TimeCounter >= 3) {

                        //            isActiveEnemy.isActive = false;
                        //            isActiveEnemy = null;
                        //            EnemyCurrent = null;
                        //            Wait_A_Second = false;
                        //        }
                        //    } else {
                        //        TimeCounter = 2;
                        //    }
                        //} else if (rand == 1) {
                        //}
                        if (TimeCounter >= 5) {

                            int randSlv = Random.Range(0, Slaves.Count);
                            SlaveCurrent = Slaves[randSlv].gameObject;
                            isActiveSlave = SlaveCurrent.GetComponent<SlaveProperties>();
                            isActiveEnemy.OnFire();
                            Final_Power_of_Shot = isActiveEnemy.PowerOfShot + Random.Range(-30, 30);
                            Shot_Timer_Minus();

                        } else if (TimeCounter >= 3) {
                            if (isActiveEnemy.Health != isActiveEnemy.FullHealth) {

                                isActiveEnemy.Health = isActiveEnemy.FullHealth;
                                Heal_Timer_Minus();
                                Heal.Play();
                                Wait_A_Second = false;

                            }
                        } else if (TimeCounter <= 2) {
                            PlayerPass();
                            YourPass = true;
                            Nullify_Timer();
                        }
                        elapsed = 0;
                    }
                }

                if (EnemyCurrent != null) {
                    if (isActiveEnemy.ShellContainer != null) {
                        if (SlaveCurrent != null) {
                            isActiveEnemy.ShellContainer.gameObject.GetComponent<BulletShell>().Target = SlaveCurrent.gameObject;
                            if (isActiveEnemy.ShellContainer.GetComponent<BulletShell>().Gotcha == true) {
                                ActivateShakes();
                                isActiveSlave.Health -= (int)isActiveEnemy.PowerOfShot;
                                if (isActiveSlave.Health <= 0) {
                                    isActiveSlave.GetComponent<Animator>().SetBool("Dead", true);
                                    int plc = 0;
                                    foreach (GameObject slv in PlayInv.SlavePlace) {
                                        if (slv != null) {
                                            if (slv == SlaveCurrent) {
                                                PlayInv.SlavePlace[plc] = null;
                                            }
                                        }
                                        plc++;
                                    }
                                    Slaves.Remove(SlaveCurrent);
                                    SlaveCurrent.GetComponent<Collider2D>().enabled = false;
                                    SlaveCurrent.transform.SetParent(Trash.transform);
                                } else {
                                    isActiveSlave.GotDamage();
                                }

                                GameObject Dmg = Instantiate(Resources.Load("DmgNum")) as GameObject;
                                Dmg.transform.position = SlaveCurrent.transform.position + new Vector3(0, 0, -0.5f);
                                Dmg.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "-" + Final_Power_of_Shot.ToString();

                                GameObject Sheel = isActiveEnemy.ShellContainer.gameObject;
                                Destroy(Sheel);
                                //isActiveSlave.isActive = false;
                                //isActiveEnemy.isActive = false;
                                //isActiveSlave = null;
                                //SlaveCurrent = null;
                                //isActiveEnemy = null;
                                //EnemyCurrent = null;

                                if (Slaves.Count == 0) {
                                    if (Slaves.Count == 0) {
                                        Info_On_End_Battle(false);
                                    }
                                    if (Enemies.Count == 0) {
                                        Info_On_End_Battle(true);
                                    }
                                    BattleIsOver = true;
                                    Nullify_Timer();
                                    TimeCounter = 0;
                                }

                                Wait_A_Second = false;
                            }
                        }
                    }
                }


            }

            //======================== Если Пуля Раба достигла Врага ===========================
            if (isActiveSlave != null) {
                if (isActiveSlave.ShellContainer != null) {
                    if (EnemyCurrent != null) {
                        isActiveSlave.ShellContainer.gameObject.GetComponent<BulletShell>().Target = EnemyCurrent.gameObject;
                        if (isActiveSlave.ShellContainer.GetComponent<BulletShell>().Gotcha == true) {
                            ActivateShakes();
                            GameObject Sheel = isActiveSlave.ShellContainer.gameObject;
                            Destroy(Sheel);
                            isActiveEnemy.Health -= (int)isActiveSlave.PowerOfShot;
                            if (isActiveEnemy.Health <= 0) {
                                isActiveSlave.Battles += isActiveEnemy.FullHealth;
                                isActiveSlave.Slaves_Level_Grade();
                                isActiveEnemy.GetComponent<Animator>().SetBool("Dead", true);
                                Enemies.Remove(EnemyCurrent);
                                EnemyCurrent.transform.SetParent(Trash.transform);
                                EnemyCurrent.GetComponent<Collider2D>().enabled = false;
                            } else {
                                isActiveSlave.Battles += (int)isActiveSlave.PowerOfShot;
                                isActiveSlave.Slaves_Level_Grade();
                                isActiveEnemy.Damaged();
                            }

                            GameObject Dmg = Instantiate(Resources.Load("DmgNum")) as GameObject;
                            Dmg.transform.position = EnemyCurrent.transform.position + new Vector3(0, 0, -0.5f);
                            Dmg.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "-" + Final_Power_of_Shot.ToString();

                            isActiveSlave.isActive = false;
                            isActiveEnemy.isActive = false;
                            isActiveSlave = null;
                            SlaveCurrent = null;
                            isActiveEnemy = null;
                            EnemyCurrent = null;

                            if (Enemies.Count == 0) {
                                if (Slaves.Count == 0) {
                                    Info_On_End_Battle(false);
                                }
                                if (Enemies.Count == 0) {
                                    Info_On_End_Battle(true);
                                }
                                Nullify_Timer();
                                BattleIsOver = true;
                                TimeCounter = 0;
                            }

                        }
                    }
                }
            }            

            //================== Нажать следующий ход =================
            if (SkipPass.gameObject.active == true) {
                if (SkipPass.isPressed == true) {
                    EnemyPass();
                    Nullify_Timer();
                    Wait_A_Second = false;
                    YourPass = false;
                }
            }

            //=============== Битва окончена ================

            if (BattleIsOver == true) {
                elapsed += 1;
                if (elapsed >= 20f) {
                    TimeCounter++;

                    if (TimeCounter == 0) {
                        PickMonitor.Play();
                        InfoText.active = true;
                    }
                    if (TimeCounter == 1) {
                        InfoText.active = false;
                    }
                    if (TimeCounter == 2) {
                        PickMonitor.Play();
                        InfoText.active = true;
                    }
                    if (TimeCounter == 3) {
                        InfoText.active = false;
                    }
                    if (TimeCounter == 4) {
                        PickMonitor.Play();
                        InfoText.active = true;
                    }
                    if (TimeCounter == 5) {
                        InfoText.active = false;
                    }
                    if (TimeCounter == 6) {
                        PickMonitor.Play();
                        InfoText.active = true;
                        Timer.active = false;
                        int rename = 0;
                        foreach (Transform Stf in LOOT.transform) {
                            Rename_Loot(Stf.gameObject, rename);
                            rename++;
                        }
                        Rotor.GetComponent<Animator>().SetBool("Activated", true);
                        FinalPanel.GetComponent<Animator>().SetBool("Activated", true);
                        FinalPanel.GetComponent<AudioSource>().Play();
                        TimeCounter++;
                    }
                    elapsed = 0;
                }
                //=============================== Забрать Лут ==================================

                if (Input.GetMouseButtonDown(0)) {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider.gameObject.layer == ItemLayer) {

                        if (hit.collider.gameObject.GetComponent<WeaponProperties>() != null) {
                            WeaponProperties wpn = hit.collider.gameObject.GetComponent<WeaponProperties>();
                            if (wpn.isActive == false) {
                                wpn.isActive = true;
                            } else {
                                wpn.isActive = false;
                            }
                            PickMonitor.Play();
                        }
                        if (hit.collider.gameObject.GetComponent<OtherStuff>() != null) {
                            OtherStuff stf = hit.collider.gameObject.GetComponent<OtherStuff>();
                            if (stf.isActive == false) {
                                stf.isActive = true;
                            } else {
                                stf.isActive = false;
                            }
                            PickMonitor.Play();
                        }
                    }
                }
                if (TakeLootButton.GetComponent<ButtonSample>().isPressed == true) {
                    foreach (GameObject loot in Loots) {
                        if (loot != null) {
                            if (loot.GetComponent<OtherStuff>() != null) {
                                OtherStuff prop = loot.GetComponent<OtherStuff>();
                                if (prop.isActive == true) {
                                    if (prop.Skin != 4) {
                                        int plcnum = 0;
                                        foreach (GameObject plc in PlayInv.Package) {
                                            if (plc == null) {
                                                PlayInv.Package[plcnum] = loot;
                                                if (prop.Skin == 1) {
                                                    TakeMedicine.Play();
                                                }
                                                if (prop.Skin == 2) {
                                                    TakeWater.Play();
                                                }
                                                if (prop.Skin == 3) {
                                                    TakeBuff.Play();
                                                }
                                                Names[int.Parse(loot.name)].gameObject.active = false;
                                                loot.transform.SetParent(Loader.ItemsSource.transform);
                                                loot.transform.localPosition = new Vector3(0, 0, 0);
                                                prop.isActive = false;
                                                OverLoad.active = false;
                                                break;
                                            } else {
                                                OverLoad.active = true;
                                            }
                                            plcnum++;
                                        }
                                    } else {
                                        Names[int.Parse(loot.name)].gameObject.active = false;
                                        PlayInv.Money += prop.Price;
                                        TakeMoney.Play();
                                        Destroy(loot);
                                    }
                                }
                            } else if(loot.GetComponent<WeaponProperties>() != null){
                                WeaponProperties prop = loot.GetComponent<WeaponProperties>();
                                if (prop.isActive == true) {
                                    int plcnum = 0;
                                    foreach (GameObject plc in PlayInv.Package) {
                                        if (plc == null) {
                                            PlayInv.Package[plcnum] = loot;
                                            TakeWeapon.Play();
                                            Names[int.Parse(loot.name)].gameObject.active = false;
                                            loot.transform.SetParent(Loader.ItemsSource.transform);
                                            loot.transform.localPosition = new Vector3(0, 0, 0);
                                            prop.isActive = false;
                                            OverLoad.active = false;
                                            break;
                                        } else {
                                            OverLoad.active = true;
                                        }
                                        plcnum++;
                                    }
                                }
                            }
                        }
                    }
                }

                if (BackToMap.GetComponent<ButtonSample>().isPressed == true) {
                    Loader.SaveAll();
                    //Debug.Log("Already");
                    Loader.Save_Enemy_Data(Enm_Data.NumberOfArea, Enm_Data.Count);
                    TakeLootButton.active = false;
                    BackToMap.active = false;
                    OverLoad.active = false;
                    LOOT.active = false;
                    foreach (TextMesh name in Names) {
                        name.gameObject.active = false;
                    }
                    TuningTable.GetComponent<Animator>().SetBool("Activation", true);
                    BackToMap.GetComponent<ButtonSample>().isPressed = false;
                }
            }


            //================================= Если нажать Game Over ===================================

            if (GameOverButton.GetComponent<ButtonSample>().isPressed == true) {
                if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {
                    File.Delete(Application.persistentDataPath + "/PlayerData.json");
                }
                if (File.Exists(Application.persistentDataPath + "/MapData.json")) {
                    File.Delete(Application.persistentDataPath + "/MapData.json");
                }
                if (File.Exists(Application.persistentDataPath + "/BanditTroop.json")) {
                    File.Delete(Application.persistentDataPath + "/BanditTroop.json");
                }
                if (File.Exists(Application.persistentDataPath + "/StoresStack.json")) {
                    File.Delete(Application.persistentDataPath + "/StoresStack.json");
                }
                SceneManager.LoadScene(0);
            }
        }
    }
}
