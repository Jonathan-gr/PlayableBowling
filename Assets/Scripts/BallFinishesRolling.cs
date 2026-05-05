using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasThrown = false;
    public float speedThreshold = 0.1f;
    private float checkDelay = 0f;
    public float delayBeforeCheck = 0.5f;


    [SerializeField] private BowlingGameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hasThrown)
        {
            checkDelay += Time.deltaTime;

            if (checkDelay > delayBeforeCheck)
            {

                if (rb.linearVelocity.magnitude < speedThreshold)
                {

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