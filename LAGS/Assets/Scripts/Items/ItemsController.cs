using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private enum ItemType { SmallHealth,Health, SmallArmor, Armor,SmallAmmo, Ammo, Speed, Damage };
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
                case ItemType.SmallHealth:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseHealth(20);
                    
                    break;
                case ItemType.Health:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseHealth(50);
                    
                    break;
                case ItemType.SmallArmor:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseArmor(20);
                    
                    break;
                case ItemType.Armor:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseArmor(20);
                    
                    break;
                case ItemType.SmallAmmo:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseArmor(20);

                    break;
                case ItemType.Ammo:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseArmor(20);

                    break;
                case ItemType.Speed:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseSpeed(20);
                    
                    break;
                case ItemType.Damage:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseDamage(20);
                    
                    break;
                default:
                    Debug.LogError("No type has been selected for this Item: " + this.gameObject.name);
                    break;
            }
        }
    }
}
