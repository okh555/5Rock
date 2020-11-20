using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuseBox : MonoBehaviour
{

    [System.Serializable]
    public class LeverDown : UnityEvent<bool> { }
    [System.Serializable]
    public class LeverUp : UnityEvent<bool> { }



    public ParticleSystem[] SparkleFuseVFX;
    public ParticleSystem[] SwitchedOnVFX;
    public ParticleSystem[] SwitchedOffVFX;

    public  LeverDown leverDown;
    public LeverUp leverUp;


    bool m_FusePresent = false;

    public void Switched(int step)
    {
        if (!m_FusePresent)
            return;

        if (step == 0)
        {
            leverUp.Invoke(true);
            foreach (var s in SwitchedOffVFX)
            {
                s.Play();
            }
        }
        else
        {
            leverDown.Invoke(true);
            foreach (var s in SwitchedOnVFX)
            {
                s.Play();
            }
        }
    
}
    
    public void FuseSocketed(bool socketed)
    {
        m_FusePresent = socketed;

        if (m_FusePresent)
        {
            foreach (var s in SparkleFuseVFX)
            {
                s.Play();
            }
        }
    }
}
