using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public List<TrapBase> trapList = new List<TrapBase>();
    public bool triggered = false;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!triggered)
            {
                TriggerTraps();
                triggered = true;
            }
        }
    }

    public void TriggerTraps()
    {
        foreach(TrapBase t in trapList)
        {
            t.triggerStarted = true;
        }    
    }

    public void ResetTraps()
    {
        foreach(TrapBase t in trapList)
        {
            t.Reset();
        }
        triggered = false;

    }
}
