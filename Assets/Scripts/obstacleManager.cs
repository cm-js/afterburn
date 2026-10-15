using UnityEngine;

public class obstacleManager : MonoBehaviour
{
    [Header("Required Scripts")]
    public gameManager gameManager;

    [Header("Objects")]
    public GameObject[] groundObjects;

    void Start()
    {  
        // I don't think anything needs to be done here.
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStarted)
        {
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
        }
    }
}
