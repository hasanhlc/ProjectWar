using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class TimeChangeManager : MonoBehaviour
{
    private bool TimeState ;
    public Volume GlobalVolume;
    public GameObject WarObjects;
    public GameObject PeaceObjects;
    public GameObject VirtualCamera;
    private ColorAdjustments colorAdjustments;
    public GameObject PeaceImage;
    public GameObject WarImage;
    
    public GeneralGame playerInputActions;
    private bool coolDownIsReady;

    public Image coolDownFıllImage;
    public float coolDownTime = 5;
    
    private void Awake()
    {
        playerInputActions = new GeneralGame();
        playerInputActions.GeneralActions.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeChangeWar();


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
            if (TimeState)
            {
                TimeChangeWar();

            }
            else
            {
                TimeChangePeace();

            }
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


    public void TimeChangeWar()
    {
            print("War");
            TimeState = false;
            WarObjects.SetActive(true);
            PeaceObjects.SetActive(false);
            GlobalVolume.profile.TryGet(out colorAdjustments);
            colorAdjustments.saturation.value = -100; // Siyah-beyaz yap

            ChangeSkıllAndCooldown(true);
            
    }

    public void TimeChangePeace()
    {
            print("Peace");
            TimeState = true;
            WarObjects.SetActive(false);
            PeaceObjects.SetActive(true);
            GlobalVolume.profile.TryGet(out colorAdjustments);
            colorAdjustments.saturation.value = 9; 

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

}
