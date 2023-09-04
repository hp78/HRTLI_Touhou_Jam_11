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
        yield return new WaitForSeconds(spikeOutDuration+0.5f);

        animator.SetTrigger("In");
        yield return new WaitForSeconds(spikeInDuration+0.5f);

        yield return 0;
    }

    public override void Reset()
    {
        triggerInterval = triggerDelay;
        animator.Play("Blank");

        triggerStarted = false;
        trapFired = false;
    }
}
