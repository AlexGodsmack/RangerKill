using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShell : MonoBehaviour
{

    public GameObject Parent;
    public GameObject Target;
    public float Timer;
    public bool Gotcha;

    //private float elapsed;
    //private int TimeCounter;

    void Start()
    {

        Timer = 0.0f;
        
    }

    void Update()
    {

        if (Parent != null && Target != null) {
            this.transform.position = Vector3.Lerp(Parent.transform.position, Target.transform.position, Timer);
            Timer += 0.2f;
        }

        //if (Timer > 1.5f) {
        //    Destroy(this.gameObject);
        //}
        
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject == Target.gameObject) {
            Gotcha = true;
            if (this.GetComponent<SpriteRenderer>().sprite.name == "Shell_18") {
                GameObject E = Instantiate(Resources.Load("Explosion_01")) as GameObject;
                E.transform.SetParent(Target.transform);
                E.transform.localPosition = new Vector3(0, 0, -0.1f);
            }
        }

    }
}
