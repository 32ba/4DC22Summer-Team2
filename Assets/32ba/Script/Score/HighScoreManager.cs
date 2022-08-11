using System;
using UnityEngine;

namespace _32ba.Script.Score
{
    public class HighScoreManager
    {
        class HighScore
        {
            public string SongUuid { get; set; }
            public long Score { get; set; }
        }
        
        public long GetHighScore(string songUuid)
        {
            var dbHighScore = DBManager.Instance.DB.GetCollection<HighScore>("HighScore");
            var highScore = dbHighScore.FindOne(x => x.SongUuid.Equals(songUuid));
            return highScore?.Score ?? 0;
        }

        public void SetHighScore(string songUuid, long score)
        {
            var highScore = new HighScore()
            {
                SongUuid = songUuid,
                Score = score
            };
            var dbHighScore = DBManager.Instance.DB.GetCollection<HighScore>("HighScore");
            dbHighScore.Upsert(highScore);
        }
    }
}