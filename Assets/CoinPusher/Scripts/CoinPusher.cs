using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPusher : MonoBehaviour
{
    public GameObject Pusher;
    Rigidbody PusherRigid;
    public float pusherSpeed = 0.2f;
    float pusherMaxDistance = 0.3f;
    bool isPusherForward = false;

    public float pushDelayTime = 0.5f;
    float pushDelay = 0f;
    bool isPushDelay = false;


    public GameObject GrabbableCoinPrefab;
    public Transform CoinExhaustPos;
    int EarnCoinCount = 0;
    bool isCoinExhaust = false;
    public float coinExhaustDelay = 0.2f;

    public TextMesh ExhaustCoinCountText;

    private void Awake()
    {
        PusherRigid = Pusher.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PusherUpdate();
    }

    void PusherUpdate()
    {
        if (isPushDelay)
        {
            if (pushDelay > pushDelayTime)
                isPushDelay = false;

            pushDelay += Time.deltaTime;

            return;
        }

        PusherRigid.MovePosition(PusherRigid.position + PusherDirection() * pusherSpeed / 100f);

        if (isPusherForward)
        {
            if (Pusher.transform.localPosition.z > 0f)
            {
                Pusher.transform.localPosition = Vector3.zero;
                isPusherForward = false;

                isPushDelay = true;
                pushDelay = 0f;
            }
        }
        else
        {
            if (Pusher.transform.localPosition.z < -pusherMaxDistance)
            {
                Pusher.transform.localPosition = new Vector3(0f, 0f, -pusherMaxDistance);
                isPusherForward = true;

                isPushDelay = true;
                pushDelay = 0f;
            }
        }
    }

    Vector3 PusherDirection()
    {
        if (isPusherForward)
        {
            return Pusher.transform.forward;
        }
        else
        {
            return -Pusher.transform.forward;
        }
    }


    public void AddCoin()
    {
        EarnCoinCount++;
        UpdateExhaustCoinCountText();
    }

    public void CoinExhaust()
    {
        if (isCoinExhaust) return;

        isCoinExhaust = true;
        StartCoroutine(ExhaustCoin());
    }

    IEnumerator ExhaustCoin()
    {
        while (EarnCoinCount > 0)
        {
            Instantiate(GrabbableCoinPrefab, CoinExhaustPos.transform.position, CoinExhaustPos.rotation);
            EarnCoinCount--;
            UpdateExhaustCoinCountText();
            yield return new WaitForSeconds(coinExhaustDelay);
        }

        isCoinExhaust = false;
    }

    void UpdateExhaustCoinCountText()
    {
        if (ExhaustCoinCountText)
        {
            int newCoinCount = (EarnCoinCount > 999) ? 999 : EarnCoinCount;
            ExhaustCoinCountText.text = string.Format("{0:000}", newCoinCount);
        }
    }
}
