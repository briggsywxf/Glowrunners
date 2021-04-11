using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    private GameObject _stopwatchText;
    private GameObject _countdownText;
    private Stopwatch _stopwatch;
    private bool playing = false;
    private bool gameOver = false;
    private bool showFinalTime;
    private int winningPlayer = 0;
    private TimeSpan ts;

    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (!gameOver)
        {
            _stopwatch = new Stopwatch();
            _stopwatchText = GameObject.Find("Timer");
            _countdownText = GameObject.Find("Countdown");
            StartCoroutine(LevelCountdown());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            ts = _stopwatch.Elapsed;
            String stopwatchElapsedText = String.Format("{0:00}:{1:00}.{2:00}",
                ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            _stopwatchText.GetComponent<TextMeshProUGUI>().text = stopwatchElapsedText;
        }

        if (gameOver && SceneManager.GetActiveScene().buildIndex == 2)
        {
            playing = false;
            GameObject.Find("Winner Text").GetComponent<TextMeshProUGUI>().text = "P" + winningPlayer + " Wins!";
            if (showFinalTime)
            {
                GameObject.Find("Final Time Text").GetComponent<TextMeshProUGUI>().text =  String.Format("{0:00}:{1:00}.{2:00}",
                    ts.Minutes, ts.Seconds,
                    ts.Milliseconds);
            }
            else
            {
                GameObject.Find("Final Time Text").GetComponent<TextMeshProUGUI>().text = "";
            }
            
        }
    }

    public void Win(int winningPlayerInput, bool showFinalTimeInput)
    {
        gameOver = true;
        winningPlayer = winningPlayerInput;
        showFinalTime = showFinalTimeInput;
        _stopwatch.Stop();
        playing = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMenu()
    {
        Debug.Log("Return to menu");
        Destroy(gameObject, 2f);
        SceneManager.LoadScene(0);
    }

    IEnumerator LevelCountdown()
    {
        Debug.Log("countdown starting");
        yield return new WaitForSeconds(3);
        _countdownText.GetComponent<AudioSource>().Play();
        for (int i = 3; i >= 0; i--)
        {
            if (i == 0)
            {
                _countdownText.GetComponent<TextMeshProUGUI>().text = "GO";
            }
            else
            {
                _countdownText.GetComponent<TextMeshProUGUI>().text = i.ToString();
            }

            yield return new WaitForSeconds(1f);
        }
        
        _stopwatch.Start();
        playing = true;
        _countdownText.GetComponent<TextMeshProUGUI>().enabled = false;
        GameObject.Find("Music").GetComponent<GameMusic>().Play();
        GameObject.Find("Start Blocker").GetComponent<BoxCollider2D>().enabled = false;
    }
}