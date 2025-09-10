using UnityEngine;

internal sealed class PlayerController : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private Transform playerTransform;

    [Header("Fields")]
    [SerializeField] private float playerSpeed = default;

    private void FixedUpdate()
    {
        playerRigidBody.AddForce(playerTransform.right * playerSpeed, ForceMode2D.Force);
    }
}
