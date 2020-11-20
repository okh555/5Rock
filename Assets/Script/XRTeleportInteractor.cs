using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRTeleportInteractor : XRRayInteractor
{
    public LayerMask teleportBlockMask;

    public override void GetValidTargets(List<XRBaseInteractable> validTargets)
    {
        base.GetValidTargets(validTargets);

        if (GetCurrentRaycastHit(out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.GetMask("TeleportBlock"))
            {
                validTargets.Clear();
            }
        }


    }
}
