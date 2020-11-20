using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class _Puller : XRBaseInteractable
{

    public float PullAmount { get; private set; } = 0.0f;

    public Transform end;
    public Transform start;

    private XRBaseInteractor pullingInteractor;

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        pullingInteractor = interactor;
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
        interactor = null;
        PullAmount = 0;

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPostion = pullingInteractor.transform.position;
                PullAmount = CalculatePull(pullPostion);
            }
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }
}
