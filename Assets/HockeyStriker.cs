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

        transform.position = new Vector3(transform.position.x, currentPos.position.y, transform.position.z);
            
      
    }

}
