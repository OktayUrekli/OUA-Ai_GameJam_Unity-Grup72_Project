using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void playGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void mainMenu() {
        SceneManager.LoadScene("Main_menu");
    }

    public void settingsMenu() {
        SceneManager.LoadScene("Settings_menu");
    }

    public void quitGame() {
        Application.Quit();
    }

    public void credits() {

    }
}

