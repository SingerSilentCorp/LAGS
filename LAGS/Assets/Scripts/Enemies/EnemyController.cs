using UnityEngine;

public class EnemyController : EnemyBase
{
    [SerializeField] private float attackRange = 10f;

    [SerializeField] private GameObject target;

    protected override void Attack()
    {
        print("Attacking Player");
    }

    private void Update()
    {
        this.transform.LookAt(target.transform.position);

        
        if (PlayerInRange())
        {
            Attack();
        }
    }

    private bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }
}
