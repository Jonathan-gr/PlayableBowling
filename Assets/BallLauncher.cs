using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 dragStartMouse;
    private bool launched = false;
    private bool isDragging = false;


    public float maxDragDistance = 3f;
    public float forceMultiplier = 20f;
    private Ball ball;

    [System.Obsolete]
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPos = transform.position;
        ball = GetComponentInChildren<Ball>();

        // lock rotation and vertical movement so it rolls, doesn't fly

    }

    void Update()
    {
        if (launched) return;

        if (Input.GetMouseButtonDown(0))
        {
            dragStartMouse = Input.mousePosition;
            isDragging = true;
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 drag = Input.mousePosition - dragStartMouse;

            // move ball visually: back on Z, left/right on X
            float dragBack = Mathf.Clamp(-drag.y / 100f, 0, maxDragDistance);
            float dragSide = drag.x / 100f;

            transform.position = startPos + new Vector3(dragSide, 0, -dragBack);
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3 drag = Input.mousePosition - dragStartMouse;

            float power = Mathf.Clamp(-drag.y / 100f, 0, maxDragDistance);
            float side = Mathf.Clamp(-drag.x / 100f, -1f, 1f);

            rb.isKinematic = false;
            rb.AddForce(new Vector3(side, 0, power) * forceMultiplier, ForceMode.Impulse);

            launched = true;
            isDragging = false;
            ball.OnThrow();

        }
    }
}