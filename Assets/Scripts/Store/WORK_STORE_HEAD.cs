using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WORK_STORE_HEAD : MonoBehaviour
{

    [Header("PANELS")]
    public GameObject SLAVES;
    public GameObject ITEMS;
    public GameObject INVENTORY;

    [Header("Source Folders")]
    public GameObject ItemsInStore;
    public GameObject BoughtItems;
    public GameObject SlavesSource;
    public GameObject ItemsSource;

    [Header("Fields")]
    public GameObject SlaveFields;
    public GameObject ItemFields;

    [Header("Active Objects")]
    public GameObject Icon;
    public GameObject isActiveSlave;
    public GameObject isActiveItem;
    public GameObject KeepBulletsItem;
    [Space]
    public ButtonSample BuyButton;
    public ButtonSwitcher MenuSwitcher;
    public ButtonSample GoToMap;
    public int CntBullets;
    public ButtonSample Plus;
    public ButtonSample Minus;
    public int Count_of_Items;
    public int TotalPrice;

    [Header("Layers")]
    public int SlaveLayer = 8;
    public int FieldLayer = 13;
    public int ItemLayer = 18;

    [Header("Classes")]
    public MainPlayerControl PlayInv;
    public SaveLoadData Loader;
    public BulletsEngine BulletsEngine;
    public Tutorial GetTutor;

    [Header("Sounds")]
    public AudioSource PickMonitor;
    public AudioSource SetPers;
    public AudioSource TakeWeap;
    public AudioSource PutWeapPlace;
    public AudioSource PutMedicine;
    public AudioSource PutWater;
    public AudioSource PutBuff;

    [Header("Text")]
    public TextMesh Money;
    public TextMesh Info;

    public void Start() {

        Application.targetFrameRate = 60;

        Loader.LoadAll();
        Loader.LoadStoreInfo(PlayInv.StoreID);

        int FillItems = 0;
        foreach (GameObject Item in PlayInv.Items) {
            Item.transform.SetParent(ItemsInStore.transform);
            Item.transform.localPosition = ItemsInStore.transform.GetChild(FillItems).transform.localPosition + new Vector3(0, 0, -0.1f);
            FillItems += 1;
        }
        if (SLAVES != null) {
            SLAVES.GetComponent<SlaveEngine>().LengthOfSlaves = PlayInv.Items.Count;
            Greeting1();
        }

        if (ITEMS != null) {
            if (ITEMS.GetComponent<ItemEngine>() != null) {
                ITEMS.GetComponent<ItemEngine>().LenghtOfItems = PlayInv.Items.Count;
                ShowYourItems();
            } else if (ITEMS.GetComponent<BulletsEngine>() != null) {
                ITEMS.GetComponent<BulletsEngine>().LenghtOfItems = PlayInv.Items.Count;
                ShowYourItems();
            }
        }

        foreach (GameObject Slave in PlayInv.SlavePlace) {
            if (Slave != null) {
                if (SLAVES != null) {
                    Slave.transform.position = BoughtItems.transform.GetChild(Slave.transform.GetSiblingIndex()).transform.position;
                    BoughtItems.transform.GetChild(Slave.transform.GetSiblingIndex()).gameObject.active = false;
                    Slave.GetComponent<SlaveProperties>().Bought = true;
                } else {
                    Slave.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
        }
        
        if (PlayInv.TypeOfStore == "Bullets") {
            Loader.FindWeapons(BulletsEngine);
            BulletsEngine.LoadWeapons();
            BulletsEngine.Deselect();
        }
        if (PlayInv.TypeOfStore == "Guns") {
            int Plc = 0;
            foreach (GameObject Item in PlayInv.Package) {
                if (Item == null) {
                    BoughtItems.transform.GetChild(Plc).gameObject.active = true;
                } else {
                    if (Item.GetComponent<WeaponProperties>() != null) {
                        Item.GetComponent<WeaponProperties>().Bought = true;
                    }
                    if (Item.GetComponent<OtherStuff>() != null) {
                        Item.GetComponent<OtherStuff>().Bought = true;
                    }
                    Item.transform.position = BoughtItems.transform.GetChild(Plc).gameObject.transform.position;
                    BoughtItems.transform.GetChild(Plc).gameObject.active = false;
                }
                Plc++;
            }
            ShowYourItems();
        }
        if (PlayInv.TypeOfStore == "Stuff") {
            int Plc = 0;
            foreach (GameObject Item in PlayInv.Package) {
                if (Item == null) {
                    BoughtItems.transform.GetChild(Plc).gameObject.active = true;
                } else {
                    if (Item.GetComponent<WeaponProperties>() != null) {
                        Item.GetComponent<WeaponProperties>().Bought = true;
                    }
                    if (Item.GetComponent<OtherStuff>() != null) {
                        Item.GetComponent<OtherStuff>().Bought = true;
                    }
                    Item.transform.position = BoughtItems.transform.GetChild(Plc).gameObject.transform.position;
                    BoughtItems.transform.GetChild(Plc).gameObject.active = false;
                }
                Plc++;
            }
            ShowYourItems();
        }

        if (GetTutor != null) {
            if (PlayInv.If_Tutorial == true) {
                GetTutor.Steps = PlayInv.Step_Of_Tutorial;
                if (GetTutor.Steps == 1) {
                    GetTutor.enabled = true;
                    GetTutor.First_Launch = true;
                }
            }
        }

    }

    public void ShowSlaveInfo() {
        SlaveProperties Slv = isActiveSlave.GetComponent<SlaveProperties>();
        if (Info != null)
            Info.text = "health: " + Slv.Health + "       damage: " + Slv.Damage + "\naccuracy: " + Slv.Accuracy + "       price: " + Slv.Price;
    }

    public void Greeting1() {
        if (Info != null)
            Info.text = "Please, select slave you wish,\nafter that press buy. To fix\nyour troop switch to inventory";
    }

    public void ShowWeaponInfo() {
        if (ITEMS.gameObject != null) {
            if (ITEMS.active == true) {
                BoughtItems.active = false;
                if(Info != null)
                    Info.gameObject.active = true;
                ItemsSource.active = false;
                if (Icon != null) {
                    Icon.GetComponent<WeaponProperties>().Skin = isActiveItem.GetComponent<WeaponProperties>().Skin;
                }
                WeaponProperties Item = isActiveItem.GetComponent<WeaponProperties>();
                string eff = "";
                for (int a = 0; a < Item.Efficiency/10; a++) {
                    if (Item.Efficiency / 10 < 60) {
                        eff += "i";
                    }
                }
                if (Info != null) {
                    Info.text = Item.WeapName + "\n\ndamage: " + Item.Damage + "\ncondition:" + Item.Condition 
                        + "\nbullets: " + Item.Bullets + "\nEfficiency: " + Item.Efficiency + "\n" + eff + "\n\nprice: " + Item.Price;
                }

            }
        }
    }

    public void ShowYourItems() {
        if (ITEMS.gameObject != null) {
            if (ITEMS.active == true) {
                if(Info != null)
                    Info.gameObject.active = false;
                BoughtItems.active = true;
                ItemsSource.active = true;
                foreach (Transform item in ItemsSource.transform) {
                    item.gameObject.active = true;
                }
            }
        }
    }

    //================ Узнаю куплен ли предмет ================
    public bool GetBought(GameObject Item) {
        bool Bought;
        if (Item.GetComponent<WeaponProperties>() != null) {
            Bought = Item.GetComponent<WeaponProperties>().Bought;
            return Bought;
        } else if (Item.GetComponent<OtherStuff>() != null) {
            Bought = Item.GetComponent<OtherStuff>().Bought;
            return Bought;
        } else {
            return false;
        }
    }

    //================ Показываю экран покупки пуль ================
    public void BuyBulletsDisplay(BulletsProperties Bullets, WeaponProperties Weapon, int count, int totalprice) {
        Info.gameObject.active = true;
        Icon.GetComponent<WeaponProperties>().Skin = isActiveItem.GetComponent<WeaponProperties>().Skin;
        BulletsProperties GetBul = KeepBulletsItem.GetComponent<BulletsProperties>();

        Info.text = GetBul.Name + " for\n" + Weapon.WeapName + "\nBullets: " + Weapon.Bullets + "\n\nCount: " + count + "\nPrice: " + Bullets.Price + "\n\nin total: " + totalprice;
        Minus.isActive = false;
        if (Bullets.Price <= PlayInv.Money) {
            Plus.isActive = true;
        } else {
            Plus.isActive = false;
        }
    }

    public void PlusAction(int price, int count, int in_store) {
        TotalPrice = price * count;
        int PreTotal = price * (count + 1);
        if (TotalPrice <= PlayInv.Money) {
            BuyBulletsDisplay(KeepBulletsItem.GetComponent<BulletsProperties>(), isActiveItem.GetComponent<WeaponProperties>(), count, TotalPrice);
        }
        if (PreTotal > PlayInv.Money) {
            Plus.isActive = false;
            Minus.isActive = true;
            BuyButton.isActive = true;
        } else {
            if (count >= in_store) {
                Plus.isActive = false;
                Minus.isActive = true;
                BuyButton.isActive = true;
            } else {
                Plus.isActive = true;
                if (count == 0) {
                    Minus.isActive = false;
                    BuyButton.isActive = false;
                } else {
                    Minus.isActive = true;
                    BuyButton.isActive = true;
                }
            }
        }
    }



    //================ Скрываю дисплей с покупкой пуль ================
    public void HideBulletsDisplay() {
        Plus.isActive = false;
        Minus.isActive = false;
        Info.gameObject.active = false;
        BoughtItems.active = true;
    }

    //================ Скрываю предметы в инвентаре раба ================
    public void HideBag(GameObject ActivePers) {
        SlaveProperties GetSlv = ActivePers.GetComponent<SlaveProperties>();
        GameObject Bag = GetSlv.InventoryPack.gameObject;
        foreach (Transform Place in Bag.transform) {
            if (Place.transform.childCount != 0) {
                Place.transform.GetChild(0).gameObject.active = false;
            }
        }
    }

    //================ Активирую предметы в инвентаре раба ================
    public void ShowBag(GameObject ActivePers) {
        SlaveProperties GetSlv = ActivePers.GetComponent<SlaveProperties>();
        GameObject Bag = GetSlv.InventoryPack.gameObject;
        foreach (Transform Place in Bag.transform) {
            if (Place.transform.childCount != 0) {
                Place.transform.GetChild(0).gameObject.active = true;
            }
        }
    }

    public void Update() {

        Money.text = PlayInv.Money.ToString();

        if (Input.GetMouseButtonDown(0)) {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            //============================================================== SELECT SLAVE ================================================================
            
            if (hit.collider.gameObject.layer == SlaveLayer) {
                //================ Если кликнул на выделенного раба ================
                if (isActiveSlave == hit.collider.gameObject) {
                    isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
                    foreach (Transform Field in SlaveFields.transform) {
                        Field.GetComponent<Fields>().isActive = false;
                    }
                    foreach (Transform Field in ItemFields.transform) {
                        Field.GetComponent<Fields>().isActive = false;
                    }
                    isActiveSlave = null;
                    if (BuyButton != null) {
                        BuyButton.isActive = false;
                    }
                    Greeting1();
                } else {
                //================ Если новое выделение ================
                    if (isActiveSlave != null) {
                        isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
                        isActiveSlave = null;
                    }
                    isActiveSlave = hit.collider.gameObject;
                    SlaveProperties GetProp = isActiveSlave.GetComponent<SlaveProperties>();
                    GetProp.isActive = true;

                    foreach (Transform Field in SlaveFields.transform) {
                        Field.GetComponent<Fields>().isActive = true;
                    }

                    ShowSlaveInfo();

                    if (GetProp.Price <= PlayInv.Money) {
                        if (GetProp.Bought == false) {
                            foreach (GameObject slvplace in PlayInv.SlavePlace) {
                                if (slvplace == null) {
                                    if(BuyButton != null)
                                        BuyButton.isActive = true;
                                        break;
                                }
                            }
                        }
                    }
                    //============================================================== DROP ITEM ================================================================
                    if (isActiveItem != null) {
                        
                        GameObject Pack = GetProp.InventoryPack.gameObject;

                        foreach (Transform Place in Pack.transform) {
                            if (Place.transform.childCount == 0) {
                                //==================== если опускаемый предмет пушка ======================
                                if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                                    if (isActiveSlave.GetComponent<SlaveProperties>().HaveGun == false) {
                                        WeaponProperties prop = isActiveItem.GetComponent<WeaponProperties>();
                                        isActiveItem.transform.SetParent(Place.transform);
                                        isActiveItem.transform.localPosition = new Vector3(0, 0, 0);
                                        prop.isActive = false;
                                        prop.Bought = true;
                                        GetProp.HaveGun = true;
                                        GetProp.WeaponXRef = isActiveItem;
                                        PutWeapPlace.Play();
                                        int removePlace = 0;
                                        foreach (GameObject Item in PlayInv.Package) {
                                            if (Item == isActiveItem) {
                                                PlayInv.Package[removePlace] = null;
                                            }
                                            removePlace++;
                                        }
                                    } else {
                                        isActiveItem.GetComponent<WeaponProperties>().isActive = false;
                                        isActiveItem = null;
                                        foreach (Transform Fields in ItemFields.transform) {
                                            Fields.GetComponent<Fields>().isActive = false;
                                        }
                                        break;
                                    }
                                }
                                //==================== другие опускаемые предметы ======================
                                if (isActiveItem.GetComponent<OtherStuff>() != null) {
                                    OtherStuff prop = isActiveItem.GetComponent<OtherStuff>();
                                    isActiveItem.transform.SetParent(Place.transform);
                                    isActiveItem.transform.localPosition = new Vector3(0, 0, 0);
                                    prop.isActive = false;
                                    prop.Bought = true;
                                    if (prop.Skin == 1) {
                                        PutMedicine.Play();
                                    }
                                    if (prop.Skin == 2) {
                                        PutWater.Play();
                                    }
                                    if (prop.Skin == 3) {
                                        PutBuff.Play();
                                    }
                                    int removePlace = 0;
                                    foreach (GameObject Item in PlayInv.Package) {
                                        if (Item == isActiveItem) {
                                            PlayInv.Package[removePlace] = null;
                                        }
                                        removePlace++;
                                    }
                                }
                                //================ Закрываем поля с предметами ================
                                foreach (Transform Field in ItemFields.transform) {
                                    Field.GetComponent<Fields>().isActive = false;
                                }
                                isActiveItem = null;
                                break;
                            }
                        }
                    }
                    //===== Скрываю все предметы в слотах рюкзака =====
                    HideBag(isActiveSlave);
                }

            }

            //============================================================== SELECT ITEM ================================================================

            if (hit.collider.gameObject.layer == ItemLayer) {

                //==================== Снимаю выделение с раба ====================
                bool getbought = GetBought(hit.collider.gameObject);
                if (getbought == false) {
                    if (isActiveSlave != null) {
                        isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
                        isActiveSlave = null;
                    }
                }

                //==================== Если клинкнул на выделенный предмет ====================
                if (isActiveItem == hit.collider.gameObject) {
                    if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                        isActiveItem.GetComponent<WeaponProperties>().isActive = false;
                        foreach (Transform Field in ItemFields.transform) {
                            Field.GetComponent<Fields>().isActive = false;
                        }
                        if (isActiveItem.GetComponent<WeaponProperties>().Bought == true) {
                            PickMonitor.Play();
                        } else {
                            TakeWeap.Play();
                        }
                        isActiveItem = null;
                        ShowYourItems();
                    } else if (isActiveItem.GetComponent<OtherStuff>() != null) {
                        isActiveItem.GetComponent<OtherStuff>().isActive = false;
                        foreach (Transform Field in ItemFields.transform) {
                            Field.GetComponent<Fields>().isActive = false;
                        }
                        if (isActiveItem.GetComponent<OtherStuff>().Bought == true) {
                            PickMonitor.Play();
                        }
                        isActiveItem = null;
                        ShowYourItems();
                    } else if (isActiveItem.GetComponent<BulletsProperties>() != null) {
                        isActiveItem.GetComponent<BulletsProperties>().isActive = false;
                        isActiveItem = null;
                        HideBulletsDisplay();
                        KeepBulletsItem = null;
                        BulletsEngine.Deselect();
                    }
                    if (BuyButton != null) {
                        BuyButton.isActive = false;
                    }
                } else {
                    //==================== Если выделение на новом объекте ====================

                    if (isActiveItem != null) {
                        if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                            isActiveItem.GetComponent<WeaponProperties>().isActive = false;
                        }
                        if (isActiveItem.GetComponent<OtherStuff>() != null) {
                            isActiveItem.GetComponent<OtherStuff>().isActive = false;
                        }
                        if (isActiveItem.GetComponent<BulletsProperties>() != null) {
                            if (hit.collider.gameObject.GetComponent<BulletsProperties>() != null) {
                                isActiveItem.GetComponent<BulletsProperties>().isActive = false;
                            } else {
                                KeepBulletsItem = isActiveItem;
                            }
                        }
                        if(BuyButton != null) {
                            BuyButton.isActive = false;
                        }
                        isActiveItem = null;
                    }


                    isActiveItem = hit.collider.gameObject;
                    //TakeWeap.Play();
                    int price = 0;
                    bool bought = false;

                    if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                        isActiveItem.GetComponent<WeaponProperties>().isActive = true;
                        price = isActiveItem.GetComponent<WeaponProperties>().Price;
                        bought = isActiveItem.GetComponent<WeaponProperties>().Bought;
                        if (isActiveItem.GetComponent<WeaponProperties>().Bought == true) {
                            PickMonitor.Play();
                        }
                        if (KeepBulletsItem == null) {
                            ShowWeaponInfo();
                        }else if (KeepBulletsItem != null) {
                            PickMonitor.Play();
                            BoughtItems.active = false;
                            BuyBulletsDisplay(KeepBulletsItem.GetComponent<BulletsProperties>(), isActiveItem.GetComponent<WeaponProperties>(), 0, 0);
                        }
                        if (GetTutor != null) {
                            if (GetTutor.Steps == 41) {
                                GetTutor.Steps += 1;
                                GetTutor.enabled = false;
                                GetTutor.enabled = true;
                                GetTutor.PickMonitor.Play();
                            }
                        }
                    }
                    if (isActiveItem.GetComponent<OtherStuff>() != null) {
                        isActiveItem.GetComponent<OtherStuff>().isActive = true;
                        price = isActiveItem.GetComponent<OtherStuff>().Price;
                        bought = isActiveItem.GetComponent<OtherStuff>().Bought;
                        if (isActiveItem.GetComponent<OtherStuff>().Bought == true) {
                            PickMonitor.Play();
                        }
                    }
                    if (isActiveItem.GetComponent<BulletsProperties>() != null) {
                        isActiveItem.GetComponent<BulletsProperties>().isActive = true;
                        price = isActiveItem.GetComponent<BulletsProperties>().Price;
                        HideBulletsDisplay();
                        BulletsEngine.ShowMatchWeapons(isActiveItem);
                        if (KeepBulletsItem != null) {
                            if (KeepBulletsItem == isActiveItem) {
                                KeepBulletsItem.GetComponent<BulletsProperties>().isActive = false;
                                KeepBulletsItem = null;
                                isActiveItem = null;
                                BulletsEngine.Deselect();
                            } else {
                                KeepBulletsItem.GetComponent<BulletsProperties>().isActive = false;
                                KeepBulletsItem = null;
                                BulletsEngine.ShowMatchWeapons(isActiveItem);
                            }
                        }
                        if (GetTutor != null) {
                            if (GetTutor.Steps == 40) {
                                GetTutor.Selected = isActiveItem;
                                GetTutor.Steps += 1;
                                GetTutor.Main.active = true;
                                //GetTutor.enabled = false;
                                //GetTutor.enabled = true;
                                GetTutor.PickMonitor.Play();
                            }
                        }
                        price = PlayInv.Money + 1;
                        Count_of_Items = 0;
                    }
                    foreach (Transform Field in ItemFields.transform) {
                        Field.GetComponent<Fields>().isActive = true;
                    }

                    //==================== Активирую кнопку Купить ====================
                    if (price <= PlayInv.Money) {
                        if (bought == false) {
                            foreach (GameObject slvplace in PlayInv.Package) {
                                if (slvplace == null) {
                                    if(BuyButton != null)
                                        BuyButton.isActive = true;
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            //============================================================== DROP ITEM TO FIELD ================================================================

            if (hit.collider.gameObject.layer == FieldLayer) {

                if (isActiveItem != null) {
                    if (hit.collider.gameObject.transform.parent.gameObject == ItemFields.gameObject) {

                        //============== Проверяю куплен ли предмет ==============
                        bool Bought = GetBought(isActiveItem);
                        if (Bought == false) {
                            GameObject FieldPlace = hit.collider.gameObject;
                            isActiveItem.transform.position = FieldPlace.transform.position + new Vector3(0, 0, -0.1f);
                            int GetNum = 0;
                            foreach (GameObject item in PlayInv.Package) {
                                if (item == isActiveItem) {
                                    PlayInv.Package[GetNum] = null;
                                }
                                GetNum += 1;
                            }
                            //=================== Отключаю поля в инвентаре ===================
                            foreach (Transform Field in ItemFields.transform) {
                                Field.GetComponent<Fields>().isActive = false;
                            }
                            //==================== Кладу предмет в головном файле ====================
                            PlayInv.Package[FieldPlace.transform.GetSiblingIndex()] = isActiveItem;

                            if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                                PutWeapPlace.Play();
                                isActiveItem.GetComponent<WeaponProperties>().isActive = false;
                                isActiveItem = null;
                            } else if (isActiveItem.GetComponent<OtherStuff>() != null) {
                                if (isActiveItem.GetComponent<OtherStuff>().Skin == 1) {
                                    PutMedicine.Play();
                                }
                                if (isActiveItem.GetComponent<OtherStuff>().Skin == 2) {
                                    PutWater.Play();
                                }
                                if (isActiveItem.GetComponent<OtherStuff>().Skin == 3) {
                                    PutBuff.Play();
                                }
                                isActiveItem.GetComponent<OtherStuff>().isActive = false;
                                isActiveItem = null;
                            }
                        } else {
                            //============== Если премдет был куплен ==============
                            isActiveItem.transform.SetParent(ItemsSource.transform);
                            if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                                isActiveItem.GetComponent<WeaponProperties>().Bought = false;
                                isActiveItem.GetComponent<WeaponProperties>().isActive = false;
                                if (isActiveSlave != null) {
                                    isActiveSlave.GetComponent<SlaveProperties>().HaveGun = false;
                                    isActiveSlave.GetComponent<SlaveProperties>().WeaponXRef = null;
                                }
                                PutWeapPlace.Play();
                            } else if (isActiveItem.GetComponent<OtherStuff>() != null) {
                                isActiveItem.GetComponent<OtherStuff>().Bought = false;
                                isActiveItem.GetComponent<OtherStuff>().isActive = false;
                                OtherStuff getstf = isActiveItem.GetComponent<OtherStuff>();
                                if (getstf.Skin == 1) {
                                    PutMedicine.Play();
                                }
                                if (getstf.Skin == 2) {
                                    PutWater.Play();
                                }
                                if (getstf.Skin == 3) {
                                    PutBuff.Play();
                                }
                            }
                            GameObject FieldPlace = hit.collider.gameObject;
                            isActiveItem.transform.position = FieldPlace.transform.position + new Vector3(0, 0, -0.1f);
                            foreach (Transform Field in ItemFields.transform) {
                                Field.GetComponent<Fields>().isActive = false;
                            }
                            foreach (Transform Field in SlaveFields.transform) {
                                Field.GetComponent<Fields>().isActive = false;
                            }
                            if (isActiveSlave != null) {
                                isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
                                isActiveSlave = null;
                            }
                            PlayInv.Package[FieldPlace.transform.GetSiblingIndex()] = isActiveItem;
                            isActiveItem = null;
                            if (GetTutor != null) {
                                if (GetTutor.Steps == 29) {
                                    GetTutor.Steps += 1;
                                    GetTutor.Main.active = false;
                                    GetTutor.Main.active = true;
                                    PickMonitor.Play();
                                }
                            }
                        }
                    }
                }

            }

        }

        //============================================================== DROP SLAVE TO FIELD ================================================================

        if (Input.GetMouseButtonUp(0)) {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (isActiveSlave != null) {
                ShowBag(isActiveSlave);
            }

            if (hit.collider.gameObject.layer == FieldLayer) {
                if (isActiveSlave != null) {
                    if (hit.collider.gameObject.transform.parent.gameObject == SlaveFields.gameObject) {
                        GameObject FieldPlace = hit.collider.gameObject;
                        isActiveSlave.transform.position = FieldPlace.transform.position + new Vector3(0, 0, -0.1f);
                        int GetNum = 0;
                        foreach (GameObject Slv in PlayInv.SlavePlace) {
                            if (Slv == isActiveSlave) {
                                PlayInv.SlavePlace[GetNum] = null;
                            }
                            GetNum += 1;
                        }
                        foreach (Transform Field in SlaveFields.transform) {
                            Field.GetComponent<Fields>().isActive = false;
                        }
                        PlayInv.SlavePlace[FieldPlace.transform.GetSiblingIndex()] = isActiveSlave;
                        isActiveSlave.GetComponent<SlaveProperties>().isActive = false;

                        isActiveSlave = null;
                        SetPers.Play();
                    }
                }
                if (isActiveItem != null) {
                    if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                        isActiveItem.GetComponent<WeaponProperties>().isActive = false;
                    } else if (isActiveItem.GetComponent<OtherStuff>() != null) {
                        isActiveItem.GetComponent<OtherStuff>().isActive = false;
                    }
                    isActiveItem = null;
                    foreach (Transform Field in ItemFields.transform) {
                        Field.GetComponent<Fields>().isActive = false;
                    }
                }
                if (GetTutor != null) {
                    if (GetTutor.Steps == 32) {
                        GetTutor.Steps += 1;
                        GetTutor.enabled = false;
                        GetTutor.enabled = true;
                        GetTutor.PickMonitor.Play();
                    }
                }
            }
        }

        //============================================================== PLUS/MINUS ================================================================

        if (Plus != null && Minus != null) {
            if (Plus.isPressed == true) {
                int price = KeepBulletsItem.GetComponent<BulletsProperties>().Price;
                int Count_in_Store = KeepBulletsItem.GetComponent<BulletsProperties>().Count;
                Count_of_Items += 1;
                PlusAction(price, Count_of_Items, Count_in_Store);
                if (GetTutor != null) {
                    if (GetTutor.Steps == 42) {
                        GetTutor.Steps += 1;
                        GetTutor.enabled = false;
                        GetTutor.enabled = true;
                    }
                }
            }

            if (Minus.isPressed == true) {
                int price = KeepBulletsItem.GetComponent<BulletsProperties>().Price;
                int Count_in_Store = KeepBulletsItem.GetComponent<BulletsProperties>().Count;
                Count_of_Items -= 1;
                PlusAction(price, Count_of_Items, Count_in_Store);
            }
        }

        //============================================================== CLICK BUY ================================================================
        if (BuyButton != null) {
            if (BuyButton.isPressed == true) {
                if (isActiveSlave != null) {
                    isActiveSlave.transform.SetParent(SlavesSource.transform);
                    int SetPlace = 0;
                    foreach (GameObject SlvPlace in PlayInv.SlavePlace) {
                        if (SlvPlace == null) {
                            PlayInv.SlavePlace[SetPlace] = isActiveSlave;
                            break;
                        }
                        SetPlace += 1;
                    }
                    PlayInv.Money -= isActiveSlave.GetComponent<SlaveProperties>().Price;
                    isActiveSlave.GetComponent<SlaveProperties>().Bought = true;
                    isActiveSlave.transform.position = BoughtItems.transform.GetChild(isActiveSlave.transform.GetSiblingIndex()).transform.position;
                    BoughtItems.transform.GetChild(isActiveSlave.transform.GetSiblingIndex()).gameObject.active = false;
                    isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
                    PlayInv.Items.Remove(isActiveSlave);
                    isActiveSlave = null;
                    BuyButton.isActive = false;
                    Greeting1();
                }
                if (isActiveItem != null) {
                    if (BulletsEngine == null) {
                        isActiveItem.transform.SetParent(ItemsSource.transform);
                    }

                    int SetPlace = 0;
                    if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                        if (KeepBulletsItem == null) {
                            foreach (GameObject ItemPlace in PlayInv.Package) {
                                if (ItemPlace == null) {
                                    PlayInv.Package[SetPlace] = isActiveItem;
                                    PlayInv.Money -= isActiveItem.GetComponent<WeaponProperties>().Price;
                                    isActiveItem.GetComponent<WeaponProperties>().isActive = false;
                                    isActiveItem.GetComponent<WeaponProperties>().Bought = true;
                                    isActiveItem.transform.position = BoughtItems.transform.GetChild(SetPlace).transform.position;
                                    BoughtItems.transform.GetChild(SetPlace).gameObject.active = false;
                                    PlayInv.Items.Remove(isActiveItem);
                                    break;
                                }
                                SetPlace++;
                            }
                        } else {
                            PlayInv.Money -= TotalPrice;
                            Debug.Log(TotalPrice);
                            isActiveItem.GetComponent<WeaponProperties>().Bullets += Count_of_Items;
                            WeaponProperties WeapRef = isActiveItem.GetComponent<WeaponProperties>().WeaponXRef.gameObject.GetComponent<WeaponProperties>();
                            WeapRef.Bullets += Count_of_Items;
                            KeepBulletsItem.GetComponent<BulletsProperties>().Count -= Count_of_Items;
                            if (KeepBulletsItem.GetComponent<BulletsProperties>().Count <= 0) {
                                PlayInv.Items.Remove(KeepBulletsItem);
                                Destroy(KeepBulletsItem);
                            } else {
                                KeepBulletsItem.GetComponent<BulletsProperties>().isActive = false;
                            }
                            BulletsEngine.Deselect();
                            HideBulletsDisplay();
                        }
                    } else if (isActiveItem.GetComponent<OtherStuff>() != null) {
                        foreach (GameObject ItemPlace in PlayInv.Package) {
                            if (ItemPlace == null) {
                                PlayInv.Package[SetPlace] = isActiveItem;
                                PlayInv.Money -= isActiveItem.GetComponent<OtherStuff>().Price;
                                isActiveItem.GetComponent<OtherStuff>().isActive = false;
                                isActiveItem.GetComponent<OtherStuff>().Bought = true;
                                isActiveItem.transform.position = BoughtItems.transform.GetChild(SetPlace).transform.position;
                                BoughtItems.transform.GetChild(SetPlace).gameObject.active = false;
                                PlayInv.Items.Remove(isActiveItem);
                                break;
                            }
                            SetPlace++;
                        }
                    }
                    if (KeepBulletsItem != null) {
                        KeepBulletsItem = null;
                    }
                    isActiveItem = null;
                    BuyButton.isActive = false;
                    ShowYourItems();
                    if (GetTutor != null) {
                        if (GetTutor.Steps == 24 || GetTutor.Steps == 43) {
                            GetTutor.Steps += 1;
                            GetTutor.enabled = false;
                            GetTutor.enabled = true;
                            GetTutor.PickMonitor.Play();
                        }
                    }
                }
            }
        }

        //============================================================== SWITCH PANELS ================================================================

        if (MenuSwitcher != null) {
            if (MenuSwitcher.isPressed == true) {
                if (isActiveSlave != null) {
                    isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
                    isActiveSlave = null;
                }
                if (isActiveItem != null) {
                    if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                        isActiveItem.GetComponent<WeaponProperties>().isActive = false;
                    }
                    if (isActiveItem.GetComponent<OtherStuff>() != null) {
                        isActiveItem.GetComponent<OtherStuff>().isActive = false;
                    }
                    if (isActiveItem.GetComponent<BulletsProperties>() != null) {
                        isActiveItem.GetComponent<BulletsProperties>().isActive = false;
                    }
                    if (KeepBulletsItem != null) {
                        KeepBulletsItem.GetComponent<BulletsProperties>().isActive = false;
                        KeepBulletsItem = null;
                    }
                    isActiveItem = null;
                }
                foreach (Transform Field in SlaveFields.transform) {
                    Field.GetComponent<Fields>().isActive = false;
                }
                foreach (Transform Field in ItemFields.transform) {
                    Field.GetComponent<Fields>().isActive = false;
                }
                if (BulletsEngine != null) {
                    BulletsEngine.Deselect();
                }
                BuyButton.isActive = false;
                Greeting1();
                if (GetTutor != null) {
                    if (GetTutor.Steps == 6 || GetTutor.Steps == 26) {
                        GetTutor.Steps += 1;
                        GetTutor.enabled = false;
                        GetTutor.enabled = true;
                        GetTutor.PickMonitor.Play();
                    }
                }
            }
        } else {
            INVENTORY.active = true;
        }

        if (GoToMap.isPressed == true) {
            if (GetTutor != null) {
                if (GetTutor.Steps == 15 || GetTutor.Steps == 33 || GetTutor.Steps == 44) {
                    GetTutor.Steps += 1;
                    GetTutor.PickMonitor.Play();
                }
            }
            Loader.SaveAll();
            Loader.SaveStoresInfo();
            SceneManager.LoadScene(5);
        }

    }

}
