using UnityEngine.InputSystem;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 15;
    private Vector3 move;

    public float gravity = -10f;
    public float jumpHeight = 2;
    private Vector3 velocity;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    public Animator animator;

    InputAction movement;
    InputAction jump;
    InputAction ride;

    public FixedJoystick joystick;
    public FixedJoystick rotationJoystick;

    public float rotationSpeed = 100f;
    public Transform cameraTransform;
    private float verticalRotation = 0f;
    public float verticalRotationLimit = 80f;

    void Start()
    {
        jump = new InputAction("Jump", binding: "<Gamepad>/a");
        jump.AddBinding("<keyboard>/space");

        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftStick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<keyboard>/w")
            .With("Up", "<keyboard>/upArrow")
            .With("Down", "<keyboard>/s")
            .With("Down", "<keyboard>/downArrow")
            .With("Left", "<keyboard>/a")
            .With("Left", "<keyboard>/leftArrow")
            .With("Right", "<keyboard>/d")
            .With("Right", "<keyboard>/rightArrow");

        movement.Enable();
        jump.Enable();
    }

    void Update()
    {
        // Movement logic
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        animator.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));

        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);

        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        if (isGrounded)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

        // Rotation logic for mobile (using rotationJoystick)
        float rotationX = rotationJoystick.Horizontal;
        float rotationY = rotationJoystick.Vertical;

        // Horizontal rotation (player)
        if (rotationX != 0)
        {
            transform.Rotate(Vector3.up * rotationX * rotationSpeed * Time.deltaTime);
        }

        // Vertical rotation (camera)
        if (rotationY != 0)
        {
            verticalRotation -= rotationY * rotationSpeed * Time.deltaTime;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }

        // Rotation logic for mouse
        if (Input.GetMouseButton(1)) // Right-click
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cash")
        {
            ScoreManager.scoreCount += 500;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Gun")
        {
            ScoreManager.scoreCount -= 0;
        }
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }
}
