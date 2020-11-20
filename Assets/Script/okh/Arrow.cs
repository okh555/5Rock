using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody myBody;
    public float lifeTimer = 10;
    private float timer = 4f;
    private bool hitSomething = false;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0 && hitSomething)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0 && hitSomething)
        {
            enabled = false;
            Destroy(gameObject);
        }
        if (!hitSomething)
        {
            transform.rotation = Quaternion.LookRotation(myBody.velocity);
            transform.Rotate(90f, 0, 0);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Arrow")
        {
            return;
        }
        if (collision.collider.tag == "Player")
        {
            return;
        }
        hitSomething = true;
        Stick();
    }

    private void Stick()
    {
        myBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
