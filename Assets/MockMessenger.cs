using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class MockMessenger : MonoBehaviour
{
    private LevelModel _levelModel;
    private ApplicationController _applicationController;

    [Inject]
    private void Construct(LevelModel levelModel,
        ApplicationController applicationController)
    {
        _levelModel = levelModel;
        _applicationController = applicationController;
    }

    #region DeleteUserPrefs

    [Button, FoldoutGroup("UserPrefs")]
    private void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    #endregion
}