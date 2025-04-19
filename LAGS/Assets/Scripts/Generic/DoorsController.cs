using UnityEngine;
using DG.Tweening;

public class DoorsController : MonoBehaviour
{
    SoundManager _sound;
    [SerializeField] private SwitchController doorTriggerObj;

    private Vector3 basePos;

    private float doorSpeed = 0.2f;
    private float doorheight = 5.0f;

    private bool isOpen;

    [HideInInspector] public bool hasTrigger, switchActivated;

    private void Awake()
    {
        _sound = SoundManager.Instance;
        basePos = this.transform.position;

        if (doorTriggerObj != null)
        {
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
                isOpen = true;
                this.transform.DOMoveY(basePos.y + doorheight, doorSpeed);
                _sound.AbrirPuerta();
            }
        }
        else if (hasTrigger)
        {
            if (!isOpen && switchActivated)
            {
                isOpen = true;
                this.transform.DOMoveY(basePos.y + doorheight, doorSpeed);
                _sound.AbrirPuerta();
            }
        }
    }

    public void CloseDoor()
    {
        if (isOpen) this.transform.DOMoveY(basePos.y, doorSpeed).OnComplete(() => isOpen = false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen && !other.CompareTag("Player")) OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null)
        {
            DOTween.Kill(this);
            CloseDoor();
        }
    }
}
