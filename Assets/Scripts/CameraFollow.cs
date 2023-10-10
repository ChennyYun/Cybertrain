using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target (player) that the camera should follow
    public float smoothSpeed = 1f; // This can be adjusted for smoother or stiffer camera movement
    public Vector3 offset; // In case you want the camera to be slightly offset from the player's position

    public float targetZoom = 5f; // The desired zoom level. Adjust as needed.
    public float zoomSpeed = 1f; // How fast the camera zooms in/out. Adjust as needed.

    void FixedUpdate() 
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;

        float smoothZoom = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
        GetComponent<Camera>().orthographicSize = smoothZoom;
    }

}
