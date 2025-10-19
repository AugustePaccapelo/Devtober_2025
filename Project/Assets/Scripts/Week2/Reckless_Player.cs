using UnityEngine;

// Author : Auguste Paccapelo

public class Reckless_Player : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    [SerializeField] private GameObject[] wheels;
    private Rigidbody2D _rigidBody;

    // ----- Others ----- \\

    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float minimumXVelo = 10f;
    [SerializeField] private float _wheelSpeed = 1f;
    public bool _isPlaying = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        Camera.main.transform.parent = gameObject.transform;
    }

    private void Update()
    {
        if (!_isPlaying) return;

        _rigidBody.linearVelocityX += _acceleration * Time.deltaTime;
        Vector3 wheelAngle;
        foreach (GameObject wheel in wheels)
        {
            wheelAngle = wheel.transform.eulerAngles;
            wheelAngle.z += Time.deltaTime * _wheelSpeed;
            wheel.transform.eulerAngles = wheelAngle;
        }
    }

    // ----- My Functions ----- \\

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Reckless_Wall>()?.DestroyWall();
        if (_rigidBody.linearVelocityX <= minimumXVelo) Debug.Log("lost");
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
