using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CraneGame : MonoBehaviour
{
    public CoinInsert coinInsert;

    public XRJoyStick joyStick;
    public ClawObjectTrigger clawObjectTrigger;
    public Transform CraneMotorAnchor;
    public GameObject CranePiston;

    public Transform RailVertical;
    public Transform RailHorizontal;

    public Transform String;
    public Transform StringStart;
    public Transform StringEnd;

    const float CranePistonMinDistance = -0.2f;
    const float CranePistonMaxDistance = -0.65f;

    public float craneMoveSpeed = 1f;
    public float craneDownSpeed = 5f;
    const float craneDownSpeedConst = 0.1f;
    public float craneGrabSpeed = 50f;
    const float craneGrabSpeedConst = 0.01f;

    public float maxCraneDownDistance = 3f;
    bool isObjectTriggered = false;

    public float craneRotateSpeed = 10f;

    public float defaultWaitTime = 0.5f;

    public Vector3 CraneInitPosition;
    public Vector3 CraneGoalPosition;

    enum CraneState
    {
        NOTSTART, START, CRANE_DOWN, CRANE_GRAB, CRANE_UP, CRANE_MOVETOGOAL, CRANE_RELEASE, CRANE_INIT
    }
    CraneState craneState = CraneState.NOTSTART;



    // Start is called before the first frame update
    void Start()
    {
        joyStick.OnJoyStickChange += JoyStickInput;
        clawObjectTrigger.OnObjectEnter += ObjectTrigger;
        //SetNewState(CraneState.NOTSTART);
        SetNewState(CraneState.START);
    }

    private void ObjectTrigger()
    {
        isObjectTriggered = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (craneState)
        {
            case CraneState.NOTSTART:
                StartCraneGame();
                break;

            case CraneState.START:
                break;

            case CraneState.CRANE_DOWN:
                CraneDownUpdate();
                break;

            case CraneState.CRANE_GRAB:
                CraneGrab();
                break;

            case CraneState.CRANE_UP:
                CraneUpUpdate();
                break;

            case CraneState.CRANE_MOVETOGOAL:
                CraneMoveToGoal();
                break;

            case CraneState.CRANE_RELEASE:
                CraneRelease();
                break;

            case CraneState.CRANE_INIT:
                CraneInit();
                break;
        }

        CalcString();
    }

    void MoveRight()
    {
        if (craneState == CraneState.START)
        {
            Vector3 moveVal = Vector3.right * craneMoveSpeed * Time.deltaTime;
            CraneMotorAnchor.Translate(moveVal, Space.Self);
            RailHorizontal.Translate(moveVal, Space.Self);

            Vector3 newPos = CraneMotorAnchor.localPosition;
            if (newPos.x >= CraneInitPosition.x)
            {
                newPos.x = CraneInitPosition.x;
                CraneMotorAnchor.localPosition = newPos;

                Vector3 movePos = RailHorizontal.localPosition;
                movePos.x = CraneInitPosition.x;
                RailHorizontal.localPosition = movePos;
            }
        }
    }

    void MoveLeft()
    {
        if (craneState == CraneState.START)
        {
            Vector3 moveVal = Vector3.left * craneMoveSpeed * Time.deltaTime;
            CraneMotorAnchor.Translate(moveVal, Space.Self);
            RailHorizontal.Translate(moveVal, Space.Self);

            Vector3 newPos = CraneMotorAnchor.localPosition;
            if (newPos.x <= CraneGoalPosition.x)
            {
                newPos.x = CraneGoalPosition.x;
                CraneMotorAnchor.localPosition = newPos;

                Vector3 movePos = RailHorizontal.localPosition;
                movePos.x = CraneGoalPosition.x;
                RailHorizontal.localPosition = movePos;
            }
        }
    }

    void MoveForward()
    {
        if (craneState == CraneState.START)
        {
            Vector3 moveVal = Vector3.forward * craneMoveSpeed * Time.deltaTime;
            CraneMotorAnchor.Translate(moveVal, Space.Self);
            RailVertical.Translate(moveVal, Space.Self);

            Vector3 newPos = CraneMotorAnchor.localPosition;
            if (newPos.z >= CraneInitPosition.z)
            {
                newPos.z = CraneInitPosition.z;
                CraneMotorAnchor.localPosition = newPos;

                Vector3 movePos = RailVertical.localPosition;
                movePos.z = CraneInitPosition.z;
                RailVertical.localPosition = movePos;
            }
        }
    }

    void MoveBackward()
    {
        if (craneState == CraneState.START)
        {
            Vector3 moveVal = Vector3.back * craneMoveSpeed * Time.deltaTime;
            CraneMotorAnchor.Translate(moveVal, Space.Self);
            RailVertical.Translate(moveVal, Space.Self);

            Vector3 newPos = CraneMotorAnchor.localPosition;
            if (newPos.z <= CraneGoalPosition.z)
            {
                newPos.z = CraneGoalPosition.z;
                CraneMotorAnchor.localPosition = newPos;

                Vector3 movePos = RailVertical.localPosition;
                movePos.z = CraneGoalPosition.z;
                RailVertical.localPosition = movePos;
            }
        }
    }


    void StartCraneGame()
    {
        if (coinInsert.UseCoin())
            SetNewState(CraneState.START);
    }

    public void CraneDown()
    {
        if (craneState == CraneState.START)
        {
            isObjectTriggered = false;
            SetNewState(CraneState.CRANE_DOWN);
        }
    }

    void CraneDownUpdate()
    {
        Vector3 newPos = transform.localPosition;

        transform.Translate(Vector3.down * craneDownSpeed * craneDownSpeedConst * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * craneRotateSpeed * Time.deltaTime);

        if (isObjectTriggered)
        {
            SetNewState(CraneState.CRANE_GRAB, defaultWaitTime);
            if (newPos.y <= -maxCraneDownDistance)
            {
                newPos.y = -maxCraneDownDistance;
                transform.localPosition = newPos;
            }
        }
        else if (newPos.y <= -maxCraneDownDistance)
        {
            SetNewState(CraneState.CRANE_GRAB, defaultWaitTime);
            newPos.y = -maxCraneDownDistance;
            transform.localPosition = newPos;
        }
    }

    void CraneUpUpdate()
    {
        Vector3 newPos = transform.localPosition;

        transform.Translate(Vector3.up * craneDownSpeed * craneDownSpeedConst * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.down * craneRotateSpeed * Time.deltaTime);

        if (newPos.y >= 0f)
        {
            SetNewState(CraneState.CRANE_MOVETOGOAL, defaultWaitTime);
            newPos.y = 0f;
            transform.localPosition = newPos;
        }
    }

    void CraneGrab()
    {
        Vector3 newPos = CranePiston.transform.localPosition;

        newPos.y += craneGrabSpeed * craneGrabSpeedConst * Time.deltaTime;

        // if grab complete
        if (newPos.y >= CranePistonMinDistance)
        {
            SetNewState(CraneState.CRANE_UP, defaultWaitTime);
            newPos.y = CranePistonMinDistance;
        }

        CranePiston.transform.localPosition = newPos;
    }

    void CraneRelease()
    {
        Vector3 newPos = CranePiston.transform.localPosition;

        newPos.y -= craneGrabSpeed * craneGrabSpeedConst * Time.deltaTime;

        // if grab complete
        if (newPos.y <= CranePistonMaxDistance)
        {
            SetNewState(CraneState.CRANE_INIT, defaultWaitTime);
            newPos.y = CranePistonMaxDistance;
        }

        CranePiston.transform.localPosition = newPos;
    }

    void CraneMoveToGoal()
    {
        Vector3 newPos = CraneMotorAnchor.localPosition;

        if (newPos.z <= CraneGoalPosition.z)
        {
            if (newPos.x <= CraneGoalPosition.x)
            {
                CraneMotorAnchor.localPosition = CraneGoalPosition;
                RailVertical.localPosition = new Vector3(RailVertical.localPosition.x, 0f, CraneGoalPosition.z);
                RailHorizontal.localPosition = new Vector3(CraneGoalPosition.x, 0f, RailHorizontal.localPosition.z);

                SetNewState(CraneState.CRANE_RELEASE, defaultWaitTime);
            }
            else
            {
                Vector3 moveVal = Vector3.left * craneMoveSpeed * Time.deltaTime;
                CraneMotorAnchor.Translate(moveVal, Space.Self);
                RailHorizontal.Translate(moveVal, Space.Self);
            }
        }
        else
        {
            Vector3 moveVal = Vector3.back * craneMoveSpeed * Time.deltaTime;
            CraneMotorAnchor.Translate(moveVal, Space.Self);
            RailVertical.Translate(moveVal, Space.Self);
        }
    }

    void CraneInit()
    {
        Vector3 newPos = CraneMotorAnchor.localPosition;

        if (newPos.z >= CraneInitPosition.z)
        {
            if (newPos.x >= CraneInitPosition.x)
            {
                CraneMotorAnchor.localPosition = CraneInitPosition;
                RailVertical.localPosition = new Vector3(RailVertical.localPosition.x, 0f, 0f);
                RailHorizontal.localPosition = new Vector3(0f, 0f, RailHorizontal.localPosition.z);

                SetNewState(CraneState.NOTSTART);
            }
            else
            {
                Vector3 moveVal = Vector3.right * craneMoveSpeed * Time.deltaTime;
                CraneMotorAnchor.Translate(moveVal, Space.Self);
                RailHorizontal.Translate(moveVal, Space.Self);
            }
        }
        else
        {
            Vector3 moveVal = Vector3.forward * craneMoveSpeed * Time.deltaTime;
            CraneMotorAnchor.Translate(moveVal, Space.Self);
            RailVertical.Translate(moveVal, Space.Self);
        }
    }


    void SetNewState(CraneState newState, float sec = 0f)
    {
        if (sec == 0f)
        {
            craneState = newState;
        }
        else
        {
            StartCoroutine(WaitSecForNewState(newState, sec));
        }
    }

    IEnumerator WaitSecForNewState(CraneState newState, float sec)
    {
        yield return new WaitForSeconds(sec);
        craneState = newState;
    }


    void CalcString()
    {
        Vector3 centerPos = Vector3.Lerp(StringStart.position, StringEnd.position, 0.5f);
        float stringLength = Vector3.Distance(StringStart.position, StringEnd.position);

        String.position = centerPos;

        Vector3 newScale = String.localScale;
        newScale.z = stringLength;
        String.localScale = newScale;

        String.LookAt(StringStart);
    }


    void JoyStickInput(Vector2 newValue)
    {
        if (newValue.x == 1f)
            MoveRight();
        else if (newValue.x == -1f)
            MoveLeft();

        if (newValue.y == 1f)
            MoveForward();
        else if (newValue.y == -1f)
            MoveBackward();
    }
}
