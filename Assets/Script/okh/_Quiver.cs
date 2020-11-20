using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class _Quiver : XRSocketInteractor
{
    public GameObject arrowPerfab = null;
    private Vector3 attachOffset = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        CreateAndSelectArrow();
        SetAttachOffset();
    }

    protected override void OnSelectExit(XRBaseInteractable interactable)
    {
        base.OnSelectExit(interactable);
        CreateAndSelectArrow();
    }

    private void CreateAndSelectArrow()
    {
        _Arrow arrow = CreateArrow();
        SelectArrow(arrow);
    }

    private _Arrow CreateArrow()
    {
        GameObject arrowObject = Instantiate(arrowPerfab, transform.position - attachOffset, transform.rotation);
        return arrowObject.GetComponent<_Arrow>();
    }

    private void SelectArrow(_Arrow arrow)
    {
        OnSelectEnter(arrow);
        arrow.OnSelectEnter(this);

    }

    private void SetAttachOffset()
    {
        if (selectTarget is XRGrabInteractable interactable)
            attachOffset = interactable.attachTransform.localPosition;
    }
}
