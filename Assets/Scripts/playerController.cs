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
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    
    if (Input.GetKeyDown(KeyCode.Space))
{
    Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
}
}
}