using UnityEngine;
using UnityEngine.UI;

// Author : Auguste Paccapelo

public class Rowdy_Ball : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private Slider _slider;

    // ----- Others ----- \\

    [SerializeField] private float _maxHP = 100f;
    [SerializeField] private float _startHP = 100f;
    [SerializeField] private float _damages = 10f;
    private float _currentHP;

    private float _startSpeed = 10f;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake()
    {
        _currentHP = _startHP;
        _slider.maxValue = _maxHP;
        _slider.value = _currentHP;
    }

    private void Start()
    {
        float angle = Random.Range(0, Mathf.PI * 2);
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        GetComponent<Rigidbody2D>().linearVelocity = direction * _startSpeed;
    }

    private void Update() { }

    // ----- My Functions ----- \\

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Floor") return;
        _currentHP -= _damages;
        _slider.value = _currentHP;
        if (_currentHP <= 0) Destroy(gameObject);
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}