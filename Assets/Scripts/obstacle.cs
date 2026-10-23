using UnityEngine;

public class obstacle : MonoBehaviour
{
    public gameManager gameManager;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the GameManager script
        gameManager = FindAnyObjectByType<gameManager>();

        // Set the obstacle speed randomly
        speed = Random.Range(1f, 5f);

        // Move the object to a random height offscreen
        transform.position = new Vector3(12, Random.Range(-3f, 3f), transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Stops the function if the game hasn't started (or the game ended)
        if (!gameManager.gameStarted) return;

        // Makes the obstacle constantly move left
        transform.position -= new Vector3(speed *  Time.deltaTime, 0, 0);

        // Destroys the obstacle if it goes too far left (offscreen)
        if (transform.position.x < -12)
        {
            Destroy(this);
        }
    }
}
