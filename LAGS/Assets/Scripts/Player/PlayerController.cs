using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    

    SoundManager _sound;

    private Vector3 movePos;
    [SerializeField] private GameObject target;

    [Header("Configuración Raycast")]

    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask interactableLayers;

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
    private InputAction move, fire ,sprint, interact;
    private InputAction changeW1, changeW2, changeW3;
    private InputAction uiAccept, pause;

    [SerializeField] private Transform cameraTransform;
    private Vector3 currentVelocity;

    [Header("PlayerStats")]
    private float baseHealth = 100.0f;
    [HideInInspector] public float health;
    private float baseArmor = 100.0f;
    [HideInInspector] public float armor;
    private float baseAmmo = 250.0f;
    private float ammo;
    private float baseSpeed = 20.0f;
    private float speed;
    private float baseDamage = 12.0f;
    private float damage;
    [Header("Mapa")]
    [SerializeField] GameObject _mapa;

    [Header("Configuración de Sensibilidad")]
    [HideInInspector] public float mouseSensitivity = 500f;

    private void Awake()
    {
        _sound= SoundManager.Instance;


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

        if (changeW1.WasPressedThisFrame())
        {
            gameManager.ChangePlayerUI(0);
        }
        else if (changeW2.WasPressedThisFrame())
        {
            gameManager.ChangePlayerUI(1);
        }
        else if (changeW3.WasPressedThisFrame())
        {
            gameManager.ChangePlayerUI(2);
        }

        HandleMouseLook();

        if(Input.GetKeyDown(KeyCode.M))
        {
            _mapa.SetActive(_mapa.activeSelf ? false : true);
        }



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
        armor = 0.0f;

        gameManager.UpdateHP(health);
        gameManager.UpdateArmor(armor);
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

    public void IncreaseOrDecreaseHealth(float value, GameObject gameObject)
    {
        if (health >= baseHealth) health = baseHealth;
        else
        {
            health += value;
            gameObject.SetActive(false);
        }

        gameManager.UpdateHP(health);
    }

    public void IncreaseOrDecreaseDamage(float value)
    {
        damage += value;
    }

    public void IncreaseOrDecreaseArmor(float value, GameObject gameObject)
    {
        if (armor >= baseArmor) armor = baseArmor;
        else
        {
            armor += value;
            gameObject.SetActive(false);
        }

        gameManager.UpdateArmor(armor);
    }

    public void IncreaseOrDecreaseAmmo(float value, GameObject gameObject)
    {
        if (ammo >= baseAmmo) ammo = baseAmmo;
        else
        {
            ammo += value;
            gameObject.SetActive(false);
        }
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

    private void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _sound.ShootPistola();
            // Obtener el rayo desde el centro de la cámara
            Ray ray = new Ray(this.transform.position, this.transform.forward.normalized * raycastDistance);

            // Debug visual del rayo
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green, 0.1f);

            // Lanzar el raycast
            if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactableLayers))
            {
                target = hit.collider.gameObject;
                target.GetComponent<EnemyController>().GetDamage(damage);
            }
            else target = null;
        }
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        this.GetComponent<Rigidbody>().MoveRotation(this.transform.rotation * Quaternion.Euler(0f, mouseX, 0f));
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

        changeW1 = playerControls.Player.ChangeWeapon1;
        changeW1.Enable();

        changeW2 = playerControls.Player.ChangeWeapon2;
        changeW2.Enable();


        changeW3 = playerControls.Player.ChangeWeapon3;
        changeW3.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        sprint.Disable();

        //uiAccept.Disable();
        pause.Disable();

        interact.Disable();

        changeW1.Disable();
        changeW2.Disable();
        changeW3.Disable();
    }

    public void ActiveInputs(bool isActivating)
    {
        if (isActivating) OnDisable();
        else OnEnable();
    }

    private IEnumerator TxtPickSomething(int txtIndex)
    {
        gameManager.GuideTxtConfig(txtIndex);
        gameManager.ShowTxtGuide(true);
        yield return new WaitForSeconds(2.0f);
        gameManager.ShowTxtGuide(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Secret"))
        {
            gameManager.GuideTxtConfig(0);
            gameManager.ShowTxtGuide(true);
        }

        if (other.gameObject.layer == 8)
        {
            gameManager.GuideTxtConfig(0);
            gameManager.ShowTxtGuide(true);
        }

        if (other.gameObject.layer == 12)
        {
            gameManager.GuideTxtConfig(0);
            gameManager.ShowTxtGuide(true);
        }

        if(other.CompareTag("PowerUp") && other.gameObject.layer == 9 && health != baseHealth)
        {
            StartCoroutine(TxtPickSomething(3));
        }
        else if (other.CompareTag("PowerUp") && other.gameObject.layer == 10 && armor != baseArmor)
        {
            StartCoroutine(TxtPickSomething(4));
        }
        else if (other.CompareTag("PowerUp") && other.gameObject.layer == 11 && ammo != baseAmmo)
        {
            StartCoroutine(TxtPickSomething(5));
        }


        if (other.CompareTag("Exit")) gameManager.ChangeToAnotherLevel(2);
        if (other.CompareTag("Exit2")) gameManager.ChangeToAnotherLevel(3);
        if (other.CompareTag("Exit3")) gameManager.ChangeToAnotherLevel(4);
        if (other.CompareTag("Exit4")) Application.Quit();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && !other.GetComponent<DoorsController>().hasTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(other.CompareTag("SpecialDoor"))
                {
                    other.GetComponent<SpecialDoor>().ShowPlace();
                }

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
        if(!other.CompareTag("PowerUp")) gameManager.ShowTxtGuide(false);
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
                _sound.PlayerMuerte();
                health = 0.0f;
                SceneManager.LoadScene(0);
                Debug.Log("Morí");
            }
        }

        gameManager.UpdateHP(health);
        gameManager.UpdateArmor(armor);
    }
}


