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
    [SerializeField] private GameObject submitButton;

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
        levelText.text = title;
        scoreText.text = 0.ToString();
    }

    public void UpdateScoreView(int score)
    {
        scoreText.text = (Convert.ToInt16(scoreText.text) + score).ToString();
    }

    public void Submit()
    {
        _signalBus.Fire<SubmitButtonClickedSignal>();
    }

    public void SetSubmitButtonVisibility(bool state)
    {
        submitButton.SetActive(state);
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
        _mainView.Dispose();
        _mainView.gameObject.SetActive(true);
        _mainView.Init();
        gameObject.SetActive(false);
    }
}