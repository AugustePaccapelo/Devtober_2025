using UnityEngine;
using UnityEngine.InputSystem;

// Author : Auguste Paccapelo

public class Squeletal_Scanner : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    private bool _isFolowing = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start() { }

    private void Update()
    {
        if (!_isFolowing) return;

        Vector3 pos = GetMousePos();
        pos.z = transform.position.z;
        transform.position = pos;
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