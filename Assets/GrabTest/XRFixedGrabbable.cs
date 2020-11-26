using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum Axis
{
    X, Y, Z
}

public class XRFixedGrabbable : XRGrabbableObject
{
    public Axis axis;
    public float FixPosition;

    private Rigidbody rigid;

    protected override void Awake()
    {
        base.Awake();

        rigid = GetComponent<Rigidbody>();
    }


    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            Vector3 pos = transform.position;
            switch (axis)
            {
                case Axis.X:
                    pos.x = FixPosition;
                    break;
                case Axis.Y:
                    pos.y = FixPosition;
                    break;
                case Axis.Z:
                    pos.z = FixPosition;
                    break;
            }

            transform.position = pos;

            Quaternion angle = transform.rotation;
            switch (axis)
            {
                case Axis.X:
                    angle.eulerAngles = new Vector3(angle.eulerAngles.x, 90, 90);
                    break;
                case Axis.Y:
                    angle.eulerAngles = new Vector3(90, angle.eulerAngles.y, 90);
                    break;
                case Axis.Z:
                    angle.eulerAngles = new Vector3(90, 90, angle.eulerAngles.z);
                    break;
            }

            transform.rotation = angle;
        }
    }
}
