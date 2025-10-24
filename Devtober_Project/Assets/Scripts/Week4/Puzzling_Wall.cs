using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

// Author : Auguste Paccapelo

public class Puzzling_Wall : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    private float _minAngle = 0;
    private float _maxAngle = 360;

    private bool _isFolowing = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake()
    {
        float angle = Random.Range(_minAngle, _maxAngle);
        Vector3 newAngle = transform.eulerAngles;
        newAngle.z = angle;
        transform.eulerAngles = newAngle;
    }

    private void Start() { }

    private void Update()
    {
        if (!_isFolowing) return;
        transform.position = GetMousePos();
    }

    private void OnMouseDown()
    {
        _isFolowing = true;
    }

    private void OnMouseUp()
    {
        _isFolowing = false;
    }

    // ----- My Functions ----- \\

    private Vector2 GetMousePos() => Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}