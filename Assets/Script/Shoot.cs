using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Camera cam;
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public float shootForce = 10f;
    public bool arrowAttached = false;


    private Animator animator;

    

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       // shoot();
    }
    // Update is called once per frame
    public void shoot()
    {
        if(arrowAttached == true)
            animator.SetBool("Shoot", true);
    }

    public void shootArrow()
    {
        arrowAttached = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);
        Transform trans = arrow.GetComponent<Transform>();
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = arrowSpawn.forward * shootForce;
    }
    public void getArrow()
    {
        arrowAttached = true;
    }
    public void discard()
    {
        animator.SetBool("Shoot", false);
    }
}
