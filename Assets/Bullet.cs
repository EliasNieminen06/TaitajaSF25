using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 20f;

    void Start()
    {
        Destroy(gameObject, lifetime);  // Destroy after a set time
    }

    void OnCollisionEnter(Collision other)
    {
        // Destroy the bullet on impact with any object
        Destroy(gameObject);
    }
}
