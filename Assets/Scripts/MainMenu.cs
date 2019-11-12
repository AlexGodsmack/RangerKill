using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class MainMenu : MonoBehaviour
{
    //private GameObject DollPrefab;
    public GameObject localCanvas;
    public int CountOfPers = 10;
    public int CountOfWeapon = 20;
    public int CountOfBullets = 10;

    public string PersSettings;
    public string PlayerSource;
    public string WeaponBundle;
    public string BulletsCollection;
    public string CountOfAll;
    public string InventorySettings;
    public string MapGen;
    public string StartHealthOfPers;
    public string OtherStuff;

    private string PersSetPath;
    private string PlayerSourcePath;
    private string WeaponBundlePath;
    private string BulletsPath;
    private string MapGenPath;
    private string CountOfAllPath;
    private string InvSetPath;
    private string StartHealthOfPersPath;
    private string OtherStuffPath;

    public Button StartGame, Quit;

    // Start is called before the first frame update
    void Start()
    {

        StartGame.onClick.AddListener(BeginNewGame);
        Quit.onClick.AddListener(Close);
        PersSetPath = Application.persistentDataPath.ToString() + "/" + PersSettings + ".txt";
        PlayerSourcePath = Application.persistentDataPath.ToString() + "/" + PlayerSource + ".txt";
        CountOfAllPath = Application.persistentDataPath.ToString() + "/" + CountOfAll + ".txt";
        StartHealthOfPersPath = Application.persistentDataPath.ToString() + "/" + StartHealthOfPers + ".txt";
        OtherStuffPath = Application.persistentDataPath.ToString() + "/" + OtherStuff + ".txt";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginNewGame() {

        StartGame.GetComponent<AudioSource>().Play();
        PlayerSourceGen();
        InventoryGenerator();
        MapGenerator();
        CountOfAllGen();
        SceneManager.LoadScene(1);
        
    }

    void Close() {

        Quit.GetComponent<AudioSource>().Play();
        Application.Quit();

    }

    void InventoryGenerator() {
        InvSetPath = Application.persistentDataPath + "/" + InventorySettings + ".txt";
        StreamWriter InvSet = new StreamWriter(InvSetPath);
        InvSet.WriteLine("=== Number of Fields ===");
        for (int i = 1; i < 10; i++)
        {
            InvSet.WriteLine(i);
        }
        InvSet.WriteLine("=== Bought Pers Position ===");
        for (int i = 10; i < CountOfPers + 10; i++)
        {
            InvSet.WriteLine("NaN");
        }
        InvSet.WriteLine("=== Number of Weapon in Hands ===");
        for (int i = 10 + CountOfPers; i < 10 + CountOfPers + CountOfWeapon; i++)
        {
            InvSet.WriteLine("NaN");
        }
        InvSet.WriteLine("=== Which Weapon in Hand ===");
        for (int i = 1; i < 11; i++)
        {
            InvSet.WriteLine("false");
        }
        InvSet.WriteLine("=== Stuff Has Place on ===");
        for (int i = 1; i < 11; i++) {
            InvSet.WriteLine("undefined");
        }

        InvSet.Close();
    }

    void CountOfAllGen() {

        StreamWriter CountsWrite = new StreamWriter(CountOfAllPath);
        CountsWrite.WriteLine(0);
        CountsWrite.WriteLine(0);
        CountsWrite.WriteLine(0);
        CountsWrite.WriteLine(0);
        CountsWrite.Close();

    }

    void PlayerSourceGen() {

        StreamWriter PlrSrc = new StreamWriter(PlayerSourcePath);
        PlrSrc.WriteLine("=== Your Money ===");
        PlrSrc.WriteLine("5000");
        PlrSrc.Close();

        StreamWriter StartHealth = new StreamWriter(StartHealthOfPersPath);
        for (int i = 1; i < 7; i++) {
            StartHealth.WriteLine("NaN");
        }
        StartHealth.Close();

    }

    void MapGenerator() {

        MapGenPath = Application.persistentDataPath + "/" + MapGen + ".txt";
        StreamWriter WriteMap = new StreamWriter(MapGenPath);
        for (int i = 1; i < 101; i++) {
            int NumOfTile = Random.Range(1, 6);
            bool HaveSmoke = true;
            WriteMap.WriteLine(NumOfTile.ToString());
            WriteMap.WriteLine(HaveSmoke.ToString());
        }
        for (int i = 1; i < 4; i++) {
            int NumOfBand = i;
            int SizeOfArea = Random.Range(1, 4);
            int PositionOfArea = Random.Range(1, 101);
            WriteMap.WriteLine("=== Bands " + i.ToString() + "===");
            WriteMap.WriteLine(NumOfBand);
            WriteMap.WriteLine(SizeOfArea);
            WriteMap.WriteLine(PositionOfArea);
        }

        for (int b = 1; b < 6; b++) {
            int PositionOfStore = Random.Range(1, 101);
            WriteMap.WriteLine("=== Store " + b.ToString() + " ===");
            WriteMap.WriteLine(PositionOfStore);
        }

        WriteMap.WriteLine("=== Position Of Map ===");
        WriteMap.WriteLine(0);
        WriteMap.WriteLine(0);
        WriteMap.WriteLine("=== Position Of Pers On Map ===");
        WriteMap.WriteLine(0);
        WriteMap.WriteLine(0);

        WriteMap.Close();

    }

}
