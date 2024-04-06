using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int blocksInLevel = 0;
    [SerializeField] int amountOfBlocksDestroyed = 0;

    [SerializeField] int lives = 3;
    [SerializeField] TMP_Text textLives;

    //void LevelToLoad() //from scriptable object? i don't know best way to load level of blocks. call in awake whatever

    void Start()
    {
        textLives.text = "x " + lives.ToString();    
    }

    public void GetBlocks()
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
