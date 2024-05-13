using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{

    TextMeshProUGUI livesText;
    [SerializeField]
    bool initLive = false;
    [SerializeField]
    GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (initLive == true)
        {
            resetLives();
            initLive = false;
        }

        livesText.text = "Lives = " + PlayerPrefs.GetInt("Lives");
    }

    public void resetLives()
    {
        PlayerPrefs.SetInt("Lives", 3);
    }

    public void reduceLives()
    {
        int Lives = PlayerPrefs.GetInt("Lives");
        Lives = Lives - 1;
        PlayerPrefs.SetInt("Lives", Lives);

        if (Lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            //game over
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void retry()
    {
        Time.timeScale = 1;
        resetLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
