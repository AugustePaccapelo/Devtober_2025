using UnityEngine;

// Author : Auguste Paccapelo

public class Reckless_Wall : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    private ParticleSystem _particleSystem;
    private SpriteRenderer _renderer;

    // ----- Others ----- \\

    private bool _isEmitting = false;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_isEmitting) return;
        
        if (!_particleSystem.isPlaying) Destructor();
    }

    // ----- My Functions ----- \\

    public void DestroyWall()
    {
        _renderer.gameObject.SetActive(false);
        _particleSystem.Play();
        _isEmitting = true;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
