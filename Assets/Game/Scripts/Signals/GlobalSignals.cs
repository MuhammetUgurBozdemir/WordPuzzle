using Game.Scripts.Scriptables;

public readonly struct LevelEndSignal
{
}

public readonly struct TileClickedSignal
{
    public readonly TileData TileData;

    public TileClickedSignal(TileData tileData)
    {
        TileData = tileData;
    }
}

public readonly struct SubmitButtonClicked
{
    
}

public readonly struct UndoButtonClicked
{
    
}

public readonly struct WordSubmittedSignal
{
    public readonly string Word;

    public WordSubmittedSignal(string word)
    {
        Word = word;
    }
}

