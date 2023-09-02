using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Controllers;
using JetBrains.Annotations;
using Zenject;

public class ApplicationController : IInitializable, IDisposable
{
    private GameView _gameView;

    #region Injection

    private readonly ApplicationSettings _settings;
    private readonly LevelModel _levelModel;
    private DiContainer _diContainer;
    private SignalBus _signalBus;
    private LevelController _levelController;
    private WordController _wordController;

    public ApplicationController(ApplicationSettings settings,
        LevelModel levelModel,
        DiContainer diContainer,
        SignalBus signalBus,
        LevelController levelController,
        WordController wordController)
    {
        _settings = settings;
        _levelModel = levelModel;
        _diContainer = diContainer;
        _signalBus = signalBus;
        _levelController = levelController;
        _wordController = wordController;
    }

    #endregion


    public void Initialize()
    {
        _levelModel.LoadData();
        InitCurrentLevel();
    }


    public void InitCurrentLevel()
    {
        _levelController.InitLevel();
        _wordController.Init();
    }


    public void InitNextLevel()
    {
        Dispose();
        _levelModel.CurrentLevel++;
        InitCurrentLevel();
    }

    public void Dispose()
    {
        _wordController.Dispose();
    }
}