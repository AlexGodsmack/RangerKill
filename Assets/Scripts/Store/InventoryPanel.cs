using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    //[Header("Anchors")]
    //public GameObject Background;
    //public GameObject AnchorCenter;
    //public GameObject AnchorMidRight;
    //public GameObject SlavesInv;
    //public GameObject WeaponInv;
    //public GameObject StuffInv;
    //[Header("Main Objects")]
    //public GameObject HeadSwitcher;
    //public GameObject PANELS;
    //public GameObject STORE;
    ////public GameObject WEAPANDSTUFF;
    //public GameObject INVENTORY;
    //public GameObject InvSwitcher;
    //public GameObject[] Indicators;
    //[Header("Active Objects")]
    //public GameObject isActiveSlave;
    //public GameObject isActiveWeapon;
    //public GameObject isActiveStuff;
    //[Header("Heal/Repair")]
    //public GameObject RepairButton;
    //public GameObject HealButton;
    //[Header("Sounds")]
    //public AudioSource TakeWeapon;
    //public AudioSource AssignWeapon;
    //public AudioSource PutSlave;
    //public AudioSource PickMonitor;
    //public AudioSource RepairWeapon;
    //public AudioSource MedicinePut;
    //public AudioSource BuffPut;
    //public AudioSource WaterPut;
    //public AudioSource HealSlave;
    //public AudioSource Select;
    //[Header("Text")]
    //public GameObject InfoText;

    //public PlayerInventory PlayInv;

    //private int SlavesLayer = 8;
    //private int WeaponLayer = 9;
    //private int StuffLayer = 11;
    //private int FieldLayer = 13;



    //void Start()
    //{
    //    Background.transform.position = AnchorCenter.transform.position + new Vector3(0, 0, 1);
    //    SlavesInv.transform.position = AnchorCenter.transform.position + new Vector3(-0.9f, -0.1f, 0);
    //    WeaponInv.transform.position = AnchorCenter.transform.position + new Vector3(0.9f, -0.1f, 0);
    //    StuffInv.transform.position = WeaponInv.transform.position;
    //    InvSwitcher.transform.position = AnchorMidRight.transform.position + new Vector3(0, 0, -0.1f);
    //    InfoText.GetComponent<TextMesh>().text = "Select slave\nor weapon\nor switch to\nother panel";
    //    StuffInv.active = false;

    //}

    //void OnDisable() {
    //    isActiveSlave = null;
    //    isActiveWeapon = null;
    //    isActiveStuff = null;
    //}

    //void OnEnable() {
    //    CloseSlavesFields();
    //    CloseWeapFields();
    //    RepairButton.GetComponent<ButtonSample>().isActive = false;
    //    RepairButton.gameObject.active = false;
    //    HealButton.GetComponent<ButtonSample>().isActive = false;
    //    HealButton.active = false;
    //    ClearOldInventory();
    //    ImportNewInventory();
    //}

    //void ClearOldInventory() {
    //    for (int s = 0; s < INVENTORY.transform.Find("SlavesInv/Slaves").transform.childCount; s++) {
    //        GameObject Pack = INVENTORY.transform.Find("SlavesInv/Slaves").GetChild(s).gameObject.GetComponent<SlaveProperties>().InventoryPack.gameObject;
    //        Destroy(Pack);
    //        Destroy(INVENTORY.transform.Find("SlavesInv/Slaves").GetChild(s).gameObject);
    //    }
    //    for (int w = 0; w < INVENTORY.transform.Find("WeaponInv/Weapons").transform.childCount; w++) {
    //        Destroy(INVENTORY.transform.Find("WeaponInv/Weapons").GetChild(w).gameObject);
    //    }
    //    for (int s = 0; s < INVENTORY.transform.Find("StuffInv/Stuff").transform.childCount; s++) {
    //        Destroy(INVENTORY.transform.Find("StuffInv/Stuff").GetChild(s).gameObject);
    //    }
    //}

    //void ImportNewInventory() {

    //    int SlavePlace = 0;
    //    foreach (GameObject Slv in PlayInv.SlavePlace) {
    //        if (Slv != null) {
    //            GameObject Slave = Instantiate(Slv) as GameObject;
    //            Slave.name = Slv.name;

    //            SlaveProperties Prop = Slave.GetComponent<SlaveProperties>();
    //            Prop.Bought = false;
    //            Prop.isActive = false;
    //            //Prop.ShowHealthbar = true;
    //            Prop.Goal = INVENTORY.transform.Find("WeaponInv").transform.GetChild(5).gameObject;
    //            Prop.SlaveXRef = Slv;

    //            Slave.transform.position = INVENTORY.transform.Find("SlavesInv").transform.GetChild(SlavePlace).transform.position + new Vector3(0, 0, -0.2f);
    //            Slave.transform.SetParent(INVENTORY.transform.Find("SlavesInv/Slaves").transform);

    //            int ItemPlace = 0;
    //        }
    //        SlavePlace += 1;
    //    }

    //    int WeapPlace = 0;
    //    foreach (GameObject Wpn in PlayInv.WeaponPlace) {
    //        if (Wpn != null) {
    //            GameObject Weap = Instantiate(Wpn) as GameObject;

    //            Weap.transform.position = INVENTORY.transform.Find("WeaponInv").transform.GetChild(WeapPlace).transform.position + new Vector3(0, 0, -0.2f);
    //            Weap.transform.SetParent(INVENTORY.transform.Find("WeaponInv/Weapons").transform);
    //            Weap.GetComponent<WeaponProperties>().WeaponXRef = Wpn;
    //            Weap.GetComponent<WeaponProperties>().Bought = false;
    //            Weap.GetComponent<WeaponProperties>().isActive = false;
    //        }
    //        WeapPlace += 1;
    //    }

    //    int StuffPlace = 0;
    //    foreach (GameObject Stff in PlayInv.StuffPlace) {
    //        if (Stff != null) {
    //            GameObject NewStff = Instantiate(Stff) as GameObject;

    //            NewStff.transform.position = INVENTORY.transform.Find("StuffInv").transform.GetChild(StuffPlace).transform.position + new Vector3(0, 0, -0.2f);
    //            NewStff.transform.SetParent(INVENTORY.transform.Find("StuffInv/Stuff").transform);
    //            NewStff.GetComponent<OtherStuff>().StuffXRef = Stff;
    //            NewStff.GetComponent<OtherStuff>().Bought = false;
    //            NewStff.GetComponent<OtherStuff>().isActive = false;
    //        }
    //        StuffPlace += 1;
    //    }

    //}

    //void OpenSlavesFields() {
    //    for (int a = 0; a < SlavesInv.transform.childCount; a++) {
    //        if (SlavesInv.transform.GetChild(a).gameObject.layer == FieldLayer) {
    //            SlavesInv.transform.GetChild(a).GetComponent<Fields>().isActive = true;
    //        }
    //    }
    //}

    //void CloseSlavesFields() {
    //    for (int a = 0; a < SlavesInv.transform.childCount; a++) {
    //        if (SlavesInv.transform.GetChild(a).gameObject.layer == FieldLayer) {
    //            SlavesInv.transform.GetChild(a).GetComponent<Fields>().isActive = false;
    //        }
    //    }
    //}

    //void OpenWeapFields() {
    //    for (int w = 0; w < WeaponInv.transform.childCount; w++) {
    //        if (WeaponInv.transform.GetChild(w).gameObject.layer == FieldLayer) {
    //            WeaponInv.transform.GetChild(w).GetComponent<Fields>().isActive = true;
    //        }
    //    }
    //}
    //void CloseWeapFields() {
    //    for (int w = 0; w < WeaponInv.transform.childCount; w++) {
    //        if (WeaponInv.transform.GetChild(w).gameObject.layer == FieldLayer) {
    //            WeaponInv.transform.GetChild(w).GetComponent<Fields>().isActive = false;
    //        }
    //    }
    //}

    //void OpenStuffField() {
    //    for (int s = 0; s < StuffInv.transform.childCount; s++) {
    //        if (StuffInv.transform.GetChild(s).gameObject.layer == FieldLayer) {
    //            StuffInv.transform.GetChild(s).GetComponent<Fields>().isActive = true;
    //        }
    //    }
    //}

    //void CloseStuffField() {
    //    for (int s = 0; s < StuffInv.transform.childCount; s++) {
    //        if (StuffInv.transform.GetChild(s).gameObject.layer == FieldLayer) {
    //            StuffInv.transform.GetChild(s).GetComponent<Fields>().isActive = false;
    //        }
    //    }
    //}

    //void CheckWeight() {
    //    int ConstrainedPlaces = 0;
    //    int Number = isActiveSlave.GetComponent<SlaveProperties>().Number;
    //    for (int CheckPlace = 0; CheckPlace < 4; CheckPlace++) {
    //        if (PANELS.GetComponent<PlayerInventory>().SlavesBag[Number - 1].Place[CheckPlace] != null) {
    //            ConstrainedPlaces += 1;
    //        }
    //    }
    //    if (ConstrainedPlaces == 4) {
    //        isActiveSlave.GetComponent<SlaveProperties>().FullPackage = true;
    //        isActiveSlave.GetComponent<SlaveProperties>().SlaveXRef.gameObject.GetComponent<SlaveProperties>().FullPackage = true;
    //    } else {
    //        isActiveSlave.GetComponent<SlaveProperties>().FullPackage = false;
    //        isActiveSlave.GetComponent<SlaveProperties>().SlaveXRef.gameObject.GetComponent<SlaveProperties>().FullPackage = false;
    //    }
    //}

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0)) {

    //        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

    //        ///**************************************************** HIT = SLAVE *********************************************************

    //        if (hit.collider.gameObject.layer == SlavesLayer) {
    //            if (isActiveSlave != null) {

    //                //if (isActiveStuff != null) {      // Обнуляю все выделенные шмотки в инвентаре
    //                //    isActiveStuff.GetComponent<OtherStuff>().isActive = false;
    //                //    isActiveStuff = null;
    //                //    CloseStuffField();
    //                //}

    //                isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
    //                isActiveSlave = null;
    //                //isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
    //            }

    //            isActiveSlave = hit.collider.gameObject;
    //            SlaveProperties SlvProp = isActiveSlave.GetComponent<SlaveProperties>();
    //            SlvProp.isActive = true;

    //            int Number = SlvProp.Number;
    //            GameObject GetPack = SlvProp.InventoryPack.gameObject;

    //            if (isActiveWeapon != null) {
    //                WeaponProperties WpnProp = isActiveWeapon.GetComponent<WeaponProperties>();
    //                if (SlvProp.HaveGun == false && SlvProp.FullPackage == false) { // Если у раба нет пушки и его рюкзак не заполнен
    //                    if (WpnProp.Bought == false) {          // Если оружие не лежит в чьем-то другом инвентаре

    //                        WpnProp.isActive = false;
    //                        WpnProp.Bought = true;
    //                        AssignWeapon.Play();
    //                        GameObject Ref = WpnProp.WeaponXRef.gameObject;

    //                        foreach (SlavePackage Pack in PlayInv.SlavesBag) {
    //                            if (Pack.NumberOfSlave == Number) {
    //                                for (int a = 0; a < Pack.Place.Length; a++) {
    //                                    if (Pack.Place[a] == null) {
    //                                        Pack.Place[a] = Ref;
    //                                        isActiveWeapon.transform.SetParent(GetPack.transform);
    //                                        isActiveWeapon.transform.localPosition = GetPack.transform.GetChild(a).gameObject.transform.localPosition;
    //                                        GetPack.transform.GetChild(a).gameObject.active = false;
    //                                        break;
    //                                    }
    //                                }
    //                            }
    //                        }
    //                        PlayInv.WeaponPlace[Ref.transform.GetSiblingIndex()] = null;
    //                        //foreach (GameObject wpn in PlayInv.WeaponPlace) {
    //                        //    if (wpn == Ref) {
    //                        //    }
    //                        //}
    //                        SlvProp.HaveGun = true;

    //                        /// Update Power of Shot

    //                        SlvProp.WeaponXRef = isActiveWeapon;
    //                        SlvProp.SlaveXRef.gameObject.GetComponent<SlaveProperties>().WeaponXRef = WpnProp.WeaponXRef;
    //                        SlvProp.SlaveXRef.GetComponent<SlaveProperties>().HaveGun = true;
    //                    }
    //                }

    //                //isActiveWeapon.GetComponent<WeaponProperties>().isActive = false;
    //                isActiveWeapon = null;
    //                CloseWeapFields();

    //            }
    //            if (isActiveStuff != null && isActiveSlave.GetComponent<SlaveProperties>().FullPackage == false) {

    //                GameObject Ref = isActiveStuff.GetComponent<OtherStuff>().StuffXRef.gameObject;

    //                if (isActiveStuff.GetComponent<OtherStuff>().Bought == false) {
    //                    foreach (SlavePackage Pack in PlayInv.SlavesBag) {
    //                        if (Pack.NumberOfSlave == Number) {
    //                            for (int a = 0; a < Pack.Place.Length; a++) {
    //                                if (Pack.Place[a] == null) {
    //                                    Pack.Place[a] = Ref;
    //                                    break;
    //                                }
    //                            }
    //                        }
    //                    }
    //                    for (int a = 0; a < PlayInv.StuffPlace.Length; a++ ) {
    //                        if (PlayInv.StuffPlace[a] == Ref) {
    //                            PlayInv.StuffPlace[a] = null;
    //                        }
    //                    }
    //                    //for (CheckPlace = 0; CheckPlace < 4; CheckPlace++) {
    //                    //    if (PANELS.GetComponent<PlayerInventory>().SlavesBag[Number - 1].Place[CheckPlace] == null) {
    //                    //        PANELS.GetComponent<PlayerInventory>().SlavesBag[Number - 1].Place[CheckPlace] = Ref;
    //                    //        break;
    //                    //    }
    //                    //}
    //                    //for (int a = 0; a < PANELS.GetComponent<PlayerInventory>().StuffPlace.Length; a++) {
    //                    //    if (PANELS.GetComponent<PlayerInventory>().StuffPlace[a] == Ref) {
    //                    //        PANELS.GetComponent<PlayerInventory>().StuffPlace[a] = null;
    //                    //    }
    //                    //}

    //                    isActiveStuff.transform.SetParent(GetPack.transform);
    //                    GetPack.active = true;
    //                    foreach (Transform Place in GetPack.transform) {
    //                        if (Place.gameObject.active != false) {
    //                            Debug.Log(Place.name);
    //                            isActiveStuff.transform.position = Place.gameObject.transform.position;
    //                            Place.gameObject.active = false;
    //                            break;
    //                        }
    //                    }
    //                    //isActiveStuff.transform.localPosition = GetPack.transform.GetChild(CheckPlace).transform.localPosition;
    //                    //GetPack.transform.GetChild(CheckPlace).gameObject.active = false;

    //                    if (isActiveStuff.GetComponent<OtherStuff>().Skin == 1) {
    //                        MedicinePut.Play();
    //                    }
    //                    if (isActiveStuff.GetComponent<OtherStuff>().Skin == 2) {
    //                        WaterPut.Play();
    //                    }
    //                    if (isActiveStuff.GetComponent<OtherStuff>().Skin == 3) {
    //                        BuffPut.Play();
    //                    }

    //                    isActiveStuff.GetComponent<OtherStuff>().Bought = true;
    //                }

    //                isActiveStuff.GetComponent<OtherStuff>().isActive = false;
    //                isActiveStuff = null;
    //                CloseStuffField();
    //            }

    //            CheckWeight();

    //            OpenSlavesFields();

    //            GetPack.active = true;
    //            RepairButton.active = false;
    //            HealButton.active = false;
    //            isActiveSlave.GetComponent<SlaveProperties>().isActive = true;
    //            InfoText.GetComponent<TextMesh>().text = "Health: " + isActiveSlave.GetComponent<SlaveProperties>().Health +
    //                "\nDamage: " + isActiveSlave.GetComponent<SlaveProperties>().Damage +
    //                "\nAccuracy: " + isActiveSlave.GetComponent<SlaveProperties>().Accuracy +
    //                "\nLevel: " + isActiveSlave.GetComponent<SlaveProperties>().Level +
    //                "\nBattles: " + isActiveSlave.GetComponent<SlaveProperties>().Battles +
    //                "\n\nPower: " + isActiveSlave.GetComponent<SlaveProperties>().PowerOfShot;
    //            isActiveSlave.GetComponent<AudioSource>().Play();
    //        }

    //        ///**************************************************** HIT = WEAPON *********************************************************

    //        if (hit.collider.gameObject.layer == WeaponLayer) {
    //            if (isActiveWeapon != null) {
    //                isActiveWeapon.GetComponent<WeaponProperties>().isActive = false;
    //            }
    //            if (isActiveStuff != null) {
    //                isActiveStuff.GetComponent<OtherStuff>().isActive = false;
    //                isActiveStuff = null;
    //            }

    //            OpenWeapFields();
    //            CloseStuffField();

    //            isActiveWeapon = hit.collider.gameObject;
    //            isActiveWeapon.GetComponent<WeaponProperties>().isActive = true;

    //            RepairButton.active = true;
    //            HealButton.active = false;

    //            int RepairPrice = (isActiveWeapon.GetComponent<WeaponProperties>().Price / isActiveWeapon.GetComponent<WeaponProperties>().Condition) * (10 - isActiveWeapon.GetComponent<WeaponProperties>().Condition);

    //            if (isActiveWeapon.GetComponent<WeaponProperties>().Bought == false) {
    //                if (isActiveSlave != null) {
    //                    isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
    //                    isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.active = false;
    //                    CloseSlavesFields();
    //                    isActiveSlave = null;
    //                }
    //                RepairButton.active = true;
    //                if (isActiveWeapon.GetComponent<WeaponProperties>().Condition < 10) {
    //                    if (RepairPrice <= PANELS.GetComponent<PlayerInventory>().Money) {
    //                        RepairButton.GetComponent<ButtonSample>().isActive = true;
    //                    } else {
    //                        RepairButton.GetComponent<ButtonSample>().isActive = false;
    //                    }
    //                } else {
    //                    RepairButton.GetComponent<ButtonSample>().isActive = false;
    //                }
    //                InfoText.GetComponent<TextMesh>().text = isActiveWeapon.GetComponent<WeaponProperties>().WeapName +
    //                    "\nDamage: " + isActiveWeapon.GetComponent<WeaponProperties>().Damage +
    //                    "\nCondition: " + isActiveWeapon.GetComponent<WeaponProperties>().Condition +
    //                    "\nBullets: " + isActiveWeapon.GetComponent<WeaponProperties>().Bullets +
    //                    "\nEfficiency: " + isActiveWeapon.GetComponent<WeaponProperties>().Efficiency +
    //                    "\n\nRepair: " + RepairPrice +
    //                    "\nMoney: " + PANELS.GetComponent<PlayerInventory>().Money;
    //                TakeWeapon.Play();
    //                Select.Play();
    //            } else {
    //                RepairButton.active = false;
    //                PickMonitor.Play();
    //            }


    //        }

    //        ///**************************************************** HIT = STUFF *********************************************************

    //        if (hit.collider.gameObject.layer == StuffLayer) {
    //            if (isActiveStuff != null) {
    //                isActiveStuff.GetComponent<OtherStuff>().isActive = false;
    //            }
    //            if (isActiveWeapon != null) {
    //                isActiveWeapon.GetComponent<WeaponProperties>().isActive = false;
    //                isActiveWeapon = null;
    //            }

    //            CloseWeapFields();
    //            OpenStuffField();

    //            isActiveStuff = hit.collider.gameObject;
    //            isActiveStuff.GetComponent<OtherStuff>().isActive = true;
    //            if (isActiveStuff.GetComponent<OtherStuff>().Bought == false) {
    //                Select.Play();
    //                if (isActiveSlave != null) {
    //                    isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
    //                    isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.active = false;
    //                    CloseSlavesFields();
    //                    isActiveSlave = null;
    //                }
    //                InfoText.GetComponent<TextMesh>().text = isActiveStuff.GetComponent<OtherStuff>().ShortDescription;
    //            } else {
    //                PickMonitor.Play();
    //                if (isActiveStuff.GetComponent<OtherStuff>().Skin == 1) {
    //                    InfoText.GetComponent<TextMesh>().text = isActiveStuff.GetComponent<OtherStuff>().ShortDescription;
    //                    HealButton.active = true;
    //                    isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
    //                    if (isActiveSlave.GetComponent<SlaveProperties>().Health == isActiveSlave.GetComponent<SlaveProperties>().FullHealth) {
    //                        HealButton.GetComponent<ButtonSample>().isActive = false;
    //                    } else {
    //                        HealButton.GetComponent<ButtonSample>().isActive = true;
    //                    }
    //                }
    //            }
    //        }

    //        ///**************************************************** HIT = FIELD *********************************************************

    //        if (hit.collider.gameObject.layer == FieldLayer) {

    //            GameObject Place = hit.collider.gameObject;

    //            if (Place.transform.parent == SlavesInv.transform) {
    //                isActiveSlave.transform.localPosition = Place.transform.localPosition + new Vector3(0, 0, -0.2f);
    //                for (int s = 0; s < PlayInv.SlavePlace.Length; s++) {
    //                    if (isActiveSlave.GetComponent<SlaveProperties>().SlaveXRef == PlayInv.SlavePlace[s]) {
    //                        PlayInv.SlavePlace[s] = null;
    //                    }
    //                    if (s == Place.GetComponent<Fields>().Number - 1) {
    //                        PlayInv.SlavePlace[s] = isActiveSlave.GetComponent<SlaveProperties>().SlaveXRef.gameObject;
    //                    }
    //                }

    //                isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
    //                isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.active = false;
    //                isActiveSlave = null;
    //                CloseSlavesFields();
    //                CloseWeapFields();
    //                CloseStuffField();
    //                PutSlave.Play();
    //            }
    //            if (Place.transform.parent == WeaponInv.transform) {
    //                isActiveWeapon.transform.localPosition = Place.transform.localPosition + new Vector3(0, 0, -0.2f);
    //                if (isActiveWeapon.GetComponent<WeaponProperties>().Bought == false) {
    //                    for (int w = 0; w < PlayInv.WeaponPlace.Length; w++) {
    //                        if (isActiveWeapon.GetComponent<WeaponProperties>().WeaponXRef == PlayInv.WeaponPlace[w]) {
    //                            PlayInv.WeaponPlace[w] = null;
    //                        }
    //                        if (w == Place.GetComponent<Fields>().Number - 1) {
    //                            PlayInv.WeaponPlace[w] = isActiveWeapon.GetComponent<WeaponProperties>().WeaponXRef.gameObject;
    //                        }
    //                    }
    //                } else {
    //                    int NumSlave = isActiveSlave.GetComponent<SlaveProperties>().Number;
    //                    foreach (SlavePackage Pack in PlayInv.SlavesBag) {
    //                        if (Pack.NumberOfSlave == NumSlave) {
    //                            for (int w = 0; w < Pack.Place.Length; w++) {
    //                                if (Pack.Place[w] == isActiveWeapon.GetComponent<WeaponProperties>().WeaponXRef) {
    //                                    Pack.Place[w] = null;

    //                                    GameObject Bag = isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.gameObject;
    //                                    Bag.transform.GetChild(w).gameObject.active = true;

    //                                    isActiveWeapon.transform.SetParent(WeaponInv.transform.Find("Weapons").transform);
    //                                    isActiveWeapon.transform.position = Place.transform.position + new Vector3(0, 0, -0.2f);
    //                                    PlayInv.WeaponPlace[Place.transform.GetSiblingIndex()] = isActiveWeapon.GetComponent<WeaponProperties>().WeaponXRef;
    //                                    //Debug.Log(isActiveWeapon.name+ " " + isActiveWeapon.GetComponent<WeaponProperties>().WeaponXRef.name);
    //                                    break;
    //                                }
    //                            }
    //                        }
    //                    }
    //                    //for (int w = 0; w < PlayInv.SlavesBag[NumSlave - 1].Place.Length; w++) {
    //                    //    if (isActiveWeapon.GetComponent<WeaponProperties>().WeaponXRef == PlayInv.SlavesBag[NumSlave - 1].Place[w].gameObject) {
    //                    //        PlayInv.SlavesBag[NumSlave - 1].Place[w] = null;
    //                    //        GameObject Bag = isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.gameObject;
    //                    //        Bag.transform.GetChild(w).gameObject.active = true;
    //                    //        isActiveWeapon.transform.SetParent(WeaponInv.transform);
    //                    //        break;
    //                    //    }
    //                    //}
    //                    //for (int bw = 0; bw < PANELS.GetComponent<PlayerInventory>().WeaponPlace.Length; bw++) {
    //                    //    if (Place.GetComponent<Fields>().Number - 1 == bw) {
    //                    //        PANELS.GetComponent<PlayerInventory>().WeaponPlace[bw] = isActiveWeapon.GetComponent<WeaponProperties>().WeaponXRef;
    //                    //        isActiveWeapon.transform.localPosition = Place.transform.localPosition + new Vector3(0, 0, -0.2f);
    //                    //        isActiveWeapon.transform.SetParent(WeaponInv.transform.Find("Weapons").transform);
    //                    //        break;
    //                    //    }
    //                    //}
    //                    CheckWeight();
    //                    isActiveSlave.GetComponent<SlaveProperties>().HaveGun = false;
    //                    isActiveSlave.GetComponent<SlaveProperties>().SlaveXRef.GetComponent<SlaveProperties>().HaveGun = false;
    //                    isActiveSlave.GetComponent<SlaveProperties>().WeaponXRef = null;
    //                    isActiveSlave.GetComponent<SlaveProperties>().SlaveXRef.gameObject.GetComponent<SlaveProperties>().WeaponXRef = null;
    //                    isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
    //                    isActiveWeapon.GetComponent<WeaponProperties>().Bought = false;
    //                    isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
    //                    CloseSlavesFields();
    //                    isActiveSlave = null;
    //                }
    //                isActiveWeapon.GetComponent<WeaponProperties>().isActive = false;
    //                isActiveWeapon = null;
    //                CloseSlavesFields();
    //                CloseWeapFields();
    //                CloseStuffField();

    //                TakeWeapon.Play();
    //            }
    //            if (Place.transform.parent == StuffInv.transform) {
    //                if (isActiveStuff.GetComponent<OtherStuff>().Bought == false) {
    //                    isActiveStuff.transform.localPosition = Place.transform.localPosition + new Vector3(0, 0, -0.2f);
    //                    if (isActiveStuff.GetComponent<OtherStuff>().Bought == false) {
    //                        for (int s = 0; s < PANELS.GetComponent<PlayerInventory>().StuffPlace.Length; s++) {
    //                            if (isActiveStuff.GetComponent<OtherStuff>().StuffXRef == PANELS.GetComponent<PlayerInventory>().StuffPlace[s]) {
    //                                PANELS.GetComponent<PlayerInventory>().StuffPlace[s] = null;
    //                            }
    //                            if (s == Place.GetComponent<Fields>().Number - 1) {
    //                                PANELS.GetComponent<PlayerInventory>().StuffPlace[s] = isActiveStuff.GetComponent<OtherStuff>().StuffXRef.gameObject;
    //                            }
    //                        }
    //                        //isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.active = false;
    //                        //isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
    //                    }
    //                } else {
    //                    for (int s = 0; s < 4; s++) {
    //                        if (PANELS.GetComponent<PlayerInventory>().SlavesBag[isActiveSlave.GetComponent<SlaveProperties>().Number - 1].Place[s] == isActiveStuff.GetComponent<OtherStuff>().StuffXRef) {
    //                            PANELS.GetComponent<PlayerInventory>().SlavesBag[isActiveSlave.GetComponent<SlaveProperties>().Number - 1].Place[s] = null;
    //                            GameObject GetPack = isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.gameObject;
    //                            GetPack.transform.GetChild(s).gameObject.active = true;
    //                        }
    //                    }
    //                    for (int a = 0; a < PANELS.GetComponent<PlayerInventory>().StuffPlace.Length; a++) {
    //                        if (a == Place.GetComponent<Fields>().Number - 1) {
    //                            PANELS.GetComponent<PlayerInventory>().StuffPlace[a] = isActiveStuff.GetComponent<OtherStuff>().StuffXRef;
    //                            isActiveStuff.transform.SetParent(StuffInv.transform);
    //                            isActiveStuff.transform.localPosition = Place.transform.localPosition + new Vector3(0, 0, -0.2f);
    //                            isActiveStuff.transform.SetParent(StuffInv.transform.Find("Stuff").transform);
    //                        }
    //                    }
    //                    CheckWeight();
    //                    isActiveStuff.GetComponent<OtherStuff>().Bought = false;
    //                    isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
    //                    HealButton.active = false;
    //                    InfoText.GetComponent<TextMesh>().text = "Select slave\nor weapon\nor switch to\nother panel";
    //                }
    //                if (isActiveStuff.GetComponent<OtherStuff>().Skin == 1) {
    //                    MedicinePut.Play();
    //                }
    //                if (isActiveStuff.GetComponent<OtherStuff>().Skin == 2) {
    //                    WaterPut.Play();
    //                }
    //                if (isActiveStuff.GetComponent<OtherStuff>().Skin == 3) {
    //                    BuffPut.Play();
    //                }

    //                isActiveStuff.GetComponent<OtherStuff>().isActive = false;
    //                isActiveStuff = null;
    //                CloseSlavesFields();
    //                CloseWeapFields();
    //                CloseStuffField();

    //            }
    //            RepairButton.active = false;
    //            InfoText.GetComponent<TextMesh>().text = "Select slave\nor weapon\nor switch to\nother panel";
    //        }

    //        ///**************************************************** HIT = REPAIR *********************************************************

    //        if (hit.collider.gameObject.name == "RepairButton") {
    //            int RepairPrice = (isActiveWeapon.GetComponent<WeaponProperties>().Price / isActiveWeapon.GetComponent<WeaponProperties>().Condition) * (10 - isActiveWeapon.GetComponent<WeaponProperties>().Condition);
    //            PANELS.GetComponent<PlayerInventory>().Money -= RepairPrice;
    //            isActiveWeapon.GetComponent<WeaponProperties>().Condition = 10;
    //            isActiveWeapon.GetComponent<WeaponProperties>().WeaponXRef.gameObject.GetComponent<WeaponProperties>().Condition = 10;
    //            isActiveWeapon.GetComponent<WeaponProperties>().Efficiency = isActiveWeapon.GetComponent<WeaponProperties>().Damage * isActiveWeapon.GetComponent<WeaponProperties>().Condition;
    //            RepairWeapon.Play();
    //            RepairButton.GetComponent<ButtonSample>().isActive = false;
    //            RepairPrice = 0;
    //            InfoText.GetComponent<TextMesh>().text = isActiveWeapon.GetComponent<WeaponProperties>().WeapName +
    //                "\nDamage: " + isActiveWeapon.GetComponent<WeaponProperties>().Damage +
    //                "\nCondition: " + isActiveWeapon.GetComponent<WeaponProperties>().Condition +
    //                "\nBullets: " + isActiveWeapon.GetComponent<WeaponProperties>().Bullets +
    //                "\nEfficiency: " + isActiveWeapon.GetComponent<WeaponProperties>().Efficiency +
    //                "\n\nRepair: " + RepairPrice +
    //                "\nMoney: " + PANELS.GetComponent<PlayerInventory>().Money;
    //        }

    //        ///**************************************************** HIT = HEAL *********************************************************

    //        if (hit.collider.gameObject.name == "HealButton") {
    //            SlaveProperties SlvProp = isActiveSlave.GetComponent<SlaveProperties>();
    //            SlvProp.Health = SlvProp.FullHealth;
    //            SlvProp.SlaveXRef.GetComponent<SlaveProperties>().Health = SlvProp.FullHealth;
    //            //int GetPlace = 0;
    //            //for (int NumPlace = 0; NumPlace < 4; NumPlace++) {
    //            //    if (isActiveStuff.GetComponent<OtherStuff>().StuffXRef == PANELS.GetComponent<PlayerInventory>().SlavesBag[isActiveSlave.GetComponent<SlaveProperties>().Number - 1].Place[NumPlace]) {
    //            //        GetPlace = NumPlace;
    //            //        PANELS.GetComponent<PlayerInventory>().SlavesBag[isActiveSlave.GetComponent<SlaveProperties>().Number - 1].Place[NumPlace] = null;
    //            //    }
    //            //}

    //            foreach (SlavePackage Pack in PlayInv.SlavesBag) {
    //                if (Pack.NumberOfSlave == SlvProp.Number) {
    //                    for (int a = 0; a < Pack.Place.Length; a++) {
    //                        if (Pack.Place[a] == isActiveStuff.GetComponent<OtherStuff>().StuffXRef) {
    //                            Pack.Place[a] = null;
    //                            SlvProp.InventoryPack.transform.GetChild(a).gameObject.active = true;
    //                        }
    //                    }
    //                }
    //            }

    //            PlayInv.Stuff -= 1;

    //            Destroy(isActiveStuff.GetComponent<OtherStuff>().StuffXRef.gameObject);
    //            Destroy(isActiveStuff);

    //            HealSlave.Play();
    //            HealButton.GetComponent<ButtonSample>().isActive = false;
    //            HealButton.active = false;

    //            InfoText.GetComponent<TextMesh>().text = "Select slave\nor weapon\nor switch to\nother panel";
    //            CheckWeight();
    //            CloseSlavesFields();
    //            CloseStuffField();
    //            CloseWeapFields();

    //            isActiveSlave.GetComponent<SlaveProperties>().InventoryPack.gameObject.active = false;
    //            isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
    //        }

    //    }

    //        ///**************************************************** HIT = STUFF SWITCHER *********************************************************

    //    if (Input.GetMouseButton(0)) {
    //        if (InvSwitcher.GetComponent<ButtonToggle>().OnOff == true) {
    //            StuffInv.active = false;
    //            WeaponInv.active = true;
    //        } else {
    //            StuffInv.active = true;
    //            WeaponInv.active = false;
    //        }

    //    }
    //}
}
