using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasThrown = false;
    private bool ctaShown = false;
    public float speedThreshold = 0.1f;
    private float checkDelay = 0f;
    public float delayBeforeCheck = 0.5f;
    public float ctaDelay = 2f; // ← seconds to wait before showing CTA

    [SerializeField] private BowlingGameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hasThrown && !ctaShown)
        {
            checkDelay += Time.deltaTime;

            if (checkDelay > delayBeforeCheck)
            {
                if (rb.linearVelocity.magnitude < speedThreshold)
                {
                    ctaShown = true;
                    gameManager.OnBallThrown();

                }
            }
        }
    }


    public void OnThrow()
    {
        hasThrown = true;
        checkDelay = 0f;
    }
}