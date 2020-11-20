using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeInteractable : MonoBehaviour
{
    _Notch interactor;
    bool changed = false;

    float socketDelay = 0.5f;
    float detachedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponent<_Notch>();
    }

    public void ChangeActive()
    {
        interactor.socketActive = !interactor.socketActive;
        detachedTime = Time.time;
        changed =true;
    }

    void Update()
    {
        
        if(changed && Time.time - detachedTime > socketDelay)
        {
            ChangeActive();
            changed = false;
        }
    }
}
