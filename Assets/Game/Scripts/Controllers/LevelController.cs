using Game.Scripts.Scriptables;
using Game.Scripts.Tile;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class LevelController
    {
        #region Injection

        private readonly ApplicationSettings _settings;
        private readonly LevelModel _levelModel;
        private DiContainer _diContainer;
        private SignalBus _signalBus;
        private LevelSettings _levelSettings;
        private TileFacade.Factory _factory;
        private WordController _wordController;
        private GameView _gameView;

        public LevelController(ApplicationSettings settings,
            LevelModel levelModel,
            DiContainer diContainer,
            SignalBus signalBus,
            LevelSettings levelSettings
            ,TileFacade.Factory factory,
            WordController wordController,
            [Inject(Id = "GameView")] GameView gameView)
        {
            _settings = settings;
            _levelModel = levelModel;
            _diContainer = diContainer;
            _signalBus = signalBus;
            _levelSettings = levelSettings;
            _factory = factory;
            _wordController = wordController;
            _gameView = gameView;
        }

        #endregion

        public void InitLevel(int index)
        {
            Level data = new Level();
            data = JsonUtility.FromJson<Level>(_levelSettings.levels[index].ToString());
            
            foreach (TileData dataTile in data.tiles)
            {
                _factory.Create(new TileFacade.Args(dataTile));
                _wordController.allChars.Add(dataTile.character);
            }

            _gameView.gameObject.SetActive(true);
            _gameView.Init(data.title);
        }
    }
    
}