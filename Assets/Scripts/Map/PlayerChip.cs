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
    public GameObject Additive;
    public GameObject Multiply;
    public ButtonSample Button;
    public GameObject ButtonContainer;
    public GameObject TutorContainer;
    public AudioSource ActivateSound;
    [Header("Classes")]
    public WORK_Map GetInfo;
    public Tutorial Tutor;
    public WORK_Map Map;

    void Start()
    {
        //this.GetComponent<Collider2D>().enabled = false;
        //Button.isActive = false;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == SmokeLayer) {
            collision.gameObject.active = false;
        }
        if (collision.gameObject.layer == BanditLayer) {
            if (collision.gameObject.GetComponent<BanditsDoll>().Clan != "") {
                Message = "!!! You are on " + collision.gameObject.GetComponent<BanditsDoll>().Clan + " area !!!" +
                "\nPopulation: " + collision.gameObject.GetComponent<BanditsDoll>().Population;
                OnEnemyArea = true;
                TouchObject = collision.gameObject;
            } else {
                Message = "This area is free";
            }
            Button.isActive = true;
            ActivateSound.Play();
        }
        if (collision.gameObject.layer == StoresLayer) {
            Message = collision.gameObject.GetComponent<StoreChip>().TypeOfStore + "\nEnter the store(click on the chip)";
            //this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            ReadyGoToStore = true;
            TouchObject = collision.gameObject;
            Button.isActive = true;
            ActivateSound.Play();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (Map.Go == false) {
            if (Tutor.Steps == 17) {
                if (collision.gameObject == Tutor.Lighter.gameObject) {
                    Tutor.Steps += 1;
                    Tutor.enabled = false;
                    Tutor.enabled = true;
                    Tutor.PickMonitor.Play();
                }
            }
        }
        if (Map.Go == false) {
            if (Tutor.Steps == 20 || Tutor.Steps == 35) {
                if (collision.gameObject.layer == StoresLayer) {
                    Tutor.Steps += 1;
                    TutorContainer.active = true;
                    Tutor.enabled = false;
                    Tutor.enabled = true;
                    Tutor.PickMonitor.Play();
                }
            }
        }
        if (collision.gameObject.layer == BanditLayer) {
            if (collision.gameObject.GetComponent<BanditsDoll>().Clan != "") {
                Message = "!!! You are on " + collision.gameObject.GetComponent<BanditsDoll>().Clan + " area !!!" +
                    "\nPopulation: " + collision.gameObject.GetComponent<BanditsDoll>().Population;
                OnEnemyArea = true;
                TouchObject = collision.gameObject;
            } else {
                Message = "This area is free";
            }
            Button.isActive = true;
        }
        if (collision.gameObject.layer == StoresLayer) {
            Message = collision.gameObject.GetComponent<StoreChip>().TypeOfStore + "\nEnter the store(click on the chip)";
            //this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            ReadyGoToStore = true;
            TouchObject = collision.gameObject;
            Button.isActive = true;
        }
        //Button.isActive = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == BanditLayer) {
            Message = "";
            OnEnemyArea = false;
            Button.isActive = false;
        }
        if (collision.gameObject.layer == StoresLayer) {
            Message = "";
            //this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            ReadyGoToStore = false;
            Button.isActive = false;
        }
        TouchObject = null;
        
    }

    void OnCollisionStay2D(Collision2D collision) {
        GetInfo.Go = false;
    }
    void OnCollisionEnter2D(Collision2D collision) {
        GetInfo.Go = false;
    }
    void OnCollisionExit2D(Collision2D collision) {
        GetInfo.Go = true;
    }
}
