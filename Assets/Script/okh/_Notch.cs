using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class _Notch : XRSocketInteractor
{
    private _Puller puller;
    private _Arrow currentArrow;

    private float waitTime = 0.05f;
    private float releaseTime = 0;


    void Update()
    {
        if (releaseTime != 0)
        {
            if (Time.time - releaseTime > waitTime)
            {
                socketActive = true;
                releaseTime = 0;
            }
        }
    }


    protected override void Awake()
    {
        base.Awake();
        puller = GetComponent<_Puller>();
    }

    protected override void OnEnable()
    {

        base.OnEnable();
        puller.onSelectExit.AddListener(TryToReleaseArrow);
        Debug.Log("Enable");

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        puller.onSelectExit.RemoveListener(TryToReleaseArrow);
        Debug.Log("Disable");

    }

    protected override void OnSelectEnter(XRBaseInteractable interactable)
    {
        base.OnSelectEnter(interactable);
        StoreArrow(interactable);
    }

    private void StoreArrow(XRBaseInteractable interactable)
    {
        if(interactable is _Arrow arrow)
        {
            currentArrow = arrow;
        }
    }

    private void TryToReleaseArrow(XRBaseInteractor interactor)
    {
        if(currentArrow)
        {
            ForceDeselect();
            ReleaseArrow();
            releaseTime = Time.time;
            socketActive = false;
        }
    }

    private void ForceDeselect()
    {
        base.OnSelectExit(currentArrow);
        currentArrow.OnSelectExit(this);
    }

    private void ReleaseArrow()
    {
        currentArrow.Release(puller.PullAmount);
        Debug.Log("Release");
        currentArrow = null;
    }

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride
    {
        get { return XRBaseInteractable.MovementType.Instantaneous; }
    }
}
