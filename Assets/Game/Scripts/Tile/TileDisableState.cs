using Zenject;

namespace Game.Scripts.Tile
{
    public class TileDisableState : ITileState
    {
        private SignalBus _signalBus;
        private TileView _tileView;

        [Inject]
        private void Construct(SignalBus signalBus, TileView tileView)
        {
            _signalBus = signalBus;
            _tileView = tileView;
        }

        public void OnEnterState()
        {
            _tileView.SetTileState(true);
        }

        public void OnExitState()
        {
        }

        public void OnClicked()
        {
        }
    }
}