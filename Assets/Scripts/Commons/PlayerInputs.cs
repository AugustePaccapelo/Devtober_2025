using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Author : Auguste Paccapelo

public class PlayerInputs : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Paths ----- \\

    // ----- Objects ----- \\

    public Rigidbody2D rigidBody { get; private set; }
    private SpriteRenderer _renderer;

    // ----- Others ----- \\

    public bool isCrouch { get; private set; } = false;

    [SerializeField] private PlayerControlType _playerControlType;
    [SerializeField] private Dictionary<InputAxis, bool> _ignoreAxis = new Dictionary<InputAxis, bool>()
    {
        {InputAxis.PosX, false }, {InputAxis.NegX, false}, {InputAxis.PosY, false }, {InputAxis.NegY, false}
    };

    private bool _canJump = false;
    private bool _canCrouch = false;
    private bool _takeYInput = false;

    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _maxInputSpeed = 5f;
    [SerializeField] private float _jumpForce = 1000f;

    private Vector2 _currentInputDirection;

    private Color _normalColor;
    [SerializeField] private Color _crouchColor = Color.green;

    // ---------- FUNCTIONS ---------- \\

    // ----- Awake & Start & Update ----- \\

    private void Awake() { }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _normalColor = _renderer.color;

        switch (_playerControlType)
        {
            case PlayerControlType.Physics:
                SetControlTypePhysic();
                break;
            case PlayerControlType.Plan2D:
                SetControlTypePlan2D();
                break;
        }
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch (_playerControlType)
        {
            case PlayerControlType.Physics:
                MovePlayerPhysics();
                break;
            case PlayerControlType.Plan2D:
                MovePlayerPlan2D();
                break;
        }
    }

    // ----- My Functions ----- \\

    private void MovePlayerPlan2D()
    {
        Vector2 inputVelo = GetInputDirection() * _maxInputSpeed;
        rigidBody.linearVelocity = inputVelo;
    }

    private void MovePlayerPhysics()
    {
        float InputVelocity = GetInputDirection().x * _acceleration;
        if ((rigidBody.linearVelocityX >= _maxInputSpeed && InputVelocity > 0)
            || (rigidBody.linearVelocityX <= -_maxInputSpeed && InputVelocity < 0))
        {
            InputVelocity = 0f;
        }

        rigidBody.linearVelocityX += InputVelocity;
    }

    private Vector2 GetInputDirection()
    {
        Vector2 currentDirec = _currentInputDirection;

        if (!_takeYInput) currentDirec.y = 0;
        else
        {
            if (_ignoreAxis[InputAxis.PosY] && currentDirec.y > 0) currentDirec.y = 0;
            if (_ignoreAxis[InputAxis.NegY] && currentDirec.y < 0) currentDirec.y = 0;
        }

        if (_ignoreAxis[InputAxis.PosX] && currentDirec.x > 0) currentDirec.x = 0;
        if (_ignoreAxis[InputAxis.NegX] && currentDirec.x < 0) currentDirec.x = 0;

        return currentDirec;
    }

    private void SetControlTypePhysic()
    {
        _canJump = true;
        _canCrouch = true;
        _takeYInput = false;
        rigidBody.gravityScale = 1f;
    }

    private void SetControlTypePlan2D()
    {
        _canJump = false;
        _canCrouch = true;
        _takeYInput = true;
        rigidBody.gravityScale = 0f;
    }

    private void OnMove(InputValue inputValue)
    {
        _currentInputDirection = inputValue.Get<Vector2>();
    }

    private void OnCrouch(InputValue inputValue)
    {
        if (!_canCrouch) return;
        isCrouch = inputValue.Get<float>() > 0f ? true : false;
        _renderer.color = isCrouch ? _crouchColor : _normalColor;
    }

    private void OnJump()
    {
        if (!_canJump) return;
    }

    // ----- Destructor ----- \\

    public void Destructor()
    {
        Destroy(gameObject);
    }
}
