using UnityEngine;

// Author : Auguste Paccapelo

public class Shredded_PaperGenerator : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    [SerializeField] private GameObject _prefabPaper;

    // ----- Objects ----- \\

    [SerializeField] private GameObject _paperContainer;

    // ----- Others ----- \\

    [SerializeField] private float _spawnRate;
    [SerializeField] private float _minPosX;
    [SerializeField] private float _maxPosX;
    [SerializeField] private float _spawnHeight;

    private float _timer = 0f;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected virtual void Awake() { }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _spawnRate)
        {
            _timer = 0f;
            CreatePaper();
        }
    }

    // ----- My Functions ----- \\

    private GameObject CreatePaper()
    {
        GameObject newPaper = Instantiate(_prefabPaper, _paperContainer.transform);
        newPaper.transform.position = RandomPos();

        return newPaper;
    }

    private Vector2 RandomPos()
    {
        float xPos = Random.Range(_minPosX, _maxPosX);
        return new Vector2(xPos, _spawnHeight);
    }

    // ----- Destructor ----- \\

    protected virtual void OnDestroy() { }
}