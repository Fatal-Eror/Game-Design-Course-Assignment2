using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerCharacter : MonoBehaviour
{
    private InputActionManager _inputAction;
    private CharacterMovement _characterMovement;
    private GameObject _photographer;
    private LevelState _levelState;

    // Ground Detect
    [SerializeField] private GameObject groundCheckPoint;
    public float checkSphereRadius = 5f;
    private bool _isOnGround = false;
    public LayerMask groundLayer;

    private void Awake()
    {
        _inputAction = new InputActionManager();
        _characterMovement = GetComponent<CharacterMovement>();
        _photographer = GameObject.FindGameObjectWithTag("Photographer");

        _inputAction.Gameplay.Jump.performed += Jump;
        _inputAction.Gameplay.ReStart.performed += Restart;

        _levelState = GameObject.FindAnyObjectByType<LevelState>();
        _levelState.OnLevelStart += LevelStart;
        _levelState.OnLevelEnd += LevelEnd;
    }

    private void Restart(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    private void LevelEnd()
    {
        _inputAction.Disable();
    }

    private void LevelStart()
    {
        _inputAction.Enable();
    }

    private void Update()
    {
        // Check if it is on ground
        Collider[] groundCollider = Physics.OverlapSphere(groundCheckPoint.transform.position, checkSphereRadius, groundLayer);

        if (groundCollider.Length > 0)
        {
            _isOnGround = true;
        }
        else
        {
            _isOnGround = false;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (_isOnGround)
        {
            _characterMovement.CharacterJump();
        }
    }

    private void FixedUpdate()
    {
        if (_inputAction.Gameplay.Move.IsPressed())
        {
            UpdateMovementAndRotation();
        }
    }

    private void UpdateMovementAndRotation()
    {
        // Get Photographer position
        Vector3 cameraForward = _photographer.transform.forward;
        Vector3 cameraRight = _photographer.transform.right;

        // Check if the camera is vertical
        bool isCameraVertical = Mathf.Abs(Vector3.Dot(cameraForward, Vector3.up)) > 0.98f;

        Vector3 forward;
        Vector3 right;

        if (!isCameraVertical)
        {
            // Get forward and right from camera when it's not vertical
            forward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            right = new Vector3(cameraRight.x, 0, cameraRight.z).normalized;
        }
        else
        {
            // Use character's forward and right when camera is vertical
            forward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
            right = new Vector3(transform.right.x, 0, transform.right.z).normalized;
        }

        // Get Input
        Vector2 input = _inputAction.Gameplay.Move.ReadValue<Vector2>();

        // Make character move based on camera or character's direction
        _characterMovement.CharacterMove(input.y * forward + input.x * right, _isOnGround);

        // Sync character rotation and camera rotation
        if (!isCameraVertical)
        {
            _characterMovement.ChangeRotation(_photographer.transform.eulerAngles);
        }
    }
}