using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakeButton : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 15f;
    [SerializeField] private float shakeSpeed = 25f;
    [SerializeField] private float pauseBetweenShakes = 2f;

    [Header("Flicker Settings")]
    [SerializeField] private Color[] flickerColors = { Color.yellow, Color.red, Color.white };
    [SerializeField] private float flickerSpeed = 0.1f; // seconds per color

    private Vector3 originalRotation;
    private Image buttonImage;
    private Coroutine shakeRoutine;

    private void Start()
    {
        originalRotation = transform.eulerAngles;
        buttonImage = GetComponent<Image>();
    }

    public void StartShaking()
    {
        if (shakeRoutine == null)
        {
            shakeRoutine = StartCoroutine(ShakeLoop());
        }
    }

    private IEnumerator ShakeLoop()
    {
        while (true)
        {
            float elapsed = 0f;
            float flickerTimer = 0f;
            int colorIndex = 0;

            // ---- SHAKE PHASE ----
            while (elapsed < shakeDuration)
            {
                elapsed += Time.deltaTime;
                flickerTimer += Time.deltaTime;

                // Smooth shake
                float dampen = 1f - Mathf.Clamp01(elapsed / shakeDuration);
                float angle = Mathf.Sin(elapsed * shakeSpeed) * shakeStrength * dampen;
                transform.eulerAngles = originalRotation + new Vector3(0, 0, angle);

                // Flicker during shake
                if (buttonImage != null && flickerTimer >= flickerSpeed)
                {
                    buttonImage.color = flickerColors[colorIndex % flickerColors.Length];
                    colorIndex++;
                    flickerTimer = 0f;
                }

                yield return null; // smooth per frame
            }

            // ---- RESET ----
            transform.eulerAngles = originalRotation;
            if (buttonImage != null)
                buttonImage.color = Color.white;

            // ---- REST ----
            yield return new WaitForSeconds(pauseBetweenShakes);
        }
    }
}