using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 movePos;
    [SerializeField] private GameObject target;

    [Header("Configuración Raycast")]

    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask interactableLayers;
    private Ray ray;

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
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameManager gameManager;

    [Header("Inputs")]
    private InputAction move, fire, sprint, interact;
    private InputAction uiAccept, pause;

    [SerializeField] private Transform cameraTransform;
    private Vector3 currentVelocity;

    [Header("PlayerStats")]
    private float baseHealth = 100;
    [HideInInspector] public float health;
    private float baseArmor = 100;
    [HideInInspector] public float armor;
    private float baseAmmo = 20;
    private float ammo;
    private float baseSpeed = 20;
    private float speed;
    private float baseDamage = 12;
    private float damage;


    [Header("Configuración de Sensibilidad")]
    [HideInInspector] public float mouseSensitivity = 500f;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();

        gameManager.MouseVisible(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        movePos = move.ReadValue<Vector2>();


        if (sprint.IsPressed()) speed = baseSpeed * 1.5f;
        else speed = baseSpeed;

        if (uiAccept.WasPressedThisFrame() && !dialogueManager.autoDialog)
        {
            dialogueManager.IsPlayingDialog();
        }
        else if (uiAccept.WasPressedThisFrame() && dialogueManager.autoDialog)
        {
            dialogueManager.IsAutoPlayingDialog();
        }

        HandleMouseLook();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    public void ResetPlayer()
    {
        speed = baseSpeed;
        damage = baseDamage;
        health = baseHealth;
        armor = baseArmor;

        gameManager.txtPlayerStats[0].text = health.ToString() + "%";
        gameManager.txtPlayerStats[1].text = armor.ToString() + "%";
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

    public void IncreaseOrDecreaseSpeed(float percent)
    {
        speed += speed * (percent / 100.0f);
    }

    public void IncreaseOrDecreaseHealth(float percent)
    {
        if (health >= baseHealth) health = baseHealth;
        else health += health * (percent / 100.0f);

        gameManager.txtPlayerStats[0].text = health.ToString() + "%";
    }

    public void IncreaseOrDecreaseDamage(float percent)
    {
        damage += damage * (percent / 100.0f);
    }

    public void IncreaseOrDecreaseArmor(float percent)
    {
        if (armor >= baseArmor) armor = baseArmor;
        else armor += armor * (percent / 100.0f);

        gameManager.txtPlayerStats[1].text = armor.ToString() + "%";
    }

    public void IncreaseOrDecreaseAmmo(float percent)
    {
        if (ammo >= baseAmmo) ammo = baseAmmo;
        else ammo += ammo * (percent / 100.0f);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        // Obtener el rayo desde el centro de la cámara
        ray = new Ray(this.transform.position, this.transform.forward);

        // Debug visual del rayo
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green, 0.1f);

        // Lanzar el raycast
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactableLayers))
        {
            target = hit.collider.gameObject;
            target.GetComponent<TestEnemieAnimations>().GetDamage(damage);
        }
        else target = null;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        if (context.performed && !gameManager.pauseOpen)
        {
            gameManager.OpenPause(true);
        }
        else if (context.performed && gameManager.pauseOpen)
        {
            gameManager.OpenPause(false);
        }
    }

    private void HandleMouseLook()
    {
        // Solo movimiento horizontal (Mouse X)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Rotación horizontal del cuerpo del jugador (izquierda/derecha)
        this.GetComponent<Rigidbody>().MoveRotation(this.transform.rotation * Quaternion.Euler(0f, mouseX, 0f));

        // Eliminamos toda la lógica de rotación vertical (Mouse Y)
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

        uiAccept = playerControls.UI.Accept;
        uiAccept.Enable();

        pause = playerControls.Player.Pause;
        pause.Enable();

        pause.performed += Pause;

        interact = playerControls.Player.Interact;
        interact.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        sprint.Disable();

        uiAccept.Disable();
        pause.Disable();

        interact.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.GuideTxtConfig(0);

        if (other.CompareTag("Secret")) gameManager.ShowTxtGuide(true);

        if (other.gameObject.layer == 8) gameManager.ShowTxtGuide(true);

        if (other.gameObject.layer == 12) gameManager.ShowTxtGuide(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && !other.GetComponent<DoorsController>().hasTrigger)
        {
            if (interact.IsPressed())
            {
                gameManager.ShowTxtGuide(false);
                other.GetComponent<DoorsController>().OpenDoor();
            }
        }
        else if (other.gameObject.layer == 8 && other.GetComponent<DoorsController>().hasTrigger)
        {
            if (interact.IsPressed())
            {
                if (other.GetComponent<DoorsController>().switchActivated == false)
                {
                    gameManager.GuideTxtConfig(1);
                }
                else
                {
                    gameManager.ShowTxtGuide(false);
                    other.GetComponent<DoorsController>().OpenDoor();
                }
            }
        }

        if (other.gameObject.layer == 12)
        {
            if (interact.IsPressed())
            {
                gameManager.GuideTxtConfig(2);
                other.GetComponent<SwitchController>().UnlockDoor();
            }
        }

        if (other.CompareTag("Secret"))
        {
            if (interact.IsPressed()) other.GetComponent<SecretController>().StarSecretDialog();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.ShowTxtGuide(false);
    }

    public void GetDamage(float damage)
    {
        if (armor > 0.0f)
        {
            armor -= damage;
            armor = 0.0f;
        }
        else
        {
            health -= damage;

            if (health <= 0.0f)
            {
                health = 0.0f;
                Debug.Log("Morí");
            }
        }

        
        gameManager.txtPlayerStats[0].text = health.ToString() + "%";
        gameManager.txtPlayerStats[1].text = armor.ToString() + "%";
    }
}


