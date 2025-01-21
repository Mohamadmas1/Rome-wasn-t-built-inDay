using System.Collections;
using System.Collections.Generic;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Puzzles")]
    [SerializeField] private ColosseumTetrisPuzzle colosseumTetrisPuzzle;
    [SerializeField] private VaticanSlidePuzzle vaticanSlidePuzzle;

    [Header("Pisa Tower")]
    [SerializeField] private Sprite[] pisaTowerSprites;
    [SerializeField] private SpriteRenderer pisaTowerSpriteRenderer;
    private int pisaTowerHealth = 3;

    [Header("Game Over Screens")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    [Header("Scene Management")]
    [SerializeField] private SceneField mainMenuScene;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        // check if both puzzles are solved
        if (colosseumTetrisPuzzle.isSolved && vaticanSlidePuzzle.isSolved)
        {
            WinGame();
        }
    }

    public void DamagePisaTower()
    {
        Debug.Log("Pisa Tower is damaged!");
        pisaTowerHealth--;

        // check if the pisa tower is destroyed, otherwise update the sprite
        if (pisaTowerHealth <= 0) FailGame();
        else pisaTowerSpriteRenderer.sprite = pisaTowerSprites[pisaTowerHealth - 1];
    }

    public void FailGame()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void RestartGame()
    {
       // reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        // load main menu scene
        SceneManager.LoadScene(mainMenuScene.Name);
    }
}
