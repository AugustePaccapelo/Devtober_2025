using UnityEngine;

// Author : Auguste Paccapelo

public class Shredded_Player : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    [SerializeField] private GameObject _particulePrefab;

    // ----- Objects ----- \\

    [SerializeField] private GameObject _particuleContainer;

    // ----- Others ----- \\

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected virtual void Awake() { }

    protected virtual void Start() { }

    protected virtual void Update() { }

    // ----- My Functions ----- \\

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paper"))
        {
            GameObject newParticule = Instantiate(_particulePrefab, _particuleContainer.transform);
            newParticule.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
        }
    }

    // ----- Destructor ----- \\

    protected virtual void OnDestroy() { }
}