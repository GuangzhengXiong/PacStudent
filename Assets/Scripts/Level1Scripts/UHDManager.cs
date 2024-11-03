using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UHDManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Lifes;
    [SerializeField] private TextMeshProUGUI ScoreUI;
    [SerializeField] private TextMeshProUGUI TimeUI;
    [SerializeField] private GameObject ScaredTimeUIObject;
    private TextMeshProUGUI ScaredTimeUI;
    [SerializeField] private GameObject CueUIObject;
    private TextMeshProUGUI CueUI;
    public static int score;
    private float time;
    private int life = 2;
    private float scaredTime;
    public static bool isGhostsScaredStart = false;
    private bool isRecovering = false;
    public static bool ifLostLife = false;
    public static bool ifGameOver = false;
    private bool ifStart = false;


    private void Start()
    {
        score = 0;
        time = 0;
        scaredTime = 0;
        ScaredTimeUI = ScaredTimeUIObject.GetComponent<TextMeshProUGUI>();
        ScaredTimeUIObject.SetActive(false);
        CueUI = CueUIObject.GetComponent<TextMeshProUGUI>();
        Time.timeScale = 0;
        StartCoroutine(startGame());
    }


    private void Update()
    {
        if (ifStart)
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
        if (ifLostLife)
        {
            ifLostLife = false;
            lostLife();
        }
        if (ifGameOver)
        {
            gameOver();
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
        Time.timeScale = 0;
        ifStart = false;
        BackgroundMusicManager.ifStart = false;
        PacStudentController.ifStart = false;
        GhostController.ifStart = false;

        if (score > highScore || (score == highScore && time < highScoreTime))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.SetFloat("Time", time);
            PlayerPrefs.Save();
        }
        StartCoroutine(endGame());
    }


    IEnumerator startGame()
    {
        CueUI.text = "3"; 

        yield return new WaitForSecondsRealtime(1);

        CueUI.text = "2";

        yield return new WaitForSecondsRealtime(1);

        CueUI.text = "1";

        yield return new WaitForSecondsRealtime(1);

        CueUI.text = "GO!";

        yield return new WaitForSecondsRealtime(1);

        CueUIObject.SetActive(false);
        ifStart = true;
        BackgroundMusicManager.ifStart = true;
        PacStudentController.ifStart = true;
        GhostController.ifStart = true;
        Time.timeScale = 1.0f;
    }


    IEnumerator endGame()
    {
        CueUIObject.SetActive(true);
        CueUI.text = "Game Over";

        yield return new WaitForSecondsRealtime(3);

        LoadStartScene();
    }
}
