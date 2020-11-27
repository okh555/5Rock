using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class _Arrow : XRGrabInteractable
{
    public float speed = 100.0f;
    public Transform tip = null;

    private bool inAir = false;
    private Vector3 lastPosition = Vector3.zero;

    private Rigidbody rigidbody =null;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(inAir)
        {
            CheckForCollision();
            lastPosition = tip.position;
        }
    }

    private void CheckForCollision()
    {
        if (Physics.Linecast(lastPosition, tip.position))
        {
            Stop();
        }
    }

    private void Stop()
    {
        inAir = false;
        SetPhysics(false);
    }

    public void Release(float pullValue)
    {
        inAir = true;
        SetPhysics(true);

        MaskAndFire(pullValue);
        StartCoroutine(RotateWithVelocity());

        lastPosition = tip.position;
    }

    private void SetPhysics(bool usePhysics)
    {
        rigidbody.isKinematic = !usePhysics;
        rigidbody.useGravity = usePhysics;

    }

    private void MaskAndFire(float power)
    {
        colliders[0].enabled = false;
        interactionLayerMask = 1 << LayerMask.NameToLayer("Ignore");

        Vector3 force = transform.forward * (speed * power);
        rigidbody.AddForce(force);

    }

    private IEnumerator RotateWithVelocity()
    {

        yield return new WaitForFixedUpdate();

        while(inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    public new void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
    }

    public new void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
    }
}
