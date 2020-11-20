using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    Light[] lights = null;

    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }


    public void TurnOn()
    {
        foreach(Light light in lights)
        {
            light.enabled = true;
        }
    }
    public void TurnOff()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }
}
