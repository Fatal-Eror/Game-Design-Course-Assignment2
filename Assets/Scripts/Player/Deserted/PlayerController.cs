using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private InputActionManager _inputAction;
    private Rigidbody _rb;

    private void Awake()
    {
        _inputAction = new InputActionManager();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        Vector2 moveData = _inputAction.Gameplay.Move.ReadValue<Vector2>() * _moveSpeed;
        Vector3 newForce = new Vector3(moveData.y, _rb.velocity.y, -moveData.x);
        _rb.velocity = newForce;
    }
}