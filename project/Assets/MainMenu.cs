using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUISkin layout;              // Fonte do placar
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI() {
        GUI.skin = layout;
    }

    public void PlayGame() {
        SceneManager.LoadScene("MainStory");
    }

    public void Instructions() {
        SceneManager.LoadScene("Instructions");
    }

    public void Credits() {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("QUIT!");
    }

    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
