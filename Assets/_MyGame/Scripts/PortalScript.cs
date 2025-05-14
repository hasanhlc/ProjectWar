using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour
{

    public IngameUi ingameui;
    public TimeChangeManager timeChangeManager;
    public GameObject endLevelUi;

    public Image fadeInFadeOutImage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FinishLevel());
        }
    }

    IEnumerator FinishLevel()
    {
        Debug.Log("GİRİLDİ");
        yield return StartCoroutine(FadeIn(2f));
        ShowEndLevelScreen();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

    private void ShowEndLevelScreen()
    {
        endLevelUi.SetActive(true);
    }



    void SetImageAlpha(float alpha)
    {
        Color color = fadeInFadeOutImage.color;
        color.a = Mathf.Clamp01(alpha);
        fadeInFadeOutImage.color = color;
    }




    public IEnumerator FadeIn(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            SetImageAlpha(t / duration);
            yield return null;
        }
        SetImageAlpha(1f);
    }
}
