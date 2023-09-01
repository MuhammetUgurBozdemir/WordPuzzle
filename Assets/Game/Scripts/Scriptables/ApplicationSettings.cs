using UnityEngine;

[CreateAssetMenu(fileName = nameof(ApplicationSettings), menuName = "ApplicationSettings")]
public class ApplicationSettings : ScriptableObject
{
    public GameView gameView;
}