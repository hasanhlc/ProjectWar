using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class TimeChangeManager : MonoBehaviour
{
    private bool TimeState;
    public Volume GlobalVolume;
    public GameObject WarObjects;
    public GameObject PeaceObjects;
    public GameObject VirtualCamera;
    private ColorAdjustments colorAdjustments;
    public GameObject PeaceImage;
    public GameObject WarImage;

    public GameObject WarPlayerAsset;
    public GameObject PeacePlayerAsset;

    public GameObject playerGeometry;

    public GeneralGame playerInputActions;
    private bool coolDownIsReady;

    public Image coolDownFıllImage;
    public float coolDownTime = 5;

    public Image ProgressBarTime;
    public GameObject TimerProgressBar;
    public float ProgressDuration = 15f;

    private Coroutine WarDurationCoroutine;
    private SkyManager skyManager;

    private float fadeInFadeOutDuration = 1f; // Fade süresi (saniye cinsinden)
    public Image fadeInFadeOutImage; // Fade işlemi için kullanılacak UI Image bileşeni

    private void Awake()
    {
        skyManager = GetComponent<SkyManager>();
        playerInputActions = new GeneralGame();
        playerInputActions.GeneralActions.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeChangePeace();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTimeAbility();
    }


    public void ChangeTimeAbility()

    {
        if (playerInputActions.GeneralActions.ChangeTime.WasPressedThisFrame() && coolDownIsReady)
        {
            StartCoroutine(MySequence());
        }

        if (coolDownIsReady == false)
        {
            coolDownFıllImage.fillAmount -= 1 / coolDownTime * Time.deltaTime;

            if (coolDownFıllImage.fillAmount <= 0)
            {
                coolDownFıllImage.fillAmount = 0;
                coolDownIsReady = true;
            }

        }
    }

    IEnumerator MySequence()
    {
        // Fade In yap
        yield return StartCoroutine(FadeIn(fadeInFadeOutDuration));

        // Bekleme veya UI işlemleri
        yield return new WaitForSeconds(1f);
        ChangeTimeAfterFadeIn();


        // Fade Out yap
        yield return StartCoroutine(FadeOut(fadeInFadeOutDuration));

        // Fade out sonrası işler
        Debug.Log("Tamamlandı.");
    }




    private void ChangeTimeAfterFadeIn()
    {
        // zaman değiştirme işlemi
        Debug.Log("zaman değişiyor");
        if (TimeState)
        {
            TimeChangeWar();
        }
        else
        {
            TimeChangePeace();

        }
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

    public IEnumerator FadeOut(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            SetImageAlpha(1f - (t / duration));
            yield return null;
        }
        SetImageAlpha(0f);
    }








    void SetImageAlpha(float alpha)
    {
        Color color = fadeInFadeOutImage.color;
        color.a = Mathf.Clamp01(alpha);
        fadeInFadeOutImage.color = color;
    }


    public void TimeChangeWar()
    {
        print("War");
        TimerProgressBar.SetActive(true);
        StartRopeTimer();
        TimeState = false;
        WarObjects.SetActive(true);
        PeaceObjects.SetActive(false);
        GlobalVolume.profile.TryGet(out colorAdjustments);
        //colorAdjustments.saturation.value = -100; // Siyah-beyaz yap
        skyManager.ChangeSky(TimeState);

        Debug.Log("War Çağırıldı");
        ChangeSkıllAndCooldown(true);

    }

    public void TimeChangePeace()
    {
        print("Peace");
        TimerProgressBar.SetActive(false);
        if (WarDurationCoroutine != null)
        {
            StopCoroutine(WarDurationCoroutine);
        }
        TimeState = true;
        WarObjects.SetActive(false);
        PeaceObjects.SetActive(true);
        GlobalVolume.profile.TryGet(out colorAdjustments);
        //colorAdjustments.saturation.value = 9; 
        skyManager.ChangeSky(TimeState);
        Debug.Log("Peace Çağırıldı");

        ChangeSkıllAndCooldown(false);

    }


    private void ChangeSkıllAndCooldown(bool WarState)


    {
        coolDownIsReady = false;
        coolDownFıllImage.fillAmount = 1;

        if (WarState)
        {
            PeaceImage.SetActive(true);
            WarImage.SetActive(false);
        }
        else
        {
            PeaceImage.SetActive(false);
            WarImage.SetActive(true);
        }
    }


    private void StartRopeTimer()
    {
        StopAllCoroutines();
        ProgressBarTime.fillAmount = 1f;
        WarDurationCoroutine = StartCoroutine(RopeProgressDecrase());
    }

    private IEnumerator RopeProgressDecrase()
    {
        float elapsedTime = 0f;
        float startValue = 1f; // Başlangıç değeri (dolu)
        float endValue = 0f;   // Bitiş değeri (boş)
        while (elapsedTime < ProgressDuration)
        {
            elapsedTime += Time.deltaTime;
            ProgressBarTime.fillAmount = Mathf.Lerp(startValue, endValue, elapsedTime / ProgressDuration);
            yield return null; // Bir sonraki frame'e geç
        }

        ProgressBarTime.fillAmount = 0f; // Son değeri kesin olarak sıfırla
        StartCoroutine(MySequence());
        Debug.Log("Zaman değişti, bar sıfırlandı.");
    }


    private void ChangePlayerAsset()
    {
        foreach (Transform child in playerGeometry.transform)
        {
            Destroy(child.gameObject);
        }


        if (TimeState)
        {
            GameObject childassetprefab = Instantiate(PeacePlayerAsset, playerGeometry.transform);
            childassetprefab.SetActive(true);

        }
        else
        {
            GameObject childassetprefab = Instantiate(WarPlayerAsset, playerGeometry.transform);
            childassetprefab.SetActive(true);
        }
    }

}

