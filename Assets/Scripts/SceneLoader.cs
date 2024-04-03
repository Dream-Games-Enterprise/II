using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int currentSceneIndex;
    bool isSplashScreen;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current Scene Index: " + currentSceneIndex);

        SplashScreen();
    }

    void Update()
    {
        if (!isSplashScreen) { return; }
        else
        {
            if (Input.anyKey) { StartSceneLoad(1, 0); }
        }
    }

    void SplashScreen()
    {
        if (currentSceneIndex == 0)
        {
            isSplashScreen = true;
            StartSceneLoad(1, 2);
        }
        else { isSplashScreen = false; }
    }

    void StartSceneLoad(int scene, int delay)
    {
        StartCoroutine(LoadScene(scene, delay));
    }

    IEnumerator LoadScene(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}
