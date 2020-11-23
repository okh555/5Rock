using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private XRRig xrRig;
    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    private XRController leftController;
    private XRController rightController;

    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        xrRig = FindObjectOfType<XRRig>();

        headRig = xrRig.transform.Find("Camera Offset/Main Camera");
        leftHandRig = xrRig.transform.Find("Camera Offset/LeftHand Controller");
        rightHandRig = xrRig.transform.Find("Camera Offset/RightHand Controller");

        leftController = leftHandRig.GetComponent<XRController>();
        rightController = rightHandRig.GetComponent<XRController>();

        if (photonView.IsMine)
        {
            foreach(var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            MapPosition(this.transform, xrRig.transform);
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

            // animation
            UpdateHandAnimation(leftController, leftHandAnimator);
            UpdateHandAnimation(rightController, rightHandAnimator);
        }
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }

    private void UpdateHandAnimation(XRController targetDevice, Animator handAnimator)
    {
<<<<<<< Updated upstream
        //if (targetDevice.selectInteractionState.activatedThisFrame)
        //    handAnimator.SetTrigger(targetDevice.modelSelectTransition);
        //else if (targetDevice.selectInteractionState.deActivatedThisFrame)
        //    handAnimator.SetTrigger(targetDevice.modelDeSelectTransition);  
=======
>>>>>>> Stashed changes
       /* if (targetDevice.selectInteractionState.activatedThisFrame)
            handAnimator.SetTrigger(targetDevice.modelSelectTransition);
        else if (targetDevice.selectInteractionState.deActivatedThisFrame)
            handAnimator.SetTrigger(targetDevice.modelDeSelectTransition);  */
    }
}
