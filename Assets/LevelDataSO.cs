using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    [System.Serializable]
    public struct BlockData
    {
        public GameObject blockWithinGame;
        public Vector2 pos;
    }

    public BlockData[] blocks;
}

public enum BlockType
{
    Breakable,
    Unbreakable,
    PowerUp
}
