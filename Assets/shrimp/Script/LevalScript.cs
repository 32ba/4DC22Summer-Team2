using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevalScript : MonoBehaviour
{
    public static string selectLevelID;

    void Start() 
    {
        selectLevelID = "0"; 
    }

    public void OnClickA()
    {
        selectLevelID = "0f1b605e-53e1-45ca-92a8-8dc97a63071e"; 
    }
    public void OnClickB()
    {
        selectLevelID = "0"; 
    }

    public void OnClickStart()
    {
        if(selectLevelID != "0")
        {
            TrasitionToGameScene();
        }
    }
    
    private void TrasitionToGameScene() //ゲームへ遷移する際はこれを呼ぶ
    {
        SceneManager.sceneLoaded += SendSongUuidToGame;
        SceneManager.LoadScene("deltaEichi/Scenes/deltaEichi_test");
    }

    private void SendSongUuidToGame(Scene next, LoadSceneMode mode)
    {
        var gameManager = GameObject.FindWithTag("GameController").GetComponent<Main>();
        gameManager.songUuid = selectLevelID;
        SceneManager.sceneLoaded -= SendSongUuidToGame;
    }
}
