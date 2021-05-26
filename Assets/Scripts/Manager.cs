using System.Collections;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections.Generic;

public class Manager: MonoBehaviour
{
    public static Manager Instance { get; private set; }
    // you can subscribe to this selection event in any class 
    public UnityEvent SphereSelectEvent = new UnityEvent();
    public Canvas scene2Canvas;
    public float fadeOutDuration;

    public static readonly int SCENE1IDX = 0;
    public static readonly int SCENE2IDX = 1;
    public static readonly int SCENE3IDX = 2;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<Manager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /**
     * Use this from button onClick events because only methods returning void work
     */
    public void ChangeScene(int sceneIdx)
    {
        // HACK
        // in scene 3, there was a bug where the dsiabled scenefadeout script on
        // the sphere group would run in addition to the one on the cheese,
        // so I had to fix it last minute in a nonelegant way
        if (SceneManager.GetActiveScene().name == "Scene3")
        {
            GameObject spheres = GameObject.FindWithTag("SphereGroup");
            // if opened scene 3 through right-clicking a sphere
            if (spheres != null)
            {
                spheres.GetComponent<SceneFadeOut>().enabled = false;
                spheres.GetComponent<SceneFadeIn>().enabled = false;
                SceneManager.MoveGameObjectToScene(spheres, SceneManager.GetActiveScene());
                Renderer correctSphereRenderer;
                foreach (GlobeTransition gt in spheres.GetComponentsInChildren<GlobeTransition>())
                {
                    if (gt.thisGlobeSelected == true)
                    {
                        correctSphereRenderer = gt.gameObject.GetComponent<MeshRenderer>();
                        GameObject.FindGameObjectWithTag("Scene3FadeOut").
                                                        GetComponent<SceneFadeOut>().
                                                            meshesToFadeOut.Add(correctSphereRenderer);
                    }

                }
            }
        }

        Instance.StartCoroutine(ChangeSceneCoroutine(sceneIdx));
    }

    public IEnumerator ChangeSceneCoroutine(int sceneIdx)
    {
        // HACK
        // in scene 3, there was a bug where the dsiabled scenefadeout script on
        // the sphere group would run in addition to the one on the cheese,
        // so I had to fix it last minute in a nonelegant way
        List<SceneFadeOut> correctFadeOut = FindObjectsOfType<SceneFadeOut>().ToList();
        SceneFadeOut toRemove = null;
        foreach (SceneFadeOut sfo in correctFadeOut)
        {
            if (sfo.gameObject.tag == "SphereGroup" && SceneManager.GetActiveScene().name == "Scene3")
                toRemove = sfo;
        }
        if (toRemove !=null)
            correctFadeOut.Remove(toRemove);


        yield return Instance.StartCoroutine(correctFadeOut[0].FadeOutCoroutine(2));
        SceneManager.LoadScene(sceneIdx);
    }


}