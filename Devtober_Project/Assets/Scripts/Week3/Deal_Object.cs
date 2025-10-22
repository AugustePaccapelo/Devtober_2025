using UnityEngine;

// Author : Auguste Paccapelo

public class Deal_Object : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    private Deal_CoinsManager _coinManager;

    // ----- Others ----- \\

    [SerializeField] private int _prize = 1;
    [SerializeField] private Deal_ObjectsTypes _objectType;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        _coinManager = Deal_CoinsManager.Instance;
    }

    private void Update() { }

    // ----- My Functions ----- \\

    private void OnMouseDown()
    {
        if (_coinManager.coins >= _prize)
        {
            _coinManager.MakeDeal(_prize, _objectType);
        }
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}