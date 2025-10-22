using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Author : Auguste Paccapelo

public class Deal_CoinsManager : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Singleton ----- \\

    public static Deal_CoinsManager Instance {get; private set;}

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private TextMeshProUGUI _textCoins;
    [SerializeField] private TextMeshProUGUI _textInventory;

    // ----- Others ----- \\

    [SerializeField] private int _startCoins = 10;
    private List<string> _listObjects = new List<string>() { "Square", "Triangle", "Circle"};

    private int _numCoins;
    public int coins => _numCoins;
    public Action<int, Deal_ObjectsTypes> MakeDeal;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable()
    {
        MakeDeal += OnMakeDeal;
    }

    private void OnDisable()
    {
        MakeDeal -= OnMakeDeal;
    }

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Debug.Log(nameof(Deal_CoinsManager) + " Instance already exist, destorying last added.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _numCoins = _startCoins;
        _textCoins.text = _numCoins.ToString();
    }

    void Start() { }

    void Update() { }

    // ----- My Functions ----- \\

    private void OnMakeDeal(int prize, Deal_ObjectsTypes objectType)
    {
        _numCoins -= prize;
        _textCoins.text = _numCoins.ToString();
        string text = _textInventory.text;
        text += ", " + ObjTypeToString(objectType);
        _textInventory.text = text;
    }

    private string ObjTypeToString(Deal_ObjectsTypes objType)
    {
        return _listObjects[(int)objType];
    }

    // ----- Destructor ----- \\

    protected virtual void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}