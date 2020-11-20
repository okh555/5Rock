using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardTest : MonoBehaviour
{
    public Vector3 dir;
    public float force;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.AddForce(dir * force);
        }
    }
}
