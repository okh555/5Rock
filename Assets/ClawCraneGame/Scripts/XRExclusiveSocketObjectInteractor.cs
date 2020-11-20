using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Subclass of the classic Socket Interactor from the Interaction toolkit that will only accept object with the right
/// SocketTarget 
/// </summary>
public class XRExclusiveSocketObjectInteractor : XRSocketInteractor
{
    public string AcceptedType;

    // 소켓에 부착할때 손으로 집은 물체만 부착될지
    public bool isAttachedByOnlyGrab;

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        SocketTargetObject socketTarget = interactable.GetComponent<SocketTargetObject>();

        if (socketTarget == null)
            return false;

        XRGrabbableObject grabInteractable = (interactable as XRGrabbableObject);
        if (grabInteractable == null)
            return false;

        return base.CanSelect(interactable) && (socketTarget.SocketType == AcceptedType) && ((isAttachedByOnlyGrab) ? grabInteractable.CanSocketed() : true);
    }

    public override bool CanHover(XRBaseInteractable interactable)
    {
        return CanSelect(interactable);
    }
}
