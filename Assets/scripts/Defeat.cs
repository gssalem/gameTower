using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip defeat;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().clip = defeat;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
