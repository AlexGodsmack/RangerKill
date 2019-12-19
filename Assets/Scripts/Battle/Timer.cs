using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public GameObject TimerContainer;
    public GameObject PersContainer;
    public GameObject EnemyContainer;
    public GameObject BulletsContainer;
    public GameObject StuffContainer;
    public GameObject Pers;
    public GameObject Enemy;
    public GameObject PrevSelected;
    public GameObject SelectedAction;
    public GameObject MainPanel;
    public GameObject Sounds;
    public GameObject FinalBattlePanel;
    public Text InfoText;

    public int TimerCount;
    public float Seconds;

    public bool YourPass;
    public bool Shout = false;
    public bool ReadyToShout = false;
    public GameObject GetBullets;
    public int GetClipOfPers;

    private bool PersRushed = false;
    private float SecStep = 0.015f;

    void Start()
    {

        YourPass = true;
        TimerCount = 11;
        Seconds = 0.0f;

        for (int i = 0; i < EnemyContainer.transform.childCount; i++) {
            EnemyContainer.transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
        }

        TurnPass();

    }

    void Update()
    {

        if (Input.touchCount > 0) {
            for (int q = 0; q < Input.touchCount; ++q) {
                Touch touch = Input.GetTouch(q);
                if (touch.phase == TouchPhase.Began) {

                }
            }
        }

        if (Input.GetMouseButtonDown(0)) {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit) {
                if (hit.collider.gameObject.layer == 8) {
                    PrevSelected = Pers;
                    PrevSelected.transform.Find("Lighter").gameObject.active = false;
                    CloseStuff();
                    Pers = hit.collider.gameObject;
                    Pers.GetComponent<AudioSource>().Play();
                    Pers.transform.Find("Lighter").gameObject.active = true;
                    OpenStuff();

                    for (int a = 0; a < BulletsContainer.transform.childCount; a++) {
                        if (BulletsContainer.transform.GetChild(a).GetComponent<Bullets>().BulletsSkin == Pers.GetComponent<PersProperties>().WeaponSkin) {
                            GetBullets = BulletsContainer.transform.GetChild(a).gameObject;
                            GetClipOfPers = GetBullets.GetComponent<Bullets>().ClipOfWeapon;
                        }
                    }

                    for (int i = 0; i < EnemyContainer.transform.childCount; i++) {
                        EnemyContainer.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                    }
                }

                if (hit.collider.gameObject.layer == 18) {
                    if (SelectedAction.gameObject != null) {
                        if (SelectedAction.GetComponent<WeaponProperties>() != null) {
                            SelectedAction.GetComponent<WeaponProperties>().IsActive = false;
                        }
                        if (SelectedAction.GetComponent<OtherStuff>() != null) {
                            SelectedAction.GetComponent<WeaponProperties>().IsActive = false;
                        }
                    }
                    SelectedAction = hit.collider.gameObject;
                    MainPanel.GetComponent<AudioSource>().Play();
                    if (SelectedAction.GetComponent<WeaponProperties>() != null) {
                        Sounds.transform.Find("Reload").GetComponent<AudioSource>().Play();
                        SelectedAction.GetComponent<WeaponProperties>().IsActive = true;
                    } else if(SelectedAction.GetComponent<OtherStuff>() != null) {
                        SelectedAction.GetComponent<OtherStuff>().IsActive = true;
                        if (SelectedAction.GetComponent<OtherStuff>().Skin == 1) {
                            StuffContainer.transform.Find("Pos" + SelectedAction.GetComponent<OtherStuff>().NumberOfStuffInInv).GetComponent<SpriteRenderer>().enabled = true;
                            Sounds.transform.Find("Heal").GetComponent<AudioSource>().Play();
                            for (int i = 0; i < 4; i++) {
                                if (Pers.GetComponent<PersProperties>().Package[i] == SelectedAction.name) {
                                    Pers.GetComponent<PersProperties>().Package[i] = "None";
                                }
                            }
                            Destroy(SelectedAction);
                            Pers.GetComponent<PersProperties>().Health = Pers.GetComponent<PersProperties>().FullHealth;
                            Shout = false;
                            YourPass = false;
                            TimerCount = 11;
                            CloseStuff();
                            TurnPass();
                        }
                        if (SelectedAction.GetComponent<OtherStuff>().Skin == 3) {
                            StuffContainer.transform.Find("Pos" + SelectedAction.GetComponent<OtherStuff>().NumberOfStuffInInv).GetComponent<SpriteRenderer>().enabled = true;
                            Sounds.transform.Find("Fury").GetComponent<AudioSource>().Play();
                            for (int i = 0; i < 4; i++) {
                                if (Pers.GetComponent<PersProperties>().Package[i] == SelectedAction.name) {
                                    Pers.GetComponent<PersProperties>().Package[i] = "None";
                                }
                            }
                            Camera.main.GetComponent<Tremor>().Delay = 1.5f;
                            Camera.main.GetComponent<Tremor>().Activated = true;
                            Destroy(SelectedAction);
                            Pers.GetComponent<PersProperties>().PowerOfShot = Pers.GetComponent<PersProperties>().PowerOfShot * 3;
                            PersRushed = true;
                        }
                    }
                }

                if(hit.collider.gameObject.layer == 16) {
                    if (GetBullets.GetComponent<Bullets>().CountOfBullets - GetClipOfPers >= 0) {
                        Enemy = hit.collider.gameObject;
                        Enemy.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
                        if (Pers.GetComponent<PersProperties>().WeaponSkin == 10) {
                            Camera.main.GetComponent<Tremor>().Activated = true;
                        }
                        Pers.transform.Find("Lighter").gameObject.active = false;
                        Enemy.GetComponent<EnemyProperties>().Health = Enemy.GetComponent<EnemyProperties>().Health - Pers.GetComponent<PersProperties>().PowerOfShot;
                        if (PersRushed == true) {
                            PersRushed = false;
                            Pers.GetComponent<PersProperties>().PowerOfShot = Pers.GetComponent<PersProperties>().PowerOfShot / 3;
                        }
                        GetBullets.GetComponent<Bullets>().CountOfBullets -= GetClipOfPers;
                        Seconds = 0.0f;
                        Shout = true;
                    } else {
                        CloseStuff();
                        InfoText.text = "Out of bullets";
                    }
                }
            }
        }

        if (YourPass == true) {

            if (Shout == true) {
                Seconds = Seconds + SecStep;
                if (Seconds >= 1.0f) {
                    if (Enemy.GetComponent<EnemyProperties>().Health <= 0) {
                        Destroy(Enemy);
                    } else {
                        Enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }
                    Seconds = 0.0f;
                    TimerCount = 11;
                    Shout = false;
                    YourPass = false;
                    TurnPass();
                    CloseStuff();
                }
            }
            else if (Shout == false) {
                Seconds = Seconds + SecStep;
                if (Seconds >= 1.0f) {
                    TimerCount = TimerCount - 1;
                    GameObject.Find(this.name + "/Sec" + TimerCount.ToString()).GetComponent<Image>().enabled = false;
                    Seconds = 0.0f;
                }
                if (TimerCount == 1) {
                    TimerCount = 11;
                    YourPass = false;
                    TurnPass();
                    CloseStuff();
                }
            }

        }
        else {

            Seconds = Seconds + SecStep;
            if (Seconds >= 1.0f) {
                TimerCount = TimerCount - 1;
                Seconds = 0.0f;
            }

            if (TimerCount == 11) {
                for (int i = 0; i < PersContainer.transform.childCount; i++) {
                    PersContainer.transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
                }
                for (int i = 0; i < EnemyContainer.transform.childCount; i++) {
                    EnemyContainer.transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
                }
                for (int i = 0; i < 10; i++) {
                    TimerContainer.transform.GetChild(i).GetComponent<Image>().enabled = false;
                }
                Pers.transform.Find("Lighter").gameObject.active = false;
                Pers = GameObject.Find("DummySelector").gameObject;
                PrevSelected = GameObject.Find("DummySelector").gameObject;
            }

            if (EnemyContainer.transform.childCount != 0) {

                if (TimerCount == 10 && Seconds == 0.0f) {
                    int e = EnemyContainer.transform.childCount;
                    int RandomEnemy = Random.Range(0, e);
                    Enemy = EnemyContainer.transform.GetChild(RandomEnemy).gameObject;
                    Enemy.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
                }

                if (TimerCount == 9 && Seconds == 0.0f) {
                    int p = PersContainer.transform.childCount;
                    int RandomPers = Random.Range(0, p);
                    Pers = PersContainer.transform.GetChild(RandomPers).gameObject;
                    Pers.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
                    Pers.GetComponent<PersProperties>().Health = Pers.GetComponent<PersProperties>().Health - (Enemy.GetComponent<EnemyProperties>().PowerOfShot - Random.Range(10, 30));
                }

                if (TimerCount == 8 && Seconds == 0.0f) {
                    Enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    Pers.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    if (Pers.GetComponent<PersProperties>().Health <= 0) {
                        for (int i = 0; i < 4; i++) {
                            if (Pers.GetComponent<PersProperties>().Package[i] != "None") {
                                Destroy(StuffContainer.transform.Find(Pers.GetComponent<PersProperties>().Package[i].ToString()).gameObject);
                            }
                        }
                        Destroy(Pers);
                    }
                }
                if (PersContainer.transform.childCount != 0) {
                    if (TimerCount == 7 && Seconds == 0.0f) {
                        for (int i = 1; i < 11; i++) {
                            GameObject.Find(this.name + "/Sec" + i.ToString()).GetComponent<Image>().enabled = true;
                        }
                        for (int i = 0; i < PersContainer.transform.childCount; i++) {
                            PersContainer.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                        }

                        Pers = GameObject.Find("DummySelector").gameObject;
                        PrevSelected = GameObject.Find("DummySelector").gameObject;
                        YourPass = true;
                        TimerCount = 11;
                        TurnPass();
                    }
                }else if(PersContainer.transform.childCount == 0) {
                    Camera.main.GetComponent<FinalOfBattle>().Defeat = true;
                    Camera.main.GetComponent<FinalOfBattle>().StopRandomize = true;
                    TurnPass();
                    FinalBattlePanel.GetComponent<AudioSource>().Play();
                    this.GetComponent<Timer>().enabled = false;
                }

            } else {
                Camera.main.GetComponent<FinalOfBattle>().Winner = true;
                Camera.main.GetComponent<FinalOfBattle>().StopRandomize = true;
                TurnPass();
                FinalBattlePanel.GetComponent<AudioSource>().Play();
                this.GetComponent<Timer>().enabled = false;
            }
        }
    }

    void OpenStuff() {
        InfoText.gameObject.active = false;
        if (Pers.GetComponent<PersProperties>() != null) {
            for (int i = 0; i < 4; i++) {
                if (Pers.GetComponent<PersProperties>().Package[i] != "None") {
                    GameObject FindSome = StuffContainer.transform.Find(Pers.GetComponent<PersProperties>().Package[i]).gameObject;
                    FindSome.GetComponent<SpriteRenderer>().enabled = true;
                    FindSome.GetComponent<Collider2D>().enabled = true;
                    if (FindSome.GetComponent<OtherStuff>() != null) {
                        if (FindSome.GetComponent<OtherStuff>().Skin == 2) {
                            FindSome.GetComponent<Collider2D>().enabled = false;
                        }
                    }
                }else{
                    StuffContainer.transform.Find("Pos" + (i + 1).ToString()).GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }

    void CloseStuff() {
        for (int i = 0; i < StuffContainer.transform.childCount; i++) {
            StuffContainer.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            if (StuffContainer.transform.GetChild(i).GetComponent<Collider2D>() != null) {
                StuffContainer.transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void TurnPass() {
        InfoText.gameObject.active = true;
        if (YourPass == true) {
            InfoText.text = "1.Choose your slave\n2.Choose action";
        }else if(YourPass == false) {
            InfoText.text = "Now your enemy turn";
        }
        if (PersContainer.transform.childCount == 0) {
            InfoText.text = "You lose \nGAME OVER";
        }
        if (EnemyContainer.transform.childCount == 0) {
            InfoText.text = "You Win";
        }
    }

}
