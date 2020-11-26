using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBilliardBall : MonoBehaviour
{
    public GameObject balls;
    public Transform spawnPosition;

    private GameObject spawnedBalls;

    void Start()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        if (spawnedBalls != null)
        {
            Destroy(spawnedBalls);
        }

        spawnedBalls = Instantiate(balls);
        spawnedBalls.transform.position = spawnPosition.position;
    }
}
