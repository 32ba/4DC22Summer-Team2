using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    [SerializeField] private GameObject creditUIButtonGameObject;
    [SerializeField] private GameObject setNameUIGameObject;
    public void OnClickStart()
    {
        var dbUserInfo = DBManager.Instance.DB.GetCollection<UserInfo>("UserInfo");
        var userInfo = dbUserInfo.FindOne(x => x.Id.Equals(1));
        if(userInfo.Name == null)setNameUIGameObject.SetActive(true);
        else SceneManager.LoadScene("Select Mode");
    }
    public void OnClickCredit()
    {
        creditUIButtonGameObject.SetActive(true);
    }
}
