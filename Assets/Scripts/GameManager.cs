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
        Cursor.visible = false;
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        if (player == null && sceneNum != 0)
        {
            player = Instantiate(playerPrefab, spawnPos, new Quaternion());
        }
    }
}

