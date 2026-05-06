using System.Collections;
using UnityEngine;

public class BowlingGameManager : MonoBehaviour
{
    [SerializeField] private PinManager pinManager;

    public GameObject bannerPrefab;
    public bool bannerSpawned = false;

    private int totalScore = 0;

    public void OnBallThrown()
    {
        StartCoroutine(pinManager.WaitForPinsToSettle(OnPinsSettled));
    }

    private void OnPinsSettled(int knockedCount)
    {
        int points = knockedCount * 10;
        totalScore += points;


        // Show banner first
        ScoreBanner.Instance.ShowBanner(knockedCount, totalScore);
        SpawnBanner();

        if (knockedCount == 10)
            TriggerStrikeEffect();

        // Show CTA after banner has been visible a moment
        StartCoroutine(ShowCTAAfterBanner());
    }
    void SpawnBanner()
    {
        if (bannerSpawned) return;

        bannerSpawned = true;
        Instantiate(bannerPrefab, new Vector3(0, 0), Quaternion.identity);
        Instantiate(bannerPrefab, new Vector3(-5, 0), Quaternion.identity);
        Instantiate(bannerPrefab, new Vector3(5, 0), Quaternion.identity);
        Instantiate(bannerPrefab, new Vector3(0, 0, -4), Quaternion.identity);


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