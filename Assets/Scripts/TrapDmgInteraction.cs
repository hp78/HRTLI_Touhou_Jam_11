using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDmgInteraction : MonoBehaviour
{
    // Start is called before the first frame update

    public bool ifKillsClone;
    public bool ifKillsSelfOnHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var temp = collision.GetComponent<PlayerController>();
        if(temp)
        {
            temp.PlayerDie();
            if (ifKillsSelfOnHit)
                this.gameObject.SetActive(false);
        }

        var temp2 = collision.GetComponent<CloneController>();
        if(temp2)
        {
            temp2.CloneDie(ifKillsClone);
            if (ifKillsSelfOnHit)
                this.gameObject.SetActive(false);
        }

    }
}
