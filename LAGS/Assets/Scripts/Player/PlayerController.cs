using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private Vector3 movePos;

    private Rigidbody rb;

    private PlayerInputActions playerControls;
    private InputAction move, fire;
    
    

    public float MouseSensitivity;

    public GameObject target;

    [Header("Configuración Raycast")]
    public float raycastDistance = 3f; // Distancia máxima de detección
    public LayerMask interactableLayers; // Capas con las que puede interactuar

    [Header("Daño")]
    public int damageAmount = 10;

    private Camera mainCamera;






    [Header("Movement Settings")]
    private float speed = 20;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float deceleration = 12f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    private Vector3 currentVelocity;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        movePos = move.ReadValue<Vector2>();

        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);

        
        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayers))
        {
            target = hit.collider.gameObject;
            Debug.Log("Objeto detectado: " + hit.collider.name);
        }
        else
        {
            target = null;
        }
    }

    private void FixedUpdate()
    {
        //rb.linearVelocity = new Vector3(movePos.x * speed, 0, movePos.y * speed);

        PlayerMovement();
    }

    private void RotateTowardsDirection(Vector3 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void PlayerMovement()
    {
        Vector3 inputDirection = new Vector3(movePos.x, 0f, movePos.y).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

            
            Vector3 targetVelocity = moveDirection * speed;

            
            currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

            
            //RotateTowardsDirection(moveDirection);
        }
        else
        {
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        currentVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = currentVelocity;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if(target != null) Debug.Log("Fire: " + target.name);
    }


    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Attack;
        fire.Enable();

        fire.performed += Fire;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }
}


