using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemieAnimations : MonoBehaviour
{
    private enum EnemyType { pistola, escopeta, metralleta, jefe1, jefe2}
    [SerializeField] private EnemyType type;
    private enum EnemysStates { walk, ViewPlayer, Attack, escape, dead };
    [SerializeField] private EnemysStates _enemieState = EnemysStates.walk;

    [Header("References")]
    private Animator _anim;
    private NavMeshAgent agent;

    [Header("Enemy Stats")]
    [SerializeField] private float baseHealth;
    private float health;
    [SerializeField] private float _speed;
    [HideInInspector] public float damage;

    [Header("Others")]
    private float currentX;
    private float lastX;
    private float timer;
    private Vector3 startPosition;
    private bool isWandering;

    [Header("Wandering Settings")]
    [SerializeField] private float minWanderRadius;
    [SerializeField] private float maxWanderRadius;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private bool returnToStartArea = true;

    [Header("Attacking Settings")]
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private Transform target;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatDistance;

    [Header("JefeConfig")]
    [SerializeField] private bool isABoss;
    [SerializeField] private DoorsController door;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        startPosition = transform.position;
        timer = Random.Range(minWaitTime, maxWaitTime);
        isWandering = false;

        ResetEnemy();
    }

    private void Start()
    {
        //lastX = transform.position.x;
    }

    private void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        switch (_enemieState)
        {
            case EnemysStates.walk:

                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    if (!isWandering)
                    {
                        timer -= Time.deltaTime;

                        if (timer <= 0)
                        {
                            WanderToNewPosition();
                            isWandering = true;
                        }
                    }
                }
                else
                {
                    isWandering = false;
                }

                if (PlayerInRange())
                {
                    isWandering = false;
                    _enemieState = EnemysStates.ViewPlayer;
                    _anim.CrossFade("Walk", 0.0001f);
                }

                break;

            case EnemysStates.ViewPlayer:

                MoveToTarget();

                if (PlayerInRange())
                {
                    Attack();
                }

                if (health <= 0.0f)
                {
                    DEAD();
                    _enemieState = EnemysStates.dead;
                }

                break;

            case EnemysStates.escape:


                if (health < 1)
                {
                    
                }

                break;
            case EnemysStates.dead:

                break;
        }
    }

    private void MovementAnimationEjeX()
    {
        currentX = transform.position.x;

        if (currentX > lastX)
        {
            _anim.CrossFade("Right", 0.0001f);
        }
        else if (currentX < lastX)
        {
            _anim.CrossFade("Left", 0.0001f);
        }

        lastX = currentX;
    }

    private void ESCAPE() => _anim.CrossFade("StartBack", 0.0001f);

    private void DEAD()
    {
        if (isABoss)
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<SwitchController>().UnlockDoor();
        }
        else
        {
            _anim.CrossFade("Dead", 0.0001f);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }


    private IEnumerator StartViewPlayer()
    {
        _anim.CrossFade("StartBack", 0.0001f);
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        _anim.CrossFade("Back", 0.0001f);
    }

    private void WanderToNewPosition()
    {
        if(_enemieState == EnemysStates.walk)
        {
            agent.updateRotation = true;
            Vector3 wanderTarget;

            if (returnToStartArea && Random.value > 0.7f) // 30% chance to return closer to start
            {
                float radius = Random.Range(minWanderRadius * 0.5f, minWanderRadius);
                wanderTarget = RandomNavPosition(startPosition, radius);
            }
            else
            {
                float radius = Random.Range(minWanderRadius, maxWanderRadius);
                wanderTarget = RandomNavPosition(transform.position, radius);
            }

            agent.SetDestination(wanderTarget);
            timer = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    private Vector3 RandomNavPosition(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);

        return navHit.position;
    }

    private void MoveToTarget()
    {
        agent.updateRotation = false;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > stoppingDistance)
        {
            agent.SetDestination(target.position);
        }
        else if (distanceToTarget < retreatDistance)
        {
            Vector3 retreatDirection = (transform.position - target.position).normalized;
            Vector3 retreatPosition = transform.position + retreatDirection * stoppingDistance;
            agent.SetDestination(retreatPosition);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    private bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    private void Attack()
    {
        print("Attacking Player");
        target.GetComponent<PlayerController>().GetDamage(damage);
    }

    public void GetDamage(float damage)
    {
        health -= damage;

        if (health <= 0.0) 
        {
            health = 0.0f;
            DEAD();
        }
    }

    public void ResetEnemy()
    {
        switch (type)
        {
            case EnemyType.pistola:

                damage = Random.Range(6.0f, 10.01f);
                health = 24.0f;
                break;
            case EnemyType.escopeta:
                damage = Random.Range(15.0f, 20.01f);
                health = 36.0f;
                break;
            case EnemyType.metralleta:
                damage = Random.Range(8.0f, 12.01f);
                health = 60.0f;
                break;
            case EnemyType.jefe1:
                damage = Random.Range(10.0f, 15.01f);
                health = 400.0f;
                break;
            case EnemyType.jefe2:
                damage = Random.Range(12.0f, 15.01f);
                health = 430.0f;
                break;
            default:
                break;
        }

        health = baseHealth;
    }
}