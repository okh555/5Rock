using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGame : MonoBehaviour
{
    List<Target> Targets = new List<Target>();
    public Gun gun;

    bool isGameStart;

    int score;
    int hitTargetCount;
    bool isResetTargets;
    float time;
    public float timeLimit = 60f;

    public TextMesh scoreText;
    public TextMesh timeText;
    public TextMesh bulletText;

    private void Awake()
    {
        Target[] targets = GetComponentsInChildren<Target>();
        foreach (Target target in targets)
        {
            Targets.Add(target);
            target.OnTargetHit += AddScore;
        }

        gun.OnGunFire += SetBulletText;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            if (time > timeLimit)
            {
                GameEnd();
                return;
            }

            time += Time.deltaTime;
            SetTimeText();

            if (isResetTargets == false && hitTargetCount > 0 && hitTargetCount % 9 == 0)
            {
                ResetTargets();
                isResetTargets = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            StartGame();
        }
    }


    void InitGame()
    {
        isGameStart = false;
        score = 0;
        hitTargetCount = 0;
        isResetTargets = false;
        time = 0f;
    }

    void ResetTargets()
    {
        foreach (Target target in Targets)
        {
            target.ResetTarget();
        }
        Debug.Log("Target Reset");
    }

    public void StartGame()
    {
        if (isGameStart) return;

        
        InitGame();

        isGameStart = true;
        
        gun.Reload();

        foreach (Target target in Targets)
        {
            target.GameStart();
        }
        Debug.Log("Shooting Game Start!");
    }

    void GameEnd()
    {
        isGameStart = false;

        foreach (Target target in Targets)
        {
            target.GameEnd();
        }

        time = timeLimit;
        SetTimeText();

        Debug.Log("Shooting Game End!");
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        hitTargetCount++;
        isResetTargets = false;

        scoreText.text = "Score : " + score.ToString();
    }

    void SetTimeText()
    {
        timeText.text = "Time : " + Mathf.FloorToInt(timeLimit - time).ToString();
    }

    public void SetBulletText(int remainBullet)
    {
        bulletText.text = "Bullet : " + remainBullet.ToString();
    }
}
