using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieMove : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemyRb;

    public float speed = 3f;
    public float rotationSpeed = 5f;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player"); // safer than GameObject.Find("Player")
    }

    void Update()
    {
        if (player == null) return;

        // ✅ Step 1: Rotate smoothly to face the player
        Vector3 direction = (player.transform.position - transform.position).normalized * speed; 
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
     void OnCollisionEnter(Collision collision)
    {
        // If zombie gets hit by bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
         // also destroy bullet
        }
    }
    
}
