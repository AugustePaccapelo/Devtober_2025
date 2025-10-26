using UnityEngine;

// Author : Auguste Paccapelo

public class Vacant_AI : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    private Vector2 _minCoord;
    private Vector2 _maxCoord;

    [SerializeField] private float _maxAngleChange = 30;
    [SerializeField] private float _minSpeed = 0.5f;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _angleChangeRate = 0.5f;

    private float _timerAngleChange = 0f;
    private float _speed;
    private Vector2 _direction;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake()
    {
        SetBounds();
        _direction = GetRandomDirection();
        _speed = GetRandomSpeed();
    }

    private void Start() { }

    private void Update()
    {
        _timerAngleChange += Time.deltaTime;
        if (_timerAngleChange >= _angleChangeRate)
        {
            _timerAngleChange = 0f;
            float changeAngle = GetRandomAngle();
            _direction = RotateVector(_direction, changeAngle);
        }

        Vector3 pos = transform.position;
        if (transform.position.x < _minCoord.x)
        {
            pos.x = _minCoord.x;
            _direction.x = -_direction.x;
        }
        if (transform.position.x >= _maxCoord.x)
        {
            pos.x = _maxCoord.x;
            _direction.x = -_direction.x;
        }
        if (transform.position.y < _minCoord.y)
        {
            pos.y = _minCoord.y;
            _direction.y = -_direction.y;
        }
        if (transform.position.y >= _maxCoord.y)
        {
            pos.y = _maxCoord.y;
            _direction.y = -_direction.y;
        }

        transform.position = pos + (Vector3)_direction * _speed * Time.deltaTime;        
    }

    // ----- My Functions ----- \\

    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        float x = vector.x * cos - vector.y * sin;
        float y = vector.x * sin + vector.y * cos;
        return new Vector2(x, y);
    }

    private float GetRandomAngle()
    {
        return Random.Range(-_maxAngleChange * 0.5f, _maxAngleChange * 0.5f);
    }

    private float GetRandomSpeed()
    {
        return Random.Range(_minSpeed, _maxSpeed);
    }

    private Vector2 GetRandomDirection()
    {
        float x = Random.value;
        float y = Random.value;
        return new Vector2(x, y).normalized;
    }

    private void SetBounds()
    {
        Vector2 screenSize = GetScreenSize();
        _minCoord = (Vector2)Camera.main.transform.position - screenSize * 0.5f;
        _maxCoord = (Vector2)Camera.main.transform.position + screenSize * 0.5f;
    }

    private Vector2 GetScreenSize()
    {
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Camera.main.aspect;
        return new Vector2(width, height);
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}