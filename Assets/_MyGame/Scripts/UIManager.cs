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
    private float scrollSpeed = 100f;
    public float endY = 1700f; // Distance to scroll the text
    // starty = -1000f; // Starting position of the text

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
    } 


    public void IntroLevelOne()
    {
        CloseAllMenus();
        introLevelOneUI.SetActive(true);
        currentState = MenuState.IntroLevel;
        textTransform.anchoredPosition = new Vector2(0,-1000f); // Starting position of the text
        StartCoroutine(ScrollUp());
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
        mainMenuUI.SetActive(false);
        levelSelectionUI.SetActive(true);
        currentState = MenuState.LevelSelection;
    }

    public void OpenSettings()
    {
        CloseAllMenus();
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
        currentState = MenuState.Settings;
    }

    public void OpenCredits()
    {
        CloseAllMenus();
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
        currentState = MenuState.Credits;
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
            break;
        }
    }
}
