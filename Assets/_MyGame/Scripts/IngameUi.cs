using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IngameUi : MonoBehaviour
{

    GeneralGame inputActions;

    private PlayerInput playerInput;
    private bool isPaused = false;

    public GameObject IngameUI;
    public GameObject PlayerController;




    void Awake()
    {
        IngameUI.SetActive(false);
        inputActions = new GeneralGame();
        inputActions.UIIngame.Enable();
    }

    void Start()
    {
        playerInput = PlayerController.GetComponent<PlayerInput>();
        ResumeGame();
    }
    void Update()
    {
        if (inputActions.UIIngame.OpenUIIngame.WasPressedThisFrame())
        {
            if (IngameUI.activeSelf == false)
            {
                // Oyun içi menü açık oyun durdu
                PauseGame();
            }
            else
            {
                // Oyun içi menü kapalı oyun devam ediyor
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        // Oyun içi menü kapalı oyun devam ediyor
        IngameUI.SetActive(false);
        playerInput.enabled = true;
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        // Oyun içi menü açık oyun durdu
        IngameUI.SetActive(true);
        playerInput.enabled = false;
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnApplicationFocus(bool focus)
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Cursor kilenmiyor");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Cursor kilitleniyor");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Çıkış yapılıyor...");
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnDestroy()
    {
        inputActions.UIIngame.Disable();
    }
}
