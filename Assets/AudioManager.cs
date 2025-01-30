using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource damage;
    public AudioSource hit;
    public AudioSource shoot;
    public AudioSource explode;
    public AudioSource button;
    public AudioSource fall;

    void Awake(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Damage(){
        damage.Play();
    }
    public void Hit(){
        hit.Play();
    }
    public void Shoot(){
        shoot.Play();
    }
    public void Explode(){
        explode.Play();
    }
    public void Button(){
        button.Play();
    }
    public void Fall(){
        fall.Play();
    }
}
