using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharedVariables : MonoBehaviour
{
    public float startSpeed;
    public float startBoostCoefficient;
    public float increaseSpeedBy;

    private static float startSpeedP;
    private static float startBoostCoefficientP;
    private static float increaseSpeedByP;

    public static float speed;

    public static float boostCoefficient;

    public static float currentSpeed;

    public static bool attackable;

    public static float increaseSpeed;
    public void Start()
    {
        startSpeedP = currentSpeed = speed = startSpeed;
        startBoostCoefficientP = boostCoefficient = startBoostCoefficient;
        attackable = true;
        increaseSpeedByP = increaseSpeed = increaseSpeedBy;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().score = 0;
    }
    public static void Boost()
    {
        currentSpeed = speed * boostCoefficient;
        attackable = false;
    }
    public static void NoBoost()
    {
        currentSpeed = speed;
        attackable = true;
    }
    public static void IncreaseSpeed()
    {
        speed += increaseSpeed;
    }
    public static void Restart()
    {
        currentSpeed = speed = startSpeedP;
        boostCoefficient = startBoostCoefficientP;
        attackable = true;
        increaseSpeed = increaseSpeedByP;
    }
}
