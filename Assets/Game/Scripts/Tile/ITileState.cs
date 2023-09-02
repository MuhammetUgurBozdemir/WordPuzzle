using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileState
{
    void OnEnterState();
    void OnExitState();
    
    void OnClicked();
}