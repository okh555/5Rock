using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    Vector3 currentPos;
    Rigidbody rig;

    float slipperyTime = 2f;
    float decreaseTime = 0.01f;

    float collisionTime = 0;
    float decreaseTempTime = 0;

    HockeyManager hockeyManager;
        

    // Start is called before the first frame update
    void Start()
    {
        hockeyManager = GetComponentInParent<HockeyManager>();
        currentPos = GetComponent<Transform>().position;
        rig = GetComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Collider>().material.dynamicFriction = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y != currentPos.y)
        {
            transform.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
        }
        /*if (Time.time - collisionTime > slipperyTime)
        {
            if (Time.time - decreaseTempTime > decreaseTime)
            {
                rig.velocity = new Vector3(rig.velocity.x * 0.99f, 0, rig.velocity.z * 0.99f);
                decreaseTime = Time.time;
            }

        }*/
    }


    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Wall")
        {
            if(collider.gameObject.name == "Wall1")
            {
                rig.velocity = new Vector3(rig.velocity.x, 0, -rig.velocity.z);
            }
            if (collider.gameObject.name == "Wall2")
            {
                rig.velocity = new Vector3(-rig.velocity.x, 0, rig.velocity.z);
            }
            if (collider.gameObject.name == "Score")
            {
                hockeyManager.Score(true);
                Destroy(this.gameObject);
            }
            if (collider.gameObject.name == "Score2")
            {
                hockeyManager.Score(false);
                Destroy(this.gameObject);
            }
        }
        
    }

    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag != "HockeyGround")
             collisionTime = Time.time;
    }

}
