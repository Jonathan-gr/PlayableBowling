using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PinManager : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> pinRigidbodies = new List<Rigidbody>();
    [SerializeField] private float knockedAngleThreshold = 45f;
    [SerializeField] private float sleepVelocityThreshold = 0.05f; // consider pin "settled" below this
    [SerializeField] private float maxWaitTime = 5f; // failsafe timeout

    public IEnumerator WaitForPinsToSettle(System.Action<int> onSettled)
    {
        float elapsed = 0f;

        // Keep waiting until ALL pins are sleeping/still OR timeout
        while (elapsed < maxWaitTime)
        {
            elapsed += Time.deltaTime;

            if (AllPinsSettled())
                break;

            yield return null; // check every frame
        }

        int knocked = GetKnockedPinCount();
        onSettled?.Invoke(knocked);
    }

    private bool AllPinsSettled()
    {
        foreach (var rb in pinRigidbodies)
        {
            if (rb == null) continue;

            // Pin is still moving
            if (rb.linearVelocity.magnitude > sleepVelocityThreshold ||
                rb.angularVelocity.magnitude > sleepVelocityThreshold)
                return false;
        }
        return true;
    }

    private bool IsPinKnockedDown(Rigidbody pinRb)
    {
        float angle = Vector3.Angle(pinRb.transform.up, Vector3.up);
        return angle > knockedAngleThreshold;
    }

    public int GetKnockedPinCount()
    {
        int count = 0;
        foreach (var rb in pinRigidbodies)
            if (rb != null && IsPinKnockedDown(rb))
                count++;
        return count;
    }

    public bool IsStrike() => GetKnockedPinCount() == pinRigidbodies.Count;
}