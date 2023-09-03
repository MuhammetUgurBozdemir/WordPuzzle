using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModel
{
    public int CurrentLevel;
    public int ReachedLevel;

    public void LoadData()
    {
        CurrentLevel = ES3.Load("CurrentLevel", 0);
        ReachedLevel = ES3.Load("ReachedLevel", 0);
    }
}