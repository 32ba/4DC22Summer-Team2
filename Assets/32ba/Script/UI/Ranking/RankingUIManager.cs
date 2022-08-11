using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RankingUIManager : MonoBehaviour
{
    private GetRankingResponse _ranking;
    public string songUuid;

    [SerializeField] private GameObject scoreboardPrefab;
    [SerializeField] private GameObject scoreboardObj;

    private async void OnEnable()
    {
        var getRankingRequest = new GetRankingRequest
        {
            RankingType = "top100",
            SongUuid = songUuid
        };
        _ranking = await APIManager.GetRanking(getRankingRequest);
        foreach (var rank in _ranking.ranking)
        {
            var scoreboardElementObj = Instantiate(scoreboardPrefab, scoreboardObj.transform, true);
            scoreboardElementObj.transform.Find("RankText").GetComponent<TextMeshProUGUI>().text = rank.rank.ToString();
            scoreboardElementObj.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = rank.name;
            scoreboardElementObj.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = rank.score.ToString();
        }

        _ranking = null;
    }

    private void OnDisable()
    {
        foreach (Transform t in scoreboardObj.transform)
        {
            Destroy(t.gameObject);
        }
    }

    public void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }
}
