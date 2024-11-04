using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShooter : MonoBehaviour
{
    [Header("General")]
    public Transform shootPoint;
    public Transform gunPoint;
    public LayerMask layerMask;

    [Header("Gun")]
    public Vector3 spread = new Vector3(0.2f, 0.2f, 0.2f);
    public TrailRenderer bulletTrail;
    private EnemyReferences enemyReferences;

    [Header("Target")]
    public Transform target;

    [SerializeField] private PlayerHealth playerHealth;

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void Shoot()
    {
        enemyReferences.shootSound.PlayOneShot(enemyReferences.shootSound.clip);
        Vector3 direction = GetDirection();
        if (Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, float.MaxValue, layerMask))
        {
            Debug.DrawLine(shootPoint.position, shootPoint.position + direction * 10f, Color.red, 1f);

            TrailRenderer trail = Instantiate(bulletTrail,gunPoint.position,Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            
            if (hit.collider.CompareTag("Player"))
            {
                  
                if(playerHealth != null)
                {
                    playerHealth.TakeDamage(10f);
                }

            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = (target.position - shootPoint.position).normalized;
        direction += new Vector3(Random.Range(-spread.x, spread.x), Random.Range(-spread.y, spread.y), Random.Range(-spread.z, spread.z));
        direction.Normalize();
        return direction;
    }

    private IEnumerator SpawnTrail (TrailRenderer trail, RaycastHit hit)
    {
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while(time < 1f)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return trail;
        }

        trail.transform.position = hit.point;
        Destroy(trail.gameObject, trail.time);
    }
}
