using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Barrier : MonoBehaviour
{
    GameObject Player;
    public float SpeedCoef;//коэффициент для уменьшения прироста скорости со временем

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        //реклама
        Advertisement.Initialize("3709401", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sharedVariables.IncreaseSpeed();

        if(sharedVariables.increaseSpeed >= 0)
        {
            sharedVariables.increaseSpeed -= 0.02f;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (sharedVariables.attackable)
            {
                Ads(Menu.adsCount);
                GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>().ToRipMenu();
                collision.GetComponent<LevelMeneger>().Start();
                sharedVariables.Restart();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (sharedVariables.attackable)
            {
                Ads(Menu.adsCount);
                GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>().ToRipMenu();
                collision.GetComponent<LevelMeneger>().Start();
                sharedVariables.Restart();
            }
        }
    }
    private void Ads(int adsCount)
    {
        print(adsCount);
        if (adsCount < 4)
        {
            Menu.adsCount++;
        }
        if (adsCount >= 4)
        {
            Menu.adsCount = 1;
            //реклама
            print("тут рекламка должна быть");
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
    }
}
