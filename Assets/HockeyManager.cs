﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class HockeyManager : MonoBehaviour
{
    public GameObject Puck;
    public GameObject HockeyStriker;

    public Transform puckPos;
    public Transform player1Pos;
    public Transform player2Pos;

    TMP_Text ScoreBoard;
    int player1_score = 0;
    int player2_score = 0;
    bool scoreChange = false;

    GameObject player1;
    GameObject player2;

    bool start = false;

    private PhotonView pv;

    // Start is called before the first frame update
    void Awake()
    {
     //   Reset();
        ScoreBoard = GetComponentInChildren<TMP_Text>();
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreBoard.SetText(player1_score + " : " + player2_score);
        if(pv)
            pv.RPC("scoreSetText", RpcTarget.AllBuffered, player1_score, player2_score);

        if(start == true)
        {
            //Reset();
            pv.RPC("Reset", RpcTarget.MasterClient);
            start = false;
        }
    }

    [PunRPC]
    public void Score(bool player)
    {
        pv.RPC("Reset", RpcTarget.MasterClient);
        //Reset();

        if (player)
        {
            player1_score++;
        }
        else
        {
            player2_score++;
        }
    }

    [PunRPC]
    public void scoreSetText(int score1, int score2)
    {
        ScoreBoard.SetText(score1 + " : " + score2);
    }


    [PunRPC]
    void Reset()
    {
        GameObject puck = PhotonNetwork.Instantiate("AirHockeyPuck", puckPos.position, Puck.transform.rotation);
        puck.transform.parent = gameObject.transform;
        if (player2 == null)
        {
            player2 = PhotonNetwork.Instantiate("StrikerMain", player2Pos.position, HockeyStriker.transform.rotation);
            player2.transform.parent = gameObject.transform;
        }
        if (player1 == null)
        {
            player1 = PhotonNetwork.Instantiate("StrikerMain", player1Pos.position, HockeyStriker.transform.rotation);
            player1.transform.parent = gameObject.transform;
        }
        if (Random.Range(0,2)  == 0)
        {
            puck.GetComponent<Rigidbody>().AddForce(new Vector3(20f, 0, 0));
        }
        else
        {
            puck.GetComponent<Rigidbody>().AddForce(new Vector3(-20f, 0, 0));
        }
    }
    
    public void _Start()
    {
        //start = true;
        pv.RPC("gameStart", RpcTarget.MasterClient);
    }

    [PunRPC]
    void gameStart() => start = true;
}

