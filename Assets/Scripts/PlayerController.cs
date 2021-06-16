using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //ускорение
    public float NitroFill;
    public Slider NitroBar;
    public float NitroFillPlus;
    public float NitroFillMinus;
    //изображение таракана
    private SpriteRenderer PlayerSprite;
    public Sprite roachHide, roachNoHide;
    //счет
    public float score;
    [Multiline]
    public Text scoreText;
    //звук ускорения
    public AudioClip soundUp;
    bool isPlayedSoundUp = false;
    //камера и тряска
    public GameObject mainCamera;
    Animator mainCameraShake;

    void Start()
    {
        NitroFill = 1;
        PlayerSprite = GetComponent<SpriteRenderer>();
        mainCameraShake = mainCamera.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0)
        {
            if (NitroFill > 0)
            {
                PlayerSprite.sprite = roachHide;
                sharedVariables.Boost();
                NitroFill -= NitroFillMinus * Time.deltaTime;
                DisplayScore();
                if (Menu.inGame == true)
                {
                    mainCameraShake.SetBool("shake", true);
                    PlayAudio(soundUp);
                }
            }
            else
            {
                PlayerSprite.sprite = roachNoHide;
                sharedVariables.NoBoost();
                if (NitroFill < 1)
                {
                    DisplayScore();
                    StopAudio();
                    mainCameraShake.SetBool("shake", false);
                }
            }
        }
        else
        {
            PlayerSprite.sprite = roachNoHide;
            sharedVariables.NoBoost();
            if (NitroFill < 1)
            {
                NitroFill += NitroFillPlus * Time.deltaTime;
            }
            DisplayScore();
            StopAudio();
            mainCameraShake.SetBool("shake", false);
        }
        NitroBar.value = NitroFill;
    }

    private void DisplayScore()
    {
        score = score + sharedVariables.currentSpeed * Time.deltaTime;
        scoreText.text = "score: \n" + (int)score;
    }
    private void PlayAudio(AudioClip clip)
    {
        if (isPlayedSoundUp == false)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            //audioSource.Stop();

            audioSource.loop = true;
            isPlayedSoundUp = true;

            audioSource.clip = clip;
            audioSource.Play();
        }
    }
    private void StopAudio()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        isPlayedSoundUp = false;
    }
}