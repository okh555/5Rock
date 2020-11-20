using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBall : MonoBehaviour
{

    public GameObject ball;

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject returnBall()
    {
        return ball;
    }
   


    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "BasketBall")
        {
            ball = collider.gameObject;
        }
    }
}
