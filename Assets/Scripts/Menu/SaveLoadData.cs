using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadData : MonoBehaviour
{

    //public GameObject GotoMapButton;
    public MainPlayerControl PlayInv;
    public Tutorial Tutor;
    //public WORK_Store StoreStack;
    public GameObject ItemsSource;
    public GameObject SlaveSource;

    //======================================================= SAVE INVENTORY ===========================================================

    public void SaveAll() {
        PlayerSource newInventory = new PlayerSource();
        newInventory.Money = PlayInv.Money;
        newInventory.CurrentStore = PlayInv.StoreID;
        newInventory.If_Tutorial = PlayInv.If_Tutorial;
        newInventory.Step_Of_Tutorial = Tutor.Steps;

        foreach (GameObject Slave in PlayInv.SlavePlace) {
            SlaveDoll newSlave = new SlaveDoll();
            if (Slave != null) {
                SlaveProperties GetSlv = Slave.GetComponent<SlaveProperties>();
                newSlave.StatusEmpty = false;
                newSlave.Number = GetSlv.Number;
                newSlave.Health = GetSlv.Health;
                newSlave.FullHealth = GetSlv.FullHealth;
                newSlave.Damage = GetSlv.Damage;
                newSlave.Accuracy = GetSlv.Accuracy;
                newSlave.Battles = GetSlv.Battles;
                newSlave.Level = GetSlv.Level;
                newSlave.Skin = GetSlv.Skin;
                newSlave.Price = GetSlv.Price;
                newSlave.Efficiency = GetSlv.Efficiency;
                newSlave.WeaponSkin = GetSlv.WeaponSkin;
                newSlave.HaveGun = GetSlv.HaveGun;
                newSlave.FullPackage = GetSlv.FullPackage;
                newSlave.Start_Fhp = GetSlv.Start_Fhp;
                newSlave.Start_Dmg = GetSlv.Start_Dmg;
                newSlave.Start_Acc = GetSlv.Start_Acc;
                newSlave.Heal_Units = GetSlv.Heal_Units;
                newSlave.Shot_Units = GetSlv.Shot_Units;
                newSlave.Rush_Units = GetSlv.Rush_Units;
                int GetStuffPlace = 0;
                GetSlv.InventoryPack.gameObject.active = true;
                foreach (Transform Place in GetSlv.InventoryPack.transform) {
                    if (Place.transform.childCount != 0) {
                        GameObject GetItem = Place.transform.GetChild(0).gameObject;
                        if (GetItem.GetComponent<WeaponProperties>() != null) {
                            ItemDoll GetPlace = new ItemDoll();
                            WeaponProperties GetWpn = GetItem.GetComponent<WeaponProperties>();
                            GetPlace.TypeOfItem = "Weapon";
                            GetPlace.Skin = GetWpn.Skin;
                            GetPlace.Name = GetWpn.WeapName;
                            GetPlace.Damage = GetWpn.Damage;
                            GetPlace.Condition = GetWpn.Condition;
                            GetPlace.Efficiency = GetWpn.Efficiency;
                            GetPlace.Price = GetWpn.Price;
                            GetPlace.Bullets = GetWpn.Bullets;
                            newSlave.Package[Place.transform.GetSiblingIndex()] = GetPlace;
                        }
                        if (GetItem.GetComponent<OtherStuff>() != null) {
                            ItemDoll GetPlace = new ItemDoll();
                            OtherStuff GetStf = GetItem.GetComponent<OtherStuff>();
                            GetPlace.TypeOfItem = "Stuff";
                            GetPlace.Skin = GetStf.Skin;
                            GetPlace.Price = GetStf.Price;
                            GetPlace.Liters = GetStf.Liters;
                            GetPlace.Name = GetStf.Name;
                            newSlave.Package[Place.transform.GetSiblingIndex()] = GetPlace;
                        }
                    }
                }
            } else {
                newSlave.StatusEmpty = true;
            }
            newInventory.Slaves.Add(newSlave);
        }

        int NumPlace = 0;
        foreach (GameObject Item in PlayInv.Package) {
            if (Item != null) {
                if (Item.GetComponent<WeaponProperties>() != null) {
                    ItemDoll GetPlace = new ItemDoll();
                    WeaponProperties GetWpn = Item.GetComponent<WeaponProperties>();
                    GetPlace.TypeOfItem = "Weapon";
                    GetPlace.Skin = GetWpn.Skin;
                    GetPlace.Name = GetWpn.WeapName;
                    GetPlace.Damage = GetWpn.Damage;
                    GetPlace.Condition = GetWpn.Condition;
                    GetPlace.Efficiency = GetWpn.Efficiency;
                    GetPlace.Price = GetWpn.Price;
                    GetPlace.Bullets = GetWpn.Bullets;
                    newInventory.Items[NumPlace] = GetPlace;
                }
                if (Item.GetComponent<OtherStuff>() != null) {
                    ItemDoll GetPlace = new ItemDoll();
                    OtherStuff GetStf = Item.GetComponent<OtherStuff>();
                    GetPlace.TypeOfItem = "Stuff";
                    GetPlace.Skin = GetStf.Skin;
                    GetPlace.Price = GetStf.Price;
                    GetPlace.Liters = GetStf.Liters;
                    GetPlace.Name = GetStf.Name;
                    newInventory.Items[NumPlace] = GetPlace;
                }
            }
            NumPlace += 1;
        }

        string SaveData = JsonUtility.ToJson(newInventory);
        StreamWriter WriteData = new StreamWriter(Application.persistentDataPath + "/PlayerData.json");
        WriteData.Write(SaveData);
        WriteData.Close();
    }

    //======================================================= LOAD INVENTORY ===========================================================

    public void LoadAll() {
        string json = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json").ToString();
        PlayerSource LoadData = JsonUtility.FromJson<PlayerSource>(json);

        PlayInv.Money = LoadData.Money;
        PlayInv.StoreID = LoadData.CurrentStore;
        PlayInv.If_Tutorial = LoadData.If_Tutorial;
        PlayInv.Step_Of_Tutorial = LoadData.Step_Of_Tutorial;

        int GetSlave = 0;
        foreach (SlaveDoll LoadSlave in LoadData.Slaves) {
            if (LoadSlave.StatusEmpty == false) {
                GameObject Slave = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
                Slave.name = "Slv_" + GetSlave;
                SlaveProperties SlvProp = Slave.GetComponent<SlaveProperties>();
                SlvProp.Number = LoadSlave.Number;
                SlvProp.Health = LoadSlave.Health;
                SlvProp.FullHealth = LoadSlave.FullHealth;
                SlvProp.Damage = LoadSlave.Damage;
                SlvProp.Accuracy = LoadSlave.Accuracy;
                SlvProp.Battles = LoadSlave.Battles;
                SlvProp.Level = LoadSlave.Level;
                SlvProp.Skin = LoadSlave.Skin;
                SlvProp.Price = LoadSlave.Price;
                SlvProp.Efficiency = LoadSlave.Efficiency;
                SlvProp.WeaponSkin = LoadSlave.WeaponSkin;
                SlvProp.HaveGun = LoadSlave.HaveGun;
                SlvProp.FullPackage = LoadSlave.FullPackage;
                SlvProp.Start_Fhp = LoadSlave.Start_Fhp;
                SlvProp.Start_Dmg = LoadSlave.Start_Dmg;
                SlvProp.Start_Acc = LoadSlave.Start_Acc;
                SlvProp.Shot_Units = LoadSlave.Shot_Units;
                SlvProp.Heal_Units = LoadSlave.Heal_Units;
                SlvProp.Rush_Units = LoadSlave.Rush_Units;

                GameObject Pack = SlvProp.InventoryPack.gameObject;
                int PackPlace = 0;
                foreach (ItemDoll Item in LoadSlave.Package) {
                    if (Item != null) {
                        if (Item.TypeOfItem == "Weapon") {
                            GameObject Wpn = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
                            WeaponProperties WpnProp = Wpn.GetComponent<WeaponProperties>();
                            WpnProp.Skin = Item.Skin;
                            WpnProp.WeapName = Item.Name;
                            WpnProp.Damage = Item.Damage;
                            WpnProp.Condition = Item.Condition;
                            WpnProp.Bullets = Item.Bullets;
                            WpnProp.Efficiency = Item.Efficiency;
                            WpnProp.Price = Item.Price;
                            WpnProp.Bought = true;
                            Wpn.name = WpnProp.WeapName + PackPlace;
                            Wpn.transform.SetParent(Pack.transform.GetChild(PackPlace).transform);
                            Wpn.transform.localPosition = new Vector3(0, 0, 0);
                            SlvProp.WeaponXRef = Wpn;
                        }
                        if (Item.TypeOfItem == "Stuff") {
                            GameObject Stuff = Instantiate(Resources.Load("OtherStuff")) as GameObject;
                            OtherStuff StfProp = Stuff.GetComponent<OtherStuff>();
                            StfProp.Skin = Item.Skin;
                            StfProp.Name = Item.Name;
                            StfProp.Liters = Item.Liters;
                            StfProp.Price = Item.Price;
                            StfProp.Bought = true;
                            Stuff.name = StfProp.Name + PackPlace;
                            Stuff.transform.SetParent(Pack.transform.GetChild(PackPlace).transform);
                            Stuff.transform.localPosition = new Vector3(0, 0, 0);
                        }
                    }
                    PackPlace += 1;
                }

                PlayInv.SlavePlace[GetSlave] = Slave;
                Slave.transform.SetParent(SlaveSource.transform);
            }
            GetSlave += 1;
        }

        if (ItemsSource != null) {
            int NumItem = 0;
            foreach (ItemDoll Item in LoadData.Items) {
                if (Item != null) {
                    if (Item.TypeOfItem == "Weapon") {
                        GameObject Wpn = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
                        WeaponProperties WpnProp = Wpn.GetComponent<WeaponProperties>();
                        WpnProp.Skin = Item.Skin;
                        WpnProp.WeapName = Item.Name;
                        WpnProp.Damage = Item.Damage;
                        WpnProp.Condition = Item.Condition;
                        WpnProp.Bullets = Item.Bullets;
                        WpnProp.Efficiency = Item.Efficiency;
                        WpnProp.Price = Item.Price;
                        WpnProp.Bought = true;
                        Wpn.name = WpnProp.WeapName + NumItem;
                        PlayInv.Package[NumItem] = Wpn;
                        Wpn.transform.SetParent(ItemsSource.transform);
                        Wpn.transform.localPosition = new Vector3(0, 0, 0);
                    }
                    if (Item.TypeOfItem == "Stuff") {
                        GameObject Stuff = Instantiate(Resources.Load("OtherStuff")) as GameObject;
                        OtherStuff StfProp = Stuff.GetComponent<OtherStuff>();
                        StfProp.Skin = Item.Skin;
                        StfProp.Name = Item.Name;
                        StfProp.Liters = Item.Liters;
                        StfProp.Price = Item.Price;
                        StfProp.Bought = true;
                        Stuff.name = StfProp.Name + NumItem;
                        PlayInv.Package[NumItem] = Stuff;
                        Stuff.transform.SetParent(ItemsSource.transform);
                        Stuff.transform.localPosition = new Vector3(0, 0, 0);
                    }
                }
                NumItem += 1;
            }
        }

    }

    //======================================================= LOAD STORE ITEMS ===========================================================

    public void LoadStoreInfo(int StoreID) {
        if (File.Exists(Application.persistentDataPath + "/StoresStack.json")) {
            if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {

                string GetPlayInfo = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
                PlayerSource GetPlaySource = JsonUtility.FromJson<PlayerSource>(GetPlayInfo);

                string GetStoresInfo = File.ReadAllText(Application.persistentDataPath + "/StoresStack.json");
                StoreStack GetStore = JsonUtility.FromJson<StoreStack>(GetStoresInfo);

                PlayInv.StoreID = GetPlaySource.CurrentStore;
                PlayInv.Money = GetPlaySource.Money;

                int SlaveNum = 0;
                int WeaponNum = 0;
                int BulletNum = 0;
                int StuffNum = 0;
                foreach (StorePoint Store in GetStore.storePoint) {
                    if (Store.StoreID == PlayInv.StoreID) {
                        PlayInv.TypeOfStore = Store.TypeOfStore;
                        foreach (SlvLot Slave in Store.Lot1) {
                            GameObject Slv = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
                            Slv.name = "Slv_" + SlaveNum;
                            SlaveProperties SlvProp = Slv.GetComponent<SlaveProperties>();
                            SlvProp.Skin = Slave.Skin;
                            SlvProp.FullHealth = Slave.FullHealth;
                            SlvProp.Health = Slave.Health;
                            SlvProp.Damage = Slave.Damage;
                            SlvProp.Accuracy = Slave.Accuracy;
                            SlvProp.Price = Slave.Price;
                            SlvProp.Level = Slave.Level;
                            SlvProp.Start_Fhp = Slave.St_Health;
                            SlvProp.Start_Dmg = Slave.St_Damage;
                            SlvProp.Start_Acc = Slave.St_Accuracy;
                            SlvProp.Shot_Units = Slave.Shot_Units;
                            SlvProp.Heal_Units = Slave.Heal_Units;
                            SlvProp.Rush_Units = Slave.Rush_Units;
                            PlayInv.Items.Add(Slv);
                            SlaveNum += 1;
                        }
                        foreach (WpnLot Weapon in Store.Lot2) {
                            GameObject Wpn = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
                            Wpn.name = "Wpn_" + WeaponNum;
                            WeaponProperties WpnProp = Wpn.GetComponent<WeaponProperties>();
                            WpnProp.WeapName = Weapon.Name;
                            WpnProp.Skin = Weapon.Skin;
                            WpnProp.Price = Weapon.Price;
                            WpnProp.Damage = Weapon.Damage;
                            WpnProp.Condition = Weapon.Condition;
                            WpnProp.Bullets = Weapon.Bullets;
                            PlayInv.Items.Add(Wpn);
                            WeaponNum += 1;
                        }
                        foreach (BulLot Bullets in Store.Lot3) {
                            GameObject Bul = Instantiate(Resources.Load("BulletsDoll")) as GameObject;
                            Bul.name = "Bul_" + BulletNum;
                            BulletsProperties BulProp = Bul.GetComponent<BulletsProperties>();
                            BulProp.Name = Bullets.Name;
                            BulProp.Skin = Bullets.Skin;
                            BulProp.Count = Bullets.Count;
                            BulProp.Price = Bullets.Price;
                            PlayInv.Items.Add(Bul);
                            BulletNum += 1;
                        }
                        foreach (StffLot Stuff in Store.Lot4) {
                            GameObject Stf = Instantiate(Resources.Load("OtherStuff")) as GameObject;
                            Stf.name = "Stf_" + StuffNum;
                            OtherStuff StfProp = Stf.GetComponent<OtherStuff>();
                            StfProp.Skin = Stuff.Skin;
                            StfProp.Price = Stuff.Price;
                            StfProp.Liters = Stuff.Liters;
                            PlayInv.Items.Add(Stf);
                            StuffNum += 1;
                        }
                    }
                }

            }
        }
    }
    
    //======================================================= SAVE STORE ITEMS ===========================================================

    public void SaveStoresInfo() {
        if (File.Exists(Application.persistentDataPath + "/StoresStack.json")) {
            string GetInfo = File.ReadAllText(Application.persistentDataPath + "/StoresStack.json");
            StoreStack StoresInfo = JsonUtility.FromJson<StoreStack>(GetInfo);
            int StoreID = PlayInv.StoreID;

            foreach (StorePoint Store in StoresInfo.storePoint) {
                if (Store.StoreID == StoreID) {
                    Store.Lot1.Clear();
                    Store.Lot2.Clear();
                    Store.Lot3.Clear();
                    Store.Lot4.Clear();
                    foreach (GameObject GetItem in PlayInv.Items) {
                        if (GetItem.GetComponent<SlaveProperties>() != null) {

                            SlaveProperties CheckProp = GetItem.GetComponent<SlaveProperties>();
                            SlvLot NewSlv = new SlvLot();

                            NewSlv.Skin = CheckProp.Skin;
                            NewSlv.Health = CheckProp.Health;
                            NewSlv.FullHealth = CheckProp.FullHealth;
                            NewSlv.Damage = CheckProp.Damage;
                            NewSlv.Accuracy = CheckProp.Accuracy;
                            NewSlv.Level = CheckProp.Level;
                            NewSlv.Price = CheckProp.Price;
                            NewSlv.St_Health = CheckProp.Start_Fhp;
                            NewSlv.St_Damage = CheckProp.Start_Dmg;
                            NewSlv.St_Accuracy = CheckProp.Start_Acc;
                            NewSlv.Shot_Units = CheckProp.Shot_Units;
                            NewSlv.Heal_Units = CheckProp.Heal_Units;
                            NewSlv.Rush_Units = CheckProp.Rush_Units;

                            Store.Lot1.Add(NewSlv);
                        }
                        if (GetItem.GetComponent<WeaponProperties>() != null) {
                            WeaponProperties CheckProp = GetItem.GetComponent<WeaponProperties>();
                            WpnLot NewWpn = new WpnLot();

                            NewWpn.Name = CheckProp.name;
                            NewWpn.Skin = CheckProp.Skin;
                            NewWpn.Price = CheckProp.Price;
                            NewWpn.Damage = CheckProp.Damage;
                            NewWpn.Condition = CheckProp.Condition;
                            NewWpn.Bullets = CheckProp.Bullets;

                            Store.Lot2.Add(NewWpn);
                        }
                        if (GetItem.GetComponent<BulletsProperties>() != null) {
                            BulletsProperties CheckProp = GetItem.GetComponent<BulletsProperties>();
                            BulLot NewBul = new BulLot();

                            NewBul.Skin = CheckProp.Skin;
                            NewBul.Name = CheckProp.Name;
                            NewBul.Count = CheckProp.Count;
                            NewBul.Price = CheckProp.Price;

                            Store.Lot3.Add(NewBul);
                        }
                        if (GetItem.GetComponent<OtherStuff>() != null) {
                            OtherStuff CheckProp = GetItem.GetComponent<OtherStuff>();
                            StffLot NewStff = new StffLot();

                            NewStff.Skin = CheckProp.Skin;
                            NewStff.Price = CheckProp.Price;
                            NewStff.Liters = CheckProp.Liters;

                            Store.Lot4.Add(NewStff);
                        }
                    }
                }
            }

            string NewStoreData = JsonUtility.ToJson(StoresInfo);
            StreamWriter WriteNewData = new StreamWriter(Application.persistentDataPath + "/StoresStack.json");
            WriteNewData.Write(NewStoreData);
            WriteNewData.Close();

        }
    }

    //========================== Create New Player Data =============================

    public void CreateNewPlayerData() {

        PlayerSource NewPlayer = new PlayerSource();
        NewPlayer.CurrentStore = 0;
        NewPlayer.Money = Random.Range(600, 800) * 5;
        NewPlayer.If_Tutorial = true;
        NewPlayer.Step_Of_Tutorial = 1;

        SlaveDoll AddSlv = new SlaveDoll();
        AddSlv.Skin = Random.Range(1, 6);
        AddSlv.Health = Random.Range(50, 90) * 5;
        AddSlv.FullHealth = AddSlv.Health;
        AddSlv.Damage = Random.Range(4, 12) * 5;
        AddSlv.Accuracy = Random.Range(3, 10);
        AddSlv.Start_Fhp = AddSlv.FullHealth;
        AddSlv.Start_Dmg = AddSlv.Damage;
        AddSlv.Start_Acc = AddSlv.Accuracy;
        AddSlv.Heal_Units = 3;
        AddSlv.Shot_Units = 5;
        AddSlv.Rush_Units = 3;
        AddSlv.Level = 1;
        NewPlayer.Slaves.Add(AddSlv);

        for (int a = 0; a < 2; a++) {
            ItemDoll Item = new ItemDoll();
            Item.TypeOfItem = "Stuff";
            Item.Name = "Water";
            Item.Skin = 2;
            Item.Liters = 100;
            Item.Price = 100;
            NewPlayer.Items[a] = Item;
        }

        ItemDoll wpn = new ItemDoll();
        wpn.TypeOfItem = "Weapon";
        wpn.Skin = Random.Range(1, 11);
        wpn.Condition = Random.Range(1, 5);
        wpn.Price = 30;
        NewPlayer.Items[2] = wpn;

        string PlayerToStr = JsonUtility.ToJson(NewPlayer);
        StreamWriter WriteData = new StreamWriter(Application.persistentDataPath + "/PlayerData.json");
        WriteData.Write(PlayerToStr);
        WriteData.Close();

    }

    //========================== Choose only Weapon =============================
    public void FindWeapons(BulletsEngine Bullets) {
        foreach (GameObject slv in PlayInv.SlavePlace) {
            if (slv != null) {
                SlaveProperties prop = slv.GetComponent<SlaveProperties>();
                GameObject Pack = prop.InventoryPack.gameObject;
                foreach (Transform Plc in Pack.transform) {
                    if (Plc.transform.childCount != 0) {
                        GameObject Item = Plc.transform.GetChild(0).gameObject;
                        if (Item.GetComponent<WeaponProperties>() != null) {
                            Bullets.Weapons.Add(Item);
                        }
                    }
                }
            }
        }
        foreach (GameObject Item in PlayInv.Package) {
            if (Item != null) {
                if (Item.GetComponent<WeaponProperties>() != null) {
                    Bullets.Weapons.Add(Item);
                }
            }
        }
    }

    //========================== Fix Enenmy Data on Map After Fight =============================
    public void Save_Enemy_Data(int Num_Of_Area, int Count_Of_Bodies) {
        if (File.Exists(Application.persistentDataPath + "/MapData.json")) {
            string GetInfo = File.ReadAllText(Application.persistentDataPath + "/MapData.json");
            MapData Map = JsonUtility.FromJson<MapData>(GetInfo);
            foreach (BanditArea Area in Map.GenerateIndexes.Bandits) {
                if (Area.NumberOfArea == Num_Of_Area) {
                    Area.Population -= Count_Of_Bodies;
                    Area.Attacks += 1;
                    if (Area.Population == 0) {
                        Area.Clan = "";
                    }
                }
            }
            string NewMapData = JsonUtility.ToJson(Map);
            StreamWriter WriteMapData = new StreamWriter(Application.persistentDataPath + "/MapData.json");
            WriteMapData.Write(NewMapData);
            WriteMapData.Close();
        }
    }
}

//=============== PlayerSource ===============
[System.Serializable]
public class PlayerSource {
    public int Money;
    public int CurrentStore;
    public bool If_Tutorial;
    public int Step_Of_Tutorial;
    public List<SlaveDoll> Slaves = new List<SlaveDoll>();
    public ItemDoll[] Items = new ItemDoll[9];
}

//=============== Doll ===============
[System.Serializable]
public class SlaveDoll {
    public bool StatusEmpty;
    public int Number;
    public int Health;
    public int FullHealth;
    public int Damage;
    public int Accuracy;
    public int Battles;
    public int Level;
    public int Skin;
    public int Price;
    public int Efficiency;
    public int WeaponSkin;
    public bool HaveGun;
    public bool FullPackage;
    [Space]
    public int Start_Fhp;
    public int Start_Dmg;
    public int Start_Acc;
    public int Heal_Units;
    public int Shot_Units;
    public int Rush_Units;
    public ItemDoll[] Package = new ItemDoll[4];
    //public int[] Package = new int[4];
}

//=============== Item ===============
[System.Serializable]
public class ItemDoll {
    public string TypeOfItem;
    public int ID;
    public string Name;
    public int Damage;
    public int Condition;
    public int Skin;
    public int Bullets;
    public int Efficiency;
    public int Price;
    public int Liters;
}

//[System.Serializable]
//public class StuffDoll {
//    public int ID;
//    public int Skin;
//}

//[System.Serializable]
//public class INVENTORY {
//    public PlayerSource PlayerSource = new PlayerSource();
//    public List<SaveSlave> AllSlaves = new List<SaveSlave>();
//    public List<SaveWeapon> AllWeapons = new List<SaveWeapon>();
//    public List<SaveStuff> AllStuff = new List<SaveStuff>();
