using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieMove : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemyRb;

    public float speed = 3f;
    public float rotationSpeed = 5f;
    public GameObject deathEffect;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player"); // safer than GameObject.Find("Player")
    }

    void Update()
    {
       if (player == null) return;
    float adjustedSpeed = speed;
    if (DifficultyManager.Instance != null)
    {
        adjustedSpeed *= DifficultyManager.Instance.zombieSpeedMultiplier;
    }
     
        Vector3 direction = (player.transform.position - transform.position).normalized * speed; 
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<GameManager>().AddScore(1);
                if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
            Destroy(collision.gameObject);
        
        }
    }
    
    
}
