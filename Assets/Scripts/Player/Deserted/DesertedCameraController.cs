using UnityEngine;

public class DesertedCameraController : MonoBehaviour
{
    private InputActionManager _inputAction;
    private GameObject player;

    //Camera Move and Rotation
    [Header("CameraAttributes")]
    public float cameraOffsetOnX = 0;

    public float cameraOffsetOnY = 2;
    public float cameraOffsetOnZ = -10f;
    public float sensitive = 1;
    public float limitOnMinAngle = -5;
    public float limitOnMaxAngle = 90;
    private float _pitch = 0;
    private float _yaw = 0;

    private void Awake()
    {
        _inputAction = new InputActionManager();
        player = GameObject.FindGameObjectWithTag("Player");
        //  _playerRigidBody = player.GetComponent<Rigidbody>();

        transform.position = new Vector3(player.transform.position.x + cameraOffsetOnX,
            player.transform.position.y + cameraOffsetOnY,
            player.transform.position.z + cameraOffsetOnZ);
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Player Movement

        // Get input
        _yaw += _inputAction.Gameplay.Look.ReadValue<Vector2>().x * sensitive;
        _pitch -= _inputAction.Gameplay.Look.ReadValue<Vector2>().y * sensitive;

        // Limit yaw
        _pitch = Mathf.Clamp(_pitch, limitOnMinAngle, limitOnMaxAngle);

        // Camera Rotation and Location
        Quaternion newRotation = Quaternion.Euler(_pitch, _yaw, 0);
        Vector3 newPosition = newRotation * new Vector3(cameraOffsetOnX, cameraOffsetOnY, cameraOffsetOnZ)
                            + player.transform.position;

        transform.position = newPosition;
        transform.rotation = newRotation;
    }
}