using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform cameraTransform, playerTransform;

    [Header("Fields")]
    [SerializeField] private float cameraInterpolationRateX, cameraInterpolationRateY = default;

    private Vector3 targetPositionX, targetPositionY = default;

    private void Update()
    {
        // smoothly interpolating camera towards player (different for X and Y axis)
        targetPositionX = 
            new Vector3(playerTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);
        targetPositionY =
            new Vector3(cameraTransform.position.x, playerTransform.position.y, cameraTransform.position.z);
        cameraTransform.position = 
            Vector3.Lerp(cameraTransform.position, targetPositionX, cameraInterpolationRateX * Time.deltaTime);
        cameraTransform.position =
            Vector3.Lerp(cameraTransform.position, targetPositionY, cameraInterpolationRateY * Time.deltaTime);
    }
}
