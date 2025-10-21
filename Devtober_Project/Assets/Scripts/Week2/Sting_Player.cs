using UnityEngine;
using System.Collections.Generic;

// Author : Auguste Paccapelo

public class Sting_Player : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    private Dictionary<GameObject, Vector3> _beesHited = new Dictionary<GameObject, Vector3>();

    // ----- Others ----- \\

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start() { }

    private void Update()
    {
        foreach (GameObject bee in _beesHited.Keys)
        {
            bee.transform.position = _beesHited[bee] + transform.position;
        }
    }

    // ----- My Functions ----- \\

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy") return;
        if (!_beesHited.ContainsKey(collision.gameObject)) 
            _beesHited.Add(collision.gameObject, collision.transform.position - transform.position);
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}