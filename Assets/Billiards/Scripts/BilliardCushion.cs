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
        PhotonView otherPV = other.GetComponent<PhotonView>();
        if (otherPV)
        {
            if (otherPV.name.Contains("Ball"))
            {
                int otherPVID = otherPV.ViewID;
                pv.RPC("cushionRPC", RpcTarget.AllBuffered, otherPVID, this.NormalVector);
            }
        }
    }
}
