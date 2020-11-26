using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    Vector3 currentPos;
    
    // Start is called before the first frame update
    void Start()
    {
        currentPos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y != currentPos.y)
        {
            transform.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
        }
    }
}
