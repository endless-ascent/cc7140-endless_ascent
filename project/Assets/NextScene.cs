using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Required for scene management

public class NextScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "FinalCutscene")
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Acampamento", LoadSceneMode.Single);
        }
    }
}
