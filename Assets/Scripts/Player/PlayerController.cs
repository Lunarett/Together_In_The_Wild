using UnityEngine;
using Photon.Pun;
using PulsarDevKit.Scripts.Debug;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5.0f;

    private Rigidbody2D _rb2d;
    private PhotonView _pv;
    private PlayerStateMachine _sm;
    private Camera _camera;
    private Vector2 _targetPosition;
    private PlayerActionHandler _actionHandler;

    public Camera PlayerCamera => _camera;
    public PlayerStateMachine StateMachine => _sm;
    public PlayerActionHandler ActionHandler => _actionHandler;

    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
        _rb2d = GetComponent<Rigidbody2D>();
        _actionHandler = GetComponent<PlayerActionHandler>();
        _camera = GetComponentInChildren<Camera>();
        _targetPosition = _rb2d.position;
    }

    private void Start()
    {
        _sm = new PlayerStateMachine(this);

        // Initialize All States
        _sm.InitializeState(new IdleState());
        _sm.InitializeState(new KeyMovementState());
        _sm.InitializeState(new ClickMovementState());
        _sm.InitializeState(new InteractState());

        // Set starting state
        _sm.ChangeState(EStateID.Idle);
    }

    private void Update()
    {
        if(!_pv.IsMine) return;
        _sm.UpdateStateMachine();

        UpdatePlayerMovement();
    }

    private void UpdatePlayerMovement()
    {
        if(Vector2.Distance(_targetPosition, _rb2d.position) < 0.5f) return;
        Vector2 direction = (_targetPosition - _rb2d.position).normalized;
        _rb2d.MovePosition(_rb2d.position + direction * _movementSpeed * Time.fixedDeltaTime);
    }

    public void SetTargetLocation(Vector2 newLocation)
    {
        _targetPosition = newLocation;
    }
}