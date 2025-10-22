using UnityEngine;

// Author : Auguste Paccapelo

public class Blast_Player : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    [SerializeField] private Blast_Rocket _rocketPrefab;

    // ----- Objects ----- \\

    [SerializeField] private LookAtMouse _weaponLookAt;
    private Rigidbody2D _rigidbody;

    // ----- Others ----- \\

    [SerializeField] private float _blastForce = 20f;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() { }

    // ----- My Functions ----- \\

    private void OnLeftClick()
    {
        Shoot();
    }

    private void Shoot()
    {
        Blast_Rocket _rocket = Instantiate(_rocketPrefab);
        _rocket.direction = _weaponLookAt.currentDirection;
        _rocket.transform.position = _weaponLookAt.transform.position;
        _rigidbody.AddForce(-_weaponLookAt.currentDirection * _blastForce);
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}