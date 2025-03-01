using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeChangeManager : MonoBehaviour
{
    private bool TimeState = true;
    public Volume GlobalVolume;
    public GameObject WarObjects;
    public GameObject PeaceObjects;
    public GameObject VirtualCamera;
    private ColorAdjustments colorAdjustments;
    private bool CoolDownReady= true;
    
    
    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeChangeWar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (TimeState)
            {
                TimeChangeWar();
                TimeState = false;
            }
            else
            {
                TimeChangePeace();
                TimeState = true;
            }
        }
    }

    public void TimeChangeWar()
    {
        if (CoolDownReady)
        {
            WarObjects.SetActive(true);
            PeaceObjects.SetActive(false);
            GlobalVolume.profile.TryGet(out colorAdjustments);
            colorAdjustments.saturation.value = -100; // Siyah-beyaz yap
        }

    }

    public void TimeChangePeace()
    {
        if (CoolDownReady)
        {
            WarObjects.SetActive(false);
            PeaceObjects.SetActive(true);
            GlobalVolume.profile.TryGet(out colorAdjustments);
            colorAdjustments.saturation.value = 9; // Siyah-beyaz yap
        }
    }
}
