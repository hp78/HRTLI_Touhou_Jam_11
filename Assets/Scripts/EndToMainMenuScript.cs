using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndToMainMenuScript : MonoBehaviour
{
    public void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
