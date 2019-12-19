using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalOfBattle : MonoBehaviour
{

    public bool Winner = false;
    public bool Defeat = false;
    public Button TakeLoot;
    public Button GameOver;
    public bool StopRandomize = false;
    public GameObject LootPanel;
    public GameObject PersContainer;
    public GameObject StuffContainer;
    public GameObject BulletsContainer;
    public GameObject FinalPanel;

    public int Money;
    public int CoPers;
    public int CoWeap;
    public int CoBullets;
    public int CoStuff;

    private string[] CountOfAll;
    private string[] PlaySrc;
    private string[] InvSet;
    private string[] MapGen;
    private string[] StartHealth;

    private int NumPersParam = 8;
    private int NumWeapParam = 6;
    private int Columns = 10;
    private int Rows = 10;

    void Start()
    {
        CountOfAll = File.ReadAllLines(Application.persistentDataPath + "/CountOfAll.txt");
        PlaySrc = File.ReadAllLines(Application.persistentDataPath + "/PlayerSource.txt");
        InvSet = File.ReadAllLines(Application.persistentDataPath + "/InventorySettings.txt");
        MapGen = File.ReadAllLines(Application.persistentDataPath + "/MapGen.txt");
        StartHealth = File.ReadAllLines(Application.persistentDataPath + "/StartHealthOfPers.txt");

        TakeLoot.onClick.AddListener(TakeAndBack);
        GameOver.onClick.AddListener(GameOverAndBack);

        Money = int.Parse(PlaySrc[1]);
        
    }

    void TakeAndBack() {
        TakeLoot.GetComponent<AudioSource>().Play();

        int NewBulletsNum = 0;

        for (int i = 0; i < 3; i++) {
            GameObject Loot = LootPanel.transform.Find("Lot" + (i + 1).ToString()).gameObject;
            if (Loot.GetComponent<WeaponProperties>() != null) {
                Loot.transform.SetParent(StuffContainer.transform);
                CoWeap += 1;
                Loot.name = "Weapon" + CoWeap.ToString();
            }
            if (Loot.GetComponent<OtherStuff>() != null) {
                Loot.transform.SetParent(StuffContainer.transform);
                CoStuff += 1;
                Loot.name = "Stuff" + CoStuff.ToString();
            }
            if (Loot.GetComponent<Bullets>() != null) {
                Loot.transform.SetParent(BulletsContainer.transform);
                NewBulletsNum += 1;
                Loot.name = "NewBullets" + NewBulletsNum.ToString();
            }
        }

        for (int b = 1; b < NewBulletsNum + 1; b++) {
            for (int ob = 1; ob < CoBullets + 1; ob++) {
                if (BulletsContainer.transform.Find("Bullets" + ob.ToString()).GetComponent<Bullets>().BulletsSkin == BulletsContainer.transform.Find("NewBullets" + b.ToString()).GetComponent<Bullets>().BulletsSkin) {
                    BulletsContainer.transform.Find("Bullets" + ob.ToString()).GetComponent<Bullets>().CountOfBullets += BulletsContainer.transform.Find("NewBullets" + b.ToString()).GetComponent<Bullets>().CountOfBullets;
                    Destroy(BulletsContainer.transform.Find("NewBullets" + b.ToString()).gameObject);
                }
            }
        }

        CoBullets = BulletsContainer.transform.childCount;

        for (int p = 0; p < CoPers; p++) {
            PersContainer.transform.GetChild(p).gameObject.name = "Pers" + (p + 1).ToString();
            PersContainer.transform.GetChild(p).GetComponent<PersProperties>().NumberOfPersInInventory = p + 1;
        }

        string[] NewCountOfAll = CountOfAll;

        NewCountOfAll[0] = CoPers.ToString();
        NewCountOfAll[1] = CoWeap.ToString();
        NewCountOfAll[2] = CoBullets.ToString();
        NewCountOfAll[3] = CoStuff.ToString();

        File.WriteAllLines(Application.persistentDataPath + "/CountOfAll.txt", NewCountOfAll);

        ///============================================================= IMPORT NEW DATA =================================================================

        StreamWriter NewPlay = new StreamWriter(Application.persistentDataPath + "/PlayerSource.txt");
        NewPlay.WriteLine("=== Your Money ===");
        NewPlay.WriteLine(Money.ToString());

        for (int p = 1; p < CoPers + 1; p++) {
            NewPlay.WriteLine("=== Person " + p.ToString() + " ===");
            NewPlay.WriteLine(PersContainer.transform.GetChild(p - 1).GetComponent<PersProperties>().Health.ToString());
            NewPlay.WriteLine(PersContainer.transform.GetChild(p - 1).GetComponent<PersProperties>().Damage.ToString());
            NewPlay.WriteLine(PersContainer.transform.GetChild(p - 1).GetComponent<PersProperties>().Accuracy.ToString());
            NewPlay.WriteLine(PersContainer.transform.GetChild(p - 1).GetComponent<PersProperties>().Level.ToString());
            NewPlay.WriteLine(PersContainer.transform.GetChild(p - 1).GetComponent<PersProperties>().CountOfBattles.ToString());
            NewPlay.WriteLine(PersContainer.transform.GetChild(p - 1).GetComponent<PersProperties>().Price.ToString());
            NewPlay.WriteLine(PersContainer.transform.GetChild(p - 1).GetComponent<PersProperties>().Skin.ToString());
        }

        int Weap = 0;

        for (int w = 0; w < StuffContainer.transform.childCount; w++) {
            GameObject WeapLoot = StuffContainer.transform.GetChild(w).gameObject;
            if (WeapLoot.GetComponent<WeaponProperties>() != null) {
                Weap += 1;
                NewPlay.WriteLine("=== Weapon " + Weap.ToString() + " ===");
                NewPlay.WriteLine(WeapLoot.GetComponent<WeaponProperties>().Damage.ToString());
                NewPlay.WriteLine(WeapLoot.GetComponent<WeaponProperties>().Condition.ToString());
                NewPlay.WriteLine(WeapLoot.GetComponent<WeaponProperties>().Clip.ToString());
                NewPlay.WriteLine(WeapLoot.GetComponent<WeaponProperties>().Price.ToString());
                NewPlay.WriteLine(WeapLoot.GetComponent<WeaponProperties>().Skin.ToString());
            }
        }

        int Bullets = 0;

        for (int b = 0; b < BulletsContainer.transform.childCount; b++) {
            GameObject BulletLoot = BulletsContainer.transform.GetChild(b).gameObject;
            Bullets += 1;
            NewPlay.WriteLine("=== Bullets " + Bullets.ToString() + " ===");
            NewPlay.WriteLine(BulletLoot.GetComponent<Bullets>().BulletsSkin.ToString());
            NewPlay.WriteLine(BulletLoot.GetComponent<Bullets>().CountOfBullets.ToString());
        }

        int Stuff = 0;

        for (int s = 0; s < StuffContainer.transform.childCount; s++) {
            GameObject StuffLoot = StuffContainer.transform.GetChild(s).gameObject;
            if (StuffLoot.GetComponent<OtherStuff>() != null) {
                Stuff += 1;
                NewPlay.WriteLine("=== Stuff " + Stuff.ToString() + " ===");
                NewPlay.WriteLine(StuffLoot.GetComponent<OtherStuff>().Skin.ToString());
            }
        }

        NewPlay.Close();

        StreamWriter WriteCounts = new StreamWriter(Application.persistentDataPath + "/CountOfAll.txt");
        WriteCounts.WriteLine(CoPers.ToString());
        WriteCounts.WriteLine(CoWeap.ToString());
        WriteCounts.WriteLine(CoBullets.ToString());
        WriteCounts.WriteLine(CoStuff.ToString());
        WriteCounts.Close();

        int StuffNum = 0;

        string[] NewMapGen = MapGen;
        for (int i = 0; i < StuffContainer.transform.childCount; i++) {
            if (StuffContainer.transform.GetChild(i).GetComponent<OtherStuff>() != null) {
                StuffNum += 1;
                if (StuffContainer.transform.GetChild(i).GetComponent<OtherStuff>().Skin == 2) {
                    NewMapGen[2 * Columns * Rows + 12 + 10 + 6 + StuffNum] = StuffContainer.transform.GetChild(i).GetComponent<OtherStuff>().WaterLiters.ToString();
                } else {
                    NewMapGen[2 * Columns * Rows + 12 + 10 + 6 + StuffNum] = "Empty";
                }
            }
        }
        for (int a = StuffNum + 1; a < 11; a++) {
            NewMapGen[2 * Columns * Rows + 12 + 10 + 6 + a] = "Empty";
        }
        File.WriteAllLines(Application.persistentDataPath + "/MapGen.txt", NewMapGen);

        string[] NewInvSet = InvSet;
        for (int w = 1; w < 16; w++) {
            NewInvSet[21 + w] = "NaN";
        }
        for (int s = 1; s < 11; s++) {
            NewInvSet[48 + s] = "undefined";
        }
        for (int p = 0; p < 10; p++) {
            if (p < PersContainer.transform.childCount) {
                NewInvSet[11 + p] = PersContainer.transform.GetChild(p).GetComponent<PersProperties>().PositionOnField.ToString();
                if (PersContainer.transform.GetChild(p).GetComponent<PersProperties>().WeaponInHands != 0) {
                    NewInvSet[21 + PersContainer.transform.GetChild(p).GetComponent<PersProperties>().NumberOfPersInInventory] = PersContainer.transform.GetChild(p).GetComponent<PersProperties>().WeaponInHands.ToString();
                }
            } else {
                NewInvSet[11 + p] = "NaN";
            }
        }

        int WeapNum = 0;
        StuffNum = 0;

        for (int w = 0; w < StuffContainer.transform.childCount; w++) {
            if (StuffContainer.transform.GetChild(w).GetComponent<WeaponProperties>() != null) {
                WeapNum += 1;
                if (StuffContainer.transform.GetChild(w).GetComponent<WeaponProperties>().InHands == true) {
                    NewInvSet[37 + WeapNum] = "true";
                } else {
                    NewInvSet[37 + WeapNum] = "false";
                }
            }
            if (StuffContainer.transform.GetChild(w).GetComponent<OtherStuff>() != null) {
                StuffNum += 1;
                if (StuffContainer.transform.GetChild(w).GetComponent<OtherStuff>().WhichPersUseIt != 0) {
                    NewInvSet[48 + StuffNum] = StuffContainer.transform.GetChild(w).GetComponent<OtherStuff>().WhichPersUseIt.ToString();
                } else {
                    NewInvSet[48 + StuffNum] = "undefined";
                }
            }
        }

        File.WriteAllLines(Application.persistentDataPath + "/InventorySettings.txt", NewInvSet);

        SceneManager.LoadScene(2);

    }

    void GameOverAndBack() {
        GameOver.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (Winner == true && StopRandomize == true) {

            CoStuff = 0;
            CoWeap = 0;
            CoBullets = 0;
            CoPers = 0;

            for (int i = 0; i < StuffContainer.transform.childCount; i++) {
                if (StuffContainer.transform.GetChild(i).gameObject.GetComponent<OtherStuff>() != null) {
                    StuffContainer.transform.GetChild(i).gameObject.name = "Stuff" + (CoStuff + 1).ToString();
                    CoStuff += 1;
                }
                if (StuffContainer.transform.GetChild(i).gameObject.GetComponent<WeaponProperties>() != null){
                    //    if (StuffContainer.transform.GetChild(i).gameObject.GetComponent<WeaponProperties>().NumberOfWeaponInInventory != 0) {
                    //        int PersNum = StuffContainer.transform.GetChild(i).GetComponent<WeaponProperties>().NumberOfWeaponInInventory;
                    //        GameObject Pers = PersContainer.transform.Find("Pers" + PersNum.ToString()).gameObject;
                    //        GameObject Weap = StuffContainer.transform.GetChild(i).gameObject;
                    //        for (int a = 0; a < 4; a++) {
                    //            if (Pers.GetComponent<PersProperties>().Package[a] == Weap.name) {
                    //                Weap.name = "Weapon" + WeapNum.ToString();
                    //                Pers.GetComponent<PersProperties>().Package[a] = "Weapon" + WeapNum.ToString();
                    //                Weap.GetComponent<WeaponProperties>().NumberOfWeaponInInventory = 0;
                    //            }
                    //            break;
                    //        }
                    //    } else {
                    //        StuffContainer.transform.GetChild(i).gameObject.name = "Weapon" + WeapNum.ToString();
                    //    }
                    CoWeap += 1;
                }
            }

            CoBullets = BulletsContainer.transform.childCount;

            for (int i = 0; i < PersContainer.transform.childCount; i++) {
                PersContainer.transform.GetChild(i).gameObject.name = "Pers" + (i + 1).ToString();
                PersContainer.transform.GetChild(i).gameObject.GetComponent<PersProperties>().CountOfBattles += 1; 
                CoPers = CoPers + 1;
            }

            for (int a = 1; a < 4; a++) {
                GameObject BS = Instantiate(Resources.Load("LootDoll")) as GameObject;
                BS.name = "Loot" + a.ToString();
                BS.transform.SetParent(FinalPanel.transform);
                BS.transform.localPosition = FinalPanel.transform.Find("Pos" + a.ToString()).transform.localPosition;
                BS.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                GameObject FindLoot = LootPanel.transform.Find("Lot" + a.ToString()).gameObject;
                GameObject InfoText = FinalPanel.transform.Find("LootInfo" + a.ToString()).gameObject;
                if (FindLoot.GetComponent<Bullets>() != null) {
                    BS.GetComponent<LootScreen>().KindOfLoot = 0;
                    BS.GetComponent<LootScreen>().Skin = FindLoot.GetComponent<Bullets>().BulletsSkin;
                    InfoText.GetComponent<Text>().text = "for " + FindLoot.GetComponent<Bullets>().Name + "\nCount: " + FindLoot.GetComponent<Bullets>().CountOfBullets.ToString();
                }

                if (FindLoot.GetComponent<WeaponProperties>() != null) {
                    if (CoWeap < 10) {
                        BS.GetComponent<LootScreen>().KindOfLoot = 1;
                        BS.GetComponent<LootScreen>().Skin = FindLoot.GetComponent<WeaponProperties>().Skin;
                        InfoText.GetComponent<Text>().text = FindLoot.GetComponent<WeaponProperties>().Name.ToString() + "\nCondition: " + FindLoot.GetComponent<WeaponProperties>().Condition.ToString();

                        //CoWeap += 1;
                    } else {
                        BS.GetComponent<LootScreen>().KindOfLoot = 3;
                        int GetMoney = Random.Range(1, 4) * 150; ;
                        BS.GetComponent<LootScreen>().MoneyLoot = GetMoney;
                        InfoText.GetComponent<Text>().text = "Money: " + GetMoney;
                        Money = Money + GetMoney;
                        Destroy(FindLoot.gameObject);
                    }
                }
                if (FindLoot.GetComponent<OtherStuff>() != null) {
                    if (CoStuff < 10) {
                        BS.GetComponent<LootScreen>().KindOfLoot = 2;
                        BS.GetComponent<LootScreen>().Skin = FindLoot.GetComponent<OtherStuff>().Skin;
                        InfoText.GetComponent<Text>().text = FindLoot.GetComponent<OtherStuff>().Name.ToString();
                        //CoStuff += 1;
                    } else {
                        BS.GetComponent<LootScreen>().KindOfLoot = 3;
                        int GetMoney = Random.Range(1, 4) * 150; ;
                        BS.GetComponent<LootScreen>().MoneyLoot = GetMoney;
                        InfoText.GetComponent<Text>().text = "Money: " + GetMoney;
                        Money = Money + GetMoney;
                        Destroy(FindLoot.gameObject);
                    }
                }
            }

            FinalPanel.GetComponent<Animator>().SetBool("Activated", true);
            TakeLoot.gameObject.active = true;
            StopRandomize = false;
        }

        if (Defeat == true && StopRandomize == true) {
            FinalPanel.GetComponent<Animator>().SetBool("Activated", true);
            GameOver.gameObject.active = true;
            StopRandomize = false;
        }
    }
}
