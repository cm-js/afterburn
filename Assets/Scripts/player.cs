using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    [Header("Required Script(s)")]
    public gameManager gameManager;

    void Start()
    {
        // Freezes the player in place
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    // Initialises the player once the game starts
    // This function is called in gameManager.cs
    public void PlayerStart()
    {
        // Disables all rigidbody contstraints
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }


    // Update is called once per frame
    void Update()
    {
        // Only runs movement input detections if the game has started
        if (gameManager.gameStarted)
        {
            // Detects two different things to initiate a jump:
            // If W or UpArrow is pressed, and the player isn't already in the air
            if ((Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) && this.transform.position.y <= -2.45)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, 8);
                Debug.Log("Player has jumped!");
            }

            // Detects if the player pressed A or LeftArrow and moves left
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-5, GetComponent<Rigidbody2D>().linearVelocity.y);
            }

            // Detects if the player pressed D or RightArrow and moves right
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(5, GetComponent<Rigidbody2D>().linearVelocity.y);
            }

            // Detects if the player is moving sideways and slows horizontal movement if so
            if (GetComponent<Rigidbody2D>().linearVelocity.x > 0.1 || GetComponent<Rigidbody2D>().linearVelocity.x < 0.1)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x / 1.02f, GetComponent<Rigidbody2D>().linearVelocity.y);
            }
        }
    }
}
