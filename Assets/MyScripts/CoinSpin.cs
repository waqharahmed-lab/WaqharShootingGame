using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotationSpeed = 200f; // Adjust in Inspector
    public bool rotateContinuously = true;

    void Update()
    {
        if (rotateContinuously)
        {
            
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        }
    
    }
}
