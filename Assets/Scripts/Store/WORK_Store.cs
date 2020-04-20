using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WORK_Store : MonoBehaviour
{
    public GameObject PosRightTop;
    public GameObject PosLeftTop;
    public GameObject PosRightBottom;
    public GameObject PosMidBottom;
    public GameObject PosLeftBottom;
    public GameObject PosMidLeft;
    public GameObject SlavesBuyPos;
    public GameObject Center;

    public GameObject MainMenuPanel;
    public GameObject WeaponProperties;
    public GameObject WeaponBuy;
    public GameObject WeaponBack;
    public GameObject InventoryProperties;
    public GameObject SLAVES;
    public GameObject WEAPANDSTUFF;
    public GameObject INVENTORY;

    public GameObject GeneralText;
    public GameObject WeaponInfo;
    public GameObject WeapInvInfo;
    public GameObject StuffInvInfo;
    public GameObject BulletInfo;
    public GameObject StuffInfo;

    public GameObject MenuHead;
    public GameObject MenuExit;

    public GameObject AreaForSlaves;

    public GameObject SlavesBuyPanel;
    public GameObject SlavesBoughtPanel;
    public GameObject SlavesPropertiesPanel;

    public int CountOfSlaves;
    public int CountOfWeapon;
    public int CountOfBullets;
    public int CountOfStuff;

    public GameObject isActiveSlave;
    public GameObject isActiveWeapon;
    public GameObject isActiveBullet;
    public GameObject isActiveStuff;
    public GameObject BuySlaveButton;
    public GameObject BuyWeapStuffButton;
    public GameObject Switcher;
    public AudioSource PickSound;

    public TextMesh MoneyInfo;

    private int SlavesLayer = 8;
    private int WeaponLayer = 9;
    private int BulletsLayer = 10;
    private int HeadLayer = 12;
    private int StuffLayer = 11;

    private int BulPrice;
    private int BulCount;
    private string BulInfo;

    private PlayerInventory PlayInv;

    void Start()
    {

        MainMenuPanel.transform.position = PosRightTop.transform.position + new Vector3(0, 0, 0.1f);
        PlayInv = this.GetComponent<PlayerInventory>();
        MenuHead.transform.position = PosLeftTop.transform.position;
        SlavesBoughtPanel.transform.position = PosMidBottom.transform.position;
        SlavesBuyPanel.transform.position = new Vector3(PosRightBottom.transform.position.x, -0.2f, SlavesBoughtPanel.transform.position.z);
        SlavesPropertiesPanel.transform.position = PosMidLeft.transform.position;
        WeaponProperties.transform.position = PosRightBottom.transform.position;
        WeaponBuy.transform.position = PosLeftBottom.transform.position;
        InventoryProperties.transform.position = PosMidBottom.transform.position;

        ///*********************************************** LOAD ALL INVENTORY *****************************************************

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {
            this.GetComponent<SaveLoadData>().LoadAll();

            int ReNumSlaves = 0;
            foreach (GameObject Slv in this.GetComponent<PlayerInventory>().SlavePlace) {
                if (Slv != null) {
                    foreach (SlavePackage Pack in this.GetComponent<PlayerInventory>().SlavesBag) {
                        if (Slv.GetComponent<SlaveProperties>().Number == Pack.NumberOfSlave) {
                            Slv.GetComponent<SlaveProperties>().Number = Slv.GetInstanceID();
                            Pack.NumberOfSlave = Slv.GetInstanceID();
                        }
                    }
                }
            }
            foreach (GameObject Slv in this.GetComponent<PlayerInventory>().SlavePlace) {
                if (Slv != null) {
                    ReNumSlaves += 1;
                    foreach (SlavePackage Pack in this.GetComponent<PlayerInventory>().SlavesBag) {
                        if (Slv.GetComponent<SlaveProperties>().Number == Pack.NumberOfSlave) {
                            Slv.GetComponent<SlaveProperties>().Number = ReNumSlaves;
                            Pack.NumberOfSlave = ReNumSlaves;
                        }
                    }
                }
            }

            int SetSlavePlace = 0;
            foreach (GameObject Slv in this.GetComponent<PlayerInventory>().SlavePlace) {
                if (Slv != null) {
                    GameObject GetPlace = SlavesBoughtPanel.transform.Find("Places").transform.GetChild(SetSlavePlace).gameObject;
                    Slv.transform.position = GetPlace.transform.position;
                    GetPlace.active = false;
                    Slv.transform.SetParent(SlavesBoughtPanel.transform.Find("Places/BoughtSlaves").transform);
                    Slv.GetComponent<SlaveProperties>().Bought = true;
                    SetSlavePlace += 1;
                }
            }

            int SetWpnPlace = 0;
            foreach (GameObject wpn in this.GetComponent<PlayerInventory>().WeaponPlace) {
                if (wpn != null) {
                    GameObject GetPlace = WeapInvInfo.transform.GetChild(SetWpnPlace).gameObject;
                    wpn.transform.position = GetPlace.transform.position;
                    GetPlace.active = false;
                    wpn.GetComponent<WeaponProperties>().Bought = true;
                    wpn.transform.SetParent(WeapInvInfo.transform.Find("BoughtWeapons").transform);
                    SetWpnPlace += 1;
                }
            }

            int SetStffPlace = 0;
            foreach (GameObject stff in this.GetComponent<PlayerInventory>().StuffPlace) {
                if (stff != null) {
                    GameObject GetPlace = StuffInvInfo.transform.GetChild(SetStffPlace).gameObject;
                    stff.transform.position = GetPlace.transform.position;
                    GetPlace.active = false;
                    stff.GetComponent<OtherStuff>().Bought = true;
                    stff.transform.SetParent(StuffInvInfo.transform.Find("BoughtStuff").transform);
                    SetStffPlace += 1;
                }
            }
            foreach (SlavePackage Pack in this.GetComponent<PlayerInventory>().SlavesBag) {
                foreach (GameObject Item in Pack.Place) {
                    if (Item != null) {
                        if (Item.GetComponent<WeaponProperties>() != null) {
                            GameObject GetPlace = WeapInvInfo.transform.GetChild(SetWpnPlace).gameObject;
                            Item.transform.position = GetPlace.transform.position;
                            GetPlace.active = false;
                            Item.GetComponent<WeaponProperties>().Bought = true;
                            Item.transform.SetParent(WeapInvInfo.transform.Find("BoughtWeapons").transform);
                            SetWpnPlace += 1;
                        }
                        if (Item.GetComponent<OtherStuff>() != null) {
                            GameObject GetPlace = StuffInvInfo.transform.GetChild(SetStffPlace).gameObject;
                            Item.transform.position = GetPlace.transform.position;
                            GetPlace.active = false;
                            Item.GetComponent<OtherStuff>().Bought = true;
                            Item.transform.SetParent(StuffInvInfo.transform.Find("BoughtStuff").transform);
                            SetStffPlace += 1;
                        }
                    }
                }
            }
        }

        ///*********************************************** GENERATE SLAVES *****************************************************

        CountOfSlaves = Random.Range(3, 20);
        this.transform.Find("SLAVES").GetComponent<SlavesPanel>().LengthOfSlaves = CountOfSlaves;

        for (int p = 1; p < CountOfSlaves; p++) {

            GameObject P = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
            P.name = "S_" + p;
            P.GetComponent<SlaveProperties>().Number = p;
            int RandomHealth = Random.Range(9, 90) * 5; //between 45  and  450
            P.GetComponent<SlaveProperties>().Health = RandomHealth;
            P.GetComponent<SlaveProperties>().FullHealth = RandomHealth;
            if (RandomHealth >= 225) {
                P.GetComponent<SlaveProperties>().Damage = Random.Range(4, 12) * 5;//between 20 to 60
            }else {
                P.GetComponent<SlaveProperties>().Damage = Random.Range(12, 21) * 5;//between 60 to 105
            }
            P.GetComponent<SlaveProperties>().Accuracy = Random.Range(3, 10);
            P.GetComponent<SlaveProperties>().Battles = 0;
            P.GetComponent<SlaveProperties>().Level = 1;
            P.GetComponent<SlaveProperties>().Skin = Random.Range(1, 6);
            P.transform.SetParent(AreaForSlaves.transform);
            P.transform.localPosition = new Vector3(-8.5f + 0.5f * p, 0.15f, 2.5f);

        }

        ///*********************************************** GENERATE WEAPON *****************************************************

        CountOfWeapon = Random.Range(3, 12);
        this.transform.Find("WEAP&STUFF").GetComponent<WeapAndStuffPanel>().LenghtOfWeapons = CountOfWeapon;

        for (int w = 1; w < CountOfWeapon; w++) {
            GameObject W = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
            W.name = "W_" + w;
            W.GetComponent<WeaponProperties>().Skin = Random.Range(1, 11);
            W.GetComponent<WeaponProperties>().Condition = Random.Range(1, 11);
            W.transform.SetParent(WeaponBack.transform);
            W.transform.localPosition = WeaponBack.transform.GetChild(w - 1).transform.localPosition + new Vector3( 0, -0.08f, -0.2f);
            W.transform.SetParent(WeaponBack.transform.Find("Weapons").transform);
            GameObject L = Instantiate(Resources.Load("Lighter_01")) as GameObject;
            L.transform.SetParent(W.transform);
            L.name = "Lighter";
            L.transform.localPosition = new Vector3(0, 0, 0.1f);

        }

        ///*********************************************** GENERATE BULLETS *****************************************************

        CountOfBullets = Random.Range(3, 12);

        for (int b = 1; b < CountOfBullets; b++) {
            GameObject B = Instantiate(Resources.Load("BulletsDoll")) as GameObject;
            B.name = "B_" + b;
            B.GetComponent<BulletsProperties>().Skin = Random.Range(1, 11);
            B.GetComponent<BulletsProperties>().Count = Random.Range(1, 20);
            B.transform.SetParent(WeaponBack.transform);
            B.transform.localPosition = WeaponBack.transform.GetChild(b - 1).transform.localPosition + new Vector3(0, -0.08f, -0.2f);
            B.transform.SetParent(WeaponBack.transform.Find("Bullets").transform);
            GameObject L = Instantiate(Resources.Load("Lighter_01")) as GameObject;
            L.transform.SetParent(B.transform);
            L.name = "Lighter";
            L.transform.localPosition = new Vector3(0, 0, 0.1f);
        }

        ///*********************************************** GENERATE STUFF *****************************************************

        CountOfStuff = Random.Range(3, 12);

        for (int s = 1; s < CountOfStuff; s++) {
            GameObject S = Instantiate(Resources.Load("OtherStuff")) as GameObject;
            S.name = "S_" + s;
            S.GetComponent<OtherStuff>().Skin = Random.Range(1, 4);
            if (S.GetComponent<OtherStuff>().Skin == 2) {
                S.GetComponent<OtherStuff>().IfNewWater = true;
            }
            S.transform.SetParent(WeaponBack.transform);
            S.transform.localPosition = WeaponBack.transform.GetChild(s - 1).transform.localPosition + new Vector3(0, -0.08f, -0.2f);
            S.transform.SetParent(WeaponBack.transform.Find("Stuff").transform);
            GameObject L = Instantiate(Resources.Load("Lighter_01")) as GameObject;
            L.transform.SetParent(S.transform);
            L.name = "Lighter";
            L.transform.localPosition = new Vector3(0, 0, 0.1f);
        }

        ///*********************************************** GENERATE NEW PLAYER INVENTORY *****************************************************

        if (this.GetComponent<PlayerInventory>().Money == 0) {
            this.GetComponent<PlayerInventory>().Money = Random.Range(600, 800) * 5;
        }
        MoneyInfo.text = this.GetComponent<PlayerInventory>().Money.ToString();
        MenuHead.GetComponent<ButtonSwitcher>().Number = 1;
        WeaponBack.transform.Find("Weapons").gameObject.active = true;
        WeaponBack.transform.Find("Bullets").gameObject.active = false;
        WeaponBack.transform.Find("Stuff").gameObject.active = false;

        WEAPANDSTUFF.active = false;
        INVENTORY.active = false;
    }

    void ClearOldInventory() {
        for (int s = 0; s < INVENTORY.transform.Find("SlavesInv/Slaves").transform.childCount; s++) {
            GameObject Pack = INVENTORY.transform.Find("SlavesInv/Slaves").GetChild(s).gameObject.GetComponent<SlaveProperties>().InventoryPack.gameObject;
            Destroy(Pack);
            Destroy(INVENTORY.transform.Find("SlavesInv/Slaves").GetChild(s).gameObject); 
        }
        for (int w = 0; w < INVENTORY.transform.Find("WeaponInv/Weapons").transform.childCount; w++) {
            Destroy(INVENTORY.transform.Find("WeaponInv/Weapons").GetChild(w).gameObject);
        }
        for (int s = 0; s < INVENTORY.transform.Find("StuffInv/Stuff").transform.childCount; s++) {
            Destroy(INVENTORY.transform.Find("StuffInv/Stuff").GetChild(s).gameObject);
        }
    }

        ///*********************************************** IMPORT ACTUAL INV TO 3 PANEL *****************************************************

    void ImportNewInventory() {

        PlayerInventory PlayInv = this.GetComponent<PlayerInventory>();

        int SlavePlace = 0;
        foreach (GameObject Slv in PlayInv.SlavePlace) {
            if (Slv != null) {
                GameObject Slave = Instantiate(Slv) as GameObject;
                Slave.name = Slv.name;

                SlaveProperties Prop = Slave.GetComponent<SlaveProperties>();
                Prop.Bought = false;
                Prop.isActive = false;
                Prop.ShowHealthbar = true;
                Prop.Goal = INVENTORY.transform.Find("WeaponInv").transform.GetChild(5).gameObject;
                Prop.SlaveXRef = Slv;

                Slave.transform.position = INVENTORY.transform.Find("SlavesInv").transform.GetChild(SlavePlace).transform.position + new Vector3(0, 0, -0.2f);
                Slave.transform.SetParent(INVENTORY.transform.Find("SlavesInv/Slaves").transform);

                GameObject Pack = Instantiate(Resources.Load("SlavePack")) as GameObject;
                Pack.name = "Pack_" + SlavePlace;
                Pack.transform.SetParent(InventoryProperties.transform);
                Pack.transform.localPosition = new Vector3(0, 0.32f, -0.2f);
                Prop.InventoryPack = Pack;
                Pack.active = false;

                int ItemPlace = 0;
                foreach (SlavePackage GetPack in PlayInv.SlavesBag) {
                    if (GetPack.NumberOfSlave == Prop.Number) {
                        foreach (GameObject Item in GetPack.Place) {
                            if (Item != null) {

                                GameObject newItem = Instantiate(Item) as GameObject;
                                Destroy(newItem.transform.GetChild(0).gameObject);
                                GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
                                L.name = "Lighter";
                                L.transform.SetParent(newItem.transform);
                                L.transform.localPosition = new Vector3(0, 0, -0.1f);

                                if (newItem.GetComponent<WeaponProperties>() != null) {
                                    newItem.GetComponent<WeaponProperties>().Bought = true;
                                    newItem.GetComponent<WeaponProperties>().isActive = false;
                                    Prop.WeaponXRef = newItem;
                                    newItem.transform.position = Pack.transform.GetChild(ItemPlace).transform.position;
                                    newItem.transform.SetParent(Pack.transform);
                                    newItem.GetComponent<WeaponProperties>().WeaponXRef = Item;
                                }

                                if (newItem.GetComponent<OtherStuff>() != null) {
                                    newItem.GetComponent<OtherStuff>().Bought = true;
                                    newItem.GetComponent<OtherStuff>().isActive = false;
                                    newItem.transform.position = Pack.transform.GetChild(ItemPlace).transform.position;
                                    newItem.transform.SetParent(Pack.transform);
                                    newItem.GetComponent<OtherStuff>().StuffXRef = Item;
                                }

                                Pack.transform.GetChild(ItemPlace).gameObject.active = false;
                            }
                            ItemPlace += 1;
                        }
                    }
                }
            }
            SlavePlace += 1;
        }

        int WeapPlace = 0;
        foreach (GameObject Wpn in PlayInv.WeaponPlace) {
            if (Wpn != null) {
                GameObject Weap = Instantiate(Wpn) as GameObject;

                Destroy(Weap.transform.GetChild(0).gameObject);
                GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
                L.name = "Lighter";
                L.transform.SetParent(Weap.transform);
                L.transform.localPosition = new Vector3(0, 0, -0.1f);

                Weap.transform.position = INVENTORY.transform.Find("WeaponInv").transform.GetChild(WeapPlace).transform.position + new Vector3(0, 0, -0.2f);
                Weap.transform.SetParent(INVENTORY.transform.Find("WeaponInv/Weapons").transform);
                Weap.GetComponent<WeaponProperties>().WeaponXRef = Wpn;
                Weap.GetComponent<WeaponProperties>().Bought = false;
                Weap.GetComponent<WeaponProperties>().isActive = false;
            }
            WeapPlace += 1;
        }

        int StuffPlace = 0;
        foreach (GameObject Stff in PlayInv.StuffPlace) {
            if (Stff != null) {
                GameObject NewStff = Instantiate(Stff) as GameObject;

                Destroy(NewStff.transform.GetChild(0).gameObject);
                GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
                L.name = "Lighter";
                L.transform.SetParent(NewStff.transform);
                L.transform.localPosition = new Vector3(0, 0, -0.1f);

                NewStff.transform.position = INVENTORY.transform.Find("StuffInv").transform.GetChild(StuffPlace).transform.position + new Vector3(0, 0, -0.2f);
                NewStff.transform.SetParent(INVENTORY.transform.Find("StuffInv/Stuff").transform);
                NewStff.GetComponent<OtherStuff>().StuffXRef = Stff;
                NewStff.GetComponent<OtherStuff>().Bought = false;
                NewStff.GetComponent<OtherStuff>().isActive = false;
            }
            StuffPlace += 1;
        }

    }

    void Update()
    {
        if (MenuHead.GetComponent<ButtonSwitcher>().isPressed == true) {
            if (MenuHead.GetComponent<ButtonSwitcher>().PrevNumber == 1) {
                if (isActiveSlave != null) {
                    isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
                    BuySlaveButton.GetComponent<ButtonSample>().isActive = false;
                }
                isActiveSlave = null;
                BuySlaveButton.GetComponent<ButtonSample>().isActive = false;
            }
            if (MenuHead.GetComponent<ButtonSwitcher>().PrevNumber == 2) {
                if (isActiveWeapon != null) {
                    isActiveWeapon.GetComponent<WeaponProperties>().isActive = false;
                    isActiveWeapon.transform.Find("Lighter").gameObject.active = false;
                }
                if (isActiveBullet != null) {
                    isActiveBullet.GetComponent<BulletsProperties>().isActive = false;
                    isActiveBullet.transform.Find("Lighter").gameObject.active = false;
                }
                if (isActiveStuff != null) {
                    isActiveStuff.GetComponent<OtherStuff>().isActive = false;
                }
                GeneralText.active = true;
                WeaponInfo.active = false;
                WeapInvInfo.active = false;
                StuffInvInfo.active = false;
                StuffInfo.active = false;
                BulletInfo.active = false;
                isActiveWeapon = null;
                BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
            }
            if (MenuHead.GetComponent<ButtonSwitcher>().PrevNumber == 3) {

            }
        }

        if (Input.GetMouseButtonDown(0)) {

            if (MenuHead.GetComponent<ButtonSwitcher>().Number == 1) {
                MoneyInfo.text = this.GetComponent<PlayerInventory>().Money.ToString();
                SLAVES.gameObject.active = true;
                WEAPANDSTUFF.gameObject.active = false;
                INVENTORY.gameObject.active = false;
            }
            if (MenuHead.GetComponent<ButtonSwitcher>().Number == 2) {
                SLAVES.gameObject.active = false;
                WEAPANDSTUFF.gameObject.active = true;
                INVENTORY.gameObject.active = false;
            }
            if (MenuHead.GetComponent<ButtonSwitcher>().Number == 3) {
                SLAVES.gameObject.active = false;
                WEAPANDSTUFF.gameObject.active = false;
                INVENTORY.gameObject.active = true;
                ClearOldInventory();
                ImportNewInventory();

                INVENTORY.GetComponent<InventoryPanel>().enabled = true;
                this.GetComponent<WORK_Store>().enabled = false;
            }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            
            ///*******************************************************************************************************************
            ///**************************************************** SELECT SLAVES ************************************************
            ///*******************************************************************************************************************
            
            if (hit.collider.gameObject.layer == SlavesLayer) {
                if (isActiveSlave != null) {
                    isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
                }
                isActiveSlave = hit.collider.gameObject;
                isActiveSlave.GetComponent<SlaveProperties>().isActive = true;
                isActiveSlave.GetComponent<AudioSource>().Play();
                SLAVES.GetComponent<SlavesPanel>().Health.text = isActiveSlave.GetComponent<SlaveProperties>().Health.ToString();
                SLAVES.GetComponent<SlavesPanel>().Damage.text = isActiveSlave.GetComponent<SlaveProperties>().Damage.ToString();
                SLAVES.GetComponent<SlavesPanel>().Accuracy.text = isActiveSlave.GetComponent<SlaveProperties>().Accuracy.ToString();
                SLAVES.GetComponent<SlavesPanel>().Level.text = isActiveSlave.GetComponent<SlaveProperties>().Level.ToString();
                SLAVES.GetComponent<SlavesPanel>().Price.text = isActiveSlave.GetComponent<SlaveProperties>().Price.ToString();

                int HealthGrade = Mathf.RoundToInt((isActiveSlave.GetComponent<SlaveProperties>().Health - 45.0f) / ((450.0f - 45.0f) / 4));
                int DamageGrade = Mathf.RoundToInt((isActiveSlave.GetComponent<SlaveProperties>().Damage - 20.0f)/ ((100.0f - 20.0f) / 4));
                int AccuracyGrade = Mathf.RoundToInt((isActiveSlave.GetComponent<SlaveProperties>().Accuracy - 3.0f)/ ((9.0f - 3.0f) / 4));
                SLAVES.GetComponent<SlavesPanel>().HealthGrade = HealthGrade;
                SLAVES.GetComponent<SlavesPanel>().DamageGrade = DamageGrade;
                SLAVES.GetComponent<SlavesPanel>().AccuracyGrade = AccuracyGrade;

                if (this.GetComponent<PlayerInventory>().Money >= isActiveSlave.GetComponent<SlaveProperties>().Price) {
                    if (isActiveSlave.GetComponent<SlaveProperties>().Bought == false) {
                        if (this.GetComponent<PlayerInventory>().Slaves < 9) {
                            BuySlaveButton.GetComponent<ButtonSample>().isActive = true;
                        } else {
                            BuySlaveButton.GetComponent<ButtonSample>().isActive = false;
                        }
                    } else {
                        BuySlaveButton.GetComponent<ButtonSample>().isActive = false;
                    }
                } else {
                    BuySlaveButton.GetComponent<ButtonSample>().isActive = false;
                }
            }

            ///********************************************************************************************************************
            ///**************************************************** SELECT WEAPONS ************************************************
            ///********************************************************************************************************************
            
            if (hit.collider.gameObject.layer == WeaponLayer) {
                if (isActiveWeapon != null) {
                    isActiveWeapon.GetComponent<WeaponProperties>().isActive = false;
                }

                isActiveWeapon = hit.collider.gameObject;
                GameObject WpnInfo = WeaponInfo.transform.Find("InfoText").gameObject;
                GameObject WpnDoll = WeaponInfo.transform.Find("WeaponDoll").gameObject;
                GameObject WpnGrade = WeaponInfo.transform.Find("GradeDoll").gameObject;
                GameObject BackButton = WeaponInfo.transform.Find("BackToWeapInv").gameObject;
                string WpnName = isActiveWeapon.GetComponent<WeaponProperties>().WeapName;
                int Damage = isActiveWeapon.GetComponent<WeaponProperties>().Damage;
                int Condition = isActiveWeapon.GetComponent<WeaponProperties>().Condition;
                int Bullets = isActiveWeapon.GetComponent<WeaponProperties>().Bullets;
                int Efficiency = isActiveWeapon.GetComponent<WeaponProperties>().Efficiency;
                int Price = isActiveWeapon.GetComponent<WeaponProperties>().Price;

                if (isActiveWeapon.GetComponent<WeaponProperties>().Bought == false) {
                    isActiveWeapon.GetComponent<WeaponProperties>().isActive = true;
                    isActiveWeapon.GetComponent<AudioSource>().Play();

                    GeneralText.active = false;
                    WeaponInfo.active = true;
                    WeapInvInfo.active = false;
                    StuffInvInfo.active = false;
                    StuffInfo.active = false;
                    BulletInfo.active = false;
                    BackButton.active = false;

                    WpnInfo.GetComponent<TextMesh>().text = WpnName +
                        "\nDamage: " + Damage.ToString() +
                        "\nCondition: " + Condition.ToString() +
                        "\nBullets: " + Bullets.ToString() +
                        "\n\n      Efficiency: " + Efficiency.ToString() +
                        "\n\n\nPrice: " + Price.ToString() + "    Money: " + this.GetComponent<PlayerInventory>().Money.ToString();

                    WpnDoll.GetComponent<WeaponProperties>().Skin = isActiveWeapon.GetComponent<WeaponProperties>().Skin;

                    WpnGrade.GetComponent<GradeStore>().GetGrade = Mathf.RoundToInt((Efficiency - 10.0f) / ((650.0f - 10.0f) / 7));

                    if (this.GetComponent<PlayerInventory>().Money >= isActiveWeapon.GetComponent<WeaponProperties>().Price) {
                        if (this.GetComponent<PlayerInventory>().Weapons < 9) {
                            BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = true;
                        } else {
                            BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
                        }
                    } else {
                        BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
                    }
                }
                if (isActiveWeapon.GetComponent<WeaponProperties>().Bought == true) {
                    GeneralText.active = false;
                    WeapInvInfo.active = false;
                    BulletInfo.active = false;
                    StuffInfo.active = false;
                    StuffInvInfo.active = false;
                    WeaponInfo.active = true;
                    BackButton.active = true;
                    PickSound.Play();

                    WpnGrade.GetComponent<GradeStore>().GetGrade = Mathf.RoundToInt((Efficiency - 10.0f) / ((650.0f - 10.0f) / 7));
                    WpnDoll.GetComponent<WeaponProperties>().Skin = isActiveWeapon.GetComponent<WeaponProperties>().Skin;
                   
                    WeaponInfo.transform.Find("InfoText").GetComponent<TextMesh>().text = WpnName +
                        "\nDamage: " + Damage.ToString() +
                        "\nCondition: " + Condition.ToString() +
                        "\nBullets: " + Bullets.ToString() +
                        "\n\n      Efficiency: " + Efficiency.ToString();
                }

            }
            if (hit.collider.gameObject.name == "BackToWeapInv") {
                PickSound.Play();
                WeaponInfo.active = false;
                WeapInvInfo.active = true;
            }

            if (hit.collider.gameObject.name == "BackToBulletsInv") {
                PickSound.Play();
                BulletInfo.active = false;
                WeapInvInfo.active = true;
            }

            if (hit.collider.gameObject.name == "BackToStuffInv") {
                PickSound.Play();
                StuffInfo.active = false;
                StuffInvInfo.active = true;
            }

            ///********************************************************************************************************************
            ///**************************************************** SELECT BULLETS ************************************************
            ///********************************************************************************************************************

            if (hit.collider.gameObject.layer == BulletsLayer) {
                if (isActiveBullet != null) {
                    isActiveBullet.GetComponent<BulletsProperties>().isActive = false;
                }

                isActiveBullet = hit.collider.gameObject;
                isActiveBullet.GetComponent<BulletsProperties>().isActive = true;
                isActiveBullet.GetComponent<AudioSource>().Play();
                GeneralText.active = false;
                WeapInvInfo.active = true;
                WeaponInfo.active = false;
                StuffInfo.active = false;
                StuffInvInfo.active = false;
                BulletInfo.active = true;
                for (int i = 0; i < this.GetComponent<PlayerInventory>().WeaponPlace.Length; i++) {
                    if (this.GetComponent<PlayerInventory>().WeaponPlace[i] != null) {
                        if (this.GetComponent<PlayerInventory>().WeaponPlace[i].GetComponent<WeaponProperties>().Skin == isActiveBullet.GetComponent<BulletsProperties>().Skin) {
                            BulCount = 0;
                            BulPrice = 0;
                            BulInfo = "Bullets:\n" + isActiveBullet.GetComponent<BulletsProperties>().Name.ToString() +
                                "\n\nIn stock: " + isActiveBullet.GetComponent<BulletsProperties>().Count.ToString() +
                                "\n\nPrice: " + BulPrice.ToString() + "\nCount: " + BulCount.ToString() + "\nMoney: " + this.GetComponent<PlayerInventory>().Money.ToString();

                            BulletInfo.transform.Find("InfoText").GetComponent<TextMesh>().text = BulInfo;

                            BulletInfo.transform.Find("ArrowsDown").GetComponent<ButtonSample>().isActive = false;
                            if (isActiveBullet.GetComponent<BulletsProperties>().Price > this.GetComponent<PlayerInventory>().Money) {
                                BulletInfo.transform.Find("ArrowsUp").GetComponent<ButtonSample>().isActive = false;
                            } else {
                                BulletInfo.transform.Find("ArrowsUp").GetComponent<ButtonSample>().isActive = true;
                            }
                            break;
                        } else {
                            BulInfo = "Bullets:\n" + isActiveBullet.GetComponent<BulletsProperties>().Name.ToString() +
                                "\n\nIn stock: " + isActiveBullet.GetComponent<BulletsProperties>().Count.ToString() +
                                "\n\nYou don't have \nthis kind \nof weapon";
                            BulletInfo.transform.Find("InfoText").GetComponent<TextMesh>().text = BulInfo;

                            BulletInfo.transform.Find("ArrowsDown").GetComponent<ButtonSample>().isActive = false;
                            BulletInfo.transform.Find("ArrowsUp").GetComponent<ButtonSample>().isActive = false;
                        }
                    }
                }
                WeapInvInfo.active = false;
            }
            ///********************************************************************************************************************
            ///**************************************************** SELECT STUFF **************************************************
            ///********************************************************************************************************************

            if (hit.collider.gameObject.layer == StuffLayer) {
                if (isActiveStuff != null) {
                    isActiveStuff.GetComponent<OtherStuff>().isActive = false;
                }

                isActiveStuff = hit.collider.gameObject;
                isActiveStuff.GetComponent<OtherStuff>().isActive = true;
                if (isActiveStuff.GetComponent<OtherStuff>().Bought == false) {
                    isActiveStuff.GetComponent<AudioSource>().Play();
                } else {
                    PickSound.Play();
                }
                GeneralText.active = false;
                WeapInvInfo.active = false;
                WeaponInfo.active = false;
                StuffInvInfo.active = false;
                BulletInfo.active = false;
                StuffInfo.active = true;

                StuffInfo.transform.Find("InfoText").GetComponent<TextMesh>().text = isActiveStuff.GetComponent<OtherStuff>().Description + 
                    "\n\nPrice: " + isActiveStuff.GetComponent<OtherStuff>().Price.ToString() + 
                    "\nMoney: " + this.GetComponent<PlayerInventory>().Money.ToString();

                if (isActiveStuff.GetComponent<OtherStuff>().Price < this.GetComponent<PlayerInventory>().Money) {
                    if (isActiveStuff.GetComponent<OtherStuff>().Bought == false) {
                        if (this.GetComponent<PlayerInventory>().Stuff < 9) {
                            BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = true;
                        } else {
                            BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
                        }
                    } else {
                        BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
                    }
                } else {
                    BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
                }
            }
        }
        if (BulletInfo.transform.Find("ArrowsUp").GetComponent<ButtonSample>().isPressed == true) {
            
            PickSound.Play();
            BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = true;
            BulletInfo.transform.Find("ArrowsDown").GetComponent<ButtonSample>().isActive = true;
            BulCount += 1;
            BulPrice = BulCount * isActiveBullet.GetComponent<BulletsProperties>().Price;
            if (BulCount >= isActiveBullet.GetComponent<BulletsProperties>().Count) {
                BulletInfo.transform.Find("ArrowsUp").GetComponent<ButtonSample>().isActive = false;
            }
            if (this.GetComponent<PlayerInventory>().Money - BulPrice - isActiveBullet.GetComponent<BulletsProperties>().Price < 0) {
                BulletInfo.transform.Find("ArrowsUp").GetComponent<ButtonSample>().isActive = false;
            }
            BulInfo = "Bullets:\n" + isActiveBullet.GetComponent<BulletsProperties>().Name.ToString() +
                "\n\nIn stock: " + isActiveBullet.GetComponent<BulletsProperties>().Count.ToString() +
                "\n\nPrice: " + BulPrice.ToString() + "\nCount: " + BulCount.ToString() + "\nMoney: " + this.GetComponent<PlayerInventory>().Money.ToString();

            BulletInfo.transform.Find("InfoText").GetComponent<TextMesh>().text = BulInfo;

        }

        if (BulletInfo.transform.Find("ArrowsDown").GetComponent<ButtonSample>().isPressed == true) {
            PickSound.Play();
            BulletInfo.transform.Find("ArrowsUp").GetComponent<ButtonSample>().isActive = true;
            BulCount -= 1;
            BulPrice = BulCount * isActiveBullet.GetComponent<BulletsProperties>().Price;
            if (BulCount == 0) {
                BulletInfo.transform.Find("ArrowsDown").GetComponent<ButtonSample>().isActive = false;
                BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
            }
            BulInfo = "Bullets:\n" + isActiveBullet.GetComponent<BulletsProperties>().Name.ToString() +
                "\n\nIn stock: " + isActiveBullet.GetComponent<BulletsProperties>().Count.ToString() +
                "\n\nPrice: " + BulPrice.ToString() + "\nCount: " + BulCount.ToString() + "\nMoney: " + this.GetComponent<PlayerInventory>().Money.ToString();

            BulletInfo.transform.Find("InfoText").GetComponent<TextMesh>().text = BulInfo;
        }


        ///*************************************************************************************************************************
        ///**************************************************** IF PRESS BUY *******************************************************
        ///*************************************************************************************************************************

        if (BuySlaveButton.GetComponent<ButtonSample>().isPressed == true) {
            if (PlayInv.Slaves < 10) {
                PlayInv.Slaves += 1;
                PlayInv.Money -= isActiveSlave.GetComponent<SlaveProperties>().Price;
                isActiveSlave.GetComponent<SlaveProperties>().Number = this.GetComponent<PlayerInventory>().Slaves;

                for (int s = 0; s < PlayInv.SlavePlace.Length; s++) {
                    if (PlayInv.SlavePlace[s] == null) {
                        PlayInv.SlavePlace[s] = isActiveSlave;
                        break;
                    }
                }
                //for (int sp = 0; sp < this.GetComponent<PlayerInventory>().SlavePlace.Length; sp++) {
                //    if(this.GetComponent<PlayerInventory>().SlavePlace[sp] == null) {
                //        this.GetComponent<PlayerInventory>().SlavePlace[sp] = isActiveSlave;
                //        break;
                //    }
                //}
                isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
                isActiveSlave.transform.SetParent(SlavesBoughtPanel.transform.Find("Places").transform);
                GameObject Place = SlavesBoughtPanel.transform.Find("Places").transform.GetChild(isActiveSlave.GetComponent<SlaveProperties>().Number - 1).gameObject;
                isActiveSlave.transform.localPosition = Place.transform.localPosition;
                isActiveSlave.transform.SetParent(SlavesBoughtPanel.transform.Find("Places/BoughtSlaves").transform);
                isActiveSlave.GetComponent<SlaveProperties>().Bought = true;
                Place.active = false;
                BuySlaveButton.GetComponent<ButtonSample>().isActive = false;
                MoneyInfo.text = PlayInv.Money.ToString();

                SlavePackage newPack = new SlavePackage();
                newPack.NumberOfSlave = isActiveSlave.GetComponent<SlaveProperties>().Number;
                PlayInv.SlavesBag.Add(newPack);
            }
        }

        if (BuyWeapStuffButton.GetComponent<ButtonSample>().isPressed == true) {

            ///************************************************* BUYING WEAPON ************************************************
            
            if (Switcher.GetComponent<ButtonSwitcher>().Number == 1) {
                if (this.GetComponent<PlayerInventory>().Weapons < 10) {
                    this.GetComponent<PlayerInventory>().Weapons += 1;
                    this.GetComponent<PlayerInventory>().Money -= isActiveWeapon.GetComponent<WeaponProperties>().Price;
                    isActiveWeapon.transform.Find("Lighter").gameObject.active = false;
                    //isActiveWeapon.GetComponent<WeaponProperties>().Number = this.GetComponent<PlayerInventory>().Weapons;
                    isActiveWeapon.GetComponent<WeaponProperties>().WeapName = "W_" + this.GetComponent<PlayerInventory>().Weapons.ToString();
                    isActiveWeapon.name = isActiveWeapon.GetComponent<WeaponProperties>().WeapName;
                    int wp = 0;
                    for (wp = 0; wp < this.GetComponent<PlayerInventory>().WeaponPlace.Length; wp++) {
                        if (this.GetComponent<PlayerInventory>().WeaponPlace[wp] == null) {
                            this.GetComponent<PlayerInventory>().WeaponPlace[wp] = isActiveWeapon;
                            break;
                        }
                        //wp += 1;
                    }

                    isActiveWeapon.GetComponent<WeaponProperties>().isActive = false;
                    isActiveWeapon.transform.SetParent(WeapInvInfo.transform);
                    GameObject Place = WeapInvInfo.transform.GetChild(this.GetComponent<PlayerInventory>().Weapons - 1).gameObject;
                    isActiveWeapon.transform.localPosition = Place.transform.localPosition;
                    isActiveWeapon.transform.SetParent(WeapInvInfo.transform.Find("BoughtWeapons").transform);
                    isActiveWeapon.GetComponent<WeaponProperties>().Bought = true;
                    Place.active = false;
                    BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;

                    GeneralText.active = false;
                    WeaponInfo.active = false;
                    WeapInvInfo.active = true;
                    BulletInfo.active = false;
                    StuffInvInfo.active = false;
                    StuffInfo.active = false;
                } else {
                    GeneralText.active = true;
                    WeaponInfo.active = false;
                    WeapInvInfo.active = false;
                    BulletInfo.active = false;
                    StuffInvInfo.active = false;
                    StuffInfo.active = false;
                    GeneralText.GetComponent<TextMesh>().text = "Your inventory is full \nplease choose \nanother action";
                }
            }

            ///************************************************* BUYING BULLETS ************************************************

            if (Switcher.GetComponent<ButtonSwitcher>().Number == 2) {
                for (int w = 0; w < this.GetComponent<PlayerInventory>().WeaponPlace.Length; w++) {
                    if (this.GetComponent<PlayerInventory>().WeaponPlace[w] != null) {
                        if (this.GetComponent<PlayerInventory>().WeaponPlace[w].gameObject.GetComponent<WeaponProperties>().Skin == isActiveBullet.GetComponent<BulletsProperties>().Skin) {
                            GameObject GetWeapon = this.GetComponent<PlayerInventory>().WeaponPlace[w].gameObject;
                            GetWeapon.GetComponent<WeaponProperties>().Bullets += BulCount;
                        }
                    }
                }
                isActiveBullet.GetComponent<BulletsProperties>().Count -= BulCount;
                this.GetComponent<PlayerInventory>().Money -= BulPrice;
                BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;

                GeneralText.active = false;
                WeaponInfo.active = false;
                StuffInfo.active = false;
                StuffInvInfo.active = false;
                BulletInfo.active = false;
                WeapInvInfo.active = true;

                if (isActiveBullet.GetComponent<BulletsProperties>().Count == 0) {
                    Destroy(isActiveBullet.gameObject);
                }
            }

            ///************************************************* BUYING STUFF ************************************************

            if (Switcher.GetComponent<ButtonSwitcher>().Number == 3) {
                if (this.GetComponent<PlayerInventory>().Stuff < 10) {
                    this.GetComponent<PlayerInventory>().Stuff += 1;
                    this.GetComponent<PlayerInventory>().Money -= isActiveStuff.GetComponent<OtherStuff>().Price;
                    isActiveStuff.GetComponent<OtherStuff>().Number = this.GetComponent<PlayerInventory>().Stuff;
                    isActiveStuff.name = "S_" + this.GetComponent<PlayerInventory>().Stuff.ToString();
                    isActiveStuff.GetComponent<OtherStuff>().isActive = false;
                    isActiveStuff.transform.Find("Lighter").gameObject.active = false;
                    isActiveStuff.GetComponent<OtherStuff>().Bought = true;
                    isActiveStuff.transform.SetParent(StuffInvInfo.transform);
                    GameObject Place = StuffInvInfo.transform.GetChild(this.GetComponent<PlayerInventory>().Stuff - 1).gameObject;
                    isActiveStuff.transform.localPosition = Place.transform.localPosition;
                    isActiveStuff.transform.SetParent(StuffInvInfo.transform.Find("BoughtStuff").transform);
                    Place.active = false;

                    for (int sp = 0; sp < this.GetComponent<PlayerInventory>().StuffPlace.Length; sp++) {
                        if (this.GetComponent<PlayerInventory>().StuffPlace[sp] == null) {
                            this.GetComponent<PlayerInventory>().StuffPlace[sp] = isActiveStuff;
                            break;
                        }
                    }

                    GeneralText.active = false;
                    WeapInvInfo.active = false;
                    WeaponInfo.active = false;
                    BulletInfo.active = false;
                    StuffInfo.active = false;
                    StuffInvInfo.active = true;

                    BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
                } else {
                    GeneralText.active = true;
                    WeaponInfo.active = false;
                    WeapInvInfo.active = false;
                    BulletInfo.active = false;
                    StuffInvInfo.active = false;
                    StuffInfo.active = false;
                    GeneralText.GetComponent<TextMesh>().text = "Your inventory is full \nplease choose \nanother action";
                }
            }
        }

        if (Switcher.GetComponent<ButtonSwitcher>().isPressed == true) {
            if (Switcher.GetComponent<ButtonSwitcher>().Number == 1) {
                WeaponBack.transform.Find("Weapons").gameObject.active = true;
                WeaponBack.transform.Find("Bullets").gameObject.active = false;
                WeaponBack.transform.Find("Stuff").gameObject.active = false;
                GeneralText.active = true;
                WeaponInfo.active = false;
                WeapInvInfo.active = false;
                StuffInvInfo.active = false;
                StuffInfo.active = false;
                BulletInfo.active = false;
                BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
            }
            if (Switcher.GetComponent<ButtonSwitcher>().Number == 2) {
                WeaponBack.transform.Find("Weapons").gameObject.active = false;
                WeaponBack.transform.Find("Bullets").gameObject.active = true;
                WeaponBack.transform.Find("Stuff").gameObject.active = false;
                GeneralText.active = false;
                WeaponInfo.active = false;
                WeapInvInfo.active = true;
                StuffInvInfo.active = false;
                StuffInfo.active = false;
                BulletInfo.active = false;
                BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
            }
            if (Switcher.GetComponent<ButtonSwitcher>().Number == 3) {
                WeaponBack.transform.Find("Weapons").gameObject.active = false;
                WeaponBack.transform.Find("Bullets").gameObject.active = false;
                WeaponBack.transform.Find("Stuff").gameObject.active = true;
                GeneralText.active = false;
                WeaponInfo.active = false;
                WeapInvInfo.active = false;
                StuffInvInfo.active = true;
                StuffInfo.active = false;
                BulletInfo.active = false;
                BuyWeapStuffButton.GetComponent<ButtonSample>().isActive = false;
            }
        }
    }
}
