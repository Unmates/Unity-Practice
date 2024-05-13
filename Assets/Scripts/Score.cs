using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "Coins = " + PlayerPrefs.GetInt("Score");
    }

    public void addScore()
    {
        score = score + 1;
        scoreText.text = "Coins = " + score;

        PlayerPrefs.SetInt("Score", score);
    }
}
