using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Author : Auguste Paccapelo

public class Rivals_GameManager : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private TextMeshProUGUI _textZ;
    [SerializeField] private TextMeshProUGUI _textO;
    [SerializeField] private Slider _sliderZ;
    [SerializeField] private Slider _sliderO;
    [SerializeField] private TextMeshProUGUI _leftWin;
    [SerializeField] private TextMeshProUGUI _rightWin;
    [SerializeField] private TextMeshProUGUI _draw;

    // ----- Others ----- \\

    [SerializeField] private int _numPress = 100;
    private int _numPressZ = 0;
    private int _numPressO = 0;

    private bool _wasZPress = false;
    private bool _wasOPress = false;
    private bool _isRunning = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (!_isRunning) return;

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (!_wasZPress)
            {
                _numPressZ++;
                _wasZPress = true;
            }
        }
        else _wasZPress = false;

        if (Input.GetKeyUp(KeyCode.O))
        {
            if (!_wasOPress)
            {
                _numPressO++;
                _wasOPress = true;
            }
        }
        else _wasOPress = false;

        _sliderZ.value = _numPressZ;
        _sliderO.value = _numPressO;

        if (_numPressZ >= _numPress && _numPressO >= _numPress)
        {
            _draw.gameObject.SetActive(true);
            _isRunning = false;
        }
        else if (_numPressZ >= _numPress && _numPressO < _numPress)
        {
            _leftWin.gameObject.SetActive(true);
            _isRunning = false;
        }
        else if (_numPressZ < _numPress && _numPressO >= _numPress)
        {
            _rightWin.gameObject.SetActive(true);
            _isRunning = false;
        }
    }

    // ----- My Functions ----- \\

    public void ResetGame()
    {
        _numPressZ = _numPressO = 0;
        _sliderZ.value = _numPressZ;
        _sliderO.value = _numPressO;
        _sliderO.maxValue = _sliderZ.maxValue = _numPress;
        _isRunning = false;
        _draw.gameObject.SetActive(false);
        _leftWin.gameObject.SetActive(false);
        _rightWin.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        _isRunning = true;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}