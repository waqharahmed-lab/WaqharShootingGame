using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    
    public float beatSpeed = 1.2f;      // How fast the heartbeat is
    public float beatScale = 1.2f;      // How large it grows on each beat

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(Beat());
    }

    private System.Collections.IEnumerator Beat()
    {
        while (true)
        {
            // Grow
            yield return ScaleTo(originalScale * beatScale, beatSpeed / 2);
            // Shrink
            yield return ScaleTo(originalScale, beatSpeed / 2);
        }
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 target, float duration)
    {
        Vector3 start = transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = target;
    }
}
