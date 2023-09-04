using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlotPageController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }
}
