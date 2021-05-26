using UnityEngine;

/**
 * If this is on a GO, it should be able to orbit an orbit parent. Maybe this script isn't needed?
 */
public class OrbitChild : MonoBehaviour
{
    public OrbitParent parent;
    private Vector3 orbitAxis;
    private float orbitSpeed = 30;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        // if manager called sphere select event, all orbit child scripts everywhere should pause
        Manager.Instance.SphereSelectEvent.AddListener(PauseOrbit); 
        // relative to parent's rotation
        orbitAxis = parent.transform.rotation * parent.transform.forward;
        Vector3 offset = Vector3.Scale(parent.transform.lossyScale, new Vector3(parent.Radius, 0, 0));
        transform.position = parent.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
            return;
        orbitAxis = parent.transform.rotation * Vector3.forward;
        transform.RotateAround(parent.transform.position, orbitAxis, orbitSpeed * Time.deltaTime);
    }

    private void PauseOrbit()
    {
        isPaused = true;
    }
}
