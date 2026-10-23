using UnityEngine;

public class obstacleManager : MonoBehaviour
{
    [Header("Required Scripts")]
    public gameManager gameManager;
    public player player;

    [Header("Objects")]
    public GameObject[] groundObjects;
    public GameObject[] barrierGlows;
    public GameObject[] obstacles;

    [Header("Variables")]
    public float cooldownTimer;

    void Start()
    {  
        // I don't think anything needs to be done here.
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStarted && !gameManager.gameOver)
        {
            // Update the ground objects
            for (int i = 0; i < groundObjects.Length; i++)
            {
                // Changes the x-position of the ground object based on speed & time. Keeps the y and z coordinates unchanged 
                groundObjects[i].transform.position -= new Vector3(gameManager.speed * Time.deltaTime, 0, 0);

                // Detect if a ground object has gone too far left, and moves it back to the right
                if (groundObjects[i].transform.position.x <= -50)
                {
                    groundObjects[i].transform.position = new Vector3(groundObjects[i].transform.position.x + 150, groundObjects[i].transform.position.y, groundObjects[i].transform.position.z);
                }
            }

            // Update the barrier glows
            for (int i = 0; i < barrierGlows.Length; i++)
            {
                // Detect the distance from the player ONLY on the X axis
                Vector3 playerPos = new Vector3(player.transform.position.x, 0, 0);
                Vector3 barrierPos = new Vector3(barrierGlows[i].transform.position.x, 0, 0);
                var distance = Vector2.Distance(playerPos, barrierPos);

                var rend = barrierGlows[i].GetComponent<SpriteRenderer>();

                // Calculate the right opacity
                var multi = (distance * 200 > 255) ? 255 : distance * 200;
                var opacity = 255 - (multi);

                // Set the opacity of the glow based on the player's distance to it
                rend.color = new Color32(255, 0, 0, (byte)opacity);
            }

            // Spawns obstacles after a randomised cooldown
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0.01)
            {
                cooldownTimer = Random.Range(2f, 8f);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)]);
            }

        }

        // Disables the barrier glows if no game is active
        for (int i = 0;i < barrierGlows.Length; i++)
        {
            barrierGlows[i].SetActive(gameManager.gameStarted);
        }
    }

    public void gameEnd()
    {
        // Update the barrier glows
        for (int i = 0; i < barrierGlows.Length; i++)
        {
            barrierGlows[i].GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
        }

        // Update variables
        cooldownTimer = 0;

        // TODO -- Remove all existing obstacles
    }
}
