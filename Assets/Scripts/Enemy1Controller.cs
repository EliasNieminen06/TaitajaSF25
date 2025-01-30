using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float explosionRadius = 5f;
    public float maxDamage = 5f;
    public float minDamage = 0f;
    public GameObject explodeParticle;
    public int health;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
        if(health <= 0){
            TriggerExplosion();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(player);
        }
    }
    public void TriggerExplosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                float damage = Mathf.Lerp(maxDamage, minDamage, distance / explosionRadius);
                PlayerController.Instance.TakeDamage((int)damage);
            }
        }
        Instantiate(explodeParticle, transform.position, Quaternion.identity);
        Destroy(gameObject); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")){
            health--;
        }
        if (collision.gameObject.CompareTag("Player")){
            PlayerController.Instance.TakeDamage(5);
            Instantiate(explodeParticle, transform.position, Quaternion.identity);
            Destroy(gameObject); 
        }
    }
}
