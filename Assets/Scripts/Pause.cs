using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    Lives liveScript;

    public void pauseGame()
    {
        Time.timeScale = 0f;
    }
    
    public void resumeGame()
    {
        Time.timeScale = 1f;
    }

    public void quitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
        liveScript.resetLives();
    }
}
