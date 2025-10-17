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

    [SerializeField]
    private List<InputAxis> _ignoreAxisList = new List<InputAxis>();

    private bool _canJump = false;
    [SerializeField] private bool _canCrouch = true;
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
        if (_renderer != null) _normalColor = _renderer.color;

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
        Vector2 InputVelocity = GetInputDirection() * _maxInputSpeed;

        Handle2DXMove(InputVelocity.x);

        Handle2DYMove(InputVelocity.y);
    }

    private void Handle2DXMove(float xVelo)
    {
        bool ignorePosX = _ignoreAxisList.Contains(InputAxis.PositiveX);
        bool ignoreNegX = _ignoreAxisList.Contains(InputAxis.NegativeX);
        if (ignorePosX && ignoreNegX) return;

        if (!ignorePosX && rigidBody.linearVelocityX >= _maxInputSpeed && xVelo > 0)
        {
            rigidBody.linearVelocityX = 0f;
        }
        else if (!ignoreNegX && rigidBody.linearVelocityX <= -_maxInputSpeed && xVelo < 0)
        {
            rigidBody.linearVelocityX = 0f;
        }
        else rigidBody.linearVelocityX = xVelo;
    }

    private void Handle2DYMove(float yVelo)
    {
        bool ignorePosY = _ignoreAxisList.Contains(InputAxis.PositiveY);
        bool ignoreNegY = _ignoreAxisList.Contains(InputAxis.NegativeY);
        if (ignorePosY && ignoreNegY) return;

        if (!ignorePosY && rigidBody.linearVelocityY >= _maxInputSpeed && yVelo > 0)
        {
            rigidBody.linearVelocityY = 0f;
        }
        else if (!ignoreNegY && rigidBody.linearVelocityY <= -_maxInputSpeed && yVelo < 0)
        {
            rigidBody.linearVelocityY = 0f;
        }
        else rigidBody.linearVelocityY = yVelo;
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
            if (_ignoreAxisList.Contains(InputAxis.PositiveY) && currentDirec.y > 0) currentDirec.y = 0;
            if (_ignoreAxisList.Contains(InputAxis.NegativeY) && currentDirec.y < 0) currentDirec.y = 0;
        }

        if (_ignoreAxisList.Contains(InputAxis.PositiveX) && currentDirec.x > 0) currentDirec.x = 0;
        if (_ignoreAxisList.Contains(InputAxis.NegativeX) && currentDirec.x < 0) currentDirec.x = 0;

        return currentDirec;
    }

    private void SetControlTypePhysic()
    {
        _canJump = true;
        _takeYInput = false;
        rigidBody.gravityScale = 1f;
    }

    private void SetControlTypePlan2D()
    {
        _canJump = false;
        _takeYInput = true;
        rigidBody.gravityScale = 0f;
    }

    private void OnMove(InputValue inputValue)
    {
        _currentInputDirection = inputValue.Get<Vector2>().normalized;
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
