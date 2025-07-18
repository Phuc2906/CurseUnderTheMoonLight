using UnityEngine;

public class BackgroundFollowCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 initialOffset;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        initialOffset = transform.position - cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = transform.position;
        newPos.x = cameraTransform.position.x + initialOffset.x;
        newPos.y = cameraTransform.position.y + initialOffset.y;
        transform.position = newPos;
    }
}
