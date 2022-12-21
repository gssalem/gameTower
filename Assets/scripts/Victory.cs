using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip victory;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().clip = victory;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
