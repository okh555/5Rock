using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardCushion : MonoBehaviour
{
    public Vector3 NormalVector;
    public float CushionFriction = 0.2F;

    private void Start()
    {
        CushionFriction = 1f - CushionFriction;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.velocity = Vector3.Reflect(other.attachedRigidbody.velocity, NormalVector) * CushionFriction;
    }
}
