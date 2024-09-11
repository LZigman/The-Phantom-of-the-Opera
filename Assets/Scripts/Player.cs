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
    private bool isSprinting, isCrouching;
    public bool isInteracting;
    
    [Header("Player components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    [Header("Character stats")]
    public float health = 100;
    [SerializeField] private float defaultMoveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float jumpForce = 10f;
    [Range(0f, 1f)]
    [SerializeField] private float speedModifier = 0.5f;
    private float moveSpeed;
    
    [Header("Death")]
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float deathTimeScaler = 0.2f;
    
    [Header("Audio")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip deathClip;
    
    private static readonly int Jump = Animator.StringToHash(JumpAnimationVariable);
    private static readonly int Move = Animator.StringToHash(MoveAmountAnimationVariable);
    private const string MoveAmountAnimationVariable = "Move amount";
    private const string JumpAnimationVariable = "Jump";

	private void Start()
    {
        Time.timeScale = 1;
        targetForwardDirection = transform.forward;
        rb = GetComponent<Rigidbody>();
        moveSpeed = defaultMoveSpeed;
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
    
    public void OnSprint (InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isSprinting = !isSprinting;
            if (isSprinting == true && isCrouching == true)
            {
                isCrouching = false;
            }
			Debug.Log("Sprint pressed!");
            ApplySpeedModifier();
		}
    }
    
    public void OnInteract (InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isInteracting = true;
            Debug.Log("Interacting!");
        }
    }
    
    public void OnCrouch (InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isCrouching = !isCrouching;
            if (isCrouching == true && isSprinting == true)
            {
                isSprinting = false;
            }
            Debug.Log("Crouch pressed!");
    		ApplySpeedModifier();
        }
	}
    
    private void ApplySpeedModifier ()
    {
		if (isCrouching == true)
		{
			moveSpeed = defaultMoveSpeed * speedModifier;
            Debug.Log("Crouching!");
		}
		if (isSprinting == true)
		{
			moveSpeed = defaultMoveSpeed / speedModifier;
            Debug.Log("Sprinting!");
		}
        else if ((isSprinting == false && isCrouching == false))
        {
            moveSpeed = defaultMoveSpeed;
        }
        Debug.Log($"moveSpeed: {moveSpeed}");
	}
    
    #region Performing

    private void FixedUpdate()
    {
        Movement();
        Rotation();
        
        //Animation
        float moveAmount = Mathf.Clamp01(Mathf.Abs(inputAmount.x) + Mathf.Abs(inputAmount.y));
        animator.SetFloat(Move, moveAmount);
    }

    #endregion

    #region Movement

    private void Movement()
    {
        // move player by moveSpeed
        rb.position += movementDirection * (moveSpeed * Time.fixedDeltaTime);
    }

    private void Rotation()
    { 
        if (movementDirection != Vector3.zero)
            targetForwardDirection = movementDirection;
        rb.rotation = Quaternion.Slerp
            (rb.rotation, Quaternion.LookRotation(targetForwardDirection), Time.deltaTime * rotationSpeed);
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
        animator.SetBool(Jump, !onGround);
    }

    #endregion
    
    #region Checks

    public void CheckIfDead()
    {
        if (health <= 0)
        {
            // Disable player
            gameObject.SetActive(false);

            // Play audio clip
            playerAudioSource.clip = deathClip;
            playerAudioSource.Play();

            // Instantiate particle death effect
            Destroy(Instantiate(deathEffect, transform.position, Quaternion.Euler(-90, 0, 0)), 2);

            // Set time to sloaw down
            Time.timeScale = deathTimeScaler;

            // Disable this script
            this.enabled = false;
        }
    }

    #endregion
}
