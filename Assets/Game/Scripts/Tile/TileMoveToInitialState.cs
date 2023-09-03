using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Tile;
using UnityEngine;
using Zenject;

public class TileMoveToInitialState : ITileState
{
    private SignalBus _signalBus;
    private TileView _tileView;
    private LetterBoxHolder _letterBoxHolder;
    private TileFacade _tileFacade;

    [Inject]
    private void Construct(SignalBus signalBus,
        TileView tileView,
        [Inject(Id = "LetterBoxHolder")] LetterBoxHolder letterBoxHolder,
        TileFacade tileFacade)
    {
        _signalBus = signalBus;
        _tileView = tileView;
        _letterBoxHolder = letterBoxHolder;
        _tileFacade = tileFacade;
    }

    public void OnEnterState()
    {
        _letterBoxHolder.MakeAvailableLastHolder();
        _tileView.transform.DOMove(_tileFacade.tileData.position, .35f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            _tileFacade.SetStateEnable();
        });
    }

    public void OnExitState()
    {
    }

    public void OnClicked()
    {
    }
}