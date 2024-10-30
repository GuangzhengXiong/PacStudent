using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    private int highScore;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        time = PlayerPrefs.GetFloat("Time", 0);
        highScoreText.text = "High Score: " + highScore.ToString()
            + "\nTime: " + string.Format("{0:00}:{1:00}:{2:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60), Mathf.FloorToInt((time * 100) % 100));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveHighScore(int score, float t)
    {
        if(score > highScore)
        {
            highScore = score;
            time = t;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.SetFloat("Time", time);
            PlayerPrefs.Save();
        }
    }
}
