using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class gameManager : MonoBehaviour
{
    [Header("Required Scripts")]
    // This is how you import scripts into other scripts. Treat them like GameObjects
    public obstacleManager obstacleManager;
    public player player;

    [Header("Menu Management")]
    public GameObject startMenu;
    public Button startButton;
    [Space(5)]
    public GameObject overheadUI;
    [Space(5)]
    public GameObject gameOverMenu;
    public Button restartButton;
    public Button returnMenuButton;

    [Header("Game Management Vars")]
    public bool gameStarted;
    public bool gameOver;
    public float speed;

    [Header("Score Vars")]
    public float score;
    public float highScore;
    [Space(5)]
    public float distance;
    public float bestDistance;

    [Header("Score Displays")]
    public TextMeshProUGUI distanceDisplay;
    public TextMeshProUGUI scoreDisplay;


    void Awake()
    {
        gameStarted = false;
        gameOver = false;
        distance = 0;
        score = 0;

        overheadUI.SetActive(false);
        startMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }


    // A "public" thing means it can be accessed in other scripts/components
    public void gameStart()
    {
        // Voids the function if the game already started.
        if (gameStarted) return;

        // Reset scoring & other variables
        score = 0;
        distance = 0;

        // Lets other scripts know the game has started
        gameStarted = true;
        player.PlayerStart();
        speed = 5;
        Debug.Log("Starting game...");

        // Disables the Start Menu so the actual game can be seen
        // Also enables the proper game UI
        startMenu.SetActive(false);
        overheadUI.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameStarted && !gameOver)
        {
            // Updates the text fields of the TextMeshPro objects
            distanceDisplay.text = "Distance: " + (Math.Round(distance * 10) / 10).ToString();
            scoreDisplay.text = "Score: " + score.ToString();

            // Updates distance based on speed
            distance += speed * Time.deltaTime;
        }
    }

    public void gameEnd()
    {
        obstacleManager.gameEnd();

        if (score > highScore) highScore = score;
        if (distance > bestDistance) bestDistance = distance;

        gameOver = true;
        gameStarted = false;

        overheadUI.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void gameRestart()
    {
        Debug.Log("Restarting game...");
        gameOverMenu.SetActive(false);
        gameOver = false;

        gameStart();
    }

    public void backToMenu()
    {
        Debug.Log("Going back to menu...");
        gameOverMenu.SetActive(false);
        gameOver = false;
        startMenu.SetActive(true);
    }
}
