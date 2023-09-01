using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class ApplicationController : IInitializable, IDisposable
{
    private GameView _gameView;

    #region Injection

    private readonly ApplicationSettings _settings;
    private readonly LevelModel _levelModel;
    private DiContainer _diContainer;
    private SignalBus _signalBus;

    public ApplicationController(ApplicationSettings settings,
        LevelModel levelModel,
        DiContainer diContainer,
        SignalBus signalBus)
    {
        _settings = settings;
        _levelModel = levelModel;
        _diContainer = diContainer;
        _signalBus = signalBus;
    }

    #endregion


    public void Initialize()
    {
        _gameView = _diContainer.InstantiatePrefabForComponent<GameView>(_settings.gameView);
        _levelModel.LoadData();
        InitCurrentLevel().Forget();
    }


    public async UniTask InitCurrentLevel()
    {
        _gameView.Initialize();
    }


    public void InitNextLevel()
    {
        Dispose();
        _levelModel.CurrentLevel++;
        InitCurrentLevel().Forget();
    }

    public void Dispose()
    {
        _gameView.Dispose();
    }
}