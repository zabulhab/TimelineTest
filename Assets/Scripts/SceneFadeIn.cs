using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/**
 * Fades in all elements (other than timeline) when a scene opens.
 * Works on the canvas group alpha and any provided mesh renderers.
 * This should be attached to a canvas.
 */
public class SceneFadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    public float secondsDuration = 2;
    [SerializeField]
    public List<Renderer> meshesToFadeIn;
    public CanvasGroup canvasGroup;

    void Start()
    {
        Debug.Log("fade in start");
        if (canvasGroup != null)
            canvasGroup.alpha = 0; // for canvas renderer components
        foreach (Renderer go in meshesToFadeIn) // for non-canvas mesh renderer components
        {
            go.material.color = new Color(1f, 1f, 1f, 0f);
        }
        StartCoroutine(FadeInCoroutine(secondsDuration));
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // fade objects except this object
            // disable all orbiting scripts
            if (canvasGroup != null)
                canvasGroup.alpha += Time.deltaTime / duration;
            foreach (Renderer go in meshesToFadeIn)
            {
                float curAlpha = go.material.color.a;
                go.material.color = new Color(1f, 1f, 1f,
                                                            curAlpha + Time.deltaTime / duration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
