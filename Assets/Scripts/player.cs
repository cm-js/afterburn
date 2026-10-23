using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class player : MonoBehaviour
{
    [Header("Required Script(s)")]
    public gameManager gameManager;

    public bool isTouchingGround;
    public bool isTouchingBarrier;

    void Start()
    {
        // Freezes the player in place
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        // Update detection variables
        isTouchingGround = false;
        isTouchingBarrier = false;
    }

    // Initialises the player once the game starts
    // This function is called in gameManager.cs
    public void PlayerStart()
    {
        // Disables all rigidbody contstraints but prevents the player from falling over
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        // Moves the player to a reasonable spot
        transform.position = new Vector3(-5, 0, transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        // Only runs movement input detections if the game has started
        if (gameManager.gameStarted)
        {
            // Detects two different things to initiate a jump:
            // If W or UpArrow is pressed, and the player isn't already in the air
            if ((Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) && (isTouchingGround || gameObject.transform.position.y <= -2.48))
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, 8);
                Debug.Log("Player has jumped!");
            }

            // Detects if the player pressed A or LeftArrow and moves left
            if ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) && !isTouchingBarrier)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-5, GetComponent<Rigidbody2D>().linearVelocity.y);
            }

            // Detects if the player pressed D or RightArrow and moves right
            if ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) && !isTouchingBarrier)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(5, GetComponent<Rigidbody2D>().linearVelocity.y);
            }

            // Detects if the player is moving sideways and slows horizontal movement if so
            if (GetComponent<Rigidbody2D>().linearVelocity.x > 0.1 || GetComponent<Rigidbody2D>().linearVelocity.x < 0.1)
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x / 1.03f, GetComponent<Rigidbody2D>().linearVelocity.y);
            }
        }
    }

    // Detects if the player is touching an object with a collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Debug.LogWarning("Barrier hit!");
            isTouchingBarrier = true;

            playerKill();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.LogError("Enemy hit!");

            playerKill();
        }
    }

    // Detects if the player leaves the collision range of an object with a collider
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            isTouchingBarrier = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = false;
        }
    }

    private void playerKill()
    {
        // End the game (or modify lives if added later)
        gameManager.gameOver = true;

        // Freezes the player in place
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        // Runs "game end" functions in other scripts
        gameManager.gameEnd();
    }
}
