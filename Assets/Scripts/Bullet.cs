using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 20f;
    public GameObject hitParticle;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
