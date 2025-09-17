using UnityEngine;
using UnityEngine.SceneManagement;

internal sealed class PlayerController : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private ParticleSystem playerDeathParticles;

    [Header("Fields")]
    [SerializeField] private float playerSpeed, playerUpThrustForce, jumpBufferTime = default;
    [SerializeField] private LayerMask groundLayer;

    private float playerRaycastMaxDistance = 0.7f;
    private bool moveLeft, moveRight, jump, playerDied = false;
    private Vector3 spawnPosition = default;

    public uint score = default;
    public bool playerWon = default;

    private const float Y_LIMIT = -8.5f;

    private void Start()
    {
        spawnPosition = playerTransform.GetComponentInParent<Transform>().position;
    }

    private void FixedUpdate()
    {
        if (playerDied)
            return;

        // player movement
        if (moveRight)
        {
            playerRigidBody.AddForce(playerTransform.right * playerSpeed, ForceMode2D.Force);
            moveRight = false;
        }

        if (moveLeft)
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
        if (playerDied)
            return;

        // input handling
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            moveRight = true;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            moveLeft = true;
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
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
        if (playerDied)
            return;

        // other conditionals
        if (playerTransform.position.y <= Y_LIMIT)
            Die();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Death"))
            Die();
        if (collision.gameObject.CompareTag("Score"))
        {
            score++;
            ScoreObjectController scorePoint = collision.gameObject.GetComponent<ScoreObjectController>();
            scorePoint.Die();
            Destroy(collision.gameObject, 2f);
        }
        if (collision.gameObject.CompareTag("LevelCompletor")) 
        {
            playerWon = true;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            collision.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
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
        if (playerDied)
            return;
        playerSprite.enabled = false;
        playerDied = true;
        playerDeathParticles.Play();
        if (SceneManager.GetActiveScene().buildIndex == 0)
            Invoke(nameof(Restart), 1f);
        else 
        {
            score = default;
            Invoke(nameof(RePositionPlayer), 0.8f);
        }
    }

    private void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void RePositionPlayer() 
    {
        playerTransform.position = spawnPosition;
        playerSprite.enabled = true;
        playerDied = false;
    }
}
