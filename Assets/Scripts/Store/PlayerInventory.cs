using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    [Header("Objects")]
    public GameObject SlaveField;
    public GameObject ItemField;
    public GameObject Properties;
    public GameObject RightBottomAnchor;
    public TextMesh Description;
    public GameObject Heal_Button;
    public GameObject Repair_Button;
    public GameObject Sell_Button;
    public TextMesh Prc_For_repair;
    public TextMesh Prc_For_Sell;
    public int Prc_Repair;
    public int Prc_Sell;

    [Header("Selected Objects")]
    public GameObject isActiveSlave;
    public GameObject isActiveItem;

    [Header("Source Folders")]
    public GameObject SlaveSource;
    public GameObject ItemSource;
    [Header("Classes")]
    public MainPlayerControl PlayInv;
    public WORK_STORE_HEAD InputItems;
    public ButtonSample HealBtn;
    public ButtonSample RepairBtn;
    public ButtonSample SellBtn;

    [Header("Sounds")]
    public AudioSource PickMonitor;
    public AudioSource HealSound;
    public AudioSource RepairSound;
    public AudioSource SoldSound;

    public void OnEnable() {

        Properties.transform.position = RightBottomAnchor.transform.position + new Vector3(0, 0, 1);
        Greatings();

        int SlvNum = 0;
        foreach (GameObject Slave in PlayInv.SlavePlace) {
            if (Slave != null) {
                Slave.transform.position = SlaveField.transform.GetChild(SlvNum).transform.position + new Vector3(0, 0, -0.1f);
                Slave.GetComponent<SlaveProperties>().Goal = ItemField.transform.GetChild(5).gameObject;
                Slave.GetComponent<SlaveProperties>().Bought = false;
            }
            SlvNum += 1;
        }

        ItemSource.active = true;

        int ItemNum = 0;
        foreach (GameObject Item in PlayInv.Package) {
            if (Item != null) {
                Item.transform.position = ItemField.transform.GetChild(ItemNum).transform.position + new Vector3(0, 0, -0.1f);
                if (Item.GetComponent<WeaponProperties>() != null) {
                    Item.GetComponent<WeaponProperties>().Bought = false;
                }
                if (Item.GetComponent<OtherStuff>() != null) {
                    Item.GetComponent<OtherStuff>().Bought = false;
                }
            }
            ItemNum += 1;
        }
    }

    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            isActiveSlave = InputItems.isActiveSlave;
            isActiveItem = InputItems.isActiveItem;
            if (isActiveSlave == null && isActiveItem == null) {
                Greatings();
            }
            if (isActiveSlave != null) {
                Slv_Show();
            }
            if (isActiveItem != null) {
                if (isActiveSlave == null) {
                    Item_Show();
                } else {
                    Heal();
                }
                if (isActiveItem.GetComponent<OtherStuff>() != null) {
                    OtherStuff prop = isActiveItem.GetComponent<OtherStuff>();
                    if (Sell_Button != null) {
                        Sell_Button.active = true;
                        if (prop.Skin == 1) {
                            Prc_Sell = (int)(0.8f * prop.Price);
                            Prc_For_Sell.text = "for: " + Prc_Sell + "$";
                        }
                        if (prop.Skin == 2) {
                            Prc_Sell = prop.Liters;
                            Prc_For_Sell.text = "for: " + Prc_Sell + "$";
                        }
                        if (prop.Skin == 3) {
                            Prc_Sell = (int)(0.8f * prop.Price);
                            Prc_For_Sell.text = "for: " + Prc_Sell + "$";
                        }
                    }
                    if (Repair_Button != null) {
                        Repair_Button.active = false;
                    }
                } else if (isActiveItem.GetComponent<WeaponProperties>() != null) {
                    WeaponProperties prop = isActiveItem.GetComponent<WeaponProperties>();
                    if (Sell_Button != null) {
                        Sell_Button.active = true;
                        Prc_Sell = (int)(0.8f * prop.Price);
                        Prc_For_Sell.text = "for: " + Prc_Sell + "$";
                        Description.text = prop.WeapName + "\ndamage: " + prop.Damage + "\ncondition: " + prop.Condition + "\nbullets: " + prop.Bullets;
                    }
                    if (Repair_Button != null) {
                        Repair_Button.active = true;
                        if (PlayInv.Money >= prop.Price) {
                            if (prop.Condition != 10) {
                                Repair_Button.GetComponent<ButtonSample>().isActive = true;
                                Prc_Repair = (10 - prop.Condition) * (prop.Price / prop.Condition);
                                Prc_For_repair.text = "for: " + Prc_Repair + "$";
                            } else {
                                Repair_Button.GetComponent<ButtonSample>().isActive = false;
                                Prc_For_repair.text = "";
                            }
                        } else {
                            Repair_Button.GetComponent<ButtonSample>().isActive = false;
                            Prc_Repair = (10 - prop.Condition) * (prop.Price / prop.Condition);
                            Prc_For_repair.text = "for: " + Prc_Repair + "$";
                        }
                    }
                }
            } else {
                if (isActiveSlave == null) {
                    Greatings();
                } else {
                    if (Repair_Button != null) {
                        Repair_Button.active = false;
                    }
                    if (Sell_Button != null) {
                        Sell_Button.active = false;
                    }
                }
            }
        }

        if (HealBtn.isPressed == true) {
            isActiveSlave.GetComponent<SlaveProperties>().Health = isActiveSlave.GetComponent<SlaveProperties>().FullHealth;
            PickMonitor.Play();
            HealSound.Play();
            Heal_Button.GetComponent<ButtonSample>().isPressed = false;
            Heal_Button.GetComponent<ButtonSample>().isActive = false;
            Destroy(isActiveItem); 
            Greatings();
        }

        if (Repair_Button != null && Sell_Button != null) {
            if (RepairBtn.isPressed == true) {
                WeaponProperties prop = isActiveItem.GetComponent<WeaponProperties>();
                prop.Price = 10 * (prop.Price / prop.Condition);
                prop.Condition = 10;
                PlayInv.Money -= Prc_Repair;
                RepairSound.Play();
                RepairBtn.isActive = false;
                Prc_For_repair.text = "";
                Prc_For_Sell.text = "for: " + (int)(0.8f * prop.Price) + "$";
            }

            if (SellBtn.isPressed == true) {
                PlayInv.Money += Prc_Sell;
                Destroy(isActiveItem);
                isActiveItem = null;
                SellBtn.isPressed = false;
                SoldSound.Play();
                Greatings();
            }
        }
    }

    void Slv_Show() {

        SlaveProperties prop = isActiveSlave.GetComponent<SlaveProperties>();
        int health_percent = (int)(100 * ((float)prop.Health / (float)prop.FullHealth));
        Description.text = "life: " + health_percent + "%" + "\nfull hp: " + prop.FullHealth + "\nexp: " + prop.Battles + "\nlevel: " + prop.Level +
            "\ndamage: " + prop.Damage + "\naccuracy: " + prop.Accuracy;
        Heal_Button.active = false;
        //if (Repair_Button != null) {
        //    Repair_Button.active = false;
        //}
        //if (Sell_Button != null) {
        //    Sell_Button.active = false;
        //}

    }

    void Item_Show() {

        if (isActiveItem.GetComponent<OtherStuff>() != null) {
            OtherStuff prop = isActiveItem.GetComponent<OtherStuff>();
            Description.text = prop.ShortDescription;
            //if (Sell_Button != null) {
            //    Sell_Button.active = true;
            //}
        }

        if (isActiveItem.GetComponent<WeaponProperties>() != null) {
            WeaponProperties prop = isActiveItem.GetComponent<WeaponProperties>();
            Description.text = prop.WeapName + "\n\ndamage: " + prop.Damage + "\ncondition: " + prop.Condition + "\nbullets: " + prop.Bullets;
            //    int RepairPrice = (10 - prop.Condition) * (prop.Price / prop.Condition);
            //    Description.text = prop.WeapName + "\ndamage: " + prop.Damage + "\ncondition: " + prop.Condition + "\nbullets: " + prop.Bullets +
            //        "\n\n\n\nfor: " + RepairPrice.ToString() + "$" + "\n\n\n\nfor: " + (int)(0.8f * prop.Price) + "$";
            //    Repair_Button.active = true;
            //    Sell_Button.active = true;
            //    if (PlayInv.Money < RepairPrice) {
            //        Repair_Button.GetComponent<ButtonSample>().isActive = false;
            //    } else {
            //        Repair_Button.GetComponent<ButtonSample>().isActive = true;
            //    }
            //}
        }

        Heal_Button.active = false;

    }

    void Heal() {
        if (isActiveItem.GetComponent<OtherStuff>() != null) {
            if (isActiveItem.GetComponent<OtherStuff>().Skin == 1) {
                if (isActiveSlave.GetComponent<SlaveProperties>().Health != isActiveSlave.GetComponent<SlaveProperties>().FullHealth) {
                    Description.text = "Do you want \nto heal this \nslave?";
                    Heal_Button.active = true;
                    Heal_Button.GetComponent<ButtonSample>().isActive = true;
                } else {
                    Description.text = "That slave \ndoesn't need \nin heal";
                    Heal_Button.active = true;
                    Heal_Button.GetComponent<ButtonSample>().isActive = false;
                }
            } else {
                Item_Show();
            }
        } else {
            Item_Show();
        }
    }

    void Greatings() {
        Heal_Button.active = false;
        if (Sell_Button != null) {
            Sell_Button.active = false;
        }
        if (Repair_Button != null) {
            Repair_Button.active = false;
        }
        Description.text = "1.select slave\n2.select item\n3.put item \nto slave\n4.heal slaves";
    }

}
