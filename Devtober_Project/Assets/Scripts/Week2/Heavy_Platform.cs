using UnityEngine;

// Author : Auguste Paccapelo

public class Heavy_Platform : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private Heavy_Player _player;
    [SerializeField] private GameObject _leftPart;
    [SerializeField] private GameObject _rightPart;
    [SerializeField] private GameObject _goodRenderer;

    private Rigidbody2D _rigidbody;

    // ----- Others ----- \\

    [SerializeField] private float _maxMass = 5;
    private float _endRotation = 45f;
    private float _startRotation;
    private float _animDuration = 0.5f;
    private float _timer = 0f;
    private bool _isInAnim = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        _startRotation = _leftPart.transform.eulerAngles.z;
        _endRotation += _startRotation;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_timer > _animDuration) return;

        if (!_isInAnim)
        {
            if (_player.mass < _maxMass) return;
            _isInAnim = true;
            _goodRenderer.SetActive(false);
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        
        _timer += Time.deltaTime;

        float weight = _timer / _animDuration;
        float currentRotation = Mathf.Lerp(_startRotation, _endRotation, weight);

        Vector3 leftAngle = _leftPart.transform.eulerAngles;
        leftAngle.z = -currentRotation;
        _leftPart.transform.eulerAngles = leftAngle;
        
        Vector3 rightAngle = _rightPart.transform.eulerAngles;
        rightAngle.z = currentRotation;
        _rightPart.transform.eulerAngles = rightAngle;
    }

    // ----- My Functions ----- \\

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}