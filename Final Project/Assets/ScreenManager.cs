using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{

    [SerializeField] GameObject gameOverUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        gameOverUI.SetActive(true);
    }

    public void Restart() {
        Debug.Log("Restart Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        //Need to build out my main menu
        //SceneManager.LoadScene("MainMenu");
        Debug.Log("Go to main menu");
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
