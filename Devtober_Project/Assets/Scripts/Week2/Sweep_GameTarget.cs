using UnityEngine;

// Author : Auguste Paccapelo

public class GameTarget : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    private SpriteRenderer _renderer;
    public Sweep_RadarManager radarManager;

    // ----- Others ----- \\

    [SerializeField] private float _showSpeed = 0.3f;
    private bool _isClicked = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Color color = _renderer.color;
        color.a = 0f;
        _renderer.color = color;
    }

    private void Update()
    {
        if (!_isClicked) return;
        if (_renderer.color.a >= 1f)
        {
            Destructor();
            return;
        }
        Color color = _renderer.color;
        color.a += _showSpeed * Time.deltaTime;
        _renderer.color = color;
    }

    // ----- My Functions ----- \\

    private void OnMouseDown()
    {
        _isClicked = true;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        radarManager.targets[this].Destructor();
        radarManager.targets.Remove(this);
        Destroy(gameObject);
    }
}