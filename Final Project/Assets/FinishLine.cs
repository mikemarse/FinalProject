using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    [SerializeField] MainMenuManager mainMenuManager;

    [SerializeField] ScreenManager screenManager;
    [SerializeField] bool isLast;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && isLast) {
            mainMenuManager.SaveGame(SceneManager.GetActiveScene().buildIndex + 1);
            screenManager.LoadScene("Credits");
        }

        if (other.gameObject.CompareTag("Player")) {
            mainMenuManager.SaveGame(SceneManager.GetActiveScene().buildIndex + 1);
            screenManager.NextScene();
        }
    }
    
}
