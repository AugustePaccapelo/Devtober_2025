using UnityEngine;
using UnityEngine.InputSystem;

// Author : Auguste Paccapelo

public class Ornate_Stone : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    [SerializeField] private GameObject _prefab;

    // ----- Objects ----- \\

    // ----- Others ----- \\

    private bool _isFolowing = false;
    private bool _isOverTarget = false;

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

        if (!Input.GetMouseButton(0))
        {
            if (_isOverTarget) Destroy(this);
            else Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        _isFolowing = true;
        Instantiate(_prefab);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger in");
        if (collision.tag != "Target") return;
        _isOverTarget = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("trigger out");
        if (collision.tag != "Target") return;
        _isOverTarget = false;
    }

    // ----- My Functions ----- \\

    private Vector2 GetMousePos() => Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}