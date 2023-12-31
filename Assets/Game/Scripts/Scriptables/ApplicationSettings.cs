using UnityEngine;

[CreateAssetMenu(fileName = nameof(ApplicationSettings), menuName = "ApplicationSettings")]
public class ApplicationSettings : ScriptableObject
{
    public GameView gameView;
    public TileView tileView;
    public LevelButtonView levelButtonView;
}