using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCapsule : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void RemoveCraneCapsule()
    {
        meshRenderer.enabled = false;
        sphereCollider.enabled = false;
    }
}
