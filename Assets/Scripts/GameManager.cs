using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Vector2> currRecordingPositions = new List<Vector2>();
    public List<CloneController> playerClones = new List<CloneController>();

    public GameObject playerClonePF;
    public Transform playerTF;
    public Transform playerSpawn;

    float recordCountdown = 0f;
    float timeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Reset();
        }

        timeElapsed += Time.deltaTime;


        RecordMovement();
    }


    void RecordMovement()
    {
        recordCountdown -= Time.deltaTime;
        if (recordCountdown <= 0)
        {
            recordCountdown = 0.1f;
            currRecordingPositions.Add(playerTF.position);
        }
    }

    private void Reset()
    {
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
    }
}
