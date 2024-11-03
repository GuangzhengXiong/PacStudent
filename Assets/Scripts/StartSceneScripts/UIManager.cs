using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI HighScoreUI;
    public TextMeshProUGUI TimeUI;
    private int highScore;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreUI.text = "High Score: " + highScore.ToString();

        time = PlayerPrefs.GetFloat("Time", 0);
        TimeUI.text ="Time: " + string.Format("{0:00}:{1:00}:{2:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60), Mathf.FloorToInt((time * 100) % 100));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadLevel1()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadLevel2()
    {
    }
}
