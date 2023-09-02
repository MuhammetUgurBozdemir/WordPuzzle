using UnityEngine;


[CreateAssetMenu(fileName = nameof(LibrarySettings), menuName = "LibrarySettings")]
public class LibrarySettings : ScriptableObject
{
    public TextAsset words;
}