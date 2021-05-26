using UnityEngine;
/*
 * Attached to each element in the scene to make it rotatable. Gameobject needs a collider for this to work.
 */
public class Rotatable : MonoBehaviour
{
    [HideInInspector]
    public static float speed = 8;

    public void OnMouseDrag()
    {
        transform.Rotate(Input.GetAxis("Mouse Y")*speed, Input.GetAxis("Mouse X")*speed, 0);
    }
}
