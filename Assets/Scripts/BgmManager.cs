using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
            Destroy(this.gameObject);
    }


}
