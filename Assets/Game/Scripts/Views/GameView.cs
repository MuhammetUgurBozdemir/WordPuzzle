using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class GameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private SignalBus _signalBus;
    private LevelModel _levelModel;
    private MainView _mainView;

    [Inject]
    private void Construct(SignalBus signalBus,
        LevelModel levelModel,
        [Inject(Id = "MainView")] MainView mainView)
    {
        _signalBus = signalBus;
        _levelModel = levelModel;
        _mainView = mainView;
    }

    public void Init(string title)
    {
        _signalBus.Subscribe<WordSubmittedSignal>(UpdateScoreView);
        scoreText.text = _levelModel.Score.ToString();
        levelText.text = title;
    }

    private void UpdateScoreView()
    {
        scoreText.text = _levelModel.Score.ToString();
    }

    public void Submit()
    {
        _signalBus.Fire<SubmitButtonClickedSignal>();
    }

    public void Undo()
    {
        _signalBus.Fire<UndoButtonClickedSignal>();
    }

    public void ShowMainView()
    {
        Dispose();
    }


    private void Dispose()
    {
        _signalBus.Unsubscribe<WordSubmittedSignal>(UpdateScoreView);
        _mainView.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}