using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] int currentSceneIndex;
    bool isSplashScreen;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();    
    }

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current Scene Index: " + currentSceneIndex);

        if (currentSceneIndex == 4)
        {
         //   levelManager.LoadLevelBlocks();
        }

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

    /*void LoadLevel(LevelData levelData)
    {
        foreach (var blockData in levelData.blocks)
        {
            Vector3 position = new Vector3(blockData.position.x, blockData.position.y, 0f);

            GameObject blockPrefab = GetBlockPrefab(blockData.type);

            Instantiate(blockPrefab, position, Quaternion.identity);
        }
    }*/


    void SplashScreen()
    {
        if (currentSceneIndex == 0)
        {
            isSplashScreen = true;
            StartSceneLoad(1, 2);
        }
        else { isSplashScreen = false; }
    }

    public void MainMenu()
    {
        StartSceneLoad(1, 0);
    }

    public void Options()
    {
        StartSceneLoad(2, 0);
    }

    public void Shop()
    {
        StartSceneLoad(3, 0);
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
