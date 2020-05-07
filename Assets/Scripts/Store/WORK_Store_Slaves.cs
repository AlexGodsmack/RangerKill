using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WORK_Store_Slaves : MonoBehaviour
{
    
    [Header("Anchors")]
    public GameObject PosRightTop;
    public GameObject PosLeftTop;
    public GameObject PosRightBottom;
    public GameObject PosMidBottom;
    public GameObject PosLeftBottom;
    public GameObject PosMidLeft;
    public GameObject Center;

    [Header("Main Objects")]
    public GameObject MainMenuPanel;
    public GameObject InventoryProperties;
    public GameObject SLAVES;
    public GameObject INVENTORY;
    [Space]
    public GameObject AreaForSlaves;
    [Space]
    public GameObject SlavesBoughtPanel;
    public GameObject BoughtSlaves;
    [Space]
    public List<GameObject> Items;
    [Header("SourceFolders")]
    public GameObject WeapSource;
    public GameObject StffSource;
    public GameObject SlavesSource;

    [Header("Texts")]
    public GameObject GeneralText;
    public GameObject WeaponInfo;
    public GameObject WeapInvInfo;
    public GameObject StuffInvInfo;
    public GameObject BulletInfo;
    public GameObject StuffInfo;

    [Header("Menu")]
    public GameObject MenuHead;
    public GameObject MenuExit;

    [Header("Counts")]
    public int CountOfSlaves;
    public int CountOfWeapon;
    public int CountOfBullets;
    public int CountOfStuff;

    [Header("Working Elemets")]
    public GameObject isActiveSlave;
    public GameObject BuySlaveButton;
    public PlayerInventory PlayInv;
    public SaveLoadData Loader;
    public SlavesPanel SlavesEngine;
            
    public TextMesh MoneyInfo;

    private int SlavesLayer = 8;
    private int WeaponLayer = 9;
    private int BulletsLayer = 10;
    private int HeadLayer = 12;
    private int StuffLayer = 11;

    private int BulPrice;
    private int BulCount;
    private string BulInfo;

    public void OnDisable() {
        if (isActiveSlave != null) {
            isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
            isActiveSlave = null;
        }
    }

    void Start()
    {

        MainMenuPanel.transform.position = PosRightTop.transform.position + new Vector3(0, 0, 0.1f);
        MenuHead.transform.position = PosLeftTop.transform.position;
        SlavesBoughtPanel.transform.position = PosMidBottom.transform.position;

        ///*********************************************** LOAD ALL INVENTORY *****************************************************

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) { 
            Loader.LoadAll();
            Loader.SetSourceToFolders();
            foreach (SlavePackage Pack in PlayInv.SlavesBag) {
                int getPlace = 0;
                foreach (GameObject Item in Pack.Place) {
                    if (Item != null) {
                        SlaveProperties GetSlv = BoughtSlaves.transform.GetChild(Pack.NumberOfSlave - 1).GetComponent<SlaveProperties>();
                        GameObject GetPack = GetSlv.InventoryPack;
                        Item.transform.SetParent(GetPack.transform);
                        Item.transform.localPosition = GetPack.transform.GetChild(getPlace).transform.localPosition;
                        GetPack.transform.GetChild(getPlace).gameObject.active = false;
                        if (Item.GetComponent<WeaponProperties>() != null) {
                            Item.GetComponent<WeaponProperties>().Bought = true;
                            GetSlv.WeaponXRef = Item;
                        }
                        if (Item.GetComponent<OtherStuff>() != null) {
                            Item.GetComponent<OtherStuff>().Bought = true;
                        }
                        getPlace += 1;
                    }
                }
            }

        }

        if (File.Exists(Application.persistentDataPath + "/StoresStack.json")) {
            string GetInfo = File.ReadAllText(Application.persistentDataPath + "/StoresStack.json");
            StoreStack GetStore = JsonUtility.FromJson<StoreStack>(GetInfo);

            foreach (StorePoint GetStorePoint in GetStore.storePoint) {
                if (GetStorePoint.StoreID == PlayInv.StoreID) {

                    ///*********************************************** IMPORT SLAVES *****************************************************
                    foreach (SlvLot Slave in GetStorePoint.Lot1) {
                        GameObject P = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
                        P.name = "Slv_" + CountOfSlaves;
                        SlaveProperties NewSlaveProps = P.GetComponent<SlaveProperties>();
                        NewSlaveProps.Number = CountOfSlaves;
                        NewSlaveProps.Skin = Slave.Skin;
                        NewSlaveProps.Health = Slave.Health;
                        NewSlaveProps.FullHealth = Slave.FullHealth;
                        NewSlaveProps.Damage = Slave.Damage;
                        NewSlaveProps.Accuracy = Slave.Accuracy;
                        NewSlaveProps.Level = Slave.Level;
                        NewSlaveProps.Price = Slave.Price;
                        P.transform.SetParent(AreaForSlaves.transform);
                        P.transform.localPosition = new Vector3(-8.5f + 0.5f * CountOfSlaves, 0.15f, 2.5f);
                        CountOfSlaves += 1;
                        Items.Add(P.gameObject);
                    }

                }
            }
        }


    ///*********************************************** GENERATE NEW PLAYER INVENTORY *****************************************************

        SlavesEngine.LengthOfSlaves = CountOfSlaves;
        MoneyInfo.text = PlayInv.Money.ToString();
        MenuHead.GetComponent<ButtonSwitcher>().Number = 1;
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

    ///*********************************************** IMPORT ACTUAL INV TO INVENTORY PANEL *****************************************************

    void ImportNewInventory() {

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

                //int ItemPlace = 0;
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

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButtonDown(0)) {
            
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

                int HealthGrade = Mathf.RoundToInt((isActiveSlave.GetComponent<SlaveProperties>().Health - 45.0f) / ((450.0f - 45.0f) / 4));
                int DamageGrade = Mathf.RoundToInt((isActiveSlave.GetComponent<SlaveProperties>().Damage - 20.0f)/ ((100.0f - 20.0f) / 4));
                int AccuracyGrade = Mathf.RoundToInt((isActiveSlave.GetComponent<SlaveProperties>().Accuracy - 3.0f)/ ((9.0f - 3.0f) / 4));

                if (PlayInv.Money >= isActiveSlave.GetComponent<SlaveProperties>().Price) {
                    if (isActiveSlave.GetComponent<SlaveProperties>().Bought == false) {
                        if (PlayInv.Slaves < 9) {
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

            ///*************************************************************************************************************************
            ///**************************************************** IF PRESS BUY *******************************************************
            ///*************************************************************************************************************************

            if (BuySlaveButton.GetComponent<ButtonSample>().isPressed == true) {
                if (PlayInv.Slaves < 10) {
                    PlayInv.Slaves += 1;
                    PlayInv.Money -= isActiveSlave.GetComponent<SlaveProperties>().Price;
                    isActiveSlave.GetComponent<SlaveProperties>().Number = this.GetComponent<PlayerInventory>().Slaves;

                    Items.Remove(isActiveSlave.gameObject);

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

        }

    }
}
