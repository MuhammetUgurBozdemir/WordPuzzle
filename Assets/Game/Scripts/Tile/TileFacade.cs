using System.Collections.Generic;
using Game.Scripts.Scriptables;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Tile
{
    public class TileFacade : MonoBehaviour, IInitializable, IPoolable<TileFacade.Args, IMemoryPool>
    {
        private TileData _tileData;

        private List<int> _submittedTile = new List<int>();
        private List<int> _childrens;

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
            _signalBus.Unsubscribe<TileClickedSignal>(CheckHasChild);
            _signalBus.Unsubscribe<TileClickedSignal>(SaveSubmittedTile);
            _signalBus.Unsubscribe<TileRemovedFromSubmittedSignal>(UpdateSubmittedTiles);
        }

        public void OnSpawned(Args args, IMemoryPool pool)
        {
            _signalBus.Subscribe<TileClickedSignal>(CheckHasChild);
            _signalBus.Subscribe<TileClickedSignal>(SaveSubmittedTile);
            _signalBus.Subscribe<TileRemovedFromSubmittedSignal>(UpdateSubmittedTiles);

            _tileView.Init(args.TileData);
            _tileData = args.TileData;
            _pool = pool;
            _childrens = new List<int>(tileData.children);

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
        }

        private void CheckHasChild(TileClickedSignal signal)
        {
            if (!tileData.children.Contains(signal.TileFacade.tileData.id)) return;

            tileData.children.Remove(signal.TileFacade.tileData.id);

            if (tileData.children.Count > 0) return;

            SetStateEnable();
        }

        public void SetStateDisable()
        {
            _tileStateManager.CurrentState = TileState.Disable;
        }

        public void SetStateSelected()
        {
            _tileStateManager.CurrentState = TileState.Selected;
        }

        public void SetStateEnable()
        {
            _tileStateManager.CurrentState = TileState.Enable;
        }

        public void SetStateMoveToInitialPos()
        {
            _tileStateManager.CurrentState = TileState.MoveToInitialPosition;
        }

        private void SaveSubmittedTile(TileClickedSignal signal)
        {
            if (!_childrens.Contains(signal.TileFacade.tileData.id)) return;
            _submittedTile.Add(signal.TileFacade.tileData.id);
        }

        private void UpdateSubmittedTiles(TileRemovedFromSubmittedSignal signal)
        {
            if (!_submittedTile.Contains(signal.TileFacade.tileData.id)) return;

            _submittedTile.Remove(signal.TileFacade.tileData.id);
            tileData.children.Add(signal.TileFacade.tileData.id);

            SetStateDisable();
        }

        public void DespawnTile()
        {
            _pool.Despawn(this);
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