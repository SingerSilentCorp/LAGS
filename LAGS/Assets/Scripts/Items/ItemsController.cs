using DG.Tweening;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private enum ItemType { SmallHealth, Health, SmallArmor, Armor, SmallAmmo, Ammo, Speed, Damage };
    [SerializeField] private ItemType type;

    [Header("TweenVariables")]
    private Vector3 mainPos;


    private Transform target;


    private void Awake()
    {
        target = FindFirstObjectByType<PlayerController>().gameObject.transform;
        mainPos = this.transform.position;
    }

    private void Start()
    {
        MovingBehavior();
    }

    private void Update()
    {
        this.transform.LookAt(target.position);
    }

    private void MovingBehavior()
    {
        this.transform.DOMoveY(mainPos.y - 1.0f, 1.0f).SetEase(Ease.OutBack).OnComplete(()=> this.transform.DOMoveY(mainPos.y, 1.0f).SetEase(Ease.OutBack).OnComplete(() => MovingBehavior()));
        //this.transform.DOMoveY(mainPos.y - 1.0f, 1.0f).SetEase(Ease.InOutBounce).OnComplete(() => this.transform.DOMoveY(mainPos.y, 1.0f).SetEase(Ease.InOutBounce).OnComplete(() => MovingBehavior()));
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

    private void OnDisable()
    {
        DOTween.Kill(this.transform.gameObject);
    }
}