using UnityEditor.Rendering;
using UnityEngine;

public class IngameUi : MonoBehaviour
{

    GeneralGame ınputActions;

    public GameObject IngameUI;



    void Awake()
    {
        IngameUI.SetActive(false);
        ınputActions = new GeneralGame();
        ınputActions.UIIngame.Enable();
    }
    void Update()
    {
        if (ınputActions.UIIngame.OpenUIIngame.WasPressedThisFrame())
        {
            if (IngameUI.activeSelf == false)
            {
                IngameUI.SetActive(true);
            }
            else
            {
                IngameUI.SetActive(false);
            }
        }

    }
}
