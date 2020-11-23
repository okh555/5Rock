using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamesController : MonoBehaviour
{

    private ParticleSystem[] particleSystems;
    // Start is called before the first frame update
    void Start()
    {

        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void FlameOn()
    {
        foreach(ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Play();
            Debug.Log("Flame");
        }
    }
}
