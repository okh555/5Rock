using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket_Block : MonoBehaviour
{
    private Vector3 target;

    private Vector3 orginVec;
    private bool up = false;
    private bool down = false;
    private float downRange = 1f;
    private float speed = 1f;

    void Start()
    {
        orginVec = transform.position;
        target = transform.position;
        target.y = target.y - downRange;
    }

    // Update is called once per frame
    void Update()
    {
        float step = Time.deltaTime * speed;
        if(up)
        {
            transform.position = Vector3.MoveTowards(transform.position, orginVec, step);
            if (transform.position == orginVec)
                up = false;
        }
        
        if(down)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (transform.position == target)
                down = false;

        }

    }


    public void MoveUp()
    {
        up = true;
    }
    public void MoveDown()
    {
        down = true;
    }
}
