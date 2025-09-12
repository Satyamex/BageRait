using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform cameraTransform, playerTransform;

    [Header("Fields")]
    [SerializeField] private float cameraInterpolationRate = default;

    private Vector3 targetPosition = default;

    private void Update()
    {
        targetPosition = 
            new Vector3(playerTransform.position.x, playerTransform.position.y, cameraTransform.position.z);
        cameraTransform.position = 
            Vector3.Lerp(cameraTransform.position, targetPosition, cameraInterpolationRate * Time.deltaTime);

    }
}
