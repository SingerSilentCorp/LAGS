using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private enum ItemType { SmallHealth, Health, SmallArmor, Armor, SmallAmmo, Ammo, Speed, Damage };
    [SerializeField] private ItemType type;

    [SerializeField] private Transform target;

    private void Update()
    {
        this.transform.LookAt(new Vector3(0.0f, target.position.y, 0.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case ItemType.SmallHealth:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseHealth(20, this.gameObject);

                    break;
                case ItemType.Health:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseHealth(40, this.gameObject);

                    break;
                case ItemType.SmallArmor:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseArmor(50, this.gameObject);

                    break;
                case ItemType.Armor:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseArmor(100, this.gameObject);

                    break;
                case ItemType.SmallAmmo:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseAmmo(20, this.gameObject);

                    break;
                case ItemType.Ammo:
                    target.GetComponent<PlayerController>().IncreaseOrDecreaseAmmo(20, this.gameObject);

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
