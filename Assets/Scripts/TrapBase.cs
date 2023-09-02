using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : MonoBehaviour
{
    public float triggerDelay = 0;
    public float triggerInterval;
    public bool triggerStarted = false;
    public bool trapFired = false;

    virtual public void TriggerTrap ()
    {

    }

    virtual public void Reset()
    {
        triggerInterval = 0;
        triggerStarted = false;
        trapFired = false;
    }    

    
}
