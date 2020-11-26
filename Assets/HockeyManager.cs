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


    // Start is called before the first frame update
    void Awake()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Score(bool player)
    {
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
        Instantiate(Puck, puckPos.position, Puck.transform.rotation);
        Instantiate(HockeyStriker, player2Pos.position, HockeyStriker.transform.rotation);
        Instantiate(HockeyStriker, player1Pos.position, HockeyStriker.transform.rotation);
    }

}

