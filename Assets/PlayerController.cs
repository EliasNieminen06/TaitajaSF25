using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float moveSpeed = 5f;             // Movement speed
    public float rotationSpeed = 700f;       // Rotation speed
    public Camera mainCamera;               // Camera to track mouse position
    public GameObject bulletPrefab;         // Bullet prefab
    public Transform gunTransform;          // Gun transform (no rotation, just move with player)
    public float bulletSpeed = 10f;         // Bullet speed
    public float fireRate = 0.5f;           // Fire rate (time between shots)
    private float nextFireTime = 0f;        // Next time player can shoot
    private Rigidbody rb;
    public int health;

    private Vector3 moveDirection;

    void Awake(){
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get Rigidbody component
        rb.freezeRotation = true;  // Prevent Rigidbody from rotating on its own
    }

    void Update()
    {
        // Handle Rotation (Smooth)
        RotatePlayerTowardsMouse();

        // Handle Shooting
        Shoot();

        if(health <= 0){
            Die();
        }
    }

    void FixedUpdate()
    {
        // Handle Movement (Using Rigidbody)
        MovePlayer();
    }

    // Handle player movement
    void MovePlayer()
    {
        // Get player input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 targetPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);  // Move the player using Rigidbody
        }
    }

    // Rotate player to face the mouse
    void RotatePlayerTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;  // Distance from camera to player (adjust based on your setup)
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = (worldMousePosition - transform.position).normalized;
        direction.y = 0f;  // Lock rotation to the XZ plane

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
            nextFireTime = Time.time + fireRate; // Prevent spamming shots
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = gunTransform.forward * bulletSpeed; // Shoot in the direction the gun is facing
    }

    public void TakeDamage(int amount){
        health -= amount;
    }

    public void Die(){
        SceneManager.LoadScene("MenuScene");
    }
}
