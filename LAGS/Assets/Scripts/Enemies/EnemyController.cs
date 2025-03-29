using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyBase
{
    [SerializeField] private float attackRange = 10f;

    [SerializeField] private GameObject target;

    public Transform[] waypoints;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }
   
    protected override void Attack()
    {
        ChangeState(EnemyStates.Attacking);
        print("Attacking Player");
    }

    private void Update()
    {
        this.transform.LookAt(target.transform.position);

        
        if (PlayerInRange())
        {
            Attack();
        }

        if (state == EnemyStates.Wandering)
        {
            if (agent.remainingDistance < 0.5f && !agent.pathPending) MoveToNextWaypoint();
        }
        
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Vuelve al inicio después del último
    }


    private bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }
}
