using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLoaderInBattle : MonoBehaviour
{
    [Header("Panels")]
    public GameObject BattlePanel;
    public GameObject FinalPanel;
    public GameObject PackContainer;
    public GameObject Target;
    [Header("Gamers")]
    public GameObject SlavesFields;
    public GameObject SlavesContainer;
    public GameObject EnemiesFields;
    public GameObject EnemiesContainer;
    [Header("Anchores")]
    public GameObject TopCenterAnchor;
    [Header("Numbers")]
    public int Count;
    public int NumberOfArea;
    [Space]
    public float PanoramaCoef;
    [Header("Classes")]
    public MainPlayerControl PlayInv;
    public SaveLoadData Loader;
    public WORK_Battle Battle;

    void Start()
    {
        LoadPlayerData();
    }

    void Update()
    {

    }

    void LoadPlayerData() {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json")) {

            Loader.LoadAll();
            int SlvNum = 0;
            foreach (GameObject slv in PlayInv.SlavePlace) {
                if (slv != null) {
                    slv.transform.SetParent(SlavesContainer.transform);
                    slv.transform.position = SlavesFields.transform.GetChild(SlvNum).gameObject.transform.position + new Vector3(0, 0, -0.1f);
                    slv.GetComponent<SlaveProperties>().Goal = EnemiesFields.transform.GetChild(5).gameObject;
                    this.GetComponent<WORK_Battle>().Slaves.Add(slv);
                }
                SlvNum++;
            }

        }

        if (File.Exists(Application.persistentDataPath + "/BanditTroop.json")) {

            string GetEnemyData = File.ReadAllText(Application.persistentDataPath + "/BanditTroop.json");
            TroopData GetTroop = new TroopData();
            GetTroop = JsonUtility.FromJson<TroopData>(GetEnemyData);
            NumberOfArea = GetTroop.NumberOfArea;
            Count = GetTroop.TroopCount;
            Battle.Clan = GetTroop.Band;

            for (int e = 0; e < GetTroop.TroopCount; e++) {
                GameObject E = Instantiate(Resources.Load("EnemyDoll")) as GameObject;
                E.name = "Enm" + e;
                E.GetComponent<EnemyProperties>().FullHealth = GetTroop.Enemies[e].FullHealth;
                E.GetComponent<EnemyProperties>().Health = GetTroop.Enemies[e].Health;
                E.GetComponent<EnemyProperties>().Damage = GetTroop.Enemies[e].Damage;
                E.GetComponent<EnemyProperties>().Accuracy = GetTroop.Enemies[e].Accuracy;
                E.GetComponent<EnemyProperties>().Level = GetTroop.Enemies[e].Level;
                E.GetComponent<EnemyProperties>().Skin = GetTroop.Enemies[e].Skin;
                E.transform.SetParent(EnemiesContainer.transform);
                this.GetComponent<WORK_Battle>().Enemies.Add(E);
            }

            int GetEnemy = 0;
            PanoramaCoef = -0.1f;
            for (int i = 0; i < GetTroop.Places.Length; i++) {
                if (GetTroop.Places[i] == true) {

                    EnemiesContainer.transform.GetChild(GetEnemy).transform.position = EnemiesFields.transform.GetChild(i).transform.position + new Vector3(0, 0, PanoramaCoef);
                    GetEnemy += 1;
                    PanoramaCoef -= 0.1f;
                }
            }

        }
    }
}
