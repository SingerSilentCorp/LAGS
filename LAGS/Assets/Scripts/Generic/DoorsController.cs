using UnityEngine;
using DG.Tweening;
public class DoorsController : MonoBehaviour
{
    private BoxCollider detector;

    private Vector3 basePos;

    private float doorSpeed = 3f;

    //private float distance;
    //private float rangeDistance = 22f;

    private bool isOpen;

    [SerializeField] private Transform playerPos;

    private void Awake()
    {
        basePos = this.transform.position;

        detector = transform.GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        //distance = Vector3.Distance(playerPos.position, transform.position);
    }


    public void OpenDoor()
    {
        if (!isOpen)
        {
            this.transform.DOMoveY(basePos.y + 14f, 0.5f).OnComplete(()=> isOpen = true);
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            this.transform.DOMoveY(basePos.y, 0.5f).OnComplete(() => isOpen = false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(detector != null) OpenDoor();

    }

    private void OnTriggerExit(Collider other)
    {
        if (detector != null) CloseDoor();
    }
}
