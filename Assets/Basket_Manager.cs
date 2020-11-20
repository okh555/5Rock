using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket_Manager : MonoBehaviour
{
    public GameObject scorePoint1;
    public GameObject scorePoint2;

    private ReturnBall point1;
    private ReturnBall point2;

    void Awake()
    {
        point1 = scorePoint1.GetComponent<ReturnBall>();
        point2 = scorePoint2.GetComponent<ReturnBall>();
    }

    // Update is called once per frame
    void Update()
    {
        Score();
    }

    void Score()
    {
        if (point1.ball != null)
        {
            if (point1.returnBall() == point2.returnBall())
            {
                Debug.Log("Score");
                point1.ball = null;
                point2.ball = null;
            }
        }
    }


}
