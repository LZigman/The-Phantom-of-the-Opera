using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 inputAmount;
    private Vector3 targetForwardDirection;
    private Vector3 movementDirection;
    private Vector3 smoothInputMovement;
    private float xRot;
    private bool onGround;
    private Rigidbody rb;

    [Header("Character stats")]
    public float health = 100;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float jumpForce = 2.5f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        inputAmount = context.ReadValue<Vector2>();
        movementDirection = new Vector3(inputAmount.x, 0, inputAmount.y);
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    #region Performing

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    #endregion

    #region Movement

    private void Movement()
    {
        smoothInputMovement = Vector3.Lerp(smoothInputMovement, movementDirection, Time.deltaTime * moveSpeed);
        rb.MovePosition(transform.position + smoothInputMovement);
    }

    private void Rotation()
    { 
        Debug.Log(movementDirection);
        targetForwardDirection = movementDirection;
        transform.rotation = Quaternion.Slerp
        (transform.rotation, Quaternion.LookRotation(targetForwardDirection), Time.deltaTime * rotationSpeed);
    }

    #endregion

    #region On ground

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetJumpState(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetJumpState(false);
        }
    }

    private void SetJumpState(bool grounded)
    {
        this.onGround = grounded;
    }

    #endregion
}
