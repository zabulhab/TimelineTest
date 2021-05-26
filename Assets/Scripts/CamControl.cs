using UnityEngine;

/**
 * The camera this is attached to can zoom in/out and rotate around the objects in
 * the scene, but only while shift is held down. Scroll to zoom.
 */
public class CamControl : MonoBehaviour
{
    private float minFOV = 10;
    private float maxFOV = 100;
    public GameObject centerObject;

    void Update()
    {
        if (Input.GetButton("LeftShift")) 
        {
            // zoom
            
            float fov = Camera.main.fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel");
            fov = Mathf.Clamp(fov, minFOV, maxFOV);
            Camera.main.fieldOfView = fov;

            // rotation around center
            transform.RotateAround(centerObject.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 20 * Time.deltaTime);
            transform.RotateAround(centerObject.transform.position, Vector3.right, Input.GetAxis("Mouse Y") * 20 * Time.deltaTime);
        }
    }
}