using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject levelSelectionUI;
    public GameObject settingsUI;
    public GameObject creditsUI;
    public GameObject introLevelOneUI;
    public RectTransform textTransform;
    public RectTransform textTransformCredits;
    private float scrollSpeed = 100f;
    public float endY = 1700f; // Distance to scroll the text
    // starty = -1000f; // Starting position of the text
    private float scrollSpeedCredits = 150f;
    public float endYCredits = 1700f;

    public Coroutine creditsCoroutine ;
    public Coroutine IntroCoroutine ;

    public enum MenuState
    {
        MainMenu,
        LevelSelection,
        Settings,
        Credits,
        IntroLevel
    }

    private MenuState currentState;


    private void Start()
    {
        currentState = MenuState.MainMenu;
        mainMenuUI.SetActive(true);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pressBack();
            Debug.Log("Escape key pressed.");
        }
        if (Input.GetKeyDown(KeyCode.E) && currentState == MenuState.IntroLevel)
        {
            Debug.Log("E ye Basıldı");
            StopCoroutine(IntroCoroutine);
            LoadScene("SampleScene");
        }
    } 


    public void IntroLevelOne()
    {
        CloseAllMenus();
        introLevelOneUI.SetActive(true);
        currentState = MenuState.IntroLevel;
        textTransform.anchoredPosition = new Vector2(0,-1000f); // Starting position of the text
        IntroCoroutine = StartCoroutine(ScrollUp());
    }

    IEnumerator ScrollUp()
    {
         while (textTransform.anchoredPosition.y < endY)
        {
            Vector2 pos = textTransform.anchoredPosition;
            pos.y += scrollSpeed * Time.deltaTime;
            textTransform.anchoredPosition = pos;

            yield return null; // Bir frame bekle
        }
        // Scroll işlemi tamamlandığında yapılacak işlemler
        // burada fade out eklenecek
        LoadScene("SampleScene");
    }
        IEnumerator ScrollUpCredits()
    {
         while (textTransformCredits.anchoredPosition.y < endYCredits)
        {
            Vector2 pos = textTransformCredits.anchoredPosition;
            pos.y += scrollSpeedCredits * Time.deltaTime;
            textTransformCredits.anchoredPosition = pos;

            yield return null; // Bir frame bekle
        }
        OpenMainMenu();

    }



    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }

    public void OpenLevelSelection()
    {
        CloseAllMenus();
        levelSelectionUI.SetActive(true);
        currentState = MenuState.LevelSelection;
    }

    public void OpenMainMenu()
    {
        CloseAllMenus();
        currentState = MenuState.MainMenu;
        mainMenuUI.SetActive(true);
    }

    public void OpenSettings()
    {
        CloseAllMenus();
        settingsUI.SetActive(true);
        currentState = MenuState.Settings;
    }

    public void OpenCredits()
    {
        CloseAllMenus();
        creditsUI.SetActive(true);
        currentState = MenuState.Credits;
        textTransformCredits.anchoredPosition = new Vector2(0,-1350f); // Starting position of the text
        creditsCoroutine = StartCoroutine(ScrollUpCredits());
    }

    public void CloseAllMenus()
    {
        mainMenuUI.SetActive(false);
        levelSelectionUI.SetActive(false);
        settingsUI.SetActive(false);
        creditsUI.SetActive(false);
        introLevelOneUI.SetActive(false);
    }

    private void pressBack()
    {
        switch (currentState)
        {
        case MenuState.MainMenu:
            QuitGame();
            break;
        case MenuState.LevelSelection:
            levelSelectionUI.SetActive(false);
            mainMenuUI.SetActive(true);
            currentState = MenuState.MainMenu;
            break;
        case MenuState.Settings:
            settingsUI.SetActive(false);
            mainMenuUI.SetActive(true);
            currentState = MenuState.MainMenu;
            break;
        case MenuState.Credits:
            creditsUI.SetActive(false);
            mainMenuUI.SetActive(true);
            currentState = MenuState.MainMenu;
            StopCoroutine(creditsCoroutine);
            break;
        }
    }
}
