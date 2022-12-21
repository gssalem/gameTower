using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public Text cronometro;
    private int tempo = 60;
    private float tempoFloat = 60;

    public float enemyHealth = 200;
    public Transform healthBar;
    private Vector3 healthBarScale;
    private float healthPercent;

    public TMP_Text manaControler;
    private float manaTempoFloat;

    private int playerManaMaximo = 10;
    public Transform manaBar;
    private Vector3 manaBarScale;
    private float manaPercent;
    public int playerMana;

    public float playerHealth = 100;
    public Transform playerBar;
    private Vector3 playerHealthBarScale;
    private float playerHealthPercent;
    public TMP_Text playerHealthString;

    public AudioSource inGameSong;

    void Start()
    {
        inGameSong = GetComponent<AudioSource>();
        inGameSong.Play();

        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / enemyHealth;

        manaBarScale = manaBar.localScale;
        manaPercent = manaBarScale.x / playerManaMaximo;

        playerHealthBarScale = playerBar.localScale;
        playerHealthPercent = playerHealthBarScale.x/playerHealth;

        cronometro.text = tempo.ToString();
    }

    void updateHealthBar()
    {
        healthBarScale.x = healthPercent * enemyHealth;
        healthBar.localScale = healthBarScale;
    }

    public void takeDamage(float amount)
    {
        enemyHealth -= amount;
        if(enemyHealth < 0)
        {
            enemyHealth = 0;
        }
        updateHealthBar();

    }    

    private void tempoCount()
    {
        if(tempo > 0)
        {
            tempoFloat -= Time.deltaTime;
            tempo = Mathf.RoundToInt(tempoFloat);
            cronometro.text = tempo.ToString();
        }
    }

    void updateManaBar()
    {
        playerMana = Mathf.RoundToInt(manaTempoFloat);
        manaBarScale.x = manaPercent * playerMana;
        manaBar.localScale = manaBarScale;
        manaControler.text = playerMana.ToString();
    }

    private void manaCount()
    {
        manaTempoFloat += Time.deltaTime;
    }

    public void consumirMana(int mana)
    {
        playerMana -= mana;
        manaTempoFloat -= mana;
        updateManaBar();
        
    }

    void updatePlayerBar()
    {
        playerHealthBarScale.x = playerHealthPercent * playerHealth;
        playerBar.localScale = playerHealthBarScale;
        playerHealthString.text = playerHealth.ToString();
    }

    public void playerTakeDamage(float amount)
    {
        playerHealth -= amount;
        if(playerHealth < 0)
        {
            playerHealth = 0;
        }
        updatePlayerBar();

    }    

    void Update()
    {
        if(playerHealth == 0)
        {
            SceneManager.LoadScene("Defeat");
        }
        if(enemyHealth == 0)
        {
            SceneManager.LoadScene("Victory");
        }
        takeDamage(0);
        consumirMana(0);
        tempoCount();
        if(playerMana < 10)
        {
            manaCount();
        }
        updateManaBar();
        
        
        
    }
}
