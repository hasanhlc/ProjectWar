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
    private bool TimeState = true;
    public Volume GlobalVolume;
    public GameObject WarObjects;
    public GameObject PeaceObjects;
    public GameObject VirtualCamera;
    private ColorAdjustments colorAdjustments;
    public GameObject PeaceImage;
    public GameObject WarImage;
    
    
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
            WarObjects.SetActive(true);
            PeaceObjects.SetActive(false);
            GlobalVolume.profile.TryGet(out colorAdjustments);
            colorAdjustments.saturation.value = -100; // Siyah-beyaz yap
            ChangeSkillIcon(true);
            
    }

    public void TimeChangePeace()
    {

            WarObjects.SetActive(false);
            PeaceObjects.SetActive(true);
            GlobalVolume.profile.TryGet(out colorAdjustments);
            colorAdjustments.saturation.value = 9; 
            ChangeSkillIcon(false);
            
    }

    private void ChangeSkillIcon(bool WarState)
    {
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
