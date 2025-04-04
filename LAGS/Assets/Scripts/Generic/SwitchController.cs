using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private DoorsController door;

    public void UnlockDoor()
    {
        door.switchActivated = true;
    }
}
