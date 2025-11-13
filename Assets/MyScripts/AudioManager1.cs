using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
     public AudioSource gunAudio; 

    void Update()
    {
        // Detect spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gunAudio.Play();
        }
    }
}