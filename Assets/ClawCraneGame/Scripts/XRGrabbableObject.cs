﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabbableObject : XRGrabInteractable
{
    bool isSelectExit = false;
    float selectRecognitionTimeVal = 0f;
    public float SelectRecognitionTime = 0.1f;

    bool canSelect = false;

    public bool isNotGrabbableOnStart = false;
    LayerMask DefaultLayerMask;

    private void Start()
    {
        if (SelectRecognitionTime < 0f)
        {
            SelectRecognitionTime = 0f;
        }

        DefaultLayerMask = interactionLayerMask;
        if (isNotGrabbableOnStart)
            interactionLayerMask = 0;
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);

        canSelect = true;
        isSelectExit = false;
        selectRecognitionTimeVal = 0f;
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);

        isSelectExit = true;
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


    public void ChangeToGrabbable()
    {
        interactionLayerMask = DefaultLayerMask;
    }
}