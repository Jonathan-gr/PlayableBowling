using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CTAButton : MonoBehaviour
{
    public static CTAButton Instance;
    public GameObject ctaButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ctaButton.SetActive(false);
    }

    public void ShowCTA()
    {
        ctaButton.SetActive(true);
    }

    public void OnCTAClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}