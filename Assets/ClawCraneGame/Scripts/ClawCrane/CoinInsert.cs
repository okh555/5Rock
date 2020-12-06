using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class CoinInsert : XRBaseInteractor
{
    [System.Serializable]
    public class CoinInsertEvent : UnityEvent<CoinInsert> { }
    public CoinInsertEvent OnCoinInsert;

    public string CoinType;

    int CoinCount = 0;
    public float CoinInsertDelay;

    GameObject Coin = null;
    public Vector3 RemovePosition;

    XRBaseInteractable triggerdInteractable;

    protected override void OnSelectEnter(XRBaseInteractable interactable)
    {
        base.OnSelectEnter(interactable);

        if (Coin == null)
        {
            AddCoin(interactable.gameObject);
        }
    }

    void AddCoin(GameObject coin)
    {
        Coin = coin;
        CoinCount++;

        if (OnCoinInsert.GetPersistentEventCount() > 0)
        {
            UseCoin();
        }
        
        StartCoroutine(CoinInsertDelayed());
    }

    public bool UseCoin()
    {
        if (CoinCount > 0)
        {
            OnCoinInsert.Invoke(this);
            CoinCount--;
            return true;
        }

        return false;
    }

    IEnumerator CoinInsertDelayed()
    {
        yield return new WaitForSeconds(CoinInsertDelay);
        if (Coin != null)
        {
            Destroy(Coin);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerdInteractable == null)
            triggerdInteractable = other.GetComponentInParent<XRBaseInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        XRBaseInteractable newInteractable = other.GetComponentInParent<XRBaseInteractable>();
        if (newInteractable && newInteractable == triggerdInteractable)
            triggerdInteractable = null;
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        SocketTargetObject socketTarget = interactable.GetComponent<SocketTargetObject>();
        if (socketTarget == null)
            return false;

        XRGrabbableObject grabInteractable = (interactable as XRGrabbableObject);
        if (grabInteractable == null)
            return false;

        return base.CanSelect(interactable) && socketTarget.SocketType == CoinType && grabInteractable.CanSocketed() && interactable.isSelected == false && Coin == null;
    }

    public override void GetValidTargets(List<XRBaseInteractable> validTargets)
    {
        validTargets.Clear();

        if (triggerdInteractable != null && selectTarget != triggerdInteractable)
            validTargets.Add(triggerdInteractable);
    }
}
