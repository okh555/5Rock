using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabbableObject : XRGrabInteractable, IPunObservable
{
    bool isSelectExit = false;
    float selectRecognitionTimeVal = 0f;
    public float SelectRecognitionTime = 0.1f;

    bool canSelect = false;

    public bool isNotGrabbableOnStart = false;
    LayerMask DefaultLayerMask;

    /// <summary>
    
    private PhotonView pv;
    public Transform leftHandRig;
    public Transform rightHandRig;

    protected Vector3 curPos;
    protected Quaternion curRot;

    protected bool leftUse;
    protected bool rightUse;

    private void Start()
    {
        if (SelectRecognitionTime < 0f)
        {
            SelectRecognitionTime = 0f;
        }

        DefaultLayerMask = interactionLayerMask;
        if (isNotGrabbableOnStart)
            interactionLayerMask = 0;

        pv = GetComponent<PhotonView>();
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);

        canSelect = true;
        isSelectExit = false;
        selectRecognitionTimeVal = 0f;

        if(pv)
        {
            pv.RequestOwnership();
        }
        
        if(interactor.name.Contains("Left"))
        {
            leftUse = true;
        }

        if (interactor.name.Contains("Right"))
        {
            rightUse = true;
        }
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);

        isSelectExit = true;

        if (interactor.name.Contains("Left"))
        {
            leftUse = false;
        }

        if (interactor.name.Contains("Right"))
        {
            rightUse = false;
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

        if(leftUse)
        {
            this.transform.position = leftHandRig.transform.position;
            this.transform.rotation = leftHandRig.transform.rotation;
        }

        if(rightUse)
        {
            this.transform.position = rightHandRig.transform.position;
            this.transform.rotation = rightHandRig.transform.rotation;
        }
        
    }

    public bool CanSocketed()
    {
        return canSelect;
    }


    public void ChangeToGrabbable(bool setGrabbable)
    {
        if (setGrabbable)
            interactionLayerMask = DefaultLayerMask;
        else
            interactionLayerMask = 0;
    }
   
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if(stream.IsWriting)
        //{
        //    Vector3 t = new Vector3(Mathf.Round(this.transform.position.x * 10) / 10.0f,
        //        Mathf.Round(this.transform.position.y * 10) / 10.0f,
        //        Mathf.Round(this.transform.position.z * 10) / 10.0f);
               
        //    Quaternion q = new Quaternion(Mathf.Round(this.transform.rotation.x * 10) / 10.0f,
        //        Mathf.Round(this.transform.rotation.y * 10) / 10.0f,
        //        Mathf.Round(this.transform.rotation.z * 10) / 10.0f,
        //        Mathf.Round(this.transform.rotation.w * 10) / 10.0f);
            
        //    stream.SendNext(this.transform.position);
        //    stream.SendNext(this.transform.rotation);
        //}
        //else
        //{
        //    this.transform.position = (Vector3)stream.ReceiveNext();
        //    this.transform.rotation = (Quaternion)stream.ReceiveNext();
        //}
    }
}