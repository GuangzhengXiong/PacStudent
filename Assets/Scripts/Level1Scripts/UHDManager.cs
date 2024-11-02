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
    public GameObject ScaredTimeUIObject;
    private TextMeshProUGUI ScaredTimeUI;
    private int highScore;
    public static int score;
    private float time;
    private int life = 2;
    private float scaredTime;
    public static bool isGhostsScaredStart = false;
    private bool isRecovering = false;
    public static bool ifLostLife = false;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        time = 0;
        scaredTime = 0;
        ScaredTimeUI = ScaredTimeUIObject.GetComponent<TextMeshProUGUI>();
        ScaredTimeUIObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        ScoreUI.text = "Score: " + score.ToString();
        TimeUI.text = "Time: " + string.Format("{0:00}:{1:00}:{2:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60), Mathf.FloorToInt((time * 100) % 100));
        if (isGhostsScaredStart)
        {
            isGhostsScaredStart = false;
            scaredTime = 10.0f;
            ScaredTimeUIObject.SetActive(true);
        }
        if (scaredTime > 0)
        {
            scaredTime -= Time.deltaTime;
            ScaredTimeUI.text = "Scared Time: " + string.Format("{0:00}:{1:00}:{2:00}", Mathf.FloorToInt(scaredTime / 60), Mathf.FloorToInt(scaredTime % 60), Mathf.FloorToInt((scaredTime * 100) % 100));
            if(!isRecovering && scaredTime <= 3)
            {
                isRecovering = true;
                GhostController.isRecovering = true;
            }
            if (scaredTime <= 0)
            {
                isRecovering = false;
                GhostController.isScaredEnd = true;
                BackgroundMusicManager.isGhostsScaredEnd = true;
                ScaredTimeUIObject.SetActive(false);
            }
        }
        if(ifLostLife)
        {
            ifLostLife = false;
            lostLife();
        }
    }

    public void LoadStartScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void lostLife()
    {
        Destroy(Lifes[life--]);
        if (life < 0)
            gameOver();
    }

    private void gameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float highScoreTime = PlayerPrefs.GetFloat("Time", 0);
        if (score > highScore || score == highScore && time < highScoreTime)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.SetFloat("Time", time);
            PlayerPrefs.Save();
        }
    }
}
