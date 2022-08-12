using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameEditorUIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject applyButton;

    private void OnEnable()
    {
        var dbUserInfo = DBManager.Instance.DB.GetCollection<UserInfo>("UserInfo");
        var userInfo = dbUserInfo.FindOne(x => x.Id.Equals(1));
        nameInputField.text = userInfo.Name;
    }

    public async void OnClickApplyButton()
    {
        var dbUserInfo = DBManager.Instance.DB.GetCollection<UserInfo>("UserInfo");
        var userInfo = dbUserInfo.FindOne(x => x.Id.Equals(1));
        userInfo.Name = nameInputField.text;
        dbUserInfo.Update(userInfo);
        await APIManager.UserNameUpdate(nameInputField.text);
        gameObject.SetActive(false);
    }

    public void OnInputFieldValueChanged()
    {
        applyButton.GetComponent<Button>().interactable = nameInputField.text.Length <= 6;
    }
}
