using System.Collections;
using System.Collections.Generic;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneField gameScene;
    [SerializeField] private GameObject instructionsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene.Name);
    }
    
    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }
    
    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
    }
}
