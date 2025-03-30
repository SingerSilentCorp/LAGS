using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected enum EnemyStates { Wandering, Attacking, Dying, Escape};

    protected EnemyStates state = EnemyStates.Wandering;

    [SerializeField] protected float health = 100f;
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected int damage = 10;
    
    protected abstract void Attack();

    protected virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void ChangeState(EnemyStates newState) => state = newState;


    protected virtual void Die()
    {
        Debug.Log("It's death");
        //GameManager.Instance.AddScore(scoreValue);
        //Destroy(gameObject);
    }
}
