using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HockeyStriker : XRGrabableObject
{

    public Vector3 currentPos;
    public Quaternion currentRot;

    Rigidbody rig;
   

    void Start()
    {
        currentPos = GetComponent<Transform>().position;
        currentRot = GetComponent<Transform>().rotation;
        rig = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        transform.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
        transform.rotation = new Quaternion(currentRot.x, currentRot.y, currentRot.z, currentRot.w);
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            if (collider.gameObject.name == "Wall1")
            {
                rig.velocity = new Vector3(rig.velocity.x, 0, -rig.velocity.z);
            }
            else
            {
                rig.velocity = new Vector3(-rig.velocity.x, 0, rig.velocity.z);
            }
        }
    }

}
