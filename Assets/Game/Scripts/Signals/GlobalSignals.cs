using Game.Scripts.Scriptables;
using Game.Scripts.Tile;

public readonly struct LevelEndSignal
{
}

public readonly struct TileClickedSignal
{
    public readonly TileFacade TileFacade;

    public TileClickedSignal(TileFacade tileFacde)
    {
        TileFacade = tileFacde;
    }
}

public readonly struct SubmitButtonClickedSignal
{
    
}

public readonly struct UndoButtonClickedSignal
{
 
}

public readonly struct TileRemovedFromSubmittedSignal
{
    public readonly TileFacade TileFacade;

    public TileRemovedFromSubmittedSignal(TileFacade tileFacde)
    {
        TileFacade = tileFacde;
    }
}

public readonly struct WordSubmittedSignal
{
    public readonly string Word;

    public WordSubmittedSignal(string word)
    {
        Word = word;
    }
}

