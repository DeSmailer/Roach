using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject RIPMenu, MainMenu, PauseMenu, GameplayMenu;
    public static bool inGame;

    public AudioClip[] clips;
    public Text ScoreRIPText, BestScoreRipText, BestScoreMaimMenuText;
    int score;
    int bestScore;
    GameObject Player;
    public static int adsCount = 1;//счетчик для рекламы

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SaveBestScore"))
        {
            bestScore = PlayerPrefs.GetInt("SaveBestScore");
        }
    }
    void Start()
    {
        ToMainMenu();
        Time.timeScale = 0f;
        RIPMenu.SetActive(false);
        MainMenu.SetActive(true);
        GameplayMenu.SetActive(false);
        inGame = false;
        print(inGame);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Play()
    {
        PlayAudio(clips[1], true);
        Player.GetComponent<PlayerController>().score = 0;
        Time.timeScale = 1f;
        RIPMenu.SetActive(false);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        GameplayMenu.SetActive(true);
        inGame = true;
        //print(inGame);

        Player.GetComponent<LevelMeneger>().Start();
        Player.GetComponent<PlayerController>().NitroFill = 1;
    }
    public void ToRipMenu()
    {
        PlayAudio(clips[2], false);
        score = (int)GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().score;
        ScoreRIPText.text = score.ToString();
        SetBestScore();
        Time.timeScale = 0f;
        RIPMenu.SetActive(true);
        GameplayMenu.SetActive(false);
        inGame = false;
        print(inGame);

        PauseMenu.SetActive(false);
        BestScoreRipText.text = bestScore.ToString();
        //PlayerPrefs.SetInt("SaveBestScore", 0);
    }
    public void ToMainMenu()
    {
        PlayAudio(clips[0], true);
        Time.timeScale = 0f;
        RIPMenu.SetActive(false);
        MainMenu.SetActive(true);
        GameplayMenu.SetActive(false);
        inGame = false;
        print(inGame);

        PauseMenu.SetActive(false);
        BestScoreMaimMenuText.text = bestScore.ToString();
    }
    public void ToPauseMenu()
    {
        Time.timeScale = 0f;
        GameplayMenu.SetActive(false);
        inGame = false;
        print(inGame);

        PauseMenu.SetActive(true);
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
        GameplayMenu.SetActive(true);
        inGame = true;
        print(inGame);

        PauseMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void PlayAudio(AudioClip clip, bool loop)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        if (loop)
        {
            audioSource.loop = true;
        }
        else
        {
            audioSource.loop = false;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void SetBestScore()
    {
        if(score > bestScore)
        {
            PlayerPrefs.SetInt("SaveBestScore", score);
            bestScore = score;
        }
        
    }
}
