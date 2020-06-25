using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreConstructor : MonoBehaviour
{

    [Header("Anchors")]
    public GameObject PosRightTop;
    public GameObject PosLeftTop;
    public GameObject PosRightBottom;
    public GameObject PosMidBottom;
    public GameObject PosLeftBottom;
    public GameObject PosMidLeft;
    public GameObject Center;

    [Header("Main Objects")]
    public GameObject MenuHead;
    public GameObject MainMenuPanel;
    public GameObject STORE;
    public GameObject INVENTORY;
    public GameObject AreaForItems;
    public GameObject SlavesBoughtPanel;
    public GameObject BuyItemPanel;
    public GameObject ItemPropertiesPanel;
    public GameObject InvBackground;
    public GameObject InvSlvField;
    public GameObject InvItemField;

    void Start()
    {

        MainMenuPanel.transform.position = PosRightTop.transform.position + new Vector3(0, 0, 0.1f);
        MenuHead.transform.position = PosLeftTop.transform.position;
        if (SlavesBoughtPanel != null) {
            SlavesBoughtPanel.transform.position = PosMidBottom.transform.position;
        }
        if (BuyItemPanel != null) {
            BuyItemPanel.transform.position = PosLeftBottom.transform.position;
        }
        if (ItemPropertiesPanel != null) {
            ItemPropertiesPanel.transform.position = PosRightBottom.transform.position;
        }
        InvBackground.transform.position = Center.transform.position + new Vector3 (0, 0, 3);
        InvSlvField.transform.position = Center.transform.position + new Vector3(-0.8f, -0.1f, 0);
        InvItemField.transform.position = Center.transform.position + new Vector3(0.5f, -0.1f, 0);
        if (STORE != null) {
            STORE.active = true;
            INVENTORY.active = false;
        } else {
            INVENTORY.active = false;
        }

    }

    void Update()
    {
        
    }
}
