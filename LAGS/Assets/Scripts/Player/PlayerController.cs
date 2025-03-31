using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 movePos;
    public GameObject target;

    [Header("Configuración Raycast")]
    public float raycastDistance = 3f;
    public LayerMask interactableLayers;

    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float deceleration = 12f;

    [Header("References")]
    private Rigidbody rb;
    private PlayerInputActions playerControls;
    private InputAction move, fire, sprint;

    [SerializeField] private Transform cameraTransform;
    private Vector3 currentVelocity;

    [Header("PlayerStats")]
    private float baseHealth, health = 100;
    private float baseSpeed, speed = 20;
    private float baseDamage, damage = 10;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
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
        else target = null;

        if (sprint.IsPressed()) speed += (speed * 0.5f);
        else speed = baseSpeed;
    }

    private void FixedUpdate()
    {
       PlayerMovement();
    }

    private void PlayerMovement()
    {
        //Vector3 inputDirection = new Vector3(movePos.x, 0f, movePos.y).normalized;

        //if (inputDirection.magnitude >= 0.1f)
        //{

        //    Vector3 cameraForward = cameraTransform.forward;
        //    Vector3 cameraRight = cameraTransform.right;

        //    cameraForward.y = 0f;
        //    cameraRight.y = 0f;
        //    cameraForward.Normalize();
        //    cameraRight.Normalize();

        //    Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;


        //    Vector3 targetVelocity = moveDirection * speed;


        //    currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        //}
        //else
        //{
        //    currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        //}

        //currentVelocity.y = rb.linearVelocity.y;
        //rb.linearVelocity = currentVelocity;

       float horizontal = Input.GetAxis("Horizontal"); // A/D o Flechas
        float vertical = Input.GetAxis("Vertical"); // W/S o Flechas

        // Dirección según la cámara
        Vector3 direction = (cameraTransform.forward * vertical + cameraTransform.right * horizontal).normalized;
        direction.y = 0; // Evitar que el jugador se mueva en Y

        // Aplicar movimiento sin afectar la gravedad
        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (target != null) Debug.Log("Fire: " + target.name);
    }

    private void IncreaseOrDecreaseSpeed(float percent)
    {
        speed += speed * (percent / 100.0f);
    }

    private void IncreaseOrDecreaseHealth(float percent)
    {
        health += health * (percent / 100.0f);
    }

    private void IncreaseOrDecreaseDamage(float percent)
    {
        damage += damage * (percent / 100.0f);
    }


    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Attack;
        fire.Enable();

        fire.performed += Fire;

        sprint = playerControls.Player.Sprint;
        sprint.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        sprint.Disable();
    }
}


