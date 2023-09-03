using DG.Tweening;
using Zenject;

namespace Game.Scripts.Tile
{
    public class TileEnableState : ITileState
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
            _tileView.SetTileState(false);
        }

        public void OnExitState()
        {
        }

        public void OnClicked()
        {
            _tileFacade.SetStateSelected();
            _signalBus.Fire(new TileClickedSignal(_tileFacade));
        }
    }
}