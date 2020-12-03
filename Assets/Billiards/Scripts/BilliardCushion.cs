using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BilliardCushion : MonoBehaviour
{
    public Vector3 NormalVector;
    public float CushionFriction = 0.2F;
    public PhotonView pv;

    private void Start()
    {
        CushionFriction = 1f - CushionFriction;
    }

    private void OnTriggerEnter(Collider other)
    {
        //    other.attachedRigidbody.velocity = Vector3.Reflect(other.attachedRigidbody.velocity, NormalVector) * CushionFriction;
        //    other.attachedRigidbody.angularVelocity = Vector3.Reflect(other.attachedRigidbody.angularVelocity, NormalVector) * CushionFriction;
        Debug.Log("trigger");
        PhotonView otherPV = other.GetComponent<PhotonView>();
        if (otherPV)
        {
            int otherPVID = otherPV.ViewID;
            Debug.Log(otherPVID);
            pv.RPC("cushionRPC", RpcTarget.AllBuffered, otherPVID, this.NormalVector);
        }
    }
}
