using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void OnEnable()
    {
        _panelColor = gameOverPanelImage.color;
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
}
