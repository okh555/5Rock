using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangeToGrabbableTrigger : MonoBehaviour, IPunObservable
{
    private bool isUsed = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) stream.SendNext(isUsed);
        else isUsed = (bool)stream.ReceiveNext();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isUsed)
        {
            XRGrabbableObject grabbableObject = other.gameObject.GetComponent<XRGrabbableObject>();
            if (grabbableObject)
            {
                grabbableObject.ChangeToGrabbable(true);
                isUsed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isUsed = false;
    }
}
