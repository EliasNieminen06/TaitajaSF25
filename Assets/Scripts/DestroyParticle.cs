using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }

    void Update()
    {
        
    }
}
