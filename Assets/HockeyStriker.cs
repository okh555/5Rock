using UnityEngine;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HockeyStriker : XRGrabableObject
{

    public Transform currentPos;
   

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        switch (updatePhase)
        {
            case XRInteractionUpdateOrder.UpdatePhase.Dynamic:
                {
                    if (isSelected)
                    {
                        transform.position = new Vector3(transform.position.x, currentPos.position.y, transform.position.z);
                    }
                }
                break;
            case XRInteractionUpdateOrder.UpdatePhase.OnBeforeRender:
                {
                    if (isSelected)
                    {
                        transform.position = new Vector3(transform.position.x, currentPos.position.y, transform.position.z);
                    }
                }
                break;
            case XRInteractionUpdateOrder.UpdatePhase.Late:
                {
                    if (isSelected)
                    {
                        Debug.Log("3");
                    }
                }
                break;
        }
      
    }
    }
