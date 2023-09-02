using DG.Tweening;
using Zenject;

namespace Game.Scripts.Tile
{
    public class TileSelectedState : ITileState
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
            var target = _letterBoxHolder.GetFirstEmptyBox();
            
            if (target == null) return;
            
            target.State = true;
            _tileView.transform.DOMove(target.Box.position, .35f).SetEase(Ease.InOutBack);
            
            _signalBus.Subscribe<WordSubmittedSignal>(_tileFacade.DespawnTile);
        }

        public void OnExitState()
        {
            _signalBus.Unsubscribe<WordSubmittedSignal>(_tileFacade.DespawnTile);
        }
        
        
        public void OnClicked()
        {
        }
    }
}