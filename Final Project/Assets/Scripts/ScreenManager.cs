using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{

    [SerializeField] GameObject gameOverUI;

    [SerializeField] Animator transitionScene;

    public void GameOver() {
        gameOverUI.SetActive(true);
    }

    public void Restart() {
        Debug.Log("Restart Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void NextScene() {
        StartCoroutine(LoadLevel());
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadLevel() {
        transitionScene.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionScene.SetTrigger("Start");
    }
}
