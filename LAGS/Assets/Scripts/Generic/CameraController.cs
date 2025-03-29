using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Configuración de Sensibilidad")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private bool invertYAxis = false;

    [Header("Límites de Rotación")]
    [SerializeField] private float minVerticalAngle = -90f;
    [SerializeField] private float maxVerticalAngle = 90f;

    private Transform playerBody;
    private float xRotation = 0f;

    [SerializeField] private float rotationSmoothness = 5f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerBody = transform.parent;
    }

    private void Update()
    {
        HandleMouseLook();
    }
    private void HandleMouseLook()
    {
     
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        
        if (invertYAxis) mouseY *= -1;

        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void SmoothMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        
        Quaternion targetVertical = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetVertical, rotationSmoothness * Time.deltaTime);

        
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
