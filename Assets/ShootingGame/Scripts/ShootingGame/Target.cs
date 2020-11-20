using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    bool isGameStart = false;
    bool isTargetHit = false;
    bool isReset = false;
    public float resetSpeed = 150f;

    public int hitPoint = 100;

    Rigidbody rigid;

    public delegate void TargetHit(int point);
    public event TargetHit OnTargetHit;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isTargetHit == false)
        {
            if (transform.eulerAngles.x > 80f)
            {
                isTargetHit = true;
                Debug.Log("Target Hit!");

                if (isGameStart) OnTargetHit(hitPoint);
            }
        }

        ResetTargetUpdate();
    }

    public void GameStart()
    {
        ResetTarget();
        isGameStart = true;
    }

    public void GameEnd()
    {
        ResetTarget();
        isGameStart = false;
    }

    public void ResetTarget()
    {
        if (isTargetHit == false || isReset == true) return;
        isReset = true;
        
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void ResetTargetUpdate()
    {
        if (isReset == false) return;

        if (transform.localRotation.eulerAngles.x < 0f)
        {
            isReset = false;
            isTargetHit = false;
            rigid.useGravity = true;
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;

            transform.localRotation = new Quaternion();
            return;
        }

        float angle = -resetSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right, angle);
    }
}
