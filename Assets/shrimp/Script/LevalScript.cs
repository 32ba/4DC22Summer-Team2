using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevalScript : MonoBehaviour
{
    public static int selectLevelNumber;

    void Start() 
    {
        selectLevelNumber = 0; 
    }

    public void OnClickA()
    {
        selectLevelNumber = 0; 
    }
    public void OnClickB()
    {
        selectLevelNumber = 1; 
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("test");
    }
}
