using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBall : MonoBehaviour
{

    public GameObject ball;
    public GameObject preBall;
    public float colliderTime;

    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;
        if (ball == preBall || timer > 1)
        {
            ball = null;
        }
    }

    public GameObject returnBall()
    {
        return ball;
    }

    public float returnTime()
    {
        return colliderTime;
    }



    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "BasketBall")
        {
            if (ball != null)
            {
                ball = preBall;
            }
            else
                ball = collider.gameObject;
            colliderTime = Time.time;
            ball = collider.gameObject;
        }
    }


}
