using UnityEngine;
using DG.Tweening;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private SwitchController doorTriggerObj;

    private Vector3 basePos;

    private float doorSpeed = 0.5f;

    private bool isOpen;

    [HideInInspector] public bool hasTrigger, switchActivated;

    private void Awake()
    {
        basePos = this.transform.position;

        if (doorTriggerObj != null)
        {
            hasTrigger = true;
            switchActivated = false;

            Debug.Log("has triiger: " + hasTrigger + " ObjName: " + this.gameObject.name + " TriggerState: " + switchActivated);
        }
        else hasTrigger = false;
    }

    public void OpenDoor()
    {
        if (!hasTrigger)
        {
            if (!isOpen)
            {
                this.transform.DOMoveY(basePos.y + 14f, doorSpeed).OnComplete(() => isOpen = true);
            }
        }
        else if (hasTrigger)
        {
            if (!isOpen && switchActivated)
            {
                this.transform.DOMoveY(basePos.y + 14f, doorSpeed).OnComplete(() => isOpen = true);
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
