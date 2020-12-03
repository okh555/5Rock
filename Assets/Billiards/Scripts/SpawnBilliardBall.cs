using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnBilliardBall : MonoBehaviour
{
    public GameObject balls;
    public Transform spawnPosition;

    private GameObject spawnedBalls;

    public float CushionFriction = 0.2F;

    public bool isInstantiate = false;

    void Start()
    {
        //SpawnBall();
    }

    public void SpawnBall()
    {
        if (spawnedBalls != null)
        {
            foreach(PhotonView pv in spawnedBalls.GetComponentsInChildren<PhotonView>())
            {
                pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
            }
            GetComponent<PhotonView>().RPC("isSpawning", RpcTarget.AllBuffered);
        }

        if (!isInstantiate)
        {
            spawnedBalls = PhotonNetwork.Instantiate("8Balls", spawnPosition.position, Quaternion.Euler(0, 0, 0));
            spawnedBalls.transform.position = spawnPosition.position;
            GetComponent<PhotonView>().RPC("isSpawning", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void isSpawning() => isInstantiate = !isInstantiate;

    [PunRPC]
    void cushionRPC(int otherPVID, Vector3 normal)
    {
        Collider other = PhotonNetwork.GetPhotonView(otherPVID).GetComponent<Collider>();
        other.attachedRigidbody.velocity = Vector3.Reflect(other.attachedRigidbody.velocity, normal) * CushionFriction;
        other.attachedRigidbody.angularVelocity = Vector3.Reflect(other.attachedRigidbody.angularVelocity, normal) * CushionFriction;
    }
}
