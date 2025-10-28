using UnityEngine;

// Author : Auguste Paccapelo

public class Starfish_Starfish : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    [SerializeField] private Starfish_Leg _legPrefab;

    // ----- Objects ----- \\

    [SerializeField] private Starfish_Leg[] _legs;
    [SerializeField] private Transform _legsContainer;
    private Vector2[] _positions;
    private Vector3[] _rotations;

    // ----- Others ----- \\

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void OnEnable()
    {
        Starfish_Leg.OnAllReady += Init;
    }

    private void OnDisable()
    {
        Starfish_Leg.OnAllReady -= Init;
    }

    private void Init()
    {
        Debug.Log("man");
        int length = _legs.Length;
        _positions = new Vector2[length];
        _rotations = new Vector3[length];
        for (int i = 0; i < length; i++)
        {
            _positions[i] = _legs[i]._endPos;
            _rotations[i] = _legs[i].transform.eulerAngles;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        int length = _legs.Length;
        for (int i = 0; i < length; i++)
        {
            if (_legs[i].isClicked)
            {
                CreateNewLeg(i);
            }
        }
    }

    // ----- My Functions ----- \\

    private void CreateNewLeg(int index)
    {
        _legs[index] = Instantiate(_legPrefab, _legsContainer);
        _legs[index].transform.position = _positions[index];
        _legs[index].transform.eulerAngles = _rotations[index];
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
