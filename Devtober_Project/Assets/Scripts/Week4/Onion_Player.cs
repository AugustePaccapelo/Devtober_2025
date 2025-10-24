using UnityEngine;

// Author : Auguste Paccapelo

public class Onion_Player : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private ParticleSystem _particules;
    [SerializeField] private GameObject _onion;

    // ----- Others ----- \\

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start() { }

    private void Update()
    {
        ParticleSystem.EmissionModule mod = _particules.emission;
        mod.rateOverTime = 1 / (_onion.transform.position - transform.position).magnitude * 25;
        
    }

    // ----- My Functions ----- \\

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}