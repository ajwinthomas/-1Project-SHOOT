using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Transform target;

    private EnemyReferences enemyReferences;

    private float pathUpdateDeadline;

    private float shootingDistance;

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }
   
    void Start()
    {
        shootingDistance = enemyReferences.agent.stoppingDistance; 
    }

   
    void Update()
    {
        if(target != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;
            if(inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }

            enemyReferences.animator.SetBool("shooting",inRange);
        }
        enemyReferences.animator.SetFloat("Speed", enemyReferences.agent.desiredVelocity.sqrMagnitude);
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 1;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);

    }
    
    private void UpdatePath()
    {
        if(Time.time >= pathUpdateDeadline)
        {
            Debug.Log("Updating Path");
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.agent.SetDestination(target.position);
        }
    }
}
