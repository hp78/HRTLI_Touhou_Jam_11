using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTransition : MonoBehaviour
{
    public FloatVal elapsedTimeSO;
    public IntVal totalDeathsSO;

    // Start is called before the first frame update
    void Start()
    {
        if(totalDeathsSO.val > 15)
        {
            SceneManager.LoadScene("BadEnd");
        }
        else
        {
            SceneManager.LoadScene("GoodEnd");
        }
    }
}
