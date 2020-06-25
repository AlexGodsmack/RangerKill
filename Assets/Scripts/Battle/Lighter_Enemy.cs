using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter_Enemy : MonoBehaviour
{

    public GameObject Info;
    public GameObject Multiply;
    public TextMesh Health;
    public TextMesh Damage;
    public TextMesh Level;
    public TextMesh Accuracy;
    public TextMesh PowerOfShot;
    public AudioSource OpenSnd;
    public AudioSource CloseSnd;
    public GameObject HealthBar;

    public void Open() {
        Info.active = true;
        EnemyProperties prop = this.transform.parent.GetComponent<EnemyProperties>();
        Health.text = prop.FullHealth.ToString();
        Damage.text = prop.Damage.ToString();
        Level.text = prop.Level.ToString();
        Accuracy.text = prop.Accuracy.ToString();
        PowerOfShot.text = prop.PowerOfShot.ToString();
        Multiply.active = true;
        HealthBar.transform.localPosition = new Vector3(HealthBar.transform.localPosition.x, HealthBar.transform.localPosition.y, -1.1f);
    }
    public void Close() {
        Info.active = false;
    }
    public void OpenSound() {
        OpenSnd.Play();
        Multiply.GetComponent<Animator>().SetBool("Activation", true);
    }

    public void CloseSound() {
        Multiply.GetComponent<Animator>().SetBool("Activation", false);
        HealthBar.transform.localPosition = new Vector3(HealthBar.transform.localPosition.x, HealthBar.transform.localPosition.y, -0.1f);
        CloseSnd.Play();
    }
    
}
