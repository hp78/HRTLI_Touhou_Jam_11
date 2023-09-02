using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : TrapBase
{

    public float spikeOutDuration;
    public float spikeInDuration;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        triggerInterval = triggerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerStarted)
            triggerInterval -= Time.deltaTime;

        if (triggerInterval < 0.0f)
        {
            triggerStarted = false;
            if (!trapFired)
            {
                TriggerTrap();
                trapFired = true;
            }
        }
    }
    public override void TriggerTrap()
    {
        StartCoroutine(StartSpikeAnimation());
    }

    IEnumerator StartSpikeAnimation()
    {
        animator.SetTrigger("Out");
        yield return new WaitForSeconds(spikeOutDuration+1f);
        animator.SetTrigger("In");
        yield return new WaitForSeconds(spikeInDuration+1f);
        StartCoroutine(StartSpikeAnimation());
        yield return 0;
    }
}
