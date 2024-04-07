using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject breakableBlockPrefab;
    [SerializeField] GameObject unbreakableBlockPrefab;
    [SerializeField] GameObject powerUpBlockPrefab;

    [SerializeField] int blocksInLevel = 0;
    [SerializeField] int amountOfBlocksDestroyed = 0;

    [SerializeField] int lives = 3;
    [SerializeField] TMP_Text textLives;


    void Start()
    {
        //LoadLevelBlocks();
        textLives.text = "x " + lives.ToString();
    }

    public void LoadLevelBlocks(LevelDataSO levelDataSO)
    {
    }

    GameObject GetBlockPrefab(BlockType type)
    {
        switch (type)
        {
            case BlockType.Breakable:
                return breakableBlockPrefab;
            case BlockType.Unbreakable:
                return unbreakableBlockPrefab;
            case BlockType.PowerUp:
                return powerUpBlockPrefab;
            default:
                Debug.LogError("Unknown block type!");
                return null;
        }
    }

    public void GetBlocks() //Get BREAKABLE blocks...
    {
        blocksInLevel++;
    }

    public void RemoveBlock()
    {
        blocksInLevel--;
        WinCondition();
    }

    void WinCondition()
    {
        if (blocksInLevel <= 0)
        {
            Debug.Log("Player wins!");
        }
    }

    public void AddLife()
    {
        lives++;
        textLives.text = "x " + lives.ToString();
    }

    public void RemoveLife()
    {
        lives--;
        textLives.text = "x " + lives.ToString();
        LoseCondition();
    }

    void LoseCondition()
    {
        if (lives <= 0)
        {
            Debug.Log("Player loses!");
        }
    }
}
