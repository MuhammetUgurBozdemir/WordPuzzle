using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DictionarySettings), menuName = "DictionarySettings")]
public class DictionarySettings : ScriptableObject
{
    public TextAsset words;
    public List<LetterData> LetterDatas;
}

[Serializable]
public class LetterData
{
    public string Letter;
    public int Point;
}