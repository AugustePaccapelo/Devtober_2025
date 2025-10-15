using UnityEngine;
using UnityEngine.U2D;

// Author : Auguste Paccapelo

public class Crown_Crown : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    [SerializeField] private SpriteShapeRenderer shapeRenderer;
    private GameObject _targetObject;

    // ----- Others ----- \\

    [SerializeField] private float _timeConnatPicked = 2f;
    [SerializeField] private float _oscillationSpeed = 4f;
    private bool _canBePicked = true;
    private float _timer = 0f;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start() { }

    private void Update()
    {
        if (!_canBePicked)
        {
            _timer += Time.deltaTime;
            if (_timer >= _timeConnatPicked)
            {
                shapeRenderer.color = Color.white;
                _timer = 0f;
                _canBePicked = true;
                return;
            }
            Color color = Color.white;
            color.a = Mathf.Abs(Mathf.Sin(_timer * _oscillationSpeed));
            Debug.Log(color.a);
            shapeRenderer.color = color;
        }
        if (_targetObject == null) return;
        transform.position = _targetObject.transform.position;
    }

    // ----- My Functions ----- \\

    public void CrownPickedUp(GameObject targetObject)
    {
        if (!_canBePicked) return;
        _targetObject = targetObject;
    }

    public void CrownDropped(Vector3 newPos)
    {
        transform.position = newPos;
        _targetObject = null;
        _canBePicked = false;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
