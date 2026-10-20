using Unity.VisualScripting;
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
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        isTouchingGround = false;
        isTouchingBarrier = false;
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
            if ((Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) && isTouchingGround)
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
            // TODO -- Fix downward velocity when moving into a barrier
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-1.001f * transform.position.x, GetComponent<Rigidbody2D>().linearVelocity.y);
            isTouchingBarrier = true;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
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
}
