using System;
using UnityEngine;

// Author : Auguste Paccapelo

public class Starfish_Leg : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    [SerializeField] private GameObject _customPivotPoint;

    // ----- Others ----- \\

    [SerializeField] private float _growingSpeed = 2f;

    private Vector3 _startPos;
    public Vector3 _endPos { get; private set; }
    private Vector3 _startScale = Vector3.zero;
    private Vector3 _endScale;

    private float _currentScaleFactor = 0f;

    private bool _isGrowing = true;
    public bool isClicked { get; private set; } = false;
    public static event Action OnAllReady;
    private static int _numLegs = 5;
    private static int _numReadyLegs = 0;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start()
    {
        Debug.Log("leg");
        _endPos = transform.position;
        _startPos = _customPivotPoint.transform.position;
        _endScale = transform.localScale;
        transform.localScale = _startScale;
        transform.position = _startPos;
        _numReadyLegs++;
        if (_numReadyLegs == _numLegs) OnAllReady?.Invoke();
    }

    private void Update()
    {
        if (!_isGrowing) return;

        _currentScaleFactor += Time.deltaTime * _growingSpeed;
        if (_currentScaleFactor >= 1f)
        {
            transform.localScale = _endScale;
            transform.position = _endPos;
            _isGrowing = false;
            return;
        }

        transform.localScale = _endScale * _currentScaleFactor;
        transform.position = _startPos + (_endPos - _startPos) * _currentScaleFactor;
    }

    // ----- My Functions ----- \\

    private void OnMouseDown()
    {
        Destructor();
        isClicked = true;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
