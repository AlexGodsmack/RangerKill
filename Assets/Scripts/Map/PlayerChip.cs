using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChip : MonoBehaviour
{

    public bool inBorders;
    public GameObject Target;
    public int SmokeLayer = 14;
    public int BanditLayer = 15;
    public int StoresLayer = 16;
    public string Message;
    public GameObject MapInfo;
    public bool ReadyGoToStore;
    public bool OnEnemyArea;
    public GameObject TouchObject;

    void Start()
    {
        //this.GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {

    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.layer == BanditLayer) {
            Message = "!!! You are on " + collision.gameObject.GetComponent<BanditsDoll>().Clan + " area !!!" + 
                "\nPopulation: " + collision.gameObject.GetComponent<BanditsDoll>().Population;
            OnEnemyArea = true;
            TouchObject = collision.gameObject;
        }
        if (collision.gameObject.layer == StoresLayer) {
            Message = collision.gameObject.GetComponent<StoreChip>().TypeOfStore + "\nEnter the store(click on the chip)";
            this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            ReadyGoToStore = true;
            TouchObject = collision.gameObject;
        }
    }
    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer == SmokeLayer) {
            collision.gameObject.active = false;
        }
        if (collision.gameObject.layer == BanditLayer) {
            Message = "!!! You are on " + collision.gameObject.GetComponent<BanditsDoll>().Clan + " area !!!" +
                "\nPopulation: " + collision.gameObject.GetComponent<BanditsDoll>().Population;
            OnEnemyArea = true;
            TouchObject = collision.gameObject;
        }
        if (collision.gameObject.layer == StoresLayer) {
            Message = collision.gameObject.GetComponent<StoreChip>().TypeOfStore + "\nEnter the store(click on the chip)";
            this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            ReadyGoToStore = true;
            TouchObject = collision.gameObject;
        }
    }
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.layer == BanditLayer) {
            Message = "";
            OnEnemyArea = false;
        }
        if (collision.gameObject.layer == StoresLayer) {
            Message = "";
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            ReadyGoToStore = false;
        }
        TouchObject = null;
    }
}
