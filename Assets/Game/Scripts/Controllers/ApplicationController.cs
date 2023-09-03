using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Controllers;
using JetBrains.Annotations;
using Zenject;

public class ApplicationController : IInitializable, IDisposable
{
    

    #region Injection

    private readonly LevelModel _levelModel;
    private LevelController _levelController;
    private WordController _wordController;
    private MainView _mainView;

    public ApplicationController(LevelModel levelModel,
        LevelController levelController,
        WordController wordController,
        [Inject(Id = "MainView")] MainView mainView)
    {
        _levelModel = levelModel;
        _levelController = levelController;
        _wordController = wordController;
        _mainView = mainView;
    }

    #endregion


    public void Initialize()
    {
        _levelModel.LoadData();
        InitCurrentLevel();
        _mainView.Init();
    }


    public void InitCurrentLevel()
    {
        // _levelController.InitLevel();
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