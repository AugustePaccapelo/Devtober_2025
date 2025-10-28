using UnityEngine;

// Author : Auguste Paccapelo

public class Puzzling_Manager : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Singleton ----- \\

    public static Puzzling_Manager Instance {get; private set;}

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private GameObject _startPosRef;
    [SerializeField] private LineRenderer _lineRenderer;

    private float _rayDistance = 10f;
    private int _maxPosCount = 100;

    // ----- Others ----- \\

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Debug.Log(nameof(Puzzling_Manager) + " Instance already exist, destorying last added.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start() { }

    void Update()
    {
        GenerateLight();
    }

    // ----- My Functions ----- \\

    private void GenerateLight()
    {
        MakeFirstPoint();

        MakeAllPoints();
    }

    private void MakeAllPoints()
    {
        Vector2 pos = _lineRenderer.GetPosition(_lineRenderer.positionCount-1);
        Vector2 direction = (pos - (Vector2)_lineRenderer.GetPosition(_lineRenderer.positionCount-2)).normalized;
        RaycastHit2D hit = Physics2D.Raycast(pos, direction, _rayDistance);

        for (int i = 0; hit; i++)
        {
            if (i >= _maxPosCount)
            {
                Debug.Log("Max pos count reached.");
                break;
            }

            pos = hit.point;
            direction = Vector2.Reflect(direction, hit.normal);
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount-1, pos);
            hit = Physics2D.Raycast(pos + direction * 0.01f, direction, _rayDistance);
        }
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos + direction * _rayDistance);
    }
    
    private void MakeFirstPoint()
    {
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, _startPosRef.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(_lineRenderer.GetPosition(0), Vector2.right, _rayDistance);
        _lineRenderer.positionCount++;
        if (hit)
        {
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, 
                (Vector2)_lineRenderer.GetPosition(0) + Vector2.right * _rayDistance);
        }
    }

    // ----- Destructor ----- \\

    protected virtual void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}