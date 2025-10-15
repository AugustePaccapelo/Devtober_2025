using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

// Author : Auguste Paccapelo

public class Crown_Player : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    [SerializeField] private GameObject _crownPos;
    private Crown_Crown _crown;

    // ----- Others ----- \\

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start() { }

    private void Update() { }

    // ----- My Functions ----- \\

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Crown") return;
        _crown = collision.gameObject.GetComponent<Crown_Crown>();
        _crown.CrownPickedUp(_crownPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy") return;
        _crown.CrownDropped(transform.position);
        _crown = null;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
