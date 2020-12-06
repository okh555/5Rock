using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRTwoHandedGrabbable : XRGrabbableObject
{
    private XRSecondGrab secondGrab;
    public Collider FirstGrabCollider;

    private XRBaseInteractor secondHand;
    private Vector3 vec;

    protected override void Awake()
    {
        base.Awake();

        secondGrab = GetComponentInChildren<XRSecondGrab>();
        if (secondGrab == null)
        {
            Debug.LogError("XRSecondGrab Not Exist!!");
        }
    }

    private void Start()
    {
        secondGrab.onSelectEnter.AddListener(SetSecondHandToTarget);
        secondGrab.onSelectExit.AddListener(UnSetSecondHandToTarget);
    }


    public void ActiveSecondGrab(bool isActive)
    {
        secondGrab.SetGrabActive(isActive);
        

        FirstGrabCollider.enabled=!isActive;
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);

        ActiveSecondGrab(true);
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);

        ActiveSecondGrab(false);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (secondGrab != null && secondHand != null)
            {
                transform.LookAt(secondHand.transform, Vector3.right);

                transform.position = selectingInteractor.transform.position;
                
            }
        }

        if(secondHand == null)
        {
            if (leftUse)
            {
                this.transform.position = leftHandRig.transform.position;
                this.transform.rotation = leftHandRig.transform.rotation;
            }

            if (rightUse)
            {
                this.transform.position = rightHandRig.transform.position;
                this.transform.rotation = rightHandRig.transform.rotation;
            }
        }
    }


    void SetSecondHandToTarget(XRBaseInteractor interactor)
    {
        secondHand = interactor;
    }

    void UnSetSecondHandToTarget(XRBaseInteractor interactor)
    {
        if (secondHand == interactor)
            secondHand = null;
    }

}
