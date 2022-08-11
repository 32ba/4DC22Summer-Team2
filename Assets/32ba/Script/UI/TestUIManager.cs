using System;
using System.Collections;
using System.Collections.Generic;
using LiteDB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI guidText;
    [SerializeField] private TMP_InputField scoreInputField;

    public async void Awake()
    {
        var guid = await APIManager.Signup();
        guidText.text = "GUID : " + guid;
    }

    public async void OnPushScoreSendButton()
    {
        var request = new SendScoreRequest()
        {
            score = long.Parse(scoreInputField.text)
        };
        Debug.Log(await APIManager.SendScore(request)); 
    }
}
