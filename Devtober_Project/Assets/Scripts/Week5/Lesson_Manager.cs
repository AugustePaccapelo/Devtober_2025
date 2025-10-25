using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

// Author : Auguste Paccapelo

public enum ButtonColor
{
    Green, Red, Blue, Yellow
}

public class Lesson_Manager : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private Lesson_Button _greenButton;
    [SerializeField] private Lesson_Button _redButton;
    [SerializeField] private Lesson_Button _blueButton;
    [SerializeField] private Lesson_Button _yellowButton;
    private Dictionary<ButtonColor, Lesson_Button> _dicoButtons;

    // ----- Others ----- \\

    [SerializeField] private float _timeLightOn = 0.5f;
    [SerializeField] private int _startNumButton = 2;
    [SerializeField] private int _stepUpNumButton = 1;
    private int _numTarget;
    private int _currentNum;
    private int _numSucces = 0;

    private Queue<ButtonColor> _currentSequence = new Queue<ButtonColor>();

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable()
    {
        foreach (Lesson_Button button in _dicoButtons.Values)
        {
            button.OnPress += OnButtonPress;
        }
    }

    private void OnDisable()
    {
        foreach (Lesson_Button button in _dicoButtons.Values)
        {
            button.OnPress -= OnButtonPress;
        }
    }

    private void Awake()
    {
        _dicoButtons = new Dictionary<ButtonColor, Lesson_Button>()
        {
            {ButtonColor.Green, _greenButton }, {ButtonColor.Red, _redButton }, {ButtonColor.Blue, _blueButton }, {ButtonColor.Yellow, _yellowButton }
        };

        foreach (Lesson_Button button in _dicoButtons.Values )
        {
            button._lightTimeOn = _timeLightOn;
            button.doingSequence = true;
        }
    }

    private void Start()
    {
        StartCoroutine(WaitAndStart());
    }

    private void Update() { }

    // ----- My Functions ----- \\

    private void OnButtonPress(ButtonColor color)
    {
        ButtonColor targetColor = _currentSequence.Dequeue();
        if (color == targetColor) Debug.Log("Good Color");
        else
        {
            Debug.Log("Bad color");
            _currentSequence.Clear();
            _numSucces = 0;
            StartCoroutine(WaitAndStart());
            return;
        }

        if (_currentSequence.Count <= 0)
        {
            Debug.Log("Finished ! ");
            _numSucces++;
            StartCoroutine(WaitAndStart());
        }
    }

    private IEnumerator WaitAndStart()
    {
        foreach (Lesson_Button button in _dicoButtons.Values)
        {
            button.doingSequence = true;
        }
        yield return new WaitForSeconds(2f);
        MakeSequence();
    }

    private void MakeSequence()
    {
        _numTarget = _startNumButton + _numSucces * _stepUpNumButton;
        _currentNum = 0;
        foreach (Lesson_Button button in _dicoButtons.Values)
        {
            button.doingSequence = true;
        }
        StartCoroutine(SequenceCoroutine());
    }

    private IEnumerator SequenceCoroutine()
    {
        ButtonColor color;
        while (_currentNum < _numTarget)
        {
            color = GetRandomButton();
            _dicoButtons[color].TurnOn();
            _currentSequence.Enqueue(color);
            _currentNum++;
            yield return new WaitForSeconds(_timeLightOn * 1.25f);
        }

        foreach (Lesson_Button button in _dicoButtons.Values)
        {
            button.doingSequence = false;
        }
    }

    private ButtonColor GetRandomButton()
    {
        return _dicoButtons.Keys.ToArray()[Random.Range(0, _dicoButtons.Count - 1)];
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}