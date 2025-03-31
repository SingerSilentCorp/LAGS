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
    private float baseRotationSpeed = 10f;
    [SerializeField] private float rotationSpeed;
    private float baseAcceleration = 8f;
    [SerializeField] private float acceleration;
    private float baseDeceleration = 12f;
    [SerializeField] private float deceleration;

    [Header("References")]
    private Rigidbody rb;
    private PlayerInputActions playerControls;
    private InputAction move, fire, sprint;

    [SerializeField] private Transform cameraTransform;
    private Vector3 currentVelocity;

    [Header("PlayerStats")]
    private float baseHealth = 100;
    private float health;
    private float baseSpeed = 20;
    private float speed;
    private float baseDamage = 10;
    private float damage;

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

    private void ResetPlayer()
    {
        speed = baseSpeed;
        damage = baseDamage;
        health = baseHealth;
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


