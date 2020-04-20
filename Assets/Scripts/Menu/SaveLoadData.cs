using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadData : MonoBehaviour
{

    public GameObject SaveButton;
    public Component PlayerInventory;

    //public GameObject SlavesContainer;
    //public GameObject WeaponContainer;
    //public GameObject StuffContainer;

    public void SaveAll() {
        INVENTORY newInventory = new INVENTORY();
        newInventory.PlayerSource.Money = PlayerInventory.GetComponent<PlayerInventory>().Money;
        newInventory.PlayerSource.Slaves = PlayerInventory.GetComponent<PlayerInventory>().Slaves;
        newInventory.PlayerSource.Weapons = PlayerInventory.GetComponent<PlayerInventory>().Weapons;
        newInventory.PlayerSource.Stuff = PlayerInventory.GetComponent<PlayerInventory>().Stuff;
        int GetSlavePlace = 0;
        foreach (GameObject Slave in PlayerInventory.GetComponent<PlayerInventory>().SlavePlace) {
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
                foreach (SlavePackage Pack in PlayerInventory.GetComponent<PlayerInventory>().SlavesBag) {
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
        foreach (GameObject Weapon in PlayerInventory.GetComponent<PlayerInventory>().WeaponPlace) {
            if (Weapon != null) {
                SaveWeapon newWeapon = new SaveWeapon();
                newWeapon.ID = -Weapon.gameObject.GetInstanceID();
                newWeapon.Damage = Weapon.GetComponent<WeaponProperties>().Damage;
                newWeapon.Condition = Weapon.GetComponent<WeaponProperties>().Condition;
                newWeapon.Skin = Weapon.GetComponent<WeaponProperties>().Skin;
                newWeapon.Bullets = Weapon.GetComponent<WeaponProperties>().Bullets;
                newWeapon.Efficiency = Weapon.GetComponent<WeaponProperties>().Efficiency;
                newWeapon.Price = Weapon.GetComponent<WeaponProperties>().Price;
                newInventory.AllWeapons.Add(newWeapon);
            }
        }
        foreach (GameObject Stuff in PlayerInventory.GetComponent<PlayerInventory>().StuffPlace) {
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
        return ;
    }

    public void LoadAll() {
        string json = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json").ToString(); // почему-то json не читает текст через ReadAllLines
        INVENTORY LoadData = JsonUtility.FromJson<INVENTORY>(json);
        PlayerInventory.GetComponent<PlayerInventory>().Money = LoadData.PlayerSource.Money;
        PlayerInventory.GetComponent<PlayerInventory>().Slaves = LoadData.PlayerSource.Slaves;
        PlayerInventory.GetComponent<PlayerInventory>().Weapons = LoadData.PlayerSource.Weapons;
        PlayerInventory.GetComponent<PlayerInventory>().Stuff = LoadData.PlayerSource.Stuff;

        int SetBags = 0;
        foreach (SaveSlave LoadBags in LoadData.AllSlaves) {
        }

        int GetSlave = 0;
        foreach (SaveSlave LoadSlave in LoadData.AllSlaves) {
            GameObject Slave = Instantiate(Resources.Load("HeroPrefab")) as GameObject;
            Slave.name = "B_" + GetSlave + 1;
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
            PlayerInventory.GetComponent<PlayerInventory>().SlavePlace[LoadData.AllSlaves[GetSlave].PlaceOnField] = Slave;
            SlavePackage newPack = new SlavePackage();
            newPack.NumberOfSlave = LoadData.AllSlaves[GetSlave].Number;
            PlayerInventory.GetComponent<PlayerInventory>().SlavesBag.Add(newPack);


            GetSlave += 1;
        }
        int GetWeapon = 0;
        foreach (SaveWeapon LoadWeapon in LoadData.AllWeapons) {
            GameObject Weapon = Instantiate(Resources.Load("WeaponDoll")) as GameObject;
            Weapon.name = "W_" + GetWeapon + 1;
            Weapon.GetComponent<WeaponProperties>().Number = LoadData.AllWeapons[GetWeapon].ID;
            Weapon.GetComponent<WeaponProperties>().Damage = LoadData.AllWeapons[GetWeapon].Damage;
            Weapon.GetComponent<WeaponProperties>().Condition = LoadData.AllWeapons[GetWeapon].Condition;
            Weapon.GetComponent<WeaponProperties>().Skin = LoadData.AllWeapons[GetWeapon].Skin;
            Weapon.GetComponent<WeaponProperties>().Bullets = LoadData.AllWeapons[GetWeapon].Bullets;
            Weapon.GetComponent<WeaponProperties>().Price = LoadData.AllWeapons[GetWeapon].Price;
            GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(Weapon.transform);
            L.transform.localPosition = new Vector3(0, 0, -0.1f);
            PlayerInventory.GetComponent<PlayerInventory>().WeaponPlace[GetWeapon] = Weapon;
            GetWeapon += 1;
        }


        int GetStuff = 0;
        foreach (SaveStuff LoadStuff in LoadData.AllStuff) {
            GameObject Stuff = Instantiate(Resources.Load("OtherStuff")) as GameObject;
            Stuff.name = "S_" + GetStuff + 1;
            Stuff.GetComponent<OtherStuff>().Number = LoadData.AllStuff[GetStuff].ID;
            Stuff.GetComponent<OtherStuff>().Skin = LoadData.AllStuff[GetStuff].Skin;
            Stuff.GetComponent<OtherStuff>().Liters = LoadData.AllStuff[GetStuff].Liters;
            GameObject L = Instantiate(Resources.Load("Lighter_03")) as GameObject;
            L.name = "Lighter";
            L.transform.SetParent(Stuff.transform);
            L.transform.localPosition = new Vector3(0, 0, -0.1f);
            PlayerInventory.GetComponent<PlayerInventory>().StuffPlace[GetStuff] = Stuff;
            GetStuff += 1;
        }

        for (int i = 0; i < PlayerInventory.GetComponent<PlayerInventory>().WeaponPlace.Length; i++) {
            if (PlayerInventory.GetComponent<PlayerInventory>().WeaponPlace[i] != null) {
                GameObject GetThisWeap = PlayerInventory.GetComponent<PlayerInventory>().WeaponPlace[i].gameObject;
                int WNum = GetThisWeap.GetComponent<WeaponProperties>().Number;
                for (int s = 0; s < LoadData.AllSlaves.Count; s++) {
                    for (int p = 0; p < LoadData.AllSlaves[s].Package.Length; p++) {
                        if (LoadData.AllSlaves[s].Package[p] == WNum) {
                            foreach (SlavePackage GetPack in PlayerInventory.GetComponent<PlayerInventory>().SlavesBag) {
                                if (GetPack.NumberOfSlave == LoadData.AllSlaves[s].Number) {
                                    GetPack.Place[p] = GetThisWeap;
                                    PlayerInventory.GetComponent<PlayerInventory>().WeaponPlace[i] = null;
                                }
                            }
                        }
                    }
                }
            }
        }
        for (int s = 0; s < PlayerInventory.GetComponent<PlayerInventory>().StuffPlace.Length; s++) {
            if (PlayerInventory.GetComponent<PlayerInventory>().StuffPlace[s] != null) {
                GameObject GetThisStuff = PlayerInventory.GetComponent<PlayerInventory>().StuffPlace[s].gameObject;
                int NumStff = GetThisStuff.GetComponent<OtherStuff>().Number;
                for (int slv = 0; slv < LoadData.AllSlaves.Count; slv++) {
                    for (int p = 0; p < LoadData.AllSlaves[slv].Package.Length; p++) {
                        if (LoadData.AllSlaves[slv].Package[p] == NumStff) {
                            foreach (SlavePackage GetPack in PlayerInventory.GetComponent<PlayerInventory>().SlavesBag) {
                                if (GetPack.NumberOfSlave == LoadData.AllSlaves[slv].Number) {
                                    GetPack.Place[p] = GetThisStuff;
                                    PlayerInventory.GetComponent<PlayerInventory>().StuffPlace[s] = null;
                                }
                            }
                        }
                    }
                }
            }
        }
        foreach (SlavePackage Pack in PlayerInventory.GetComponent<PlayerInventory>().SlavesBag) {
            foreach (GameObject GetItem in Pack.Place) {
                if (GetItem != null && GetItem.GetComponent<WeaponProperties>() != null) {
                    foreach (GameObject Slave in PlayerInventory.GetComponent<PlayerInventory>().SlavePlace) {
                        if (Slave != null && Pack.NumberOfSlave == Slave.GetComponent<SlaveProperties>().Number) {
                            Slave.GetComponent<SlaveProperties>().WeaponXRef = GetItem;
                        }
                    }
                }
            }
        }
    }

    public void PutAllOnPlace() {

        string json = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json").ToString();
        INVENTORY LoadData = JsonUtility.FromJson<INVENTORY>(json);


    }


    void Start()
    {
        //if (SaveButton != null) {
        //    SaveButton.transform.position = TopRight.transform.position + new Vector3(-0.2f, -0.1f, 0);
        //}
    }

    void Update()
    {

        if (SaveButton != null && SaveButton.GetComponent<ButtonSample>().isPressed == true) {
            SaveAll();
            SceneManager.LoadScene(2);
        }

    }
}

[System.Serializable]
public class PlayerSource {
    public int Money;
    public int Water;
    public int Slaves;
    public int Weapons;
    public int Stuff;
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
