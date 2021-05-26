using System.Collections;
using UnityEngine;

/**
 * Notifies event listeners that sphere was clicked and moves sphere to center
 * of the screen. 
 */
public class GlobeTransition : MonoBehaviour
{
    private SceneFadeOut fadeOut;
    public bool thisGlobeSelected;

    private void Start()
    {
        fadeOut = FindObjectOfType<SceneFadeOut>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // right clicked
        {
            // remove this globe's mesh from the renderers getting faded out
            fadeOut.meshesToFadeOut.Remove(GetComponent<MeshRenderer>());
            // Tell manager sphere select event happened
            Manager.Instance.SphereSelectEvent.Invoke(); 
            StartCoroutine(LerpCoroutine(2));
            DontDestroyOnLoad(fadeOut.gameObject); // TODO: DO destroy when leaving scene3
            thisGlobeSelected = true;
        }
    }

    private IEnumerator LerpCoroutine(float duration)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = new Vector3(0, 0, 0);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
