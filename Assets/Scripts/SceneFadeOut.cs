using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeOut : MonoBehaviour
{
    public float secondsDuration = 2;
    [HideInInspector]
    public List<Renderer> meshesToFadeOut;
    private CanvasGroup canvasGroup;
    public Object scene3;

    // Start is called before the first frame update
    void Start()
    {
        Manager.Instance.SphereSelectEvent.AddListener(OpenS3WithSphereSelected);
        // fade out same meshes we faded in (unless sphere clicked in scene 2)
        meshesToFadeOut = gameObject.GetComponent<SceneFadeIn>().meshesToFadeIn;
        canvasGroup = gameObject.GetComponent<SceneFadeIn>().canvasGroup;
    }

    /**
     * Called when scene changes from timeline or from selecting sphere
     */
    public void OpenS3WithSphereSelected()
    {
        DisableSphereRightClicking();
        //StartCoroutine(FadeOutCoroutine(secondsDuration));
        StartCoroutine(Manager.Instance.ChangeSceneCoroutine(Manager.SCENE3IDX)); // once done, open scene3
    }

    /**
     * Disables the right-clicking on all spheres. Called once one has been right-clicked
     */
    private void DisableSphereRightClicking()
    {
        foreach (Renderer go in meshesToFadeOut)
        {
            go.gameObject.GetComponent<GlobeTransition>().enabled = false;
        }
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        float elapsedTime = 0;
        //sdafsd
        while (elapsedTime < duration)
        {
            // fade objects except this object
            // disable all orbiting scripts
            if (canvasGroup != null)
               canvasGroup.alpha -= Time.deltaTime / duration;
            foreach (Renderer go in meshesToFadeOut)
            {
                float curAlpha = go.material.color.a;
                go.material.color = new Color(1f, 1f, 1f,
                                                            curAlpha - Time.deltaTime / duration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
