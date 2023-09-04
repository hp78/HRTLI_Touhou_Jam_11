using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Space(5)]
    public string nextSceneName;
    public Animator canvasAnimator;

    [Space(5)]
    public FloatVal elapsedTimeSO;
    public IntVal totalDeathsSO;
    public TMP_Text deathsTxt;
    public TMP_Text elapsedTimeTxt;

    [Space(5)]
    public List<Vector2> currRecordingPositions = new List<Vector2>();
    public List<CloneController> playerClones = new List<CloneController>();

    [Space(5)]
    public GameObject playerClonePF;
    public PlayerController playerControl;
    public Transform playerTF;
    public Transform playerSpawn;

    [Space(5)]
    public GameObject pauseMenu;

    float recordCountdown = 0f;
    float timeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        deathsTxt.text = "" +totalDeathsSO.val;

        int minutes = Mathf.FloorToInt(elapsedTimeSO.val / 60F);
        int seconds = Mathf.FloorToInt(elapsedTimeSO.val - minutes * 60);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        elapsedTimeTxt.text = niceTime;
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(elapsedTimeSO.val / 60F);
        int seconds = Mathf.FloorToInt(elapsedTimeSO.val - minutes * 60);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        elapsedTimeTxt.text = niceTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ClearClones();
        }

        timeElapsed += Time.deltaTime;
        elapsedTimeSO.val += Time.deltaTime;

        recordCountdown -= Time.deltaTime;
        if (recordCountdown <= 0 && playerControl.isAlive)
        {
            RecordMovement();
        }
    }


    public void RecordMovement()
    {
        recordCountdown = 0.1f;
        currRecordingPositions.Add(playerTF.position);
    }

    public void Respawn()
    {
        totalDeathsSO.val += 1;
        deathsTxt.text = "" + totalDeathsSO.val;

        GameObject clone = Instantiate(playerClonePF);
        clone.transform.position = playerSpawn.position;
        CloneController cloneCont = clone.GetComponent<CloneController>();
        cloneCont.positions = currRecordingPositions;
        playerClones.Add(cloneCont);

        currRecordingPositions = new List<Vector2>();

        timeElapsed = 0f;

        foreach (CloneController c in playerClones)
        {
            c.Reset();
        }

        playerTF.position = playerSpawn.position + new Vector3(0,1f);

        TrapManager.instance.ResetTraps();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadNextScene("MainMenu"));
    }

    public void GotToNextScene()
    {
        StartCoroutine(LoadNextScene(nextSceneName));
    }
    IEnumerator LoadNextScene(string sceneName)
    {
        canvasAnimator.Play("StageExit");
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void SceneEntry()
    {

    }

    public void ClearClones()
    {
        foreach(CloneController cc in playerClones)
        {
            Destroy(cc.gameObject);
        }

        playerClones = new List<CloneController>();
    }
}
