using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float xRot;
    private bool onGround;
    private Rigidbody rb;

    [Header("Character stats")]
    public float health = 100;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float jumpForce = 2.5f;
    [SerializeField] private float gravity = -9.81f;

    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        
        // Find the references to the "Move" and "Jump" actions
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }
    
    #region Performing

    private void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        // your movement code here

        Movement(moveValue);
        Rotation(moveValue);
        Jump();
    }

    #endregion

    #region Movement

    private void Movement(Vector2 input)
    {
        // playerVelocity = moveDirection * moveSpeed;
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        
        playerVelocity.y += gravity * Time.deltaTime;
        
        if (controller.isGrounded && playerVelocity.y <  0)
        {
            playerVelocity.y -= 1f;
        }
        
        controller.Move((transform.TransformDirection(moveDir) * moveSpeed + playerVelocity) * Time.deltaTime);
    }

    private void Rotation(Vector2 input)
    {
        Vector3 targetForwardDirection = new Vector3(input.x, 0, input.y);
        transform.rotation = Quaternion.Slerp
        (transform.rotation, Quaternion.LookRotation(targetForwardDirection), Time.deltaTime * rotationSpeed);
    }

    private void Jump()
    {
        if (!onGround) return;

        if (jumpAction.IsPressed())
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3f * gravity);
        }
    }

    #endregion

    #region On ground

    private void OnCollisionStay(Collision collision)
    {
        SetJumpState(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        SetJumpState(false);
    }

    private void SetJumpState(bool grounded)
    {
        this.onGround = grounded;
    }

    #endregion
}
