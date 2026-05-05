using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FingerHint : MonoBehaviour
{
    public RectTransform fingerUI;
    public Vector2 startPos = new Vector2(0, -100);
    public Vector2 endPos = new Vector2(200, 100);
    public float speed = 1.5f;

    private bool playerHasInteracted = false;

    private void Start()
    {
        StartCoroutine(AnimateFinger());

    }

    private void Update()
    {
        // detect any touch or mouse input
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0))
        {
            playerHasInteracted = true;
            fingerUI.gameObject.SetActive(false); // hide finger
        }
    }

    private IEnumerator AnimateFinger()
    {
        while (!playerHasInteracted)
        {
            // swipe from start to end
            float elapsed = 0f;
            while (elapsed < 1f && !playerHasInteracted)
            {
                elapsed += Time.deltaTime * speed;
                fingerUI.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed);
                yield return null;
            }

            // pause at end
            yield return new WaitForSeconds(0.3f);

            // snap back to start
            fingerUI.anchoredPosition = startPos;

            // pause at start
            yield return new WaitForSeconds(0.3f);
        }
    }
}