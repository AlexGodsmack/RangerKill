using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadData : MonoBehaviour
{

    //public GameObject GotoMapButton;
    public PlayerInventory PlayInv;
    public WORK_Store StoreStack;
    public GameObject WeapSource;
    public GameObject StffSource;
    public GameObject SlvSource;

    public void SaveAll() {
        INVENTORY newInventory = new INVENTORY();
        newInventory.PlayerSource.Money = PlayInv.Money;
        newInventory.PlayerSource.Slaves = PlayInv.Slaves;
        newInventory.PlayerSource.Weapons = PlayInv.Weapons;
        newInventory.PlayerSource.Stuff = PlayInv.Stuff;
        int GetSlavePlace = 0;
        foreach (GameObject Slave in PlayInv.SlavePlace) {
            SaveSlave newSlave = new SaveSlave();
            if (Slave != null) {
                SlaveProperties GetSlv = Slave.GetComponent<SlaveProperties>();
                newSlave.Number = GetSlv.Number;
                newSlave.Health = GetSlv.Health;
                newSlave.FullHealth = GetSlv.FullHealth;
                newSlave.PlaceOnField = GetSlavePlace;
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
                int GetStuffPlace = 0;
                foreach (SlavePackage Pack in PlayInv.SlavesBag) {
                    if (Pack.NumberOfSlave == newSlave.Number) {
                        foreach (GameObject Stuff in Pack.Place) {
                            if (Stuff != null) {
                                if (Stuff.GetComponent<WeaponProperties>() != null) {
                                    SaveWeapon newWeapon = new SaveWeapon();
                                    newWeapon.ID = -Stuff.gameObject.GetInstanceID();
                                    newWeapon.Damage = Stuff.GetComponent<WeaponProperties>().Damage;
                                    newWeapon.Condition = Stuff.GetComponent<WeaponProperties>().Condition;
                                    newWeapon.Skin = Stuff.GetComponent<WeaponProperties>().Skin;
                                    newWeapon.Bullets = Stuff.GetComponent<WeaponProperties>().Bullets;
                                    newWeapon.Efficiency = Stuff.GetComponent<WeaponProperties>().Efficiency;
                                    newWeapon.Price = Stuff.GetComponent<WeaponProperties>().Price;
                                    newInventory.AllWeapons.Add(newWeapon);
                                    newSlave.WeaponSkin = Stuff.GetComponent<WeaponProperties>().Skin;
                                    newSlave.Package[GetStuffPlace] = newWeapon.ID;
                                }
                                if (Stuff.GetComponent<OtherStuff>() != null) {
                                    SaveStuff newStuff = new SaveStuff();
                                    newStuff.ID = -Stuff.GetComponent<OtherStuff>().GetInstanceID();
                                    newStuff.Skin = Stuff.GetComponent<OtherStuff>().Skin;
                                    newStuff.Liters = Stuff.GetComponent<OtherStuff>().Liters;
                                    newInventory.AllStuff.Add(newStuff);
                                    newSlave.Package[GetStuffPlace] = newStuff.ID;
                                    if (Stuff.GetComponent<OtherStuff>().Skin == 2) {
                                        newInventory.PlayerSource.Water += Stuff.GetComponent<OtherStuff>().Liters;
                                    }
                                }
                            } else {
                                newSlave.Package[GetStuffPlace] = 0;
                            }
                            GetStuffPlace += 1;
                        }
                    }
                }
                newInventory.AllSlaves.Add(newSlave);
            }
            GetSlavePlace += 1;
        }
        foreach (GameObject Weapon in PlayInv.WeaponPlace) {
            if (Weapon != null) {
                SaveWeapon newWeapon = new SaveWeapon();
                newWeapon.ID = -Weapon.gameObject.GetInstanceID();
                newWeapon.Name = Weapon.GetComponent<WeaponProperties>().WeapName;
                newWeapon.Damage = Weapon.GetComponent<WeaponProperties>().Damage;
                newWeapon.Condition = Weapon.GetComponent<WeaponProperties>().Condition;
                newWeapon.Skin = Weapon.GetComponent<WeaponProperties>().Skin;
                newWeapon.Bullets = Weapon.GetComponent<WeaponProperties>().Bullets;
                newWeapon.Efficiency = Weapon.GetComponent<WeaponProperties>().Efficiency;
                newWeapon.Price = Weapon.GetComponent<WeaponProperties>().Price;
                newInventory.AllWeapons.Add(newWeapon);
            }
        }
        foreach (GameObject Stuff in PlayInv.StuffPlace) {
            if (Stuff != null) {
                SaveStuff newStuff = new SaveStuff();
                newStuff.ID = -Stuff.GetComponent<OtherStuff>().GetInstanceID();
                newStuff.Skin = Stuff.GetComponent<OtherStuff>().Skin;
                newStuff.Liters = Stuff.GetComponent<OtherStuff>().Liters;
                newInventory.AllStuff.Add(newStuff);
            }
        }

        string SaveData = JsonUtility.ToJson(newInventory);
        StreamWriter WriteData = new StreamWriter(Application.persistentDataPath + "/PlayerData.json");
        WriteData.Write(SaveData);
        WriteData.Close();
    }

    public void LoadAll() {
        string json = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json").ToString(); // почему-то json не читает текст через ReadAllLines
        INVENTORY LoadData = JsonUtility.FromJson<INVENTORY>(json);

        PlayInv.Money = LoadData.PlayerSource.Money;
        PlayInv.Slaves = LoadData.PlayerSource.Slaves;
        PlayInv.Weapons = LoadData.PlayerSource.Weapons;
        PlayInv.Stuff = LoadData.PlayerSource.Stuff;
        PlayInv.StoreID = LoadData.PlayerSource.CurrentStore;

        int GetSlave = 0;
        foreach (SaveSlave LoadSlave in LoadData.AllSlaves) {
            GameObject Slave = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
            Slave.name = "Slv_" + GetSlave + 1;
            Slave.GetComponent<SlaveProperties>().Number = LoadData.AllSlaves[GetSlave].Number;
            Slave.GetComponent<SlaveProperties>().Health = LoadData.AllSlaves[GetSlave].Health;
            Slave.GetComponent<SlaveProperties>().FullHealth = LoadData.AllSlaves[GetSlave].FullHealth;
            Slave.GetComponent<SlaveProperties>().Damage = LoadData.AllSlaves[GetSlave].Damage;
            Slave.GetComponent<SlaveProperties>().Accuracy = LoadData.AllSlaves[GetSlave].Accuracy;
            Slave.GetComponent<SlaveProperties>().Battles = LoadData.AllSlaves[GetSlave].Battles;
            Slave.GetComponent<SlaveProperties>().Level = LoadData.AllSlaves[GetSlave].Level;
            Slave.GetComponent<SlaveProperties>().Skin = LoadData.AllSlaves[GetSlave].Skin;
            Slave.GetComponent<SlaveProperties>().Price = LoadData.AllSlaves[GetSlave].Price;
            Slave.GetComponent<SlaveProperties>().Efficiency = LoadData.AllSlaves[GetSlave].Efficiency;
            Slave.GetComponent<SlaveProperties>().WeaponSkin = LoadData.AllSlaves[GetSlave].WeaponSkin;
            Slave.GetComponent<SlaveProperties>().HaveGun = LoadData.AllSlaves[GetSlave].HaveGun;
            Slave.GetComponent<SlaveProperties>().FullPackage = LoadData.AllSlaves[GetSlave].FullPackage;
            PlayInv.SlavePlace[LoadData.AllSlaves[GetSlave].PlaceOnField] = Slave;
            SlavePackage newPack = new SlavePackage();
            newPack.NumberOfSlave = LoadData.AllSlaves[GetSlave].Number;
            PlayInv.SlavesBag.Add(newPack);


            GetSlave += 1;
        }
        int GetWeapon = 0;
        foreach (SaveWeapon LoadWeapon in LoadData.AllWeapons) {
            GameObject Weapon = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
            Weapon.name = "W_" + GetWeapon + 1;
            Weapon.GetComponent<WeaponProperties>().WeapName = LoadData.AllWeapons[GetWeapon].Name;
            Weapon.GetComponent<WeaponProperties>().Number = LoadData.AllWeapons[GetWeapon].ID;
            Weapon.GetComponent<WeaponProperties>().Damage = LoadData.AllWeapons[GetWeapon].Damage;
            Weapon.GetComponent<WeaponProperties>().Condition = LoadData.AllWeapons[GetWeapon].Condition;
            Weapon.GetComponent<WeaponProperties>().Skin = LoadData.AllWeapons[GetWeapon].Skin;
            Weapon.GetComponent<WeaponProperties>().Bullets = LoadData.AllWeapons[GetWeapon].Bullets;
            Weapon.GetComponent<WeaponProperties>().Price = LoadData.AllWeapons[GetWeapon].Price;
            PlayInv.WeaponPlace[GetWeapon] = Weapon;
            GetWeapon += 1;
        }


        int GetStuff = 0;
        foreach (SaveStuff LoadStuff in LoadData.AllStuff) {
            GameObject Stuff = Instantiate(Resources.Load("OtherStuff")) as GameObject;
            Stuff.name = "S_" + GetStuff + 1;
            Stuff.GetComponent<OtherStuff>().Number = LoadData.AllStuff[GetStuff].ID;
            Stuff.GetComponent<OtherStuff>().Skin = LoadData.AllStuff[GetStuff].Skin;
            Stuff.GetComponent<OtherStuff>().Liters = LoadData.AllStuff[GetStuff].Liters;
            PlayInv.StuffPlace[GetStuff] = Stuff;
            GetStuff += 1;
        }

        for (int i = 0; i < PlayInv.WeaponPlace.Length; i++) {
            if (PlayInv.WeaponPlace[i] != null) {
                GameObject GetThisWeap = PlayInv.WeaponPlace[i].gameObject;
                int WNum = GetThisWeap.GetComponent<WeaponProperties>().Number;
                for (int s = 0; s < LoadData.AllSlaves.Count; s++) {
                    for (int p = 0; p < LoadData.AllSlaves[s].Package.Length; p++) {
                        if (LoadData.AllSlaves[s].Package[p] == WNum) {
                            foreach (SlavePackage GetPack in PlayInv.SlavesBag) {
                                if (GetPack.NumberOfSlave == LoadData.AllSlaves[s].Number) {
                                    GetPack.Place[p] = GetThisWeap;
                                    PlayInv.WeaponPlace[i] = null;
                                }
                            }
                        }
                    }
                }
            }
        }
        for (int s = 0; s < PlayInv.StuffPlace.Length; s++) {
            if (PlayInv.StuffPlace[s] != null) {
                GameObject GetThisStuff = PlayInv.StuffPlace[s].gameObject;
                int NumStff = GetThisStuff.GetComponent<OtherStuff>().Number;
                for (int slv = 0; slv < LoadData.AllSlaves.Count; slv++) {
                    for (int p = 0; p < LoadData.AllSlaves[slv].Package.Length; p++) {
                        if (LoadData.AllSlaves[slv].Package[p] == NumStff) {
                            foreach (SlavePackage GetPack in PlayInv.SlavesBag) {
                                if (GetPack.NumberOfSlave == LoadData.AllSlaves[slv].Number) {
                                    GetPack.Place[p] = GetThisStuff;
                                    PlayInv.StuffPlace[s] = null;
                                }
                            }
                        }
                    }
                }
            }
        }
        foreach (SlavePackage Pack in PlayInv.SlavesBag) {
            foreach (GameObject GetItem in Pack.Place) {
                if (GetItem != null && GetItem.GetComponent<WeaponProperties>() != null) {
                    foreach (GameObject Slave in PlayInv.SlavePlace) {
                        if (Slave != null && Pack.NumberOfSlave == Slave.GetComponent<SlaveProperties>().Number) {
                            Slave.GetComponent<SlaveProperties>().WeaponXRef = GetItem;
                        }
                    }
                }
            }
        }
    }

    public void SetSourceToFolders() {
        int ReNumSlaves = 0;
        foreach (GameObject Slv in PlayInv.SlavePlace) {
            if (Slv != null) {
                foreach (SlavePackage Pack in PlayInv.SlavesBag) {
                    if (Slv.GetComponent<SlaveProperties>().Number == Pack.NumberOfSlave) {
                        Slv.GetComponent<SlaveProperties>().Number = Slv.GetInstanceID();
                        Pack.NumberOfSlave = Slv.GetInstanceID();
                    }
                }
            }
        }
        foreach (GameObject Slv in PlayInv.SlavePlace) {
            if (Slv != null) {
                ReNumSlaves += 1;
                foreach (SlavePackage Pack in PlayInv.SlavesBag) {
                    if (Slv.GetComponent<SlaveProperties>().Number == Pack.NumberOfSlave) {
                        Slv.GetComponent<SlaveProperties>().Number = ReNumSlaves;
                        Pack.NumberOfSlave = ReNumSlaves;
                    }
                }
            }
        }

        //int SetSlavePlace = 0;
        //foreach (GameObject Slv in PlayInv.SlavePlace) {
        //    if (Slv != null) {
        //        GameObject GetPlace = SlavesBoughtPanel.transform.Find("Places").transform.GetChild(SetSlavePlace).gameObject;
        //        Slv.transform.position = GetPlace.transform.position;
        //        GetPlace.active = false;
        //        Slv.transform.SetParent(SlavesBoughtPanel.transform.Find("Places/BoughtSlaves").transform);
        //        Slv.GetComponent<SlaveProperties>().Bought = true;
        //        SetSlavePlace += 1;
        //    }
        //}

        int SetSlvPlace = 0;
        foreach (GameObject slv in PlayInv.SlavePlace) {
            if (slv != null) {
                slv.transform.SetParent(SlvSource.transform);
                slv.transform.localPosition = new Vector3(0, 0, 0);
                SetSlvPlace += 1;
            }
        }

        int SetWpnPlace = 0;
        foreach (GameObject wpn in PlayInv.WeaponPlace) {
            if (wpn != null) {
                wpn.transform.SetParent(WeapSource.transform);
                wpn.transform.localPosition = new Vector3(0, 0, 0);
                SetWpnPlace += 1;
            }
        }

        int SetStffPlace = 0;
        foreach (GameObject stff in PlayInv.StuffPlace) {
            if (stff != null) {
                stff.transform.SetParent(StffSource.transform);
                stff.transform.localPosition = new Vector3(0, 0, 0);
                SetStffPlace += 1;
            }
        }
        
    }

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
                    foreach (GameObject GetItem in StoreStack.Items) {
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

    void Start()
    {

    }

    void Update()
    {

        /////************************************************* GO TO MAP ************************************************
        //if (GotoMapButton != null) {
        //    if (GotoMapButton.GetComponent<ButtonSample>().isPressed == true) {
        //        SaveAll();
        //        SaveStoresInfo();
        //        SceneManager.LoadScene(2);
        //    }
        //}

    }
}
[System.Serializable]
public class PlayerDataChanger {
    public void CreateNewPlayerData() {

        //string json = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json").ToString();
        INVENTORY NewPlayer = new INVENTORY();
        //NewPlayer.TypeOfStore = "Slaves";
        NewPlayer.PlayerSource.CurrentStore = 0;
        NewPlayer.PlayerSource.Money = Random.Range(600, 800) * 5;
        string PlayerToStr = JsonUtility.ToJson(NewPlayer);
        StreamWriter WriteData = new StreamWriter(Application.persistentDataPath + "/PlayerData.json");
        WriteData.Write(PlayerToStr);
        WriteData.Close();

    }
}

[System.Serializable]
public class PlayerSource {
    public int Money;
    public int Water;
    public int Slaves;
    public int Weapons;
    public int Stuff;
    public int CurrentStore;
}

[System.Serializable]
public class SaveSlave {
    public int Number;
    public int Health;
    public int FullHealth;
    public int PlaceOnField;
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

    public int[] Package = new int[4];
}

[System.Serializable]
public class SaveWeapon {
    public int ID;
    public string Name;
    public int Damage;
    public int Condition;
    public int Skin;
    public int Bullets;
    public int Efficiency;
    public int Price;
}

[System.Serializable]
public class SaveStuff {
    public int ID;
    public int Skin;
    public int Liters;
}

[System.Serializable]
public class INVENTORY {
    public PlayerSource PlayerSource = new PlayerSource();
    public List<SaveSlave> AllSlaves = new List<SaveSlave>();
    public List<SaveWeapon> AllWeapons = new List<SaveWeapon>();
    public List<SaveStuff> AllStuff = new List<SaveStuff>();
}
