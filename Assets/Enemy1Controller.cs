using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float moveSpeed = 5f; // Speed at which the enemy moves toward the player
    public float explosionRadius = 5f; // Radius of the explosion
    public float maxDamage = 5f; // Maximum damage the explosion can deal
    public float minDamage = 0f; // Minimum damage at the furthest radius of the explosion
    public GameObject explodeParticle;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
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
            TriggerExplosion();
        }
        if (collision.gameObject.CompareTag("Player")){
            PlayerController.Instance.TakeDamage(5);
            Destroy(gameObject); 
        }
    }
}
