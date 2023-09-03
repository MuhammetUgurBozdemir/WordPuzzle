using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = nameof(LevelSettings), menuName = "LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        public TextAsset[] levels;
    }
    
    public class Level
    {
        public string title;
        public List<TileData> tiles;
    }
    
    [System.Serializable]
    public class TileData
    {
        public int id;
        public Vector3 position;
        public string character;
        public List<int> children;
    }
}