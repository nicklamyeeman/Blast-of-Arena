using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    private void Start() {
        print("Starting");
        Screen.fullScreen = true;
    }

    public void loadMenu(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame() {
        Application.Quit();
    }
}
