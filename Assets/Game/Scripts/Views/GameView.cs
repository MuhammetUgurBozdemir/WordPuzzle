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
    private ApplicationController _applicationController;

    [Inject]
    private void Construct(SignalBus signalBus,
        LevelModel levelModel,
        ApplicationController applicationController)
    {
        _signalBus = signalBus;
        _levelModel = levelModel;
        _applicationController = applicationController;
    }

    public void Initialize()
    {
       
        
    }

    private void Init()
    {
    }

    public void Submit()
    {
        _signalBus.Fire<SubmitButtonClickedSignal>();
    }

    public void Undo()
    {
        _signalBus.Fire<UndoButtonClickedSignal>();
    }
  

    public void Dispose()
    {
    }
}