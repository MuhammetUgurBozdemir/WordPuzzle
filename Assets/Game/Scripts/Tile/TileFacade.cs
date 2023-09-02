using Game.Scripts.Scriptables;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Tile
{
    public class TileFacade : MonoBehaviour, IInitializable, IPoolable<TileFacade.Args, IMemoryPool>
    {
        private TileData _tileData;
        
        public TileData tileData => _tileData;

        #region Injection

        private IMemoryPool _pool;

        private TileStateManager _tileStateManager;
        private SignalBus _signalBus;
        private TileView _tileView;

        [Inject]
        public void Construct(TileStateManager tileStateManager,
            SignalBus signalBus,
            TileView tileView)
        {
            _signalBus = signalBus;
            _tileStateManager = tileStateManager;
            _tileView = tileView;
        }

        #endregion

        public void Initialize()
        {
        }

        public void OnDespawned()
        {
            _tileStateManager.CurrentState = TileState.Disable;
        }

        public void OnSpawned(Args args, IMemoryPool pool)
        {
            _tileView.Init(args.TileData);
            _tileData = args.TileData;
            _pool = pool;

            if (args.TileData.children.Count == 0)
            {
                SetStateEnable();
            }
            else
            {
                SetStateDisable();
            }
        }

        public void OnClick()
        {
            _tileStateManager.OnClick();
            Debug.Log(_tileData.character);
        }

        public void SetStateDisable()
        {
            _tileStateManager.CurrentState = TileState.Disable;
        }
        
        public void SetStateSelected()
        {
            _tileStateManager.CurrentState = TileState.Selected;
        }

        public void DespawnTile()
        {
            _pool.Despawn(this);
        }

        private void SetStateEnable()
        {
            _tileStateManager.CurrentState = TileState.Enable;
        }

        public class Factory : PlaceholderFactory<Args, TileFacade>
        {
        }

        public class Pool : MonoPoolableMemoryPool<Args, IMemoryPool, TileFacade>
        {
        }

        public readonly struct Args
        {
            public readonly TileData TileData;
            public Args(TileData tileData) : this()
            {
                TileData = tileData;
            }
        }
    }
}