using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CoinShooter : MonoBehaviour, IPunObservable
{
    public GameObject CoinPrefab;
    public Transform FirePoint;

    float shootSpeed = 3.5f;
    float shootRandomSpeed = 0.1f;
    float shootRandomTorqueValue = 10f;

    int coinCount = 0;
    public int coinAddChargeSize = 5;
    
    public TMP_Text CoinCountText;

    const float MaxRotateAngle = 15f;


    public void AddCoin()
    {
        coinCount += coinAddChargeSize;
        UpdateCoinCountText();
    }

    public void ShootCoin()
    {
        if (coinCount > 0)
        {
            GameObject coin = Instantiate(CoinPrefab, FirePoint.position, FirePoint.rotation);
            Rigidbody rigid = coin.GetComponent<Rigidbody>();
            float randomSpeed = Random.Range(-shootRandomSpeed, shootRandomSpeed);
            rigid.velocity = FirePoint.transform.forward * (shootSpeed + randomSpeed);
            Vector3 randomTorque = new Vector3(Random.value, Random.value, Random.value);
            rigid.AddTorque(randomTorque * shootRandomTorqueValue);

            coinCount--;
            UpdateCoinCountText();
        }
    }


    public void ChangeRotation(float newAngle)
    {
        transform.localRotation = Quaternion.Euler(-135f, -MaxRotateAngle + (newAngle / 2), 0f);
    }

    void UpdateCoinCountText()
    {
        if (CoinCountText)
        {
            int newCoinCount = (coinCount > 99) ? 99 : coinCount;
            CoinCountText.text = string.Format("{0:00}", newCoinCount);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(CoinCountText.text);
        }
        else
        {
            CoinCountText.text = (string)stream.ReceiveNext();
        }
    }
}
