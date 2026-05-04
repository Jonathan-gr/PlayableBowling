using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasThrown = false;
    private bool ctaShown = false;
    public float speedThreshHold = 0.1f;
    private float checkDelay = 0f;
    public float delayBeforeCheck = 0.5f; // wait 0.5s after throw before checking velocity

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hasThrown && !ctaShown)
        {
            checkDelay += Time.deltaTime;

            if (checkDelay > delayBeforeCheck) // only start checking after delay
            {
                if (rb.linearVelocity.magnitude < speedThreshHold)
                {
                    ctaShown = true;
                    CTAButton.Instance.ShowCTA();
                }
            }
        }
    }

    public void OnThrow()
    {
        hasThrown = true;
        checkDelay = 0f; // reset delay counter
    }
}