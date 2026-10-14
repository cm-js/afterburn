using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    [Header("Required Scripts")]
    // This is how you import scripts into other scripts. Treat them like GameObjects
    public obstacleManager obstacleManager;

    [Header("Menu Management")]
    public GameObject startMenu;
    public Button startButton;
    [Space(5)]
    public GameObject overheadUI;

    [Header("Game Management Vars")]
    public bool gameStarted;

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
        overheadUI.SetActive(false);
    }

    // A "public" thing means it can be accessed in other scripts/components
    public void gameStart()
    {
        // Voids the function if the game already started.
        if (gameStarted) return;

        // Lets other scripts know the game has started
        gameStarted = true;
        Debug.Log("Starting game...");

        // Disables the Start Menu so the actual game can be seen
        // Also enables the proper game UI
        startMenu.SetActive(false);
        overheadUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            // Updates the text fields of the TextMeshPro objects
            distanceDisplay.text = "Distance: " + distance.ToString();
            scoreDisplay.text = "Score: " + score.ToString();
        }
    }
}
