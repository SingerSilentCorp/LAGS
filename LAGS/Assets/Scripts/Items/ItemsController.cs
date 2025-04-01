using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private enum ItemType { Health, Speed, Damage };
    [SerializeField] private ItemType type;

    [SerializeField] private Transform target;

    private void Update()
    {
        this.transform.LookAt(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case ItemType.Health:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseHealth(20);
                    Debug.Log("Increse Health");
                    break;
                case ItemType.Speed:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseSpeed(20);
                    Debug.Log("Increse Speed");
                    break;
                case ItemType.Damage:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseDamage(20);
                    Debug.Log("Increse Damage");
                    break;
                default:
                    Debug.LogError("No type has been selected for this Item: " + this.gameObject.name);
                    break;
            }
        }
    }
}
