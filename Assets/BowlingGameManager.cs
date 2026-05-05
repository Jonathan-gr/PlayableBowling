using System.Collections;
using UnityEngine;

public class BowlingGameManager : MonoBehaviour
{
    [SerializeField] private PinManager pinManager;

    private int totalScore = 0;

    public void OnBallThrown()
    {
        StartCoroutine(pinManager.WaitForPinsToSettle(OnPinsSettled));
    }

    private void OnPinsSettled(int knockedCount)
    {
        int points = knockedCount * 10;
        totalScore += points;

        Debug.Log($"Pins knocked: {knockedCount} | Score: {totalScore}");

        // Show banner first
        ScoreBanner.Instance.ShowBanner(knockedCount, totalScore);

        if (knockedCount == 10)
            TriggerStrikeEffect();

        // Show CTA after banner has been visible a moment
        StartCoroutine(ShowCTAAfterBanner());
    }

    private IEnumerator ShowCTAAfterBanner()
    {
        yield return new WaitForSeconds(2f); // let banner show first
        CTAButton.Instance.ShowCTA();
    }

    private void TriggerStrikeEffect()
    {
        Debug.Log("STRIKE!");
        // play particle effect, sound, etc.
    }
}