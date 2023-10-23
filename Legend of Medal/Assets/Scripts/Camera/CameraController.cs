using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  
    public Vector3 offset;   
    public float zoomSpeed;   

    void Update()
    {
        
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Camera.main.orthographicSize -= zoom;

        
        
    }
}
