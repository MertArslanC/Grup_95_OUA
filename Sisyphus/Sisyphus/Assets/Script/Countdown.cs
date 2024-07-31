using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Countdown : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI countdownText;
    [SerializeField] public float counttime;
    [SerializeField] private GameObject gameOverPanel; 

    public float div = 1;

    private bool isGameOver = false;
    void Start()
    {
        gameOverPanel.SetActive(false); 
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (counttime > 0)
            {
                counttime -= Time.deltaTime / div;
            }
            else if (counttime < 0)
            {
                counttime = 0;
                isGameOver = true;
                countdownText.color = Color.red;
                Time.timeScale = 0;

                GameOver();
            }
        }
        int minutes = Mathf.FloorToInt(counttime / 60);
        int seconds = Mathf.FloorToInt(counttime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    void GameOver()
    {
        gameOverPanel.SetActive(true); 
        
    }
}