using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

// Author : Auguste Paccapelo

public class Sting_BeeGenerator : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    [SerializeField] private GameObject _prefabBee;

    // ----- Objects ----- \\

    [SerializeField] private GameObject _player;
    private List<GameObject> _allBees = new List<GameObject>();

    // ----- Others ----- \\

    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float _beeSpeed = 5f;

    private float _spawnRadius;

    private Vector2 _minSpawnPos;
    private Vector2 _maxSpawnPos;
    private float _timer = 0f;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        float screenHeight = 2f * Camera.main.orthographicSize;
        float screenWidth = screenHeight * Camera.main.aspect;

        _minSpawnPos = new Vector2(-screenWidth * 0.5f, -screenHeight * 0.5f);
        _maxSpawnPos = new Vector2(screenWidth * 0.5f, screenHeight * 0.5f);

        //_spawnRadius = (_maxSpawnPos - _minSpawnPos).magnitude;
        _spawnRadius = screenHeight;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > spawnRate)
        {
            _timer = 0f;
            CreateBee();
        }

        int length = _allBees.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            MoveBee(_allBees[i]);
            if (IsBeeOutOfBounds(_allBees[i]))
            {
                Destroy(_allBees[i]);
                _allBees.RemoveAt(i);
            }
        }
    }

    // ----- My Functions ----- \\

    private void MoveBee(GameObject bee)
    {
        float angle = bee.transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector3 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        bee.transform.position += direction * _beeSpeed * Time.deltaTime;
    }

    private bool IsBeeOutOfBounds(GameObject bee)
    {
        Vector2 pos = bee.transform.position;
        bool isInX = pos.x >= _minSpawnPos.x - _spawnRadius * 0.5f && pos.x <= _maxSpawnPos.x + _spawnRadius * 0.5f;
        bool isInY = pos.y >= _minSpawnPos.y - _spawnRadius * 0.5f && pos.y <= _maxSpawnPos.y + _spawnRadius * 0.5f;

        return !isInX || !isInY;
    }

    private void CreateBee()
    {
        Vector2 pos = GetRandomPos();
        GameObject newBee = Instantiate(_prefabBee);
        newBee.transform.position = pos;

        float rotation = GetDirectionToPlayer(pos);
        Vector3 angle = newBee.transform.eulerAngles;
        angle.z = rotation;
        newBee.transform.eulerAngles = angle;

        _allBees.Add(newBee);
    }

    private float GetDirectionToPlayer(Vector3 pos)
    {
        Vector2 director = _player.transform.position - pos;
        Vector2 direction = director.normalized;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private Vector2 GetRandomPos()
    {
        /*float posX = Random.Range(_minSpawnPos.x, _maxSpawnPos.x);
        float posY = Random.Range(_minSpawnPos.y, _maxSpawnPos.y);

        return new Vector2(posX, posY);*/

        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector2 pos = new Vector2(Mathf.Cos(angle) * _spawnRadius, Mathf.Sin(angle) * _spawnRadius);

        return pos;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}