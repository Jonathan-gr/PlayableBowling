using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreBanner : MonoBehaviour
{
    public static ScoreBanner Instance;

    [SerializeField] private GameObject bannerObject;
    [SerializeField] private TextMeshProUGUI bannerText;
    [SerializeField] private float displayDuration = 2.5f; // how long banner shows

    private void Awake()
    {
        Instance = this;
        bannerObject.SetActive(false);
    }

    public void ShowBanner(int knocked, int total)
    {
        bannerText.text = GetBannerMessage(knocked, total);
        bannerObject.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }

    private string GetBannerMessage(int knocked, int total)
    {
        if (knocked == 10) return " STRIKE!\nALL 10 PINS DOWN!";
        if (knocked >= 8) return $"AMAZING!\n{knocked}/10 PINS DOWN!";
        if (knocked >= 5) return $"NICE!\n{knocked}/10 PINS DOWN";
        if (knocked >= 1) return $"{knocked}/10 PINS DOWN";
        return "GUTTER BALL!\nTRY AGAIN!";
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

    }
}