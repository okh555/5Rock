﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingManager : MonoBehaviour
{
    public ShootingTarget[] targets;
    public ShootingGun shootingGun;

    bool isGameStart;

    int score;
    //int hitTargetCount;
    float time;
    public float timeLimit = 60f;

    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text bulletText;


    private void Awake()
    {
        foreach (var target in targets)
        {
            target.OnTargetHit += AddScore;
        }

        shootingGun.OnGunFire += SetBulletText;
    }

    private void Update()
    {
        if (isGameStart)
        {
            time += Time.deltaTime;

            if(time > timeLimit)
            {
                time = timeLimit;

                StopShooting();
            }

            timeText.text = "Time : " + time.ToString();
        }
    }

    void Init()
    {
        isGameStart = false;
        score = 0;
        //hitTargetCount = 0;
        time = 0f;
    }

    public void StartShooting()
    {
        if (isGameStart) return;

        Init();

        isGameStart = true;
        shootingGun.Reload();

        foreach (var target in targets)
        {
            target.SetTarget(true);
        }
    }

    void StopShooting()
    {
        isGameStart = false;

        foreach (var target in targets)
        {
            target.SetTarget(false);
        }
    }


    void AddScore(int newScore)
    {
        if (!isGameStart) return;

        score += newScore;

        scoreText.text = "Score : " + score.ToString();
    }

    void SetBulletText(int remainBullet)
    {
        bulletText.text = "Bullet : " + remainBullet.ToString();
    }
}
