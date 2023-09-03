using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : TrapBase
{
    public GameObject arrow;
    //public float speed;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        triggerInterval = triggerDelay;
    }


    private void Update()
    {
        if(triggerStarted)
        triggerInterval -= Time.deltaTime;

        if(triggerInterval <0.0f)
        {
            triggerStarted = false;
            if(!trapFired)
            {
                TriggerTrap();
                trapFired = true;
            }
        }
    }
    public override void TriggerTrap()
    {
        
        Vector3 relativePos = target.position - transform.position;

        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * relativePos;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);


        var temp = Instantiate(arrow, transform.position, targetRotation);


    }

    public override void Reset()
    {
        triggerInterval = triggerDelay;
        triggerStarted = false;
        trapFired = false;
    }
}





