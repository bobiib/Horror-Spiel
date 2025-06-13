using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;

    [Header("Stamina Settings")]
    public float maxStamina = 5f;
    public float staminaDrainRate = 1f;
    public float staminaRecoveryRate = 0.5f;

    [Header("Footstep Settings")]
    public AudioClip[] footstepClips;
    public float walkStepInterval = 0.5f;
    public float sprintStepInterval = 0.3f;

    private AudioSource audioSource;
    private float footstepTimer = 0f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isSprinting = false;
    private float currentStamina;
    private float moveSpeed;

    private bool staminaDepleted = false;
    private float staminaRechargeDelay = 5f;
    private float staminaRechargeTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
        moveSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        HandleMovement();
        HandleStamina();
        HandleFootsteps();
    }

    void HandleMovement()
    {
        bool isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleStamina()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(moveX, 0, moveZ);
        bool isMoving = moveDir.magnitude > 0.1f;

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentStamina > 0 && isMoving)
            isSprinting = true;

        if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina <= 0)
            isSprinting = false;

        if (isSprinting && isMoving)
        {
            moveSpeed = sprintSpeed;
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                staminaDepleted = true;
                staminaRechargeTimer = 0f;
                isSprinting = false;
            }
        }
        else
        {
            moveSpeed = walkSpeed;

            if (staminaDepleted)
            {
                staminaRechargeTimer += Time.deltaTime;
                if (staminaRechargeTimer >= staminaRechargeDelay)
                    staminaDepleted = false;
            }
            else
            {
                currentStamina += staminaRecoveryRate * Time.deltaTime;
                if (currentStamina > maxStamina)
                    currentStamina = maxStamina;
            }
        }
    }

    void HandleFootsteps()
    {
        bool isGrounded = controller.isGrounded;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(moveX, 0, moveZ);
        bool isMoving = moveDir.magnitude > 0.1f;

        if (isMoving && isGrounded)
        {
            footstepTimer -= Time.deltaTime;

            float currentStepInterval = isSprinting ? sprintStepInterval : walkStepInterval;

            if (footstepTimer <= 0f)
            {
                PlayFootstep();
                footstepTimer = currentStepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        int index = Random.Range(0, footstepClips.Length);
        audioSource.clip = footstepClips[index];
        audioSource.Play();
    }
}
