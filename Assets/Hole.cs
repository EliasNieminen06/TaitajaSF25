using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            AudioManager.Instance.Fall();
            PlayerController.Instance.Die();
        }
    }
}
