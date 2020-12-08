﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabableObject : XRGrabInteractable, IPunObservable
{
    bool isSelectExit = false;
    float selectRecognitionTimeVal = 0f;
    public float SelectRecognitionTime = 0.1f;

    bool canSelect = false;

    private PhotonView pv;

    public GameObject parentObject;

    private void Start()
    {
        if (SelectRecognitionTime < 0f)
        {
            SelectRecognitionTime = 0f;
        }
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);

        //canSelect = true;
        //isSelectExit = false;
        //selectRecognitionTimeVal = 0f;

        if(!pv)
            pv = GetComponent<PhotonView>();

        if (pv)
        {
            pv.RequestOwnership();
            pv.RPC("selectObject", RpcTarget.AllBuffered);
        }
    }
   
    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);

        //isSelectExit = true;

        if(pv)
        {
            pv.RPC("unSelectObject", RpcTarget.AllBuffered);
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Late)
        {
            if (isSelectExit && canSelect)
            {
                if (SelectRecognitionTime < selectRecognitionTimeVal)
                {
                    isSelectExit = false;
                    canSelect = false;
                    return;
                }
                selectRecognitionTimeVal += Time.deltaTime;
            }
        }
    }

    public bool CanSocketed()
    {
        return canSelect;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (transform.parent)
            {
                stream.SendNext(transform.localPosition);
            }
            else
            {
                stream.SendNext(transform.position);
            }
        }
        else
        {
            if (transform.parent)
            {
                transform.localPosition = (Vector3)stream.ReceiveNext();
            }
            else
            {
                transform.position = (Vector3)stream.ReceiveNext();
            }
        }
    }

    public void selectObject()
    {
        this.transform.parent = null;

        canSelect = true;
        isSelectExit = false;
        selectRecognitionTimeVal = 0f;
    }

    public void unSelectObject()
    {
        this.transform.parent = parentObject.transform;

        isSelectExit = true;
    }
}