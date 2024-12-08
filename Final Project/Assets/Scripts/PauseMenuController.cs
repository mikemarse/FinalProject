using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    public bool isPaused = false;

    public void Pause() {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }



    public void Resume() {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            Resume();
        }
    }
}
