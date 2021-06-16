using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMeneger : MonoBehaviour
{
    public GameObject[] carpets;
    public GameObject chair;

    private const float carpetLength = 19.45658f;

    public int minCountOfChair;
    public int maxCountOfChair;
    public void Start()
    {
        DestroyChair(carpets[0].transform);
        DestroyChair(carpets[1].transform);
        DestroyChair(carpets[2].transform);
        //возвращаем ковры на свое место
        carpets[1].transform.Translate(0, 0, 0);
        carpets[0].transform.position = new Vector3(0, carpets[1].transform.position.y + carpetLength, 1);
        carpets[2].transform.position = new Vector3(0, carpets[1].transform.position.y - carpetLength, 1);
        //спавним начальные 3 стула
        Instantiate(chair, new Vector3(0, 10.9f, -2), Quaternion.identity, carpets[0].transform);
        Instantiate(chair, new Vector3(0, -6.6f, -2), Quaternion.identity, carpets[0].transform);
        Instantiate(chair, new Vector3(0, 14f, -2), Quaternion.identity, carpets[1].transform);
    }

    private void Update()   
    {
        carpets[0].transform.Translate(0, -sharedVariables.currentSpeed * Time.deltaTime, 0);
        carpets[1].transform.Translate(0, -sharedVariables.currentSpeed * Time.deltaTime, 0);
        carpets[2].transform.Translate(0, -sharedVariables.currentSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Carpet"))
        {
            //делитнуть все стулья последнего ковра(2)
            DestroyChair(carpets[2].transform);
            GameObject temp = carpets[2];
            carpets[2] = carpets[1];
            carpets[1] = carpets[0];
            carpets[0] = temp;
            
            carpets[0].transform.position = new Vector3(0, carpets[1].transform.position.y + carpetLength, 1);
            //создать новые стулья
            SpawnChair(carpets[0]);
        }
    }

    private void SpawnChair(GameObject Parent)
    {
        int countOFChair = Random.Range(minCountOfChair, maxCountOfChair + 1);
        float whatLengthOfCarpetIsAlreadyTaken = 0 - carpetLength / 2;
        float minimumDifference = CalculatemMinimumDifference(countOFChair);
        float maximumDifference = CalculatemMaximumDifference(countOFChair);
        // 0 - 19.45658 / 2 это начало(ниижняя часть) ковра
        for (int i = 0; i < countOFChair; i++)
        {
            float lengthPlus = Random.Range(minimumDifference, maximumDifference);
            if (whatLengthOfCarpetIsAlreadyTaken + lengthPlus < carpetLength / 2)
            {
                Instantiate(chair, new Vector3(0, Parent.transform.position.y + 
                    whatLengthOfCarpetIsAlreadyTaken + lengthPlus, -2), Quaternion.identity, Parent.transform);
                whatLengthOfCarpetIsAlreadyTaken = whatLengthOfCarpetIsAlreadyTaken + lengthPlus;
            }
        }
    }
    private float CalculatemMinimumDifference(int countOFChair)
    {
        return 1;
    }
    private float CalculatemMaximumDifference(int countOFChair)
    {
        return carpetLength / countOFChair;
    }
    private void DestroyChair(Transform Parent)
    {
        foreach (Transform child in Parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
