using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeStore : MonoBehaviour
{

    public Sprite Active;
    public Sprite NonActive;
    public GameObject[] Grade;

    public int GetGrade;

    void Start()
    {
        
    }

    void Update()
    {
        if (GetGrade != 0) {
            for (int i = 0; i < Grade.Length; i++) {
                if (i + 1 > GetGrade) {
                    Grade[i].GetComponent<SpriteRenderer>().sprite = NonActive;
                } else {
                    Grade[i].GetComponent<SpriteRenderer>().sprite = Active;
                }
            }
        } else if(GetGrade == 0){
            for (int i = 0; i < Grade.Length; i++) {
                Grade[i].GetComponent<SpriteRenderer>().sprite = NonActive;
            }
        }
    }
}
