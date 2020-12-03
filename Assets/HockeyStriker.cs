using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HockeyStriker : XRGrabableObject
{

    public Vector3 currentPos;
    public Quaternion currentRot;

    Vector3 PrevPos;
    Vector3 NewPos;
    Vector3 ObjVelocity;

    Rigidbody rig;
   

    void Start()
    {
        currentPos = GetComponent<Transform>().position;
        currentRot = GetComponent<Transform>().rotation;
        rig = GetComponent<Rigidbody>();

        PrevPos = transform.position;
        NewPos = transform.position;
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
       // transform.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
        //transform.rotation = new Quaternion(currentRot.x, currentRot.y, currentRot.z, currentRot.w);

        NewPos = transform.position;  // each frame track the new position
        ObjVelocity = (NewPos - PrevPos) / Time.fixedDeltaTime;  // velocity = dist/time
        PrevPos = NewPos;  // update position for next frame calculation


    }

    void OnTriggerEnter(Collider collider)
    {
        if (rig.velocity.magnitude < .01)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
        }

        if (collider.gameObject.tag == "Wall")
        {
            if (collider.gameObject.name == "Wall1")
            {
                Debug.Log(rig.velocity);
                rig.velocity = new Vector3(rig.velocity.x, 0, -rig.velocity.z);
                rig.velocity *= 0.8f;
            }
            if (collider.gameObject.name == "Wall2")
            {
                Debug.Log(rig.velocity);
                rig.velocity = new Vector3(-rig.velocity.x, 0, rig.velocity.z);
                rig.velocity *= 0.8f;
            }
            if (collider.gameObject.name == "Score")
            {
                rig.velocity = new Vector3(-rig.velocity.x, 0, rig.velocity.z);
                rig.velocity *= 0.8f;
            }
            if (collider.gameObject.name == "Score2")
            {
                rig.velocity = new Vector3(-rig.velocity.x, 0, rig.velocity.z);
                rig.velocity *= 0.8f;
            }
        }
    }

}
