using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementProvider : LocomotionProvider
{
    public List<XRController> controllers = null;

    private CharacterController CharacterController = null;
    private GameObject head = null;
 /*
    protected override void Awake()
    {
       /* CharacterController = GetComponent<CharacterController>();
        head = GetComponent<XRRig>().cameraGameObject;
    }
*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
