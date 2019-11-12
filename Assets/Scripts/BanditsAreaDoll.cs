using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class BanditsAreaDoll : MonoBehaviour
{

    public int NumberOfBand;
    public int SizeOfArea;

    public Sprite SmallSkin1;
    public Sprite SmallSkin2;
    public Sprite SmallSkin3;
    public Sprite MediumSkin1;
    public Sprite MediumSkin2;
    public Sprite MediumSkin3;
    public Sprite BigSkin1;
    public Sprite BigSkin2;
    public Sprite BigSkin3;

    public Collider2D ColliderSmall;
    public Collider2D ColliderMedium;
    public Collider2D ColliderBig;
    // Start is called before the first frame update
    void Start()
    {

        if (NumberOfBand == 1) {
            if (SizeOfArea == 1) {
                this.GetComponent<SpriteRenderer>().sprite = SmallSkin1;
                ColliderSmall.enabled = true;
            }
            if (SizeOfArea == 2) {
                this.GetComponent<SpriteRenderer>().sprite = SmallSkin2;
                ColliderSmall.enabled = true;
            }
            if (SizeOfArea == 3) {
                this.GetComponent<SpriteRenderer>().sprite = SmallSkin3;
                ColliderSmall.enabled = true;
            }
        }
        if (NumberOfBand == 2) {
            if (SizeOfArea == 1) {
                this.GetComponent<SpriteRenderer>().sprite = MediumSkin1;
                ColliderMedium.enabled = true;
            }
            if (SizeOfArea == 2) {
                this.GetComponent<SpriteRenderer>().sprite = MediumSkin2;
                ColliderMedium.enabled = true;
            }
            if (SizeOfArea == 3) {
                this.GetComponent<SpriteRenderer>().sprite = MediumSkin3;
                ColliderMedium.enabled = true;
            }
        }
        if (NumberOfBand == 3) {
            if (SizeOfArea == 1) {
                this.GetComponent<SpriteRenderer>().sprite = BigSkin1;
                ColliderBig.enabled = true;
            }
            if (SizeOfArea == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = BigSkin2;
                ColliderBig.enabled = true;
            }
            if (SizeOfArea == 3)
            {
                this.GetComponent<SpriteRenderer>().sprite = BigSkin3;
                ColliderBig.enabled = true;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
