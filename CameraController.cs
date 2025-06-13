using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Mouse & Camera Settings")]
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    [Header("Head Bobbing Settings")]
    public float bobFrequency = 7f;
    public float bobAmplitude = 0.07f;

    private float xRotation = 0f;
    private float defaultCameraYPos;
    private float bobTimer = 0f;

    private CharacterController playerController;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        defaultCameraYPos = transform.localPosition.y;

        playerController = playerBody.GetComponent<CharacterController>();
        if (playerController == null)
            Debug.LogError("CharacterController auf playerBody nicht gefunden!");
    }

    void Update()
    {
        HandleMouseLook();
        HandleHeadBobbing();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void HandleHeadBobbing()
    {
        if (playerController == null)
            return;

        Vector3 horizontalVelocity = new Vector3(playerController.velocity.x, 0, playerController.velocity.z);
        bool isMoving = horizontalVelocity.magnitude > 0.1f;
        bool isGrounded = playerController.isGrounded;

        if (isMoving && isGrounded)
        {
            float speedFactor = Mathf.Max(horizontalVelocity.magnitude / 5f, 0.1f);
            bobTimer += bobFrequency * Time.deltaTime * speedFactor;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;

            Vector3 camPos = transform.localPosition;
            camPos.y = defaultCameraYPos + bobOffset;
            transform.localPosition = camPos;
        }
        else
        {
            bobTimer = 0f;
            Vector3 camPos = transform.localPosition;
            camPos.y = Mathf.Lerp(camPos.y, defaultCameraYPos, Time.deltaTime * 5f);
            transform.localPosition = camPos;
        }
    }
}