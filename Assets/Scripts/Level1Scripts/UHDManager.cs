using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UHDManager : MonoBehaviour
{
    public GameObject[] Lifes;
    public TextMeshProUGUI ScoreUI;
    public TextMeshProUGUI TimeUI;
    public GameObject ScaredTimeUI;
    private int highScore;
    private int score;
    private float time;
    private float scaredTime;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        time = 0;
        ScaredTimeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        ScoreUI.text = "Score: " + score.ToString();
        TimeUI.text = "Time: " + string.Format("{0:00}:{1:00}:{2:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60), Mathf.FloorToInt((time * 100) % 100));
    }

    public void LoadStartScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void SaveHighScore(int score, float t)
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            highScore = score;
            time = t;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.SetFloat("Time", time);
            PlayerPrefs.Save();
        }
    }
}
