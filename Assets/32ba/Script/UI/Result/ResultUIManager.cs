using System;
using System.Collections;
using System.Collections.Generic;
using LiteDB;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUIManager : MonoBehaviour
{
    private GetRankingResponse _ranking;
    private string _songUuid;

    [SerializeField] private GameObject scoreboardPrefabObj;
    [SerializeField] private Transform scoreboardElementObj;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    //[SerializeField] private GetRankingResponse _ranking;

    private async void Start()
    {
        /*var scoreObject = new ScoreGetter.ScoreClass()
        {
            SongUuid = "2a695382-7aa6-4aad-bcc0-51913909802c", //TEST UUID
            Score = 12345678
        };
        ScoreGetter.Instance.SetScore(scoreObject);*/
        _songUuid = ScoreGetter.Instance.songUuid;

        var sendScoreRequest = new SendScoreRequest
        {
            song_uuid = ScoreGetter.Instance.songUuid,
            score = ScoreGetter.Instance.score
        };
        await APIManager.SendScore(sendScoreRequest);

        var getRankingRequest = new GetRankingRequest
        {
            RankingType = "top5",
            SongUuid = ScoreGetter.Instance.songUuid
        };
        _ranking = await APIManager.GetRanking(getRankingRequest);
        foreach (var rank in _ranking.ranking)
        {
            var scoreboardObj = Instantiate(scoreboardPrefabObj, scoreboardElementObj, true);
            scoreboardObj.transform.Find("RankText").GetComponent<TextMeshProUGUI>().text = rank.rank.ToString();
            scoreboardObj.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = rank.name;
            scoreboardObj.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = rank.score.ToString();
        }

        scoreText.text = ScoreGetter.Instance.score.ToString();
        var highScore = GetHighScore(ScoreGetter.Instance.songUuid);
        if (highScore < ScoreGetter.Instance.score)
        {
            SetHighScore(ScoreGetter.Instance.songUuid, ScoreGetter.Instance.score);
            highScore = ScoreGetter.Instance.score;
        }
        highScoreText.text = highScore.ToString();
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickRetryButton()
    {
        TrasitionToGameScene();
    }
    
    private void TrasitionToGameScene() //ゲームへ遷移する際はこれを呼ぶ
    {
        SceneManager.sceneLoaded += SendSongUuidToGame;
        SceneManager.LoadScene("deltaEichi/Scenes/deltaEichi_test");
    }

    private void SendSongUuidToGame(Scene next, LoadSceneMode mode)
    {
        var gameManager = GameObject.FindWithTag("GameController").GetComponent<Main>();
        gameManager.songUuid = _songUuid;
        SceneManager.sceneLoaded -= SendSongUuidToGame;
    }
    
    private class HighScore
    {
        public string SongUuid { get; set; }
        public long Score { get; set; }
    }
        
    private static long GetHighScore(string songUuid)
    {
        var dbHighScore = DBManager.Instance.DB.GetCollection<HighScore>("HighScore");
        var highScore = dbHighScore.FindOne(x => x.SongUuid.Equals(songUuid));
        return highScore?.Score ?? 0;
    }

    private static void SetHighScore(string songUuid, long score)
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
