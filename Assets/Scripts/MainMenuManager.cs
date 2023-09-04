using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //
    [Space(5)]
    public FloatVal elapsedTime;
    public IntVal totalDeaths;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime.val = 0f;
        totalDeaths.val = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Plot");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
