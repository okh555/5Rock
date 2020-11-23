using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSecondGrab : XRBaseInteractable
{
    private LayerMask defaultLayerMask;

    private XRBaseInteractor secondHand;

    private void Start()
    {
        defaultLayerMask = interactionLayerMask;

        interactionLayerMask = 0;
    }

    public void SetGrabActive(bool isActive)
    {
        if (isActive)
        {
            interactionLayerMask = defaultLayerMask;
        }
        else
        {
            if (isSelected && secondHand != null)
            {
                OnSelectExit(secondHand);
            }
            interactionLayerMask = 0;
        }
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);

        secondHand = interactor;
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);

        secondHand = null;
    }
}
