using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLoaderInBattle : MonoBehaviour
{

    public GameObject BattlePanel;
    public GameObject FinalPanel;
    public GameObject PackContainer;
    public GameObject Target;

    public GameObject SlavesFields;
    public GameObject SlavesContainer;
    public GameObject EnemiesFields;
    public GameObject EnemiesContainer;

    public GameObject TopCenterAnchor;

    public int Count;
    public int NumberOfArea;

    public float PanoramaCoef;

    void Start()
    {

        LoadPlayerData();
        BattlePanel.transform.position = TopCenterAnchor.transform.position + new Vector3(0, 0.6f, 0);
        
    }

    void Update()
    {

    }

    void LoadPlayerData() {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {

            SaveLoadData GetLoader = new SaveLoadData();
            GetLoader.PlayerInventory = this.GetComponent<PlayerInventory>();
            GetLoader.LoadAll();

            int NumSlv = 0;
            PanoramaCoef = -0.1f;
            foreach (GameObject Slave in this.GetComponent<PlayerInventory>().SlavePlace) {
                if (Slave != null) {
                    Slave.GetComponent<SlaveProperties>().ShowHealthbar = true;
                    Slave.GetComponent<SlaveProperties>().Goal = EnemiesFields.transform.GetChild(5).gameObject;

                    Slave.transform.position = SlavesFields.transform.GetChild(NumSlv).transform.position + new Vector3(0, 0, PanoramaCoef);
                    //if (NumSlv >= 0 && NumSlv < 3) {
                    //}
                    //if (NumSlv >= 3 && NumSlv < 6) {
                    //    Slave.transform.position = SlavesFields.transform.GetChild(NumSlv).transform.position + new Vector3(0, 0, -0.2f);
                    //}
                    //if (NumSlv >= 6 && NumSlv < 9) {
                    //    Slave.transform.position = SlavesFields.transform.GetChild(NumSlv).transform.position + new Vector3(0, 0, -0.3f);
                    //}
                    Slave.transform.SetParent(SlavesContainer.transform);
                    this.GetComponent<WORK_Battle>().Slaves.Add(Slave);
                }
                NumSlv += 1;
                PanoramaCoef -= 0.1f;
            }

            foreach (SlavePackage GetPack in this.GetComponent<PlayerInventory>().SlavesBag) {
                GameObject NewPack = Instantiate(Resources.Load("SlavePackInBattle")) as GameObject;
                NewPack.name = "Pack_" + GetPack.NumberOfSlave;
                int SetPlace = 0;
                foreach (GameObject GetStff in GetPack.Place) {
                    if (GetStff != null) {
                        GetStff.transform.position = NewPack.transform.GetChild(SetPlace).transform.position;
                        GetStff.active = true;
                        if (GetStff.GetComponent<WeaponProperties>() != null) {
                            GetStff.GetComponent<WeaponProperties>().Bought = true;
                        }
                        if (GetStff.GetComponent<OtherStuff>() != null) {
                            GetStff.GetComponent<OtherStuff>().Bought = true;
                        }
                        GetStff.transform.SetParent(NewPack.transform);
                        NewPack.transform.GetChild(SetPlace).gameObject.active = false;
                    }
                    SetPlace += 1;
                }
                foreach (GameObject Slv in this.GetComponent<PlayerInventory>().SlavePlace) {
                    if (Slv != null) {
                        if (Slv.GetComponent<SlaveProperties>().Number == GetPack.NumberOfSlave) {
                            Slv.GetComponent<SlaveProperties>().InventoryPack = NewPack.gameObject;
                        }
                    }
                }
                NewPack.transform.SetParent(PackContainer.transform);
                NewPack.transform.localPosition = new Vector3(0, 0, 0);
                NewPack.active = false;
            }

            foreach (GameObject Wpn in this.GetComponent<PlayerInventory>().WeaponPlace) {
                if (Wpn != null) {
                    Wpn.active = false;
                }
            }
            foreach (GameObject Stff in this.GetComponent<PlayerInventory>().StuffPlace) {
                if (Stff != null) {
                    Stff.active = false;
                }
            }

        }

        if (File.Exists(Application.persistentDataPath + "/BanditTroop.json")) {

            string GetEnemyData = File.ReadAllText(Application.persistentDataPath + "/BanditTroop.json");
            TroopData GetTroop = new TroopData();
            GetTroop = JsonUtility.FromJson<TroopData>(GetEnemyData);
            NumberOfArea = GetTroop.NumberOfArea;
            Count = GetTroop.TroopCount;

            for (int e = 0; e < GetTroop.TroopCount; e++) {
                Debug.Log("imported");
                GameObject E = Instantiate(Resources.Load("EnemyDoll")) as GameObject;
                E.name = "Enm" + e;
                E.GetComponent<EnemyProperties>().FullHealth = GetTroop.Enemies[e].FullHealth;
                E.GetComponent<EnemyProperties>().Health = GetTroop.Enemies[e].Health;
                E.GetComponent<EnemyProperties>().Damage = GetTroop.Enemies[e].Damage;
                E.GetComponent<EnemyProperties>().Accuracy = GetTroop.Enemies[e].Accuracy;
                E.GetComponent<EnemyProperties>().Level = GetTroop.Enemies[e].Level;
                E.GetComponent<EnemyProperties>().Skin = GetTroop.Enemies[e].Skin;
                //E.GetComponent<EnemyProperties>().WeaponInHand = GetTroop.Enemies[e].Weapon;
                E.transform.SetParent(EnemiesContainer.transform);
                this.GetComponent<WORK_Battle>().Enemies.Add(E);
            }

            int GetEnemy = 0;
            PanoramaCoef = -0.1f;
            for (int i = 0; i < GetTroop.Places.Length; i++) {
                if (GetTroop.Places[i] == true) {

                    EnemiesContainer.transform.GetChild(GetEnemy).transform.position = EnemiesFields.transform.GetChild(i).transform.position + new Vector3(0, 0, PanoramaCoef);
                    //if (i >= 0 && i < 3) {
                    //}
                    //if (i >= 3 && i < 6) {
                    //    EnemiesContainer.transform.GetChild(GetEnemy).transform.position = EnemiesFields.transform.GetChild(i).transform.position + new Vector3(0, 0, -0.2f);
                    //}
                    //if (i >= 6 && i < 9) {
                    //    EnemiesContainer.transform.GetChild(GetEnemy).transform.position = EnemiesFields.transform.GetChild(i).transform.position + new Vector3(0, 0, -0.3f);
                    //}
                    GetEnemy += 1;
                    PanoramaCoef -= 0.1f;
                }
            }

        }
    }
}
