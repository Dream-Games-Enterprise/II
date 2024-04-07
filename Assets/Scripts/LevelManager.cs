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

    //void LevelToLoad() //from scriptable object? i don't know best way to load level of blocks

    void Start()
    {
        //load level blocks here without scene loader?
        textLives.text = "x " + lives.ToString();
    }

    public void LoadLevelBlocks(LevelDataSO levelDataSO)
    {
        //
        foreach (var blockData in levelDataSO.blocks)
        {
            Vector3 position = new Vector3(blockData.position.x, blockData.position.y, 0f);

            GameObject blockPrefab = GetBlockPrefab(blockData.type);

            if (blockPrefab != null)
            {
                Instantiate(blockPrefab, position, Quaternion.identity);
                // Optionally, you can keep track of the instantiated blocks or their references for further interaction.
            }
            else
            {
                Debug.LogError("Missing block prefab for type: " + blockData.type);
            }
        }
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
