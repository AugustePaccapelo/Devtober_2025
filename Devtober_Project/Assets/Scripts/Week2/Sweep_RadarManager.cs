using System.Collections.Generic;
using UnityEngine;

// Author : Auguste Paccapelo

public class Sweep_RadarManager : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    [SerializeField] private GameObject _gameTargetPrefab;
    [SerializeField] private GameObject _uiTargetPrefab;
    [SerializeField] private GameObject _MovingRadar;
    [SerializeField] private GameObject _gameTargertsParent;
    [SerializeField] private GameObject _uiTargetsParent;

    // Key : Game target; Value : UI Target
    public Dictionary<GameTarget, Sweep_UITarget> targets = new Dictionary<GameTarget, Sweep_UITarget>();

    // ----- Others ----- \\

    [SerializeField] private float _radarSpeed = 72f;
    [SerializeField] private int _wantedNumTargets = 5;
    [SerializeField] private float _targetsShowedAngleThreshold = 1f;

    private float _spawnRadius;
    private float _gameToRadarSizeRatio;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start()
    {
        Vector3 _screenSize = new Vector3(Screen.width, Screen.height);
        _screenSize = Camera.main.ScreenToWorldPoint(_screenSize);
        _spawnRadius = Mathf.Max(_screenSize.x, _screenSize.y) * 0.5f;
        RectTransform rectTrans = _MovingRadar.GetComponent<RectTransform>();
        float radarRadius = rectTrans.rect.width * 0.5f;
        
        _gameToRadarSizeRatio = radarRadius / _spawnRadius;
    }

    private void Update()
    {
        Vector3 angle = _MovingRadar.transform.eulerAngles;
        angle.z += _radarSpeed * Time.deltaTime;
        _MovingRadar.transform.eulerAngles = angle;

        for (int i = 0; i < _wantedNumTargets - targets.Count; i++)
        {
            NewTarget();
        }
        
        foreach (Sweep_UITarget uiTarget in targets.Values)
        {
            if (angle.z >= uiTarget.directionAngle - _targetsShowedAngleThreshold
                && angle.z <= uiTarget.directionAngle + _targetsShowedAngleThreshold)
            {
                uiTarget.Show();
            }
        }
    }

    // ----- My Functions ----- \\

    private void NewTarget()
    {
        Vector3 pos = GetRandomPosInCircle();
        GameObject gameTarget = Instantiate(_gameTargetPrefab, _gameTargertsParent.transform);
        GameTarget gameTargetComp = gameTarget.GetComponent<GameTarget>();
        gameTargetComp.radarManager = this;

        GameObject uiTarget = Instantiate(_uiTargetPrefab, _uiTargetsParent.transform);
        Sweep_UITarget uiTargetComp = uiTarget.GetComponent<Sweep_UITarget>();

        targets[gameTargetComp] = uiTargetComp;
        gameTarget.transform.position = pos;

        Vector2 direction = pos.normalized;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270) % 360;
        uiTargetComp.directionAngle = angle;

        uiTarget.transform.localPosition = GetRadarRelativePos(pos);
    }

    private Vector3 GetRandomPosInCircle()
    {
        float r = _spawnRadius * Random.value;
        float angle = Random.value * 2 * Mathf.PI;
        Vector3 pos = new Vector3(Mathf.Cos(angle) * r, Mathf.Sin(angle) * r);
        return pos;
    }

    private Vector3 GetRadarRelativePos(Vector3 gamePos)
    {
        return gamePos * _gameToRadarSizeRatio;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
