using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Select Mode");
    }
    public void OnClickCredit()
    {
        SceneManager.LoadScene("test");
    }
}
