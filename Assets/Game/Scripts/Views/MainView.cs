using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controllers;
using Game.Scripts.Scriptables;
using UnityEngine;
using Zenject;

public class MainView : MonoBehaviour
{
    [SerializeField] private Transform container;

    private List<LevelButtonView> _buttonViews= new List<LevelButtonView>();

    private LevelSettings _levelSettings;
    private DiContainer _diContainer;
    private ApplicationSettings _applicationSettings;

    [Inject]
    private void Construct(LevelSettings levelSettings,
        DiContainer diContainer,
        ApplicationSettings applicationSettings)
    {
        _levelSettings = levelSettings;
        _diContainer = diContainer;
        _applicationSettings = applicationSettings;
    }

    public void Init()
    {
        Level data = new Level();

        for (int i = 0; i < _levelSettings.levels.Length; i++)
        {
            data = JsonUtility.FromJson<Level>(_levelSettings.levels[i].ToString());
            LevelButtonView levelButtonView = _diContainer.InstantiatePrefabForComponent<LevelButtonView>(
                _applicationSettings.levelButtonView);

            _buttonViews.Add(levelButtonView);
            
            levelButtonView.transform.SetParent(container);

            levelButtonView.Init(data.title, i);
        }
    }

    public void Dispose()
    {
        foreach (LevelButtonView levelButtonView in _buttonViews)
        {
            Destroy(levelButtonView.gameObject);
        }
        _buttonViews.Clear();
    }
}