using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRTwoHandedGrabbable : XRGrabbableObject
{
    private XRSecondGrab secondGrab;

    private XRBaseInteractor secondHand;

    protected override void Awake()
    {
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
        
        ChangeToGrabbable(!isActive);
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
                transform.LookAt(secondHand.transform);
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
