using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Image gameOverPanelImage;
    [SerializeField] private GameObject gameOverObjects;
    
    private bool _isSceneChange = true;
    private bool _isChangeEnd;
    private float _alphaValue;
    private Color _panelColor;
    private const float TrasitionSpeed = 1f;
    private const float MaxAlphaValue = 0.8f;

    private string _songUuid;

    private void OnEnable()
    {
        _panelColor = gameOverPanelImage.color;
        _songUuid = GameObject.FindWithTag("GameController").GetComponent<Main>().songUuid;
    }

    private void Update()
    {
        if (_isSceneChange)
        {
            _alphaValue += TrasitionSpeed * Time.deltaTime;
            gameOverPanelImage.color = new Color(_panelColor.r, _panelColor.g, _panelColor.b, _alphaValue);
            if (_alphaValue >= MaxAlphaValue)
            {
                _isSceneChange = false;
                _isChangeEnd = true;
            }
        }

        if (_isChangeEnd)
        {
            gameOverObjects.SetActive(true);
        }
    }

    private void OnDisable()
    {
        gameOverPanelImage.color = new Color(_panelColor.r, _panelColor.g, _panelColor.b, 0f);
        gameOverObjects.SetActive(false);
        _isChangeEnd = false;
        _isSceneChange = true;
        _alphaValue = 0f;
    }
    
    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("shrimp/Scene/Start");
    }

    public void OnClickRetryButton()
    {
        TrasitionToGameScene();
    }
    
    private void TrasitionToGameScene() //ゲームへ遷移する際はこれを呼ぶ
    {
        SceneManager.sceneLoaded += SendSongUuidToGame;
        SceneManager.LoadScene("deltaEichi/Scenes/deltaEichi_test");
    }

    private void SendSongUuidToGame(Scene next, LoadSceneMode mode)
    {
        var gameManager = GameObject.FindWithTag("GameController").GetComponent<Main>();
        gameManager.songUuid = _songUuid;
        SceneManager.sceneLoaded -= SendSongUuidToGame;
    }
}
