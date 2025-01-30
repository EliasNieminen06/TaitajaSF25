using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    public Camera mainCamera;
    public GameObject bulletPrefab;
    public Transform gunTransform;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    private Rigidbody rb;
    public GameObject hitParticle;
    public int health;
    public int score;

    private Vector3 moveDirection;
    public float dist;
    public Animator anim;

    void Awake(){
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        RotatePlayerTowardsMouse();
        Shoot();

        if(health <= 0){
            Die();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        if (moveDirection.magnitude >= 0.1f){
            Vector3 targetPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);
            anim.SetBool("isrunning", true);
        }
        else{
            anim.SetBool("isrunning", false);
        }
    }

    // Rotate player to face the mouse
    void RotatePlayerTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = dist;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = (worldMousePosition - transform.position).normalized;
        direction.y = 0f;
        if (direction.magnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
    }

    // Handle shooting
    void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireBullet()
    {
        anim.SetTrigger("shoot");
        AudioManager.Instance.Shoot();
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = gunTransform.forward * bulletSpeed;
    }

    public void TakeDamage(int amount){
        if (amount > 0){
            health -= amount;
            Instantiate(hitParticle, transform.position, Quaternion.identity, transform);
            AudioManager.Instance.Damage();
        }
    }

    public void Die(){
        AudioManager.Instance.Fall();
        if (PlayerPrefs.GetInt("score") < score){
            PlayerPrefs.SetInt("score", score);
        }
        SceneManager.LoadScene("MenuScene");
    }
}
