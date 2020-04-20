using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{

    public GameObject Shell;
    public int Skin = 1;
    public GameObject NewPos;
    public GameObject StartPos;
    public GameObject WeaponDoll;

    void Start()
    {

        Application.targetFrameRate = 60;

    }

    void Update()
    {

        WeaponDoll.GetComponent<WeaponProperties>().Skin = Skin;

        if (Input.GetMouseButtonDown(0)) {

            StartPos.GetComponent<Animator>().SetBool("Fire", true);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            GameObject Enm = Instantiate(Resources.Load("EnemyDoll")) as GameObject;
            Enm.GetComponent<EnemyProperties>().FullHealth = Enm.GetComponent<EnemyProperties>().Health = 100;
            Enm.GetComponent<EnemyProperties>().Level = 1;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Enm.transform.position = new Vector3(mousePos.x, mousePos.y, StartPos.transform.position.z); ;
            NewPos = Enm;
        }
        Shell = StartPos.GetComponent<SlaveProperties>().ShellContainer;
        if (Shell != null) {
            //Shell = StartPos.GetComponent<SlaveProperties>().ShellContainer;
            Shell.GetComponent<BulletShell>().Target = NewPos;
            if (Shell.GetComponent<BulletShell>().Gotcha == true) {
                Destroy(NewPos);
                Destroy(Shell);
            }
        }

    }
}
