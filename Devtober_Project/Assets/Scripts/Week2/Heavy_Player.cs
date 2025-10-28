using UnityEngine;

// Author : Auguste Paccapelo

public class Heavy_Player : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    private Rigidbody2D _rigidbody;

    // ----- Others ----- \\

    public float mass
    {
        get => _rigidbody.mass;
    }

    [SerializeField] private float _scaleGrowRate = 1f;
    [SerializeField] private float _scaleReduceRate = 1f;
    [SerializeField] private float _massGrowRate = 1f;
    [SerializeField] private float _massReduceRate = 1f;
    [SerializeField] private Vector2 _minScale = new Vector2(0.05f, 0.05f);

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() { }

    private void Update()
    {
        CheckLeftMouseButton();
        CheckRightMouseButton();
    }

    // ----- My Functions ----- \\

    private void CheckLeftMouseButton()
    {
        if (!Input.GetMouseButton(0)) return;

        transform.localScale += Vector3.one * _scaleGrowRate * Time.deltaTime;
        _rigidbody.mass += _massGrowRate * Time.deltaTime;
    }

    private void CheckRightMouseButton()
    {
        if (!Input.GetMouseButton(1)) return;

        if (transform.localScale.x < _minScale.x && transform.localScale.y < _minScale.y)
        {
            transform.localScale = _minScale;
            return;
        }

        transform.localScale -= Vector3.one * _scaleReduceRate * Time.deltaTime;
        _rigidbody.mass -= _massReduceRate * Time.deltaTime;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}