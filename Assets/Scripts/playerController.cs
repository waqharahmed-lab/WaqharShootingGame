using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    public GameObject projectilePrefab;
    private GameObject focalPoint;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = (focalPoint.transform.forward * forwardInput + focalPoint.transform.right * horizontalInput).normalized;
        playerRb.AddForce(moveDirection * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Zombie1"))
    {
    
        FindObjectOfType<GameManager>().GameOver();
    }

    }
}
