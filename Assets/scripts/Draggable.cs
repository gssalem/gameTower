using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Draggable : MonoBehaviour
{
    public Camera myCam;
    
    private float startXPos;
    private float startYPos;
    private GameObject spawn;
    private Vector3 posicaoInicial;

    private GameObject HUD;
    private HUDManager HUDManager;

    private Ally allyScript;
    public GameObject tropa;

    public TMP_Text cardMana;
    public GameObject cardTroop;
    private SpriteRenderer spriteRenderer;

    private bool isDragging = false;

    private void Start()
    {
        posicaoInicial = transform.localPosition;
        
        spawn = GameObject.Find("Inicio1");

        HUD = GameObject.Find("HUD");
        HUDManager = HUD.GetComponent<HUDManager>();

        allyScript = tropa.GetComponent<Ally>();

        spriteRenderer = cardTroop.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = allyScript.GetComponent<SpriteRenderer>().sprite;
        spriteRenderer.enabled = true;

        cardMana.text = allyScript.custoMana.ToString();

    }

    private void Update()
    {
        if (isDragging)
        {
            DragObject();
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos = myCam.ScreenToWorldPoint(mousePos);
        
        startXPos = mousePos.x - transform.position.x;
        startYPos = mousePos.y - transform.position.y;

        isDragging = true;
    }

    

    private void OnMouseUp()
    {
        transform.localPosition = posicaoInicial;
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
 
        if(hit.collider.gameObject.tag == "CardEnter")
        {
            
            if(HUDManager.playerMana > allyScript.custoMana)
            {
                Instantiate(tropa, spawn.transform.position, Quaternion.identity);
                HUDManager.consumirMana(allyScript.custoMana);
            }
        }
        isDragging = false;
    }

    public void DragObject()
    {
        Vector3 mousePos = Input.mousePosition;

        if(!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mousePos.x - startXPos, mousePos.y - startYPos, transform.position.z);
    }
}
