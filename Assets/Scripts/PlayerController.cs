using UnityEngine;

internal sealed class PlayerController : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private Transform playerTransform;

    [Header("Fields")]
    [SerializeField] private float playerSpeed, playerUpThrustForce, jumpBufferTime = default;
    [SerializeField] private LayerMask groundLayer;

    private float playerRaycastMaxDistance = 0.7f;
    private bool moveLeft, moveRight, jump = false;

    private const float Y_LIMIT = -8.5f;

    private void FixedUpdate()
    {
        // player movement
        if (moveRight)
        {
            playerRigidBody.AddForce(playerTransform.right * playerSpeed, ForceMode2D.Force);
            moveRight = false;
        }

        else if (moveLeft)
        {
            playerRigidBody.AddForce(-playerTransform.right * playerSpeed, ForceMode2D.Force);
            moveLeft = false;
        }

        if (jump && IsPlayerGrounded())
        {
            playerRigidBody.linearVelocityY = 0f;
            playerRigidBody.AddForce(playerTransform.up * playerUpThrustForce, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void Update()
    {
        // input handling
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            moveRight = true;
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            moveLeft = true;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            jump = true;
            Invoke("KillPlayerInput", jumpBufferTime);
        }

        // calling LazyUpdate
        if (Random.Range(1, 60) <= 10)
            LazyUpdate();
    }

    // 1/6 chance of geting called
    private void LazyUpdate() 
    {
        // other conditionals
        if (playerTransform.position.y <= Y_LIMIT)
            Die();
    }

    private bool IsPlayerGrounded()
    {
        return Physics2D.Raycast(transform.position, -playerTransform.up, playerRaycastMaxDistance, groundLayer);
    }

    private void KillPlayerInput() 
    { 
        jump = false; 
    }

    private void Die() 
    {
        Debug.Log("Player died!");
    }
}
