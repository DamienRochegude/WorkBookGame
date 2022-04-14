using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private GameObject inputsSettings;
    [SerializeField]
    private GameObject pauseMenu;

    private bool isPaused = false;
    private Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();

        if(scene.name == "MainMenu")
        {
            inputsSettings.SetActive(false);
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(false);
            mainMenu.SetActive(true);
        }else
        {
            inputsSettings.SetActive(false);
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(false);
            mainMenu.SetActive(false);
        }
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();

        //Vérifie si on se trouve ou pas dans la scène MainMenu
        if(scene.name != "MainMenu")
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                if(!isPaused)
                {
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0.0f;
                } else
                {
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1.0f;
                }
            }
        }
    }

    //Bouton Play
    public void Play()
    {
        SoundManager.PlaySound(0);
        SceneManager.LoadScene("SampleScene");
    }

    //Bouton Settings
    public void OpenSettings()
    {
        SoundManager.PlaySound(0);
        settingsMenu.SetActive(true);

        if (scene.name == "MainMenu")
        {
            mainMenu.SetActive(false);
        } else
        {
            pauseMenu.SetActive(false);
        }
    }

    //Bouton Inputs
    public void InputsSettings()
    {
        SoundManager.PlaySound(0);
        settingsMenu.SetActive(false);
        inputsSettings.SetActive(true);

    }

    //Bouton croix du panel Inputs
    public void CloseInputs()
    {
        SoundManager.PlaySound(0);
        inputsSettings.SetActive(false);
        settingsMenu.SetActive(true);
    }

    //Bouton croix du panel Settings
    public void CloseSettings()
    {
        SoundManager.PlaySound(0);
        settingsMenu.SetActive(false);

        if (scene.name == "MainMenu")
        {
            mainMenu.SetActive(true);
        } else
        {
            pauseMenu.SetActive(true);
        }
    }

    //Bouton Exit
    public void Exit()
    {
        SoundManager.PlaySound(0);
        Application.Quit();

    }

    //Bouton Resume
    public void Resume()
    {
        SoundManager.PlaySound(0);
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    //Bouton Main Menu
    public void Menu()
    {
        SoundManager.PlaySound(0);
        SceneManager.LoadScene("MainMenu");
    }

    //Bouton Apply
    public void Apply()
    {
        SoundManager.PlaySound(0);
    }
}
