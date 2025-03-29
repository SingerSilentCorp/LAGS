using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private Vector3 movePos;

    private Rigidbody rb;

    private PlayerInputActions playerControls;
    private InputAction move, fire;
    
    private float speed;

    public float MouseSensitivity;

    public GameObject target;

    [Header("Configuración Raycast")]
    public float raycastDistance = 3f; // Distancia máxima de detección
    public LayerMask interactableLayers; // Capas con las que puede interactuar

    [Header("Daño")]
    public int damageAmount = 10;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        speed = 20;

        playerControls = new PlayerInputActions();
    }

    void Update()
    {
        movePos = move.ReadValue<Vector2>();

        // Lanzar el raycast desde la cámara (o posición del jugador)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Dibujar el raycast en el editor para debug
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);

        // Verificar si el raycast golpea algo
        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayers))
        {
            target = hit.collider.gameObject;
            // Mostrar feedback visual (opcional)
            Debug.Log("Objeto detectado: " + hit.collider.name);
        }
        else
        {
            target = null;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(movePos.x * speed, 0, movePos.y * speed);
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


