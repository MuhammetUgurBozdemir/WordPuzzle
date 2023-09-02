using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LetterBoxHolder : MonoBehaviour
{
    [SerializeField] private List<BoxesAndStates> letterBoxes;

    public BoxesAndStates GetFirstEmptyBox()
    {
        var target = letterBoxes.FirstOrDefault(x => x.State == false);
        return target;
    }
}


[Serializable]
public class BoxesAndStates
{
    public Transform Box;
    public bool State;
}