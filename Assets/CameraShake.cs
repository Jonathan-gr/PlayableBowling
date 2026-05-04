using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.1f;
    public bool isShakings = false;

    public float timeBeforeAnoherCameraShake = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake()
    {
        if (isShakings) return;
        StartCoroutine(ShakeCoroutine(shakeDuration, shakeMagnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        isShakings = true; // lock it
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(
                originalPos.x + x,
                originalPos.y + y,
                originalPos.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        yield return new WaitForSeconds(timeBeforeAnoherCameraShake);
        isShakings = false;
    }
}