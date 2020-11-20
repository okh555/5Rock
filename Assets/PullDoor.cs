using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullDoor : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public Transform player;
    public float playerDistance = 3f;
    public float angleSpeed = 0.7f;

    public Quaternion l_OpenRotation;
    public Quaternion r_OpenRotation;
    private Quaternion l_ClosedRotation;
    private Quaternion r_ClosedRotation;

    private bool open = false;
    private bool close = false;
    private bool check = false;


    void Start()
    {
        l_ClosedRotation = leftDoor.rotation;
        r_ClosedRotation = rightDoor.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(player.position, transform.position) < playerDistance && check)
            OpenDoor();
        else
            CloseDoor();

        float step = angleSpeed * Time.deltaTime;

        if (open)
        {
            leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, l_OpenRotation, step);
            rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, r_OpenRotation, step);
        }
        if(close)
        {
            leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, l_ClosedRotation, step);
            rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, r_ClosedRotation, step);
        }


    }


    public void OpenDoor()
    {
        close = false;
        open = true;
    }
    public void CloseDoor()
    {
        open = false;
        close = true;
    }
    public void Check()
    {
        check = true;
            
    }
}