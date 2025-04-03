using UnityEngine;
using DG.Tweening;
public class DoorsController : MonoBehaviour
{
    [SerializeField] private GameObject doorTrigger;

    private BoxCollider detector;

    private Vector3 basePos;

    private float doorSpeed = 0.5f;

    private bool isOpen;

    private void Awake()
    {
        basePos = this.transform.position;

        detector = transform.GetChild(0).GetComponent<BoxCollider>();
    }


    public void OpenDoor()
    {
        if (doorTrigger != null)
        {

        }
        else
        {
            if (!isOpen)
            {
                this.transform.DOMoveY(basePos.y + 14f, doorSpeed).OnComplete(() => isOpen = true);
            }
        }
    }

    public void CloseDoor()
    {
        if (doorTrigger != null)
        {

        }
        else
        {
            if (isOpen)
            {
                this.transform.DOMoveY(basePos.y, doorSpeed).OnComplete(() => isOpen = false);
            }
        }
         
    }

    private void OnTriggerEnter(Collider other)
    {
        if(doorTrigger != null)
        {
            
        }
        else
        {
            if (detector != null && !other.CompareTag("Player")) OpenDoor();
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (doorTrigger != null)
        {

        }
        else
        {
            if (detector != null) CloseDoor();
        }
    }
}
