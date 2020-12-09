using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Puck : MonoBehaviour, IPunObservable
{
    public float maxSpeed = 50f;

    Vector3 currentPos;
    Rigidbody rig;

    float slipperyTime = 2f;
    float decreaseTime = 0.01f;

    float collisionTime = 0;
    float decreaseTempTime = 0;

    HockeyManager hockeyManager;

    private PhotonView pv;
    private PhotonView hockeyPhotonView;

    Vector3 syncV;

    // Start is called before the first frame update
    void Start()
    {
        hockeyManager = GetComponentInParent<HockeyManager>();
        currentPos = GetComponent<Transform>().position;
        rig = GetComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Collider>().material.dynamicFriction = 0;

        pv = GetComponent<PhotonView>();
        hockeyPhotonView = hockeyManager.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (rig.velocity.magnitude > maxSpeed)
        //{
        //    rig.velocity = rig.velocity.normalized * maxSpeed;
        //}

        if (transform.position.y != currentPos.y)
        {
            transform.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
        }

        //if (Time.time - collisionTime > slipperyTime)
        //{
        //    if (Time.time - decreaseTempTime > decreaseTime)
        //    {
        //        rig.velocity = new Vector3(rig.velocity.x * 0.99f, 0, rig.velocity.z * 0.99f);
        //        decreaseTime = Time.time;
        //    }
        //}
        pv.RPC("synchronizedVelocity", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void synchronizedVelocity()
    {
        if (rig.velocity.magnitude > maxSpeed)
        {
            rig.velocity = rig.velocity.normalized * maxSpeed;
        }

        if (Time.time - collisionTime > slipperyTime)
        {
            if (Time.time - decreaseTempTime > decreaseTime)
            {
                rig.velocity = new Vector3(rig.velocity.x * 0.99f, 0, rig.velocity.z * 0.99f);
                decreaseTime = Time.time;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Wall")
        {
            if(collider.gameObject.name == "Wall1")
            {
                rig.velocity = new Vector3(rig.velocity.x, 0, -rig.velocity.z);
                rig.velocity *= 0.8f;
            }
            if (collider.gameObject.name == "Wall2")
            {
                rig.velocity = new Vector3(-rig.velocity.x * 0.8f, 0, rig.velocity.z * 0.8f);
                rig.velocity *= 0.8f;
            }
            if (collider.gameObject.name == "Score")
            {
                //hockeyManager.Score(true);
                //Destroy(this.gameObject);
                hockeyPhotonView.RPC("Score", RpcTarget.MasterClient, false);
                pv.RPC("destroyObject", RpcTarget.MasterClient);
            }
            if (collider.gameObject.name == "Score2")
            {
                //hockeyManager.Score(false);
                //Destroy(this.gameObject);
                hockeyPhotonView.RPC("Score", RpcTarget.MasterClient, true);
                pv.RPC("destroyObject", RpcTarget.MasterClient);
            }
            if (collider.gameObject.tag != "HockeyGround")
            {
                collisionTime = Time.time;
            }
        }
    }

    [PunRPC]
    void destroyObject() => PhotonNetwork.Destroy(this.gameObject);

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (PhotonNetwork.IsMasterClient)
                stream.SendNext(rig.velocity);
        }
        else
        {
            rig.velocity = (Vector3)stream.ReceiveNext();
        }
    }
}
