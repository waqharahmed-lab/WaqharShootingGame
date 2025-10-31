using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    public GameObject projectilePrefab;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.forward * speed * forwardInput);
    
    if (Input.GetKeyDown(KeyCode.Space))
{
    Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
}
}
}