using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;


    private void FixedUpdate()
    {
        this.transform.position = target.position + new Vector3(0, 0.5f, 0.5f);
    }
}
