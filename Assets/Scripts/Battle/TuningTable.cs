using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TuningTable : MonoBehaviour
{
    public AudioSource Snd;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Sound() {

        Snd.Play();

    }

    public void Back_To_Map() {
        SceneManager.LoadScene(5);
    }
}
