using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 10f);    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colisao detectada");
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position += transform.right * 0.25f;
    }
}
