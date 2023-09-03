using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controllers;
using TMPro;
using UnityEngine;
using Zenject;

public class LevelButtonView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Transform disablePanel;

    private int _index;

    private LevelController _levelController;
    private MainView _mainView;
    private LevelModel _levelModel;

    [Inject]
    private void Construct(LevelController levelController,
        [Inject(Id = "MainView")] MainView mainView,
        LevelModel levelModel)
    {
        _levelController = levelController;
        _mainView = mainView;
        _levelModel = levelModel;
    }

    public void Init(string title, int index)
    {
        levelText.text = title;
        _index = index;

        if (_levelModel.ReachedLevel < index)
        {
            disablePanel.gameObject.SetActive(true);
        }

        var score = ES3.Load("Level" + index, 0);
        
        scoreText.text = "Score: " + score;
    }

    public void InitLevel()
    {
        if (disablePanel.gameObject.activeSelf) return;
        
        _levelModel.CurrentLevel = _index;
        ES3.Save("CurrentLevel", _levelModel.CurrentLevel);
        
        _levelController.InitLevel(_index);
        _mainView.gameObject.SetActive(false);
    }
}