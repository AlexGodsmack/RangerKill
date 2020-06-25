using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Objects")]
    public GameObject Main;
    public GameObject LighterContainer;
    public GameObject Lighter;
    public TextMesh Info;
    public GameObject Shadow;
    public Camera Cam;
    public GameObject GotoInventory;
    public GameObject GotoMap;
    public ButtonSwitcher HeadPanel;
    public GameObject Arrow;
    public GameObject LeftAnchor;
    //public ButtonSample Buy;
    //public GameObject GoSelectThis;
    public GameObject Selected;
    [Header("Conditions")]
    public float MyTime;
    public bool First_Launch;
    public bool NoBullets;
    public bool BulletsWeNeed;
    public Vector3 OldPos;
    //public bool FirstStep = true;
    //public bool SecondStep;
    //public bool 
    public int Steps;
    [Header("Classes")]
    public MainPlayerControl PlayInv;
    public WORK_STORE_HEAD Interface;
    public StoreConstructor Constructor;
    public WORK_Map Map;
    public ConstructMonitor Monitor;
    public BulletsEngine Bullets;
    public Tutorial This;
    [Header("Sounds")]
    public AudioSource PickMonitor;

    void OnEnable() {

        Main.transform.position = Cam.transform.position + new Vector3(0, 0, 1);
        if (First_Launch = true) {
            if (Steps == 1) {
                GameObject GetSlv = PlayInv.Items[0].gameObject;
                Selected = PlayInv.Items[0].gameObject;
                LighterContainer.transform.position = new Vector3(GetSlv.transform.position.x, GetSlv.transform.position.y, LighterContainer.transform.position.z);
                Info.transform.position += new Vector3(0, 0.1f, 0);
                Lighter.transform.localScale = new Vector3(3.2f, 3.2f, 1);
                Info.text = "select slave";
            }
            if (Steps == 2) {
                Selected = null;
                LighterContainer.transform.position = new Vector3(Interface.Info.transform.position.x, Interface.Info.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(14f, 2.8f, 1);
                Info.text = "here you can see\nproperties of selected slaves\n\n(press on rectangle to move to next step)";
            }
            if (Steps == 3) {
                LighterContainer.transform.position = new Vector3(Interface.Money.transform.position.x, Interface.Money.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(4, 2, 1);
                Info.transform.position -= new Vector3(0, 0.1f, 0);
                Info.text = "if you have\nenought of money\nyou can buy\nselected slave...";
            }
            if (Steps == 4) {
                LighterContainer.transform.position = new Vector3(Interface.BuyButton.transform.position.x, Interface.BuyButton.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(6, 2.5f, 1);
                Info.text = "...by pressing this button";
            }
            if (Steps == 5) {
                LighterContainer.transform.position = new Vector3(Constructor.SlavesBoughtPanel.transform.position.x, Constructor.SlavesBoughtPanel.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(30, 9, 1);
                Info.transform.position = Info.transform.position + new Vector3(0, 1, 0);
                Info.text = "after that your bought slave\nshown be here";
            }
            if (Steps == 6) {
                LighterContainer.transform.position = new Vector3(GotoInventory.transform.position.x, GotoInventory.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(2.4f, 2.4f, 1);
                Info.transform.position = Info.transform.position - new Vector3(0, 1, 0);
                Info.text = "if you finished your shopping\nswitch to your inventory\nfor tuning your troop";
                Shadow.transform.position = new Vector3(Shadow.transform.position.x, Shadow.transform.position.y, GotoInventory.transform.position.z) + new Vector3(0, 0, 0.1f);
                Lighter.GetComponent<Collider2D>().enabled = false;
            }
            if (Steps == 7) {
                LighterContainer.transform.position = new Vector3(Interface.ItemFields.transform.position.x, Interface.ItemFields.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(12, 12, 1);
                Shadow.transform.localPosition = new Vector3(0, 0, 0);
                Lighter.GetComponent<Collider2D>().enabled = true;
                Info.transform.localPosition = new Vector3(0, 0, -0.1f);
                Info.transform.localPosition += new Vector3(-1, 0, 0);
                Info.text = "here is located\nyour items. It would be\nmedicine chests, water\n or some weapon.\ntotally your inventory\nincludes 9 slots";
            }
            if (Steps == 8) {
                LighterContainer.transform.position = new Vector3(Interface.SlaveFields.transform.position.x, Interface.SlaveFields.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(12, 12, 1);
                Info.transform.localPosition += new Vector3(2.3f, 0, 0);
                Info.text = "that is field with\nyour slaves. you can move them\ncell by cell and\nthat will be his\nposition on field\ndue fight. than nearly slave to\nopposite field then higher his\ndamage rate named 'power'";
            }
            if (Steps == 9) {
                foreach (GameObject Wpn in PlayInv.Package) {
                    if (Wpn.GetComponent<WeaponProperties>() != null) {
                        Selected = Wpn.gameObject;
                        LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
                        Lighter.transform.localScale = new Vector3(2.6f, 2.6f, 1);
                        Info.transform.localPosition = new Vector3(0, 0, -0.1f);
                        Info.transform.localPosition += new Vector3(0, -0.2f, 0);
                        Info.text = "Ok. Lets try to\nplace weapon to\npackage of your slave.\nselect weapon";
                        break;
                    }
                }
            }
            if (Steps == 10) {
                foreach (GameObject Slv in PlayInv.SlavePlace) {
                    if (Slv != null) {
                        Selected = Slv.gameObject;
                        LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
                        Shadow.transform.position += new Vector3(0, 0, 3f);
                        OldPos = Selected.transform.position;
                        Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z) + new Vector3(0, 0, -0.1f);
                        Lighter.GetComponent<Collider2D>().enabled = false;
                        Lighter.transform.localScale = new Vector3(3.2f, 3.2f, 1);
                        Info.text = "and select your slave\nto set this weapon to\nyour slave";
                        break;
                    }
                }
            }
            if (Steps == 11) {
                Selected.transform.position = OldPos;
                Selected = Selected.GetComponent<SlaveProperties>().Lighter.gameObject.transform.Find("Info/Power").gameObject;
                Shadow.transform.position -= new Vector3(0, 0, 3f);
                Lighter.active = true;
                Lighter.GetComponent<Collider2D>().enabled = true;
                Lighter.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y, Shadow.transform.position.z) + new Vector3(0, -0.1f, -0.1f);
                Lighter.transform.localScale = new Vector3(3, 2, 1);
                Arrow.active = true;
                Arrow.transform.localPosition += new Vector3(0, 0.1f, 0);
                Arrow.transform.position = new Vector3(Arrow.transform.position.x, Arrow.transform.position.y, Shadow.transform.position.z) + new Vector3(0, 0, -0.1f);
                LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
                Info.text = "As you see amount of\npower changed. it means that\nfinal damage of this slave rise up.\npower consists of damage of slave,\nhis accuracy, damage of weapon in\nhis hands and condition of this weapon";
            }
        }
        if (Steps == 12) {
            Interface.isActiveSlave.GetComponent<SlaveProperties>().isActive = false;
            Interface.isActiveSlave = null;
            foreach (GameObject Item in PlayInv.Package) {
                if (Item != null) {
                    Selected = Item;
                    break;
                }
            }
            OldPos = Selected.transform.position;
            Arrow.active = false;
            LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.position += new Vector3(0, 0.1f, 0);
            Lighter.transform.localScale = new Vector3(3.2f, 3.2f, 1);
            Interface.isActiveItem = Selected;
            Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Lighter.transform.position.z) + new Vector3(0, 0, 0.1f);
            Info.text = "Ok. now you have to\nput water bottles into\nslave's bag. water in his bag\nmake your troop able to\nmove on map quickly. Another way\nyour troop will move much slower.\nany new bottle's item has 100 liters";
        }
        if (Steps == 13) {
            Selected.transform.position = OldPos;
            foreach (GameObject slv in PlayInv.SlavePlace) {
                if (slv != null) {
                    Selected = slv.gameObject;
                    break;
                }
            }
            OldPos = Selected.transform.position;
            Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z) + new Vector3(0, 0, -0.1f);
            LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(3.2f, 3.2f, 1);
            Lighter.GetComponent<Collider2D>().enabled = false;
            Lighter.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y, Selected.transform.position.z - 0.1f);
            Info.text = "And select slave you wish\nwhich gonna have this bottles";
        }
        if (Steps == 14) {
            Selected.transform.position = OldPos;
            Selected = Selected.GetComponent<SlaveProperties>().InventoryPack.transform.GetChild(1).transform.GetChild(0).gameObject;
            LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(2.6f, 2.6f, 1);
            Lighter.GetComponent<Collider2D>().enabled = true;
            Info.text = "Well done. So now you have summary\n100 liters of water. But if other\nwater which not assigned to your troop\nwill not spent due walk through desert\nwhile you didn't assign it them";
        }
        if (Steps == 15) {
            Selected = PlayInv.SlavePlace[0];
            Selected.GetComponent<SlaveProperties>().isActive = false;
            Selected = Interface.GoToMap.gameObject;
            LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(2.4f, 2.4f, 1);
            Lighter.GetComponent<Collider2D>().enabled = false;
            Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z) + new Vector3(0, 0, -0.1f);
            Info.text = "Well now lets go to the desert\nwill look what we will\nsee outta store's border";
        }
        if (Steps == 16) {
            LighterContainer.transform.position = new Vector3(Map.Player.transform.position.x, Map.Player.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(3.2f, 3.2f, 1);
            Info.text = "Here we are. So we need\na little bit more water. We've gotta\nfind store where we will find water\n\n(press on rectangle to move to next step)";
        }
        if (Steps == 17) {
            LighterContainer.transform.position = new Vector3(LighterContainer.transform.position.x - 1, LighterContainer.transform.position.y, LighterContainer.transform.position.z);
            Info.text = "Tap on screen here\nto go there";
            OldPos = Lighter.transform.position;
        }
        if (Steps == 18) {
            Lighter.transform.position = OldPos;
            LighterContainer.transform.position = new Vector3(Monitor.InfoField.transform.position.x + 0.6f, Monitor.InfoField.transform.position.y + 0.18f, LighterContainer.transform.position.z);
            Lighter.transform.localPosition = new Vector3(0, 0, Lighter.transform.localPosition.z);
            Lighter.transform.localScale = new Vector3(12f, 3.6f, 1);
            Info.transform.localPosition = Lighter.transform.localPosition + new Vector3(0, 0.6f, 0);
            Info.text = "As you see here is decreasing\ncounter of you water liters. Also\nhere you can see any warnings\nreporting you about contacts with\ndifferent areas";
        }
        if (Steps == 19) {
            LighterContainer.transform.position = new Vector3(Monitor.LeftTopAnchor.transform.position.x, Monitor.LeftTopAnchor.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(10, 6, 1);
            Lighter.transform.position = new Vector3(Lighter.transform.position.x + 0.5f, Lighter.transform.position.y - 0.3f, Lighter.transform.position.z);
            Info.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y + 0.1f, Info.transform.position.z);
            Info.text = "Well, now try to find\nsome store by yourself";
        }
        if (Steps == 20) {
            Info.text = "Try to move\nto another position";
            Main.active = false;
        }
        if (Steps == 21) {
            if (Map.Player.GetComponent<PlayerChip>().TouchObject != null) {
                string Message = Map.Player.GetComponent<PlayerChip>().TouchObject.GetComponent<StoreChip>().TypeOfStore;
                Info.transform.position = Lighter.transform.position + new Vector3(0, 0.2f, 0);
                Info.text = "Grade!\nyou found " + Message + " center.";
                if (Message == "Stuff") {
                    Info.text += "\nhere you can buy water,\nmedicine chests, buff\nbottles and other stuff.\nthat's what we need";
                }
                if (Message == "Slaves") {
                    Info.text += "\nhere you can buy more\nslaves to your troop\nbut we need stuff center\nlets go find him";
                }
                if (Message == "Guns") {
                    Info.text += "\nhere you can buy\nweapons for your slaves\nbut we need stuff center\nlets go find him";
                }
                if (Message == "Bullets") {
                    Info.text += "\nhere you can buy bullets\nfor your weapons\nbut we need stuff center\nlets go find him";
                }
                if (Message == "Recycling") {
                    Info.text += "\nhere you can sell your\nweapons or stuff,\nbut not slaves\nbut we need stuff center\nlets go find him";
                }
            }
        }
        if (Steps == 22) {
            Lighter.active = false;
            LighterContainer.transform.position = new Vector3(Map.Player.transform.position.x, Map.Player.transform.position.y, LighterContainer.transform.position.z);
            Info.transform.localPosition = new Vector3(0, 0.5f, -0.1f);
            Info.text = "you found stuff's store\nTo enter into store you've\ngotta press on player's chip";
            Map.Player.transform.position = new Vector3(Map.Player.transform.position.x, Map.Player.transform.position.y, Shadow.transform.position.z - 0.1f);
        }
        if (Steps == 23) {
            foreach (GameObject stf in PlayInv.Items) {
                if (stf.GetComponent<OtherStuff>().Skin == 2) {
                    Selected = stf.gameObject;
                    LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
                    Lighter.transform.localScale = new Vector3(2.5f, 2.5f, 1);
                    Info.transform.localPosition = new Vector3(0.6f, 0.1f, -0.1f);
                    Info.text = "We are in stuff's store.\nso lets buy other bottle\nof water";
                    break;
                }
            }
        }
        if (Steps == 24) {
            Selected.GetComponent<OtherStuff>().isActive = true;
            GameObject GetButton = Interface.BuyButton.gameObject;
            Interface.isActiveItem = Selected;
            GetButton.GetComponent<ButtonSample>().isActive = true;
            LighterContainer.transform.position = new Vector3(GetButton.transform.position.x, GetButton.transform.position.y, LighterContainer.transform.position.z);
            OldPos = GetButton.transform.position;
            GetButton.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y, Lighter.transform.position.z + 0.1f);
            Lighter.GetComponent<Collider2D>().enabled = false;
            Lighter.transform.localScale = new Vector3(4.3f, 4.3f, 1);
            Shadow.transform.position += new Vector3(0, 0, 0.1f);
            Info.transform.localPosition = new Vector3(0, 0.5f, -0.1f);
            Info.text = "And press this button\nto buy selected item";
        }
        if (Steps == 25) {
            Interface.BuyButton.transform.position = OldPos;
            Lighter.GetComponent<Collider2D>().enabled = true;
            LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(2.5f, 2.5f, 1);
            Info.transform.position -= new Vector3(0, 0.1f, 0);
            Info.text = "And bought item is already\nin your inventory!";
        }
        if (Steps == 26) {
            LighterContainer.transform.position = new Vector3(GotoInventory.transform.position.x, GotoInventory.transform.position.y, LighterContainer.transform.position.z);
            OldPos = GotoInventory.transform.position;
            GotoInventory.transform.position = Lighter.transform.position + new Vector3(0, 0, 0.1f);
            Lighter.GetComponent<Collider2D>().enabled = false;
            Info.transform.position -= new Vector3(0, 0.6f, 0);
            Info.text = "Now go to your inventory";
        }
        if (Steps == 27) {
            GotoInventory.transform.position = OldPos;
            foreach (GameObject slv in PlayInv.SlavePlace) {
                if (slv != null) {
                    Selected = slv.gameObject;
                    LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
                    Lighter.transform.localScale = new Vector3(3.2f, 3.2f, 1);
                    Shadow.transform.position += new Vector3(0, 0, 2);
                    OldPos = Selected.transform.position;
                    Selected.transform.position = Lighter.transform.position + new Vector3(0, 0, 1.5f);
                    Info.text = "Lets change old bottle\nof water to new bottle.\nselect slave";
                    break;
                }
            }
        }
        if (Steps == 28) {
            Shadow.transform.position -= new Vector3(0, 0, 2);
            Selected.transform.position = OldPos;
            Selected = Selected.GetComponent<SlaveProperties>().InventoryPack.gameObject;
            Selected = Selected.transform.GetChild(1).transform.GetChild(0).gameObject;
            LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
            OldPos = Selected.transform.position;
            Selected.transform.position = Lighter.transform.position + new Vector3(0, 0, 0.1f);
            Lighter.transform.localScale = new Vector3(2.5f, 2.5f, 1);
            Info.transform.position = Lighter.transform.position + new Vector3(-0.5f, 0.1f, 0);
            Info.text = "and select water\nin package of\nthis slave";
        }
        if (Steps == 29) {
            Selected.transform.position = OldPos;
            Selected = Interface.ItemFields.transform.GetChild(4).gameObject;
            OldPos = Selected.transform.position;
            Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z - 0.1f);
            LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(3.2f, 3.2f, 1);
            Info.transform.position = Lighter.transform.position + new Vector3(0, -0.3f, 0);
            Info.text = "next step is select\none of cell in your inventory,\nas example here";
        }
        if (Steps == 30) {
            Selected.transform.position = OldPos;
            GameObject OldWater = PlayInv.Package[4].gameObject;
            OldWater.transform.position = OldPos + new Vector3(0, 0, -0.1f);
            GameObject NewWater = PlayInv.Package[0].gameObject;
            OldPos = NewWater.transform.position;
            NewWater.transform.position = new Vector3(NewWater.transform.position.x, NewWater.transform.position.y, Shadow.transform.position.z - 0.1f);
            LighterContainer.transform.position = new Vector3(NewWater.transform.position.x, NewWater.transform.position.y, LighterContainer.transform.position.z);
            Info.text = "Now lets try to put\nnew bottle of water\ninto slave's bag.\nselect this water";
        }
        if (Steps == 31) {
            Selected.transform.position = OldPos;
            Selected = PlayInv.SlavePlace[0].gameObject;
            OldPos = Selected.transform.position;
            Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z - 0.1f);
            LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(3.2f, 3.2f, 1);
            Info.text = "and put it into bag";
        }
        if (Steps == 32) {
            Selected.transform.position = OldPos;
            Selected = Interface.SlaveFields.transform.GetChild(4).gameObject;
            OldPos = Selected.transform.position;
            Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z - 0.1f);
            LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
            Info.text = "OK. also you can move your\nslaves on field. You have\nto select your slave and\ntap on cell you wish";
        }
        if (Steps == 33) {
            Selected.transform.position = OldPos;
            GameObject Slv = PlayInv.SlavePlace[4].gameObject;
            Slv.transform.position = OldPos + new Vector3(0, 0, -0.1f);
            GotoMap.transform.position = new Vector3(GotoMap.transform.position.x, GotoMap.transform.position.y, Shadow.transform.position.z - 0.1f);
            LighterContainer.transform.position = new Vector3(GotoMap.transform.position.x, GotoMap.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(2.3f, 2.3f, 1);
            Lighter.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            Info.text = "lets go find\nbullet's store";
        }
        if (Steps == 34) {
            //LighterContainer.transform.position = new Vector3(Monitor.LeftTopAnchor.transform.position.x, Monitor.LeftTopAnchor.transform.position.y, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(10, 6, 1);
            Lighter.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y, Lighter.transform.position.z);
            Info.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y + 0.1f, Info.transform.position.z);
            Info.text = "Ok, well done.\nGo find bullets\ncenter, because without\nbullets your slave\ncan't shoot";
        }
        if (Steps == 35) {
            LighterContainer.transform.position = new Vector3(Monitor.LeftTopAnchor.transform.position.x + 0.4f, Monitor.LeftTopAnchor.transform.position.y - 0.3f, LighterContainer.transform.position.z);
            Info.text = "Try to move\nto another position";
            Main.active = false;
        }
        if (Steps == 36) {
            if (Map.Player.GetComponent<PlayerChip>().TouchObject != null) {
                string Message = Map.Player.GetComponent<PlayerChip>().TouchObject.GetComponent<StoreChip>().TypeOfStore;
                Info.transform.position = Lighter.transform.position + new Vector3(0, 0.2f, 0);
                //Info.text = "Grade!\nyou found " + Message + " center.";
                if (Message == "Stuff") {
                    Info.text = "no. that is\nstuff's store.\nlets go find bullet's store";
                }
                if (Message == "Slaves") {
                    Info.text = "no. that is\nslave's store\nlets go find bullet's store";
                }
                if (Message == "Guns") {
                    Info.text = "no. that is\ngun's store\nlets go find bullet's store";
                }
                if (Message == "Bullets") {
                    Info.text = "Grade! that's what\nwe need!";
                }
                if (Message == "Recycling") {
                    Info.text = "no. that is\nrecycling center\nlets go find bullet's store";
                }
            }
        }
        if (Steps == 37) {
            Lighter.active = false;
            LighterContainer.transform.position = new Vector3(Map.Player.transform.position.x, Map.Player.transform.position.y, LighterContainer.transform.position.z);
            Info.transform.localPosition = new Vector3(0, 0.3f, -0.1f);
            Info.text = "That is bullet's store.\nLets go inside";
            Map.Player.transform.position = new Vector3(Map.Player.transform.position.x, Map.Player.transform.position.y, Shadow.transform.position.z - 0.1f);
        }
        if (Steps == 38) {
            Selected = Bullets.RightButton.gameObject;
            OldPos = Selected.transform.position;
            Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z - 0.1f);
            LighterContainer.transform.position = new Vector3(Bullets.RightButton.transform.position.x, Bullets.RightButton.transform.position.y, LighterContainer.transform.position.z - 0.1f);
            Lighter.transform.localScale = new Vector3(2.2f, 2.6f, 1);
            Lighter.GetComponent<Collider2D>().enabled = false;
            Info.text = "Let see do we\nhave here bullets for\nweapon we have";
        }
        if (Steps == 39) {
            WeaponProperties GetWpn = Bullets.Weapons[0].GetComponent<WeaponProperties>();
            LighterContainer.transform.position = new Vector3(0, 0, LighterContainer.transform.position.z);
            Lighter.transform.localScale = new Vector3(10, 6, 1);
            Lighter.GetComponent<Collider2D>().enabled = true;
            Selected.transform.position = OldPos;
            Info.transform.position += new Vector3(0, 0.45f, 0);
            foreach (GameObject bul in PlayInv.Items) {
                if (bul.GetComponent<BulletsProperties>().Skin == GetWpn.Skin) {
                    NoBullets = false;
                    Info.text = "As you see\nthis store have\nbullets what we need!\nselect box with bullets";
                    break;
                } else {
                    NoBullets = true;
                    Info.text = "Well... unfortunately\nthis store has't bullets\nwhat we need.\ngo find store which have those";
                }
            }
        }
        if (Steps == 40) {
            if (NoBullets == false) {
                Main.active = false;
                if (Interface.isActiveItem != null) {
                    Interface.isActiveItem.GetComponent<BulletsProperties>().isActive = false;
                    Interface.isActiveItem = null;
                }
            } else {
                Selected = Interface.GoToMap.gameObject;
                Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z - 0.1f);
                LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(2.4f, 2.4f, 1);
                Info.transform.position = Lighter.transform.position + new Vector3(0, -0.3f, 0);
                Info.text = "But don't be afraid\ngo back to map and\nfind store which has needed bullets";
            }
        }
        if (Steps == 41) {
            if (NoBullets == false) {
                WeaponProperties GetWpn = Bullets.Weapons[0].GetComponent<WeaponProperties>();
                GameObject GetRef = GetWpn.WeaponXRef.gameObject;
                if (Selected.GetComponent<BulletsProperties>().Skin == GetWpn.Skin) {
                    BulletsWeNeed = true;
                    LighterContainer.transform.position = new Vector3(GetRef.transform.position.x, GetRef.transform.position.y, LighterContainer.transform.position.z);
                    Selected = GetRef;
                    OldPos = Selected.transform.position;
                    Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z - 0.1f);
                    Lighter.GetComponent<Collider2D>().enabled = false;
                    Lighter.transform.localScale = new Vector3(2.5f, 2.5f, 1);
                    Info.transform.position += new Vector3(0, -0.3f, 0);
                    Info.text = "Grate! And now\nselect your weapon";
                } else {
                    BulletsWeNeed = false;
                    Info.text = "Nope. That's not those\nbullets what we need";
                }
            } else {
                LighterContainer.transform.position = new Vector3(Cam.transform.position.x, Cam.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(15, 8, 1);
                Info.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y + 0.3f, Info.transform.position.z);
                Info.text = "Congrats!!!\nNow you are ready to\nride through desert!\n don't forget that you can also\nnot only buy slaves or some stuff" +
                    "\nbut sell needless stuff which you\nlooted due fights\nPS: when you'll be ready to fight just\nwalk over band's area";
            }
        }
        if (Steps == 42) {
            if (NoBullets == false) {
                Selected.transform.position = OldPos;
                Selected = Interface.Plus.gameObject;
                OldPos = Selected.transform.position;
                LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
                Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z - 0.1f);
                Lighter.transform.localScale = new Vector3(2, 2, 1);
                Info.transform.position = new Vector3(Lighter.transform.position.x + 0.5f, Lighter.transform.position.y, Lighter.transform.position.z);
                Info.text = "Do one click\nto increase count of\nbullets you want buy";
            }
        }
        if (Steps == 43) {
            if (NoBullets == false) {
                Selected.transform.position = OldPos;
                Selected = Interface.BuyButton.gameObject;
                OldPos = Selected.transform.position;
                Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z - 0.1f);
                LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(4.3f, 4.3f, 1);
                Info.transform.position = Lighter.transform.position + new Vector3(0, 0.5f, 0);
                Info.text = "And press this button\nto buy 1 bullet for\nselected weapon";
            }
        }
        if (Steps == 44) {
            if (NoBullets == false) {
                Selected.transform.position = OldPos;
                Selected = Interface.GoToMap.gameObject;
                Selected.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, Shadow.transform.position.z - 0.1f);
                LighterContainer.transform.position = new Vector3(Selected.transform.position.x, Selected.transform.position.y, LighterContainer.transform.position.z);
                Lighter.transform.localScale = new Vector3(2.4f, 2.4f, 1);
                Info.transform.position = Lighter.transform.position + new Vector3(0, -0.3f, 0);
                Info.text = "And go back to desert";
            }
        }
        if (Steps == 45) {
            //Main.transform.position = new Vector3(Cam.transform.position.x, Cam.transform.position.y, Main.transform.position.z);
            Lighter.transform.localScale = new Vector3(15, 8, 1);
            Info.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y + 0.3f, Info.transform.position.z);
            Info.text = "Congrats!!!\nNow you are ready to\nride through desert!\n don't forget that you can also\nnot only buy slaves or some stuff" +
                "\nbut sell needless stuff which you\nlooted due fights\nPS: when you'll be ready to fight just\nwalk over band's area";
        }
        if (Steps == 46) {
            Map.Turn_on_Clans();
            PlayInv.If_Tutorial = false;
            PickMonitor.Play();
            Main.active = false;
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.gameObject == Lighter) {
                if (Steps == 1) {
                    Selected.GetComponent<SlaveProperties>().isActive = true;
                    Interface.isActiveSlave = Selected;
                    Interface.ShowSlaveInfo();
                }

                if (Steps == 9) {
                    Selected.GetComponent<WeaponProperties>().isActive = true;
                    Interface.isActiveItem = Selected;
                    Interface.ShowWeaponInfo();
                }

                if (Steps == 12) {
                    Selected.GetComponent<OtherStuff>().isActive = true;
                }

                if (Steps == 17) {
                    Map.OldPlayerPos = Map.Player.gameObject.transform.position;
                    Map.Goal.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y, Map.Goal.transform.position.z);
                    Map.MovingCounter = 0.0f;
                    Map.Distance = Vector3.Distance(Map.OldPlayerPos, Map.Goal.transform.position);
                    Map.CountOfWalk = Map.Distance;
                    Lighter.transform.position = new Vector3(Lighter.transform.position.x, Lighter.transform.position.y, Shadow.transform.position.z) + new Vector3(0, 0, 0.1f);
                    Map.Go = true;

                } else if (Steps == 21) {
                    if (Map.Player.GetComponent<PlayerChip>().TouchObject.GetComponent<StoreChip>().TypeOfStore != "Stuff") {
                        Steps -= 1;
                        Main.active = false;
                    } else {
                        Steps += 1;
                        Main.active = false;
                        Main.active = true;
                    }
                } else if (Steps == 36) {
                    if (Map.Player.GetComponent<PlayerChip>().TouchObject.GetComponent<StoreChip>().TypeOfStore != "Bullets") {
                        Steps -= 1;
                        Main.active = false;
                    } else {
                        Steps += 1;
                        Main.active = false;
                        Main.active = true;
                    }
                } else if (Steps == 41) {
                    if (BulletsWeNeed == false) {
                        Steps -= 1;
                        Main.active = false;
                        Main.active = true;
                    } else {
                        Steps += 1; 

                    }
                } else {
                    Steps += 1;
                    Main.active = false;
                    Main.active = true;
                }
                PickMonitor.Play();

            }
            if (Steps == 13 || Steps == 27 || Steps == 31) {
                if (hit.collider.gameObject == PlayInv.SlavePlace[0].gameObject) {
                    Steps += 1;
                    Main.active = false;
                    Main.active = true;
                    PickMonitor.Play();
                }
            }
            if (Steps == 28 || Steps == 30) {
                if (hit.collider.gameObject.GetComponent<OtherStuff>() != null) {
                    Steps += 1;
                    Main.active = false;
                    Main.active = true;
                    PickMonitor.Play();
                }
            }

            if (Selected != null) {
                if (Selected.GetComponent<SlaveProperties>() != null) {
                    if (Selected.GetComponent<SlaveProperties>().HaveGun == true) {
                        if (Steps == 10) {
                            Steps += 1;
                            Main.active = false;
                            Main.active = true;
                            PickMonitor.Play();
                        }
                    }
                }
            }
        }
        //if (Buy.isPressed == true) {
        //    if (Steps == 24) {
        //        Steps += 1;
        //        Main.active = false;
        //        Main.active = true;
        //        PickMonitor.Play();
        //    }
        //}
        //if (HeadPanel != null) {
        //    if (HeadPanel.isPressed == true) {
        //        Steps += 1;
        //        Main.active = false;
        //        Main.active = true;
        //        PickMonitor.Play();
        //    }
        //}
    }
}
