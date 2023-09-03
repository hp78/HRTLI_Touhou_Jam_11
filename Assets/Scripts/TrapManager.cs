using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public static TrapManager instance;
    public List<TrapTrigger> listOfTrigger = new List<TrapTrigger>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }


    public void ResetTraps()
    {

        foreach (TrapTrigger tt in listOfTrigger)
        {
            tt.ResetTraps();
        }
    }
}