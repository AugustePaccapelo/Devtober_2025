using System;
using System.Collections.Generic;
using UnityEngine;

// Author : Auguste Paccapelo

public class Spear_Spear : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    private Rigidbody2D _rigidBody;
    private LineRenderer _lineRenderer;
    private LookAtMouse _scriptLookMouse;
    private List<GameObject> _enemies = new List<GameObject>();

    // ----- Others ----- \\

    [SerializeField] private float _speedMultiplicator = 2.5f;
    [SerializeField] private float _chargeSpeed = 2.5f;
    private Action _currentAction;
    private bool _isPressing = false;
    private float _timer = 0f;
    private Vector2 _startPos;
    [SerializeField] private GameObject _gameObjFirstPointPos;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.simulated = false;
        _currentAction = Aiming;
        _startPos = transform.position;
        _lineRenderer = GetComponent<LineRenderer>();
        _scriptLookMouse = GetComponent<LookAtMouse>();
        _scriptLookMouse.shouldLook = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1)) Reset();
        _currentAction?.Invoke();
    }

    // ----- My Functions ----- \\

    private void Reset()
    {
        _currentAction = Aiming;
        transform.position = _startPos;
        _rigidBody.simulated = false;
        _scriptLookMouse.shouldLook = true;

        foreach (GameObject enemy in _enemies) enemy.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    private void InAir()
    {
        Vector2 currentDirection = _rigidBody.linearVelocity.normalized;
        Vector3 currentAngle = transform.eulerAngles;
        currentAngle.z = Mathf.Rad2Deg * Mathf.Atan2(currentDirection.y, currentDirection.x);
        transform.eulerAngles = currentAngle;
    }

    private void Aiming()
    {
        if (!_isPressing && Input.GetMouseButton(0)) _isPressing = true;
        if (_isPressing)
        {
            Vector3 velo = _scriptLookMouse.currentDirection * _timer;
            _lineRenderer.SetPosition(0, _gameObjFirstPointPos.transform.position);

            if (!Input.GetMouseButton(0))
            {
                _scriptLookMouse.shouldLook = false;

                _rigidBody.simulated = true;
                _currentAction = InAir;
                _lineRenderer.SetPosition(1, _gameObjFirstPointPos.transform.position);

                _rigidBody.linearVelocity = velo * _speedMultiplicator;
                _timer = 0f;
                _isPressing = false;
            }
            else
            {
                _timer += Time.deltaTime * _chargeSpeed;
                Vector3 pos = _gameObjFirstPointPos.transform.position + velo / _chargeSpeed;
                _lineRenderer.SetPosition(1, pos);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _rigidBody.simulated = false;
            _currentAction = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            if (!_enemies.Contains(collision.gameObject)) _enemies.Add(collision.gameObject);
        }
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
