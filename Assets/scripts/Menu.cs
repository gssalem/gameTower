using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public string cena;
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("inGame");
    }

    public void quitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Applicaton.Quit();
    }
}
