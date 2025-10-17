using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Author : Auguste Paccapelo

public class Reckless_WallGenerator : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    [SerializeField] private Reckless_Wall _wallPrefab;

    // ----- Objects ----- \\

    [SerializeField] private float _wallsGenerationRate = 2f;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _wallContainer;
    private List<Reckless_Wall> _allWalls = new List<Reckless_Wall>();

    private float _timer = 0f;

    // ----- Others ----- \\

    private Vector2 _screenSize;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start()
    {
        _screenSize.x = Camera.main.orthographicSize * 2f;
        _screenSize.y = _screenSize.x * Camera.main.aspect;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _wallsGenerationRate)
        {
            GenerateWall();
            _timer = 0f;
        }

        CheckAllWallsOutOfRange();
    }

    // ----- My Functions ----- \\

    private Vector3 GetRandomPos()
    {
        float xPos = _player.transform.position.x + _screenSize.x * 2f;

        float yMin = _player.transform.position.y - _screenSize.y * 0.5f;
        float yMax = _player.transform.position.y + _screenSize.y * 0.5f;

        float yPos = Random.Range(yMin, yMax);

        return new Vector3(xPos, yPos);
    }

    private Reckless_Wall GenerateWall()
    {
        Reckless_Wall newWall = Instantiate(_wallPrefab, _wallContainer.transform);
        _allWalls.Add(newWall);
        newWall.transform.position = GetRandomPos();

        return newWall;
    }

    private void CheckAllWallsOutOfRange()
    {
        Vector3 wallPos;
        Reckless_Wall wall;
        for (int i = _allWalls.Count-1; i >= 0; i--)
        {
            wall = _allWalls[i];
            if (wall.IsDestroyed())
            {
                _allWalls.Remove(wall);
                continue;
            }

            wallPos = wall.transform.position;
            if (wallPos.x <= _player.transform.position.x - _screenSize.x)
            {
                _allWalls.RemoveAt(i);
                wall.Destructor();
            }
        }
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
