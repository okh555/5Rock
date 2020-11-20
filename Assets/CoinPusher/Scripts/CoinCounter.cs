using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCounter : MonoBehaviour
{
    [System.Serializable]
    public class CoinCountEvent : UnityEvent<CoinCounter> { }
    public CoinCountEvent OnCoinCount;

    const string CoinTag = "Coin";


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == CoinTag)
        {
            Destroy(other.gameObject);
            OnCoinCount.Invoke(this);
        }
    }
}
