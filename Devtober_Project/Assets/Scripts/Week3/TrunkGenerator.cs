using System.Collections.Generic;
using UnityEngine;

// Author : Auguste Paccapelo

public class TrunkGenerator : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    [SerializeField] private GameObject _prefabTrunk;

    // ----- Objects ----- \\

    [SerializeField] private GameObject _heightRef;
    [SerializeField] private GameObject _lowLimit;
    private List<GameObject> _allTrunks = new List<GameObject>();

    // ----- Others ----- \\

    [SerializeField] private float _spawnRate = 2f;
    private float _timer = 0f;

    private Vector2 _xBounds;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        float screenHeight = Camera.main.orthographicSize;
        float screenWitdh = screenHeight * Camera.main.aspect;
        _xBounds = new Vector2(-screenWitdh*0.5f, screenWitdh*0.5f);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _spawnRate)
        {
            _timer = 0f;
            CreateTrunk();
        }

        int length = _allTrunks.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            if (IsTruckOutOfBounds(_allTrunks[i]))
            {
                Destroy(_allTrunks[i]);
                _allTrunks.RemoveAt(i);
            }
        }
    }

    // ----- My Functions ----- \\

    private bool IsTruckOutOfBounds(GameObject trunk)
    {
        return trunk.transform.position.y <= _lowLimit.transform.position.y;
    }

    private void CreateTrunk()
    {
        Vector2 pos = GetRandomPos();
        GameObject trunk = Instantiate(_prefabTrunk);
        trunk.transform.position = pos;
        _allTrunks.Add(trunk);
    }

    private Vector2 GetRandomPos()
    {
        float posY = _heightRef.transform.position.y;
        float posX = Random.Range(_xBounds.x, _xBounds.y);
        return new Vector2(posX, posY);
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}