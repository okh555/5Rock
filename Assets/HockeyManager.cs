using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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

    GameObject player1;
    GameObject player2;

    bool start = false;


    // Start is called before the first frame update
    void Awake()
    {
     //   Reset();
        ScoreBoard = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreBoard.SetText(player1_score + " : " + player2_score);
        if(start == true)
        {
            Reset();
            start = false;
        }
    }

    public void Score(bool player)
    {
        Reset();
        if(player)
        {
            player1_score++;
        }
        else
        {
            player2_score++;
        }
    }

    void Reset()
    {
        GameObject puck = Instantiate(Puck, puckPos.position, Puck.transform.rotation);
        puck.transform.parent = gameObject.transform;
        if (player2 == null)
        {
            player2 = Instantiate(HockeyStriker, player2Pos.position, HockeyStriker.transform.rotation);
            player2.transform.parent = gameObject.transform;
        }
        if (player1 == null)
        {
            player1 = Instantiate(HockeyStriker, player1Pos.position, HockeyStriker.transform.rotation);
            player1.transform.parent = gameObject.transform;
        }
        if (Random.Range(0,1)  == 0)
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
        start = true;
    }

}

