using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    
    public void StartGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame() {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SaveGame(currentLevel);
        Application.Quit();
    }

    public void SaveGame(int levelIndex) {
        PlayerPrefs.SetInt("SavedLevel", levelIndex);
        PlayerPrefs.Save();
        Debug.Log("Game Saved: Level " + levelIndex);
    }

    public void ContinueGame() {
        Debug.Log(PlayerPrefs.GetInt("SavedLevel", 1));
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        SceneManager.LoadScene(savedLevel);
    }



}
