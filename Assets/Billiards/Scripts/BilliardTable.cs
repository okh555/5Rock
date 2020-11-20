using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardTable : MonoBehaviour
{
    public float BallSlowScale = 0.995f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BilliardBall"))
        {
            Vector3 vel = other.attachedRigidbody.velocity;
            vel.Scale(new Vector3(BallSlowScale, BallSlowScale, BallSlowScale));
            other.attachedRigidbody.velocity = vel;
        }
    }
}
