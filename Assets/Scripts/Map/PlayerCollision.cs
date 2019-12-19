using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    public bool GoMotion = false;
    public Camera SceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        SceneCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            collision.transform.parent.GetComponent<Tile>().HaveSmoke = false;
            Destroy(collision.gameObject);
            Debug.Log("Smoke");
        }

        if (collision.gameObject.layer == 14)
        {
            SceneCamera.GetComponent<MainMap>().battlevar = 100;
            if (collision.gameObject.GetComponent<BanditsAreaDoll>().NumberOfBand == 1)
            {
                SceneCamera.GetComponent<MainMap>().NumberOFActiveBand = 1;
            }
            if (collision.gameObject.GetComponent<BanditsAreaDoll>().NumberOfBand == 2)
            {
                SceneCamera.GetComponent<MainMap>().NumberOFActiveBand = 2;
            }
            if (collision.gameObject.GetComponent<BanditsAreaDoll>().NumberOfBand == 3)
            {
                SceneCamera.GetComponent<MainMap>().NumberOFActiveBand = 3;
            }
            Debug.Log("Band");
        }

        if (collision.gameObject.layer == 15) {
            SceneCamera.GetComponent<MainMap>().StayOnStore = true;
            this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            SceneCamera.GetComponent<MainMap>().battlevar = 100;
            if (collision.gameObject.GetComponent<BanditsAreaDoll>().NumberOfBand == 1)
            {
                SceneCamera.GetComponent<MainMap>().NumberOFActiveBand = 1;
            }
            if (collision.gameObject.GetComponent<BanditsAreaDoll>().NumberOfBand == 2)
            {
                SceneCamera.GetComponent<MainMap>().NumberOFActiveBand = 2;
            }
            if (collision.gameObject.GetComponent<BanditsAreaDoll>().NumberOfBand == 3)
            {
                SceneCamera.GetComponent<MainMap>().NumberOFActiveBand = 3;
            }
        }

        if (collision.gameObject.layer == 15)
        {
            SceneCamera.GetComponent<MainMap>().StayOnStore = true;
            this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            SceneCamera.GetComponent<MainMap>().battlevar = 10000;
            SceneCamera.GetComponent<MainMap>().NumberOFActiveBand = 0;
        }

        if (collision.gameObject.layer == 15) {
            SceneCamera.GetComponent<MainMap>().StayOnStore = false;
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
