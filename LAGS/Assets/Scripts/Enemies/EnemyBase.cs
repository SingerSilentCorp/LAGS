using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected int damage = 10;
    
    protected abstract void Attack();

    
    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    
    protected virtual void Die()
    {
        //GameManager.Instance.AddScore(scoreValue);
        //Destroy(gameObject);
    }
}
