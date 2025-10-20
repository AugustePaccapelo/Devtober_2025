using UnityEngine;
using UnityEngine.InputSystem;

// Author : Auguste Paccapelo

public class LookAtMouse : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    public bool shouldLook = true;
    public float currentAngle { get { return transform.eulerAngles.z - _offSetAngle; } }
    public Vector3 currentDirection { get; private set; } = Vector3.right;

    [SerializeField] private float _offSetAngle;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start() { }

    private void Update()
    {
        if (!shouldLook) return;
        currentDirection = DirectionToMouse();
        Vector3 currentAngle = transform.eulerAngles;
        currentAngle.z = Mathf.Rad2Deg * Mathf.Atan2(currentDirection.y, currentDirection.x) + _offSetAngle;
        transform.eulerAngles = currentAngle;
    }

    // ----- My Functions ----- \\

    private Vector3 DirectionToMouse()
    {
        Vector3 mousePos = Mouse.current.position.value;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = (mousePos - transform.position).normalized;
        return direction;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
