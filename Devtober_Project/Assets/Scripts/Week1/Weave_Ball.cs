using UnityEngine;

// Author : Auguste Paccapelo

public class Weave_Ball : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private GameObject _player;
    [SerializeField] private Renderer _renderer;

    // ----- Others ----- \\

    [SerializeField] private float _turnsPerUnits = 2f;
    [SerializeField] private float _downScaleRatio = 1f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Vector2 _baseScale;
    private float _playerBaseDistance;
    private float _rotation = 0f;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        _baseScale = transform.localScale;
        _playerBaseDistance = Mathf.Abs(_player.transform.position.x - transform.position.x);
    }

    private void Update()
    {
        float distance = Mathf.Abs(_player.transform.position.x - transform.position.x);
        float distanceDiff = distance - _playerBaseDistance;

        float scaleFactor = 1f - distanceDiff * _downScaleRatio;
        scaleFactor = Mathf.Max(scaleFactor, 0f);

        Vector2 newScale = _baseScale * scaleFactor;
        transform.localScale = newScale;

        _rotation = newScale.x * _rotationSpeed;
        _renderer.material.SetFloat("_amount", _rotation);
    }

    // ----- My Functions ----- \\

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}