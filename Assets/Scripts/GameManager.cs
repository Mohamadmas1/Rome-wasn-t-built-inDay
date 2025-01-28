using System.Collections;
using System.Collections.Generic;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Puzzles")]
    [SerializeField] private ColosseumTetrisPuzzle colosseumTetrisPuzzle;
    [SerializeField] private VaticanSlidePuzzle vaticanSlidePuzzle;

    [Header("Pisa Tower")]
    [SerializeField] private Animator pisaTowerAnimator;
    private int pisaTowerHealth = 3;
    [SerializeField] private GameObject splashAntsPrefab;
    [SerializeField] private float hitDelay = 0.2f;
    private float hitTimer = 0.0f;

    [Header("Game Over Screens")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    [Header("Scene Management")]
    [SerializeField] private SceneField mainMenuScene;

    private AudioSource audioSource;
    [SerializeField] private AudioClip lostLifeSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip winSound;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // check if both puzzles are solved
        if (colosseumTetrisPuzzle.isSolved && vaticanSlidePuzzle.isSolved)
        {
            WinGame();
        }

        // limit the rate at which the player can splash ants
        hitTimer = Mathf.Max(0.0f, hitTimer - Time.deltaTime);
    }

    public void DamagePisaTower()
    {
        Debug.Log("Pisa Tower is damaged!");
        audioSource.PlayOneShot(lostLifeSound);
        pisaTowerHealth--;

        // check if the pisa tower is destroyed, otherwise update the sprite
        if (pisaTowerHealth <= 0)
        {
            pisaTowerAnimator.gameObject.SetActive(false);
            FailGame();
        }
        else pisaTowerAnimator.SetInteger("Health", pisaTowerHealth);
    }

    public void SpawnSplashAnts(BaseEventData eventData)
    {
        if (hitTimer > 0.0f) return;

        PointerEventData pointerEventData = (PointerEventData)eventData;
        Vector3 position = pointerEventData.position;
        position.z = 0;
        position = Camera.main.ScreenToWorldPoint(position);
        position.z = -1;
        Instantiate(splashAntsPrefab, position, Quaternion.identity);
    }

    public void FailGame()
    {
        Debug.Log("Lost game");
        audioSource.PlayOneShot(loseSound);
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    public void WinGame()
    {
        Debug.Log("Won game");
        audioSource.PlayOneShot(winSound);
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
