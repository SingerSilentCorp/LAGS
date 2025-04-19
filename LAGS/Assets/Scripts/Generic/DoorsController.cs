using UnityEngine;
using DG.Tweening;

public class DoorsController : MonoBehaviour
{
    SoundManager _sound;
    [SerializeField] private SwitchController doorTriggerObj;

    private Vector3 basePos;

    private float doorSpeed = 0.5f;

    private bool isOpen;

    [HideInInspector] public bool hasTrigger, switchActivated;

    private void Awake()
    {
        _sound = SoundManager.Instance;
        basePos = this.transform.position;

        if (doorTriggerObj != null)
        {
            Debug.Log("has trigger");
            hasTrigger = true;
            switchActivated = false;
        }
        else hasTrigger = false;
    }

    public void OpenDoor()
    {
        if (!hasTrigger)
        {
            if (!isOpen)
            {
                this.transform.DOMoveY(basePos.y + 5f, doorSpeed).OnComplete(() => isOpen = true);
                _sound.AbrirPuerta();
                Debug.Log("Puerta Abierta");
            }
        }
        else if (hasTrigger)
        {
            if (!isOpen && switchActivated)
            {
                this.transform.DOMoveY(basePos.y + 5f, doorSpeed).OnComplete(() => isOpen = true);
               // _sound.AbrirPuerta();
                Debug.Log("Puerta Abierta");
            }

        }

        
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            this.transform.DOMoveY(basePos.y, doorSpeed).OnComplete(() => isOpen = false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        CloseDoor();
    }
}
