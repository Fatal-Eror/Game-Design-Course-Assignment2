using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody _rb;

    public float maximumSpeedOnGround;
    public float maximumSpeedInJumping;

    public float accelerateSpeedOnGround = 5f;
    public float accelerateSpeedInJumping = 1f;

    public float jumpForce = 5f;
    public float extraFallForce = -9.81f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void CharacterMove(Vector3 input)
    {
        if (_rb.velocity.magnitude <= maximumSpeedOnGround)
        {
            _rb.AddForce(input * accelerateSpeedOnGround);
        }
    }

    public void CharacterMove(Vector3 input, bool isOnGround)
    {
        if (isOnGround)
        {
            if (_rb.velocity.magnitude <= maximumSpeedOnGround)
            {
                _rb.AddForce(input * accelerateSpeedOnGround);
            }
        }
        else
        {
            if (_rb.velocity.magnitude <= maximumSpeedInJumping)
            {
                _rb.AddForce(input * accelerateSpeedInJumping);
            }
            else
            {
                // Decrese character speed in jumping
                _rb.velocity = new Vector3(maximumSpeedInJumping * (_rb.velocity.x / maximumSpeedOnGround)
                    , _rb.velocity.y,
                    maximumSpeedInJumping * (_rb.velocity.z / maximumSpeedOnGround));
            }
        }
    }

    public void ChangeRotation(Vector3 rotation)
    {
        transform.eulerAngles = new Vector3(0, rotation.y, 0);
    }

    public void CharacterJump()
    {
        _rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    // Make drop phase in jumping more nature
    private void FixedUpdate()
    {
        if (_rb.velocity.y < -0.1)
        {
            _rb.AddForce(new Vector3(0, extraFallForce, 0));
        }

    }
}