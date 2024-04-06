using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    LevelManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();    
    }

    void Start()
    {
        levelManager.GetBlocks();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            levelManager.RemoveBlock();
            Destroy(gameObject);
        }
    }
}
