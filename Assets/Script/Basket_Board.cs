using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket_Board : MonoBehaviour
{
    private Quaternion target;

    private Quaternion orginRot;
    public bool up = false;
    public bool down = false;
    private float speed = 10f;

    void Start()
    {
        orginRot = transform.rotation;
        target = transform.rotation;
        target.x = 90f;

        Debug.Log(orginRot);
        Debug.Log(target);
    }

    void Update()
    {
        float step = Time.deltaTime * speed;
        if (up)
        {
            transform.Rotate(90f, 0, 0);
            
            if (transform.rotation == orginRot)
                up = false;
        }

        if (down)
        {
            transform.Rotate(-90f, 0, 0);
            if (transform.rotation == target)
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
