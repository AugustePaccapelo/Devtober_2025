using UnityEngine;

// Author : Auguste Paccapelo

public class Deer_Deer : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    [SerializeField] private GameObject _player;

    // ----- Others ----- \\

    [SerializeField] private float _rangeDetection = 5f;
    private bool _isRunning = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start() { }

    private void Update()
    {
        if (!_isRunning)
        {
            _isRunning = IsPlayerInRange();
            return;
        }
        transform.position += Vector3.right * 7.5f * Time.deltaTime;
    }

    // ----- My Functions ----- \\

    private bool IsPlayerInRange()
    {
        Vector2 vecDirector = _player.transform.position - transform.position;
        Vector2 direction = vecDirector.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _rangeDetection);
        Vector3 faitchier = direction * _rangeDetection;
        Debug.DrawLine(transform.position, (transform.position + faitchier), Color.red, 0.1f);
        if (!hit || hit.rigidbody.tag != "Player") return false;
        if (hit.rigidbody.GetComponent<PlayerInputs>().isCrouch)
        {
            return vecDirector.magnitude <= _rangeDetection * 0.5f;
        }
        return true;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
