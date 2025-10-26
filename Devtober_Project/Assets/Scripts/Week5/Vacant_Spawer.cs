using UnityEngine;
using UnityEngine.InputSystem;

// Author : Auguste Paccapelo

public class Vacant_Spawer : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    [SerializeField] private GameObject _aiPrefab;

    // ----- Objects ----- \\

    // ----- Others ----- \\

    private bool _wasPress = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start() { }

    private void Update()
    {
        if (!_wasPress && Input.GetMouseButtonDown(0))
        {
            _wasPress = true;
            GameObject ai = Instantiate(_aiPrefab);
            ai.transform.position = GetMousePos();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _wasPress = false;
        }
    }

    // ----- My Functions ----- \\

    private Vector2 GetMousePos() => Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}