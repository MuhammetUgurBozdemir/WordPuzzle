using System;
using Zenject;

namespace Game.Scripts.Tile
{
    public class TileStateManager : IInitializable, IDisposable
    {
        private TileState _currentState = TileState.Enable;
        private bool _isDisposed;

        public TileState CurrentState
        {
            get => _currentState;
            set
            {
                if (_isDisposed || _currentState == value) return;
                _states[(int)_currentState].OnExitState();
                _currentState = value;
                _states[(int)_currentState].OnEnterState();
            }
        }

        private readonly ITileState[] _states;

        public TileStateManager(TileDisableState tileDisableState,
            TileEnableState tileEnableState,
            TileSelectedState tileSelectedState)
        {
            _states = new ITileState[3];
            _states[(int)TileState.Enable] = tileEnableState;
            _states[(int)TileState.Disable] = tileDisableState;
            _states[(int)TileState.Selected] = tileSelectedState;
        }

        public void Initialize()
        {
            _states[(int)_currentState].OnEnterState();
        }

        public void OnClick()
        {
            _states[(int)_currentState].OnClicked();
        }

        public void Dispose()
        {
            _states[(int)_currentState].OnExitState();
            _isDisposed = true;
        }
    }
}