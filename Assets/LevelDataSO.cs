using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "LevelData")]
public class LevelDataSO : MonoBehaviour
{
    [System.Serializable]
    public struct BlockData
    {
        public Vector2Int position;
        public BlockType type;
    }

    public BlockData[] blocks;
}

public enum BlockType
{
    Breakable,
    Unbreakable,
    PowerUp
}
