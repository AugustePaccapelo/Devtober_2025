using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

// Author : Auguste Paccapelo

public class Lesson_Button : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private Light2D _light;
    [SerializeField] private ButtonColor _buttonColor;

    // ----- Others ----- \\

    public float _lightTimeOn = 0.5f;

    public bool doingSequence = true;

    public event Action<ButtonColor> OnPress;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake()
    {
        _light.gameObject.SetActive(false);
    }

    private void Start() { }

    private void Update() { }

    private void OnMouseDown()
    {
        if (doingSequence) return;
        OnPress?.Invoke(_buttonColor);
        TurnOn();
    }

    // ----- My Functions ----- \\

    private IEnumerator LightCoroutine()
    {
        _light.gameObject.SetActive(true);
        yield return new WaitForSeconds(_lightTimeOn);
        _light.gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        StartCoroutine(LightCoroutine());
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}