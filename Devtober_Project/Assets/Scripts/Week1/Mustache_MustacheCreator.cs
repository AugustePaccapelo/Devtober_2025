using UnityEngine;
using UnityEngine.InputSystem;

// Author : Auguste Paccapelo

public class Mustache_MustacheCreator : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    [SerializeField] private GameObject _prefabMustachePart;

    // ----- Objects ----- \\

    private LineRenderer _currentLine;

    // ----- Others ----- \\

    [SerializeField] private float _startWidth = 2f;
    [SerializeField] private float _endWidth = 0f;
    [SerializeField] private float _linePointsRate = 0.1f;
    private float _linePointsTimer = 0f;
    private bool _isDrawing = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start() { }

    private void Update()
    {
        if (!_isDrawing || _currentLine == null) return;

        UpdateLine();
        CheckIsFinished();
    }

    private void OnMouseDown()
    {
        Vector3 pos = GetMousePos();
        pos.z = -5;
        _currentLine = gameObject.AddComponent<LineRenderer>();
        _currentLine.positionCount = 1;
        _currentLine.SetPosition(0, pos);
        _currentLine.endWidth = 0f;
        _isDrawing = true;
    }

    // ----- My Functions ----- \\

    private Vector2 GetMousePos() => Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

    private void UpdateLine()
    {
        _linePointsTimer += Time.deltaTime;
        if (_linePointsTimer < _linePointsRate) return;
        
        _linePointsTimer = 0f;
        Vector3 pos = GetMousePos();
        pos.z = -5;
        _currentLine.positionCount++;
        _currentLine.SetPosition(_currentLine.positionCount-1, pos);
    }

    private void CheckIsFinished()
    {
        if (Input.GetMouseButton(0)) return;

        Vector3[] allPos = new Vector3[_currentLine.positionCount];
        _currentLine.GetPositions(allPos);
        Destroy(_currentLine);
        _currentLine = null;

        CreateMustache(allPos);
    }

    private void CreateMustache(Vector3[] allPos)
    {
        float rotation;
        Vector3 currentPos;
        Vector3 nextPos;
        Vector3 direction;
        Vector3 director;
        Vector2 scale;
        float width;
        

        int length = allPos.Length - 1;
        float widthSteps = (_endWidth - _startWidth) / length;

        for (int i = 0;  i < length - 1; i++)
        {
            currentPos = allPos[i];
            nextPos = allPos[i+1];

            direction = nextPos - currentPos;
            director = direction.normalized;
            rotation = Mathf.Atan2(director.y, director.x) * Mathf.Rad2Deg + 90;

            width = _startWidth + i * widthSteps;
            scale = new Vector2(width, direction.magnitude * 2f);

            CreateMustachePart(currentPos + direction * 0.5f, rotation, scale);
        }
    }

    private void CreateMustachePart(Vector3 pos, float rotation, Vector2 scale)
    {
        GameObject part = Instantiate(_prefabMustachePart);
        part.transform.position = pos;

        Vector3 partRotation = part.transform.eulerAngles;
        partRotation.z = rotation;
        part.transform.eulerAngles = partRotation;
        part.transform.localScale *= scale;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}