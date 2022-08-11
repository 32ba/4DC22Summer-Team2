using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGetter : SingletonMonoBehaviour<ScoreGetter>
{
    public class ScoreClass
    {
        public string SongUuid { get; set; }
        public int Score { get; set; }
    }

    public string songUuid;
    public int score;

    public void SetScore(ScoreClass model)
    {
        songUuid = model.SongUuid;
        score = model.Score;
    }
}
