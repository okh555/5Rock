using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{
    public float moveSpeed;
    public float moveLength;

    bool isMoveForward = true;

    private Vector3 initPos;
    private Vector3 targetPos;

    private bool isActiveTarget = false;
    private bool isTargetBack = true;


    public int score = 100;

    public delegate void TargetHitEvent(int point);
    public event TargetHitEvent OnTargetHit;


    private void Start()
    {
        initPos = transform.localPosition;

        targetPos = initPos + Vector3.right * moveLength;
    }


    private void Update()
    {
        if ((isTargetBack == false && isActiveTarget == false) || isActiveTarget == true)
        {
            isTargetBack = false;

            Vector3 move = Vector3.right * Time.deltaTime * moveSpeed;

            if (!isMoveForward) move *= -1;

            transform.localPosition += move;

            if (isMoveForward == true && transform.localPosition.x > targetPos.x)
            {
                isMoveForward = false;
                transform.localPosition = targetPos;
            }
            else if (isMoveForward == false && transform.localPosition.x < initPos.x)
            {
                isMoveForward = true;
                transform.localPosition = initPos;

                isTargetBack = true;
            }
        }

    }

    public void SetTarget(bool isActive)
    {
        isActiveTarget = isActive;
    }


    public void TargetHit()
    {
        OnTargetHit.Invoke(score);
    }
}
