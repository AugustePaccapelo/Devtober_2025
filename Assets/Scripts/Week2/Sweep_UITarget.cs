using UnityEngine;
using UnityEngine.UI;

// Author : Auguste Paccapelo

public class Sweep_UITarget : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    private RawImage _renderer;

    // ----- Others ----- \\

    [SerializeField] private float _fadeSpeed;
    private bool _show = false;
    public float directionAngle;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start()
    {
        _renderer = GetComponent<RawImage>();
        ResetColor();
    }

    private void Update()
    {
        if (!_show) return;
        if (_renderer.color.a <= 0f)
        {
            ResetColor();
            return;
        }
        Color color = _renderer.color;
        color.a -= _fadeSpeed * Time.deltaTime;
        _renderer.color = color;
    }

    // ----- My Functions ----- \\

    public void Show()
    {
        if (_show) return;
        _show = true;
        Color color = _renderer.color;
        color.a = 1f;
        _renderer.color = color;
    }

    private void ResetColor()
    {
        Color color = _renderer.color;
        color.a = 0f;
        _renderer.color = color;
        _show = false;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        _show = true;
        Destroy(gameObject);
    }
}
