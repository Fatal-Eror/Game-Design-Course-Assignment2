using UnityEngine;

public class CameraController : MonoBehaviour
{
    private InputActionManager _inputAction;
    private GameObject player;
    private float _pitch = 0;
    private float _yaw = 0;
    public float cameraSpeed = 0.5f;

    private void Awake()
    {
        _inputAction = new InputActionManager();
        player = GameObject.FindGameObjectWithTag("Player");
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
        transform.eulerAngles = new Vector3(0, 90, 0);
    }

    private void LateUpdate()
    {
        // Update position
        transform.position = player.transform.position;

        // Get input
        _pitch += _inputAction.Gameplay.Look.ReadValue<Vector2>().y * cameraSpeed * -1;
        _yaw += _inputAction.Gameplay.Look.ReadValue<Vector2>().x * cameraSpeed;
        _pitch = Mathf.Clamp(_pitch, -5, 90);

        // Set Rotation
        transform.eulerAngles = new Vector3(_pitch, _yaw, 0);
    }
}