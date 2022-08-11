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
        selectLevelID = "1"; 
    }

    public void OnClickStart()
    {
        if(selectLevelID == "0")
        {

        }
        else
        {
            SceneManager.LoadScene("test");
        }
    }
}
