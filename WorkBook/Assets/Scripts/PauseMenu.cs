using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pauseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0.0f;
            }
            else
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = 1.0f;
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

}
