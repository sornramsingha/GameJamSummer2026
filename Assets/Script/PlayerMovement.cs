using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 7f;
    public float sprintSpeed = 12f;
    public float groundDrag = 5f;
    public float turnSpeed = 10f;
    public float speedMultiplier = 1f;

    private float movespeed;

    [Header("Stamina System")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 15f;
    public Slider staminaBar;

    [Header("Exhaustion System")]
    public float exhaustionDelay = 2f;
    private float exhaustionTimer = 0f;
    private bool isExhausted = false;

    [Header("Jumping")]
    public float jumpForce = 12f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;
    public float jumpStaminaCost = 20f;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    bool grounded;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

        currentStamina = maxStamina;
        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = currentStamina;
        }
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        StateHandler();
        UpdateStamina();

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded && !isExhausted && currentStamina >= jumpStaminaCost)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {
        bool isMoving = horizontalInput != 0 || verticalInput != 0;

        if (grounded && Input.GetKey(sprintKey) && !isExhausted && isMoving && currentStamina > 0)
        {
            state = MovementState.sprinting;
            movespeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            movespeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void UpdateStamina()
    {
        if (state == MovementState.sprinting)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            CheckExhaustion();
        }
        else
        {
            if (isExhausted)
            {
                exhaustionTimer -= Time.deltaTime;
                if (exhaustionTimer <= 0)
                {
                    isExhausted = false;
                }
            }
            else
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
        }
    }
    private void CheckExhaustion()
    {
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            isExhausted = true;
            exhaustionTimer = exhaustionDelay;
        }
    }

    private void MovePlayer()
    {
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (grounded)
        {
            rb.AddForce(moveDirection * movespeed * speedMultiplier * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection * movespeed * speedMultiplier * 10f * airMultiplier, ForceMode.Force);
        }
    }
    public void ApplyTemporarySlow(float multiplier, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SlowRoutine(multiplier, duration));
    }

    private System.Collections.IEnumerator SlowRoutine(float multiplier, float duration)
    {
        speedMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        speedMultiplier = 1f;
    }

    private void RotatePlayer()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
    }

    private void Jump()
    {
        currentStamina -= jumpStaminaCost;
        CheckExhaustion();

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}