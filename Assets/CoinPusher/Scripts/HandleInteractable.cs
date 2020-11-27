using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class HandleInteractable : XRBaseInteractable, IPunObservable
{
    [System.Serializable]
    public class HandleActiveEvent : UnityEvent<HandleInteractable> { }
    public HandleActiveEvent OnHandleActive;

    public enum HandleRotateAxis
    {
        X, Y, Z
    }
    public HandleRotateAxis RotateAxis = HandleRotateAxis.Z;

    XRBaseInteractor grabbingInteractor;
    Vector3 interactorFIrstPosition;
    Vector3 lastPosition;

    public float handleRotateSpeed = 120f;
    float handleMinDistance = 0.005f;
    float totalChangedAngle;
    public float handleActiveEventOccurValue = 360f;

    private bool isUsed = false;

    Vector3 GetRotationAxis()
    {
        switch (RotateAxis)
        {
            case HandleRotateAxis.X:
                return Vector3.right;
            case HandleRotateAxis.Y:
                return Vector3.up;
            case HandleRotateAxis.Z:
                return Vector3.forward;
        }
        return Vector3.forward;
    }

    Vector3 GetAngleAxis()
    {
        switch (RotateAxis)
        {
            case HandleRotateAxis.X:
                return transform.right;
            case HandleRotateAxis.Y:
                return transform.up;
            case HandleRotateAxis.Z:
                return transform.forward;
        }
        return transform.forward;
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        if (!isUsed)
        {
            grabbingInteractor = interactor;
            interactorFIrstPosition = interactor.transform.position;

            totalChangedAngle = 0f;

            base.OnSelectEnter(interactor);

            isUsed = true;
        }
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {

        base.OnSelectExit(interactor);
        isUsed = false;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (isSelected)
        {
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
            {
                Vector3 interactorDirection = grabbingInteractor.transform.position - interactorFIrstPosition;

                float angle = Vector3.SignedAngle(lastPosition, interactorDirection, GetAngleAxis());
                
                if (handleMinDistance < Vector3.Distance(interactorDirection, Vector3.Project(interactorDirection, GetAngleAxis())))
                {
                    transform.Rotate(GetRotationAxis(), angle * handleRotateSpeed / 100f);

                    if (RotateAxis != HandleRotateAxis.Z) angle = -angle;
                    if (angle > 0f)
                    {
                        totalChangedAngle += angle;
                    }

                    if (totalChangedAngle > handleActiveEventOccurValue)
                    {
                        totalChangedAngle -= handleActiveEventOccurValue;
                        OnHandleActive.Invoke(this);
                    }
                }

                lastPosition = interactorDirection;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(isUsed);
        }
        else
        {
            isUsed = (bool)stream.ReceiveNext();
        }
    }
}
