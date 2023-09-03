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

    [Inject]
    private void Construct(LevelController levelController,
        [Inject(Id = "MainView")] MainView mainView)
    {
        _levelController = levelController;
        _mainView = mainView;
    }

    public void Init(string title, int score, int index)
    {
        levelText.text = title;
        _index = index;
    }

    public void InitLevel()
    {
        // if (!disablePanel.gameObject.activeSelf)
        _levelController.InitLevel(_index);
        _mainView.gameObject.SetActive(false);
    }
}