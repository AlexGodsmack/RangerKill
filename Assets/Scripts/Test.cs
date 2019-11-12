using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public Camera SceneCamera;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
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
    }
    public void OnCollisionStay2D(Collision2D collision)
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
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            SceneCamera.GetComponent<MainMap>().battlevar = 10000;
            SceneCamera.GetComponent<MainMap>().NumberOFActiveBand = 0;
        }
    }

}
