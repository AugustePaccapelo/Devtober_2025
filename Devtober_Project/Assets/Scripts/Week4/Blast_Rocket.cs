using UnityEngine;

// Author : Auguste Paccapelo

public class Blast_Rocket : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    [SerializeField] private GameObject _explosion;

    // ----- Objects ----- \\

    private Rigidbody2D _rigidbody;

    // ----- Others ----- \\

    [SerializeField] private float _speed = 10f;
    public Vector2 direction;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.linearVelocity = direction * _speed;
    }

    private void Update() { }

    // ----- My Functions ----- \\

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject explosion = Instantiate(_explosion);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}