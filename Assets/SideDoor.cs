using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDoor : MonoBehaviour
{

    public Transform target;
    public Transform player;
    public float speed = 1f;
    public float playerDistance = 1f;
    private Transform transform = null;
    private Vector3 originalPos;
    private Vector3 targetPos;
    private bool Open = false;
    private bool Close = false;

    
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        originalPos = transform.position;
        targetPos = target.position;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) >= playerDistance)
        {
            CloseDoor();
        }
        else
            OpenDoor();


        float step = speed * Time.deltaTime;
        if (Open || Close)
        {
            if(Open)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
                if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                    Open = false;
            }
            else if(Close)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPos, step);
                if (Vector3.Distance(transform.position, originalPos) < 0.1f)
                    Close = false;
            }
        }
    }

    public void OpenDoor()
    {
        Open = true;
    }
    public void CloseDoor()
    {
        Close = true;
    }
}
