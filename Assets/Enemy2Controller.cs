using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public Transform player;
    public float speed = 3f; 
    public float zigzagBaseStrength = 0.5f;
    public float zigzagMultiplier = 0.2f; 
    public float zigzagFrequency = 1f;

    private Vector3 directionToPlayer;
    private float zigzagOffset;
    private float timeElapsed;
    public int health;

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update(){
        MoveTowardsPlayer();
        if(health <= 0){
            Destroy(gameObject);
        }
    }

    void MoveTowardsPlayer(){
        directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= zigzagFrequency)
        {
            float currentZigzagStrength = zigzagBaseStrength + (distanceToPlayer * zigzagMultiplier);
            zigzagOffset = Random.Range(-currentZigzagStrength, currentZigzagStrength);
            timeElapsed = 0f;
        }
        Vector3 perpendicularDirection = Vector3.Cross(directionToPlayer, Vector3.up).normalized;
        Vector3 moveDirection = directionToPlayer + perpendicularDirection * zigzagOffset;
        transform.position += moveDirection.normalized * speed * Time.deltaTime;
        transform.LookAt(player);
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")){
            health--;
        }
        if (collision.gameObject.CompareTag("Player")){
            PlayerController.Instance.TakeDamage(5);
            Destroy(gameObject); 
        }
    }
}
