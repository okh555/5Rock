using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Basket_Manager : MonoBehaviour
{
    public GameObject scorePoint1;
    public GameObject scorePoint2;
    public TextMesh timeText;

    private ReturnBall point1;
    private ReturnBall point2;


    private float time;
    private float score = 0;

    void Awake()
    {
        point1 = scorePoint1.GetComponent<ReturnBall>();
        point2 = scorePoint2.GetComponent<ReturnBall>();
    }
    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        Score();
    }

    void Score()
    {
        if (point1.ball != null)
        {
            if (point2.returnTime() - point1.returnTime() < 0.4f)
            {
                if (point1.returnBall() == point2.returnBall())
                {
                    Debug.Log("Score");
                    score += 50;
                    timeText.text = "Score : " + score;
                    point1.ball = null;
                    point2.ball = null;
                }
            }

        }
    }
}
