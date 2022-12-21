using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip intro;
    public AudioClip introNav;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().clip = intro;
        audioSource.Play();
        
    }

    void Update()
    {
        
    }

    public void navSong()
    {
        audioSource.PlayOneShot(introNav);
    }
}
