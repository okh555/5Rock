using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRJoyStick : XRBaseInteractable
{
    XRController selectingController = null;
    public float joyStickInputLimit = 0.6f; // 조이스틱 입력 제한값
    float maxJoyStickAngle = 30f; // 조이스틱 최대 움직이는 각도

    public GameObject joyStick;
    public Transform joyStickAnchor;

    public delegate void JoyStickChange(Vector2 newValue);
    public event JoyStickChange OnJoyStickChange;

    XRBaseInteractor selectingInteractor;

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        if (!interactor)
            return;
        base.OnSelectEnter(interactor);

        selectingInteractor = interactor;
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);

        selectingInteractor = null;
        selectingController = null;
    }


    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
        {
            if (isSelected)
            {
                if (selectingInteractor != null)
                {
                    if (selectingController == null)
                    {
                        selectingController = selectingInteractor.GetComponent<XRController>();
                    }

                    if (selectingController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 value))
                    {
                        Vector2 newValue = value;
                        if (Mathf.Abs(newValue.x) > joyStickInputLimit)
                        {
                            newValue.x = 1f;
                            if (value.x < 0f)
                                newValue.x = -newValue.x;
                        }
                        else
                            newValue.x = 0f;

                        if (Mathf.Abs(newValue.y) > joyStickInputLimit)
                        {
                            newValue.y = 1f;
                            if (value.y < 0f)
                                newValue.y = -newValue.y;
                        }
                        else
                            newValue.y = 0f;

                        if (newValue.x != 0f || newValue.y != 0f)
                            OnJoyStickChange(newValue);


                        float joyStickXAngle = Mathf.LerpAngle(0f, maxJoyStickAngle, Mathf.Abs(value.x));
                        if (value.x < 0f) joyStickXAngle = -joyStickXAngle;

                        float joyStickYAngle = Mathf.LerpAngle(0f, maxJoyStickAngle, Mathf.Abs(value.y));
                        if (value.y < 0f) joyStickYAngle = -joyStickYAngle;

                        joyStick.transform.localRotation = Quaternion.Euler(joyStickYAngle, 0f, -joyStickXAngle);
                    }
                }
            }
        }
    }
}
