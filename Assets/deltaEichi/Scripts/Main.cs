using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject NotesA;
    [SerializeField] GameObject NotesB;
    [SerializeField] GameObject NotesEX;
    [SerializeField] GameObject Zone;

    private AudioSource[] sources;

    [Serializable] public class InputJson
    {
        public int BPM;
        public int LPB;
        public string music;
        public Notes[] chart;
        public string uuid;
    }

    [Serializable] public class Notes
    {
        public int time;
        public int direction;
        public int type;
    }

    private float moveSpan = 0.01f;
    private float nowTime = 0.0f;
    private int beatNum;
    private int beatCount = 0;
    private bool isBeat = false;

    private int BPM;
    private int LPB;

    private bool isEnd = false;

    private int score;
    private string songUuid;

    private int[] scoreNum;
    private int[] scoreBlock;
    private int[] scoreDirection;

    void Start() 
    {
    }

    void Awake()
    {
        sources = gameObject.GetComponents<AudioSource>();
        string path = Application.dataPath + "/deltaEichi/jsons/test.json";
        using(var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            string result = fs.ReadToEnd();
            Debug.Log(result);
            InputJson inputJson = JsonUtility.FromJson<InputJson>(result);
            Debug.Log(inputJson.BPM);
            songUuid = inputJson.uuid;
            BPM = inputJson.BPM;
            LPB = inputJson.LPB;
            scoreNum = new int[inputJson.chart.Length];
            scoreBlock = new int[inputJson.chart.Length];
            scoreDirection = new int[inputJson.chart.Length];
            for (int i = 0; i < inputJson.chart.Length; i++)
            {
                scoreNum[i] = inputJson.chart[i].time;
                scoreBlock[i] = inputJson.chart[i].type;
                scoreDirection[i] = inputJson.chart[i].direction;
            }
        }

	//string inputString = Resources.Load<TextAsset>("").ToString();
        //InvokeRepeating("NotesIns", 0.0f, moveSpan);
    }

    void GetScoreTime()
    {
        nowTime += moveSpan;

        if (beatCount >= scoreNum.Length)
        {
            Debug.Log("end");
            isEnd = true;
            score = Zone.gameObject.GetComponent<Zone>().score;
            Debug.Log(score);
            return;
        }

	    beatNum = (int)((nowTime) * BPM / 60 * LPB);
    }

    void NotesIns(){
        //Debug.Log(beatNum + ", " + nowTime);

        GetScoreTime();

        if(beatCount < scoreNum.Length){
            isBeat = (scoreNum[beatCount] == beatNum);
        }
	
        if(isBeat){
            float v3 = 0f;
            if (scoreDirection[beatCount] == 0)
            {
                v3 = 5f;
            }

            if (scoreDirection[beatCount] == 1)
            {
                v3 = 0f;
            }

            if (scoreDirection[beatCount] == 2)
            {
                v3 = -5f;
            }


            if (scoreBlock[beatCount]==0)
            {
                sources[0].Play();
            }

            if(scoreBlock[beatCount]==1){
                Instantiate(NotesA, new Vector3(10f, v3, 0f), Quaternion.identity);
            }

            if(scoreBlock[beatCount]==2){ 
                Instantiate(NotesB, new Vector3(10f, v3, 0f), Quaternion.identity);
            }

            if(scoreBlock[beatCount] == 4)
            {
                sources[1].Play();
            }

            beatCount++;
            isBeat = false;
	}

        GetScoreTime();
        //Debug.Log("nowTime" + nowTime);
        //Debug.Log("beatNum" + beatNum);

    }

    void FixedUpdate()
    {
        if(isEnd == false)
        {
            NotesIns();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void TrasitionToResultScene() //リザルトへ遷移する際はこれを呼ぶ
    {
        SceneManager.sceneLoaded += SendScoreToResult;
        SceneManager.LoadScene("32ba/Scenes/Result");
    }

    private void SendScoreToResult(Scene next, LoadSceneMode mode)
    {
        var score = new ScoreGetter.ScoreClass()
        {
            Score = this.score,
            SongUuid = songUuid
        };
        var gameManager = GameObject.FindWithTag("ScoreGetter").GetComponent<ScoreGetter>();
        gameManager.SetScore(score);
        SceneManager.sceneLoaded -= SendScoreToResult;
    }
}
