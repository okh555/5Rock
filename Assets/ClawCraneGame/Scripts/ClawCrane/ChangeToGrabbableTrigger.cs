using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToGrabbableTrigger : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        XRGrabbableObject grabbableObject = other.gameObject.GetComponent<XRGrabbableObject>();
        if (grabbableObject)
        {
            grabbableObject.ChangeToGrabbable(true);
        }
    }
}
