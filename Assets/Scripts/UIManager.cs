using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject menuScreenDisplay;
    public Text scoreText;
    public int score = 0;
    public void LivesUpdate(int currentLives)
    {
    if(livesImageDisplay!=null)
        {
            livesImageDisplay.sprite = lives[currentLives];
        }
    }

    public void ScoreUpdate(int updateValue)
    {
        if (scoreText != null)
        {
            score += updateValue;
            scoreText.text = "Score: " + score;
        }

    }

    public void SetMenuScreenActive(bool mode)
    {
        menuScreenDisplay.SetActive(mode);
    }
}
