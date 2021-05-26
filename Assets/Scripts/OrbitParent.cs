using UnityEngine;

/**
 * If this is on a gameobject, an OrbitChild should be able to orbit around it.
 * The child orbits around this object's relative z axis.
 * */
[RequireComponent(typeof(LineRenderer))]
public class OrbitParent : MonoBehaviour
{
    public float Radius = 3;

    [Range(3, 256)]
    public int numSegments = 128;

    void Start()
    {
        DoRenderer();
    }

    public void DoRenderer()
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.startWidth = .2f;
        lineRenderer.positionCount = numSegments + 1;
        lineRenderer.useWorldSpace = false;

        float deltaAngle = Mathf.PI * 2/ numSegments;
        float angle = 0f;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = Radius * Mathf.Cos(angle);
            float y = Radius * Mathf.Sin(angle);
            Vector3 pos = new Vector3(x, y, 0);
            
            lineRenderer.SetPosition(i, pos);
            angle += deltaAngle;
        }
    }
}
