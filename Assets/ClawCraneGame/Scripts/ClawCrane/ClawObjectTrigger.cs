using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawObjectTrigger : MonoBehaviour
{
    public delegate void ObjectEnter();
    public event ObjectEnter OnObjectEnter;

    private void OnTriggerEnter(Collider other)
    {
        OnObjectEnter();
    }
}
