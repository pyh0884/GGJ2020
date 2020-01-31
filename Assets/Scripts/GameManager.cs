using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;
    private GameObject player;
    public Vector3 spawnPos;
    public static int turn = 0;
    public GameObject RecorderManager;
    private GameObject RecorderInstance;
    private bool recorderChecked;
    

    private void Awake()
    {

        if (SceneManager.GetActiveScene().name == "__Main Menu")
            Destroy(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
        turn = 0;
        recorderChecked = false;
    }
    public void Respawn()
    {
        float x = PlayerPrefs.GetFloat("RespwanX", 1.5f);
        float y = PlayerPrefs.GetFloat("RespwanY", -1);
        int index = 0;
        spawnPos = new Vector3(x, y);
        /*		if(SceneManager.GetActiveScene().buildIndex!=index)*/
        SceneManager.LoadScene(index);
    }
    private void Update()
    {
        if (!recorderChecked)
        {
            recorderChecked = true;
            CheckRecorder();
        }

        Cursor.visible = false;
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        //if (player == null && sceneNum != 0)
        //{
        //    player = Instantiate(playerPrefab, spawnPos, new Quaternion());
        //}

    }

    // add death turn
    public void AddTurn()
    {
        turn++;
        Replayer.Records.Add(new List<Record>());
        recorderChecked = false;
    }

    private void CheckRecorder()
    {
        // Initialize RecorderManager
        /* TODO: If it's new level
            turn = 0;
            ReInitialize The GameObject RecorderMangager; (The following code has implemented it.)
        */
        GameObject tmpG = GameObject.FindGameObjectWithTag("RecorderManager");
        if (tmpG == null)
        {
            RecorderInstance = Instantiate(RecorderManager);
            DontDestroyOnLoad(RecorderInstance);
        }
        // we need to init each time died (in the same level)
        if (RecorderInstance != null)
        {
            RecorderInstance.GetComponent<Replayer>().InitCharacters();
            RecorderInstance.GetComponent<Recorder>().InitSingleRecord();
        }
    }
}

