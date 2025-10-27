using UnityEngine;
using static UnityEditor.PlayerSettings;

// Author : Auguste Paccapelo

public class Award_Trophy : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private PlayerInputs _playerInput;

    // ----- Others ----- \\

    [SerializeField] private float _distanceToPlayer = 2;
    [SerializeField] private float _speed = 5f;
    private Vector2 _minScreenPos;
    private Vector2 _maxScreenPos;

    private bool _isMoving = true;

    private float _timeToCenter = 2f;
    private Vector2 _finalScale = new Vector2(3, 3);
    private Vector2 _startScale;
    private float _timer = 0f;
    private Vector2 _startPos;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake()
    {
        float height = Camera.main.orthographicSize * 2f;
        float width = Camera.main.aspect * height;
        Vector2 size = new Vector2(width, height);
        _minScreenPos = -size * 0.5f;
        _maxScreenPos = size * 0.5f;

        _startScale = transform.localScale;
    }

    private void Start() { }

    private void Update()
    {
        if (_isMoving)
        {
            if (_playerInput.CurrentInputDirection != Vector2.zero && DistanceToPlayer() <= _distanceToPlayer)
            {
                Vector2 pos = (Vector2)transform.position - DirectionToPlayer() * _distanceToPlayer * Time.deltaTime * _speed;
                pos.x = Mathf.Clamp(pos.x, _minScreenPos.x, _maxScreenPos.x);
                pos.y = Mathf.Clamp(pos.y, _minScreenPos.y, _maxScreenPos.y);
                transform.position = pos;
            }
        }
        else
        {
            _timer += Time.deltaTime;
            if (_timer < _timeToCenter)
            {
                float w = _timer / _timeToCenter;
                Debug.Log(w);
                float x = Mathf.Lerp(_startPos.x, 0, w);
                float y = Mathf.Lerp(_startPos.y, 0, w);
                Vector2 pos = new Vector2(x, y);
                transform.position = pos;
                transform.localScale = _startScale + (_finalScale - _startScale) * w;
            }
            else
            {
                transform.position = Vector2.zero;
                transform.localScale = _finalScale;
            }
        }
    }

    // ----- My Functions ----- \\

    private Vector2 DirectionToPlayer()
    {
        return ((Vector2)_playerInput.transform.position - (Vector2)transform.position).normalized;
    }

    private float DistanceToPlayer()
    {
        return (_playerInput.transform.position - transform.position).magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isMoving = false;
        _startPos = transform.position;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}