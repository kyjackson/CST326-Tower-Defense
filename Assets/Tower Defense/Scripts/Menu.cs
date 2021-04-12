using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject buttonStart;
    public GameObject buttonRestart;

    private void Start()
    {
        buttonStart = GameObject.Find("Start");
        buttonRestart = GameObject.Find("Restart");
        
        Time.timeScale = 0;
    }

    public void Play()
    {
        buttonStart.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //buttonRestart.SetActive(false);
    }
}
