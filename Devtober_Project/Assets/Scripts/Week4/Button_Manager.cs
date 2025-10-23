using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

// Author : Auguste Paccapelo

public class Button_Manager : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private List<ButtonLightPair> _pairButtons = new List<ButtonLightPair>();

    [Serializable]
    public struct ButtonLightPair
    {
        public Button button;
        public Light2D light;
    }

    // ----- Others ----- \\

    [SerializeField] private float _greenLightChance = 0.1f;
    [SerializeField] private Color _goodColor = Color.green;
    [SerializeField] private Color _badColor = Color.red;
    private int _numBad = 0;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start() { }

    private void Update() { }

    // ----- My Functions ----- \\
    
    private Light2D FindLightFromButton(Button button)
    {
        foreach (ButtonLightPair pair in _pairButtons)
        {
            if (pair.button == button) return pair.light;
        }
        return null;
    }
    
    public void ButtonPressed(Button button)
    {
        float randomValue = UnityEngine.Random.value;
        button.gameObject.SetActive(false);
        Light2D light = FindLightFromButton(button);
        light.gameObject.SetActive(true);

        Color color;
        if (_numBad >= _pairButtons.Count-1) color = _goodColor;
        else color = _greenLightChance >= randomValue ? _goodColor : _badColor;

        light.color = color;
        if (color == _badColor) _numBad++;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}