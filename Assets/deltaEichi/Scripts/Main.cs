using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

class Song
{
    public string Uuid { get; set; }
    public string Path { get; set; }
}

public class Main : MonoBehaviour
{
    [SerializeField] GameObject NotesA;
    [SerializeField] GameObject NotesB;
    [SerializeField] GameObject NotesEX;
    [SerializeField] GameObject Zone;

    private AudioSource[] sources;

    [Serializable]
    public class InputJson
    {
        public int BPM;
        public int LPB;
        public string music;
        public Notes[] chart;
        public string uuid;
    }

    [Serializable]
    public class Notes
    {
        public int time;
        public int direction;
        public int type;
    }
    [SerializeField] private GameObject gameOverPanelObject;


    private float moveSpan = 0.01f;
    private float nowTime = 0.0f;
    private int beatNum;
    private int beatCount = 0;
    private bool isBeat = false;

    public int BPM;
    public int LPB;

    private bool isEnd = false;

    private int score;
    public string songUuid; // TODO ビルド時は必ず代入をやめる

    private Dictionary<string, string> _songs = new()
    {
        {"0f1b605e-53e1-45ca-92a8-8dc97a63071e", "/deltaEichi/jsons/test.json"},
        {"5676c60e-3274-4111-86c9-47b7af6ba8f7", "/deltaEichi/jsons/calmest.json"},//{UUID, PATH}
    };

    private int[] scoreNum;
    private int[] scoreBlock;
    private int[] scoreDirection;


    void Start()
    {
        sources = gameObject.GetComponents<AudioSource>();
        
        #if UNITY_STANDALONE_OSX
            var path = _songs.Where(s => s.Key == songUuid).Aggregate(Application.dataPath, (current, s) => current +"/Resources/Data/StreamingAssets" + s.Value);
        #else
            var path = _songs.Where(s => s.Key == songUuid).Aggregate(Application.dataPath, (current, s) => current +"/StreamingAssets" + s.Value); //自動変換でキモいコードになってしまった
        #endif
        
        //string path = Application.dataPath + "/deltaEichi/jsons/test.json";
        using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            string result = fs.ReadToEnd();
            InputJson inputJson = JsonUtility.FromJson<InputJson>(result);
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
    }

    void Awake()
    {


        //string inputString = Resources.Load<TextAsset>("").ToString();
        //InvokeRepeating("NotesIns", 0.0f, moveSpan);
    }

    void GetScoreTime()
    {
        nowTime += moveSpan;

        if (beatCount >= scoreNum.Length)
        {
            StartCoroutine("End");
        }

        beatNum = (int)((nowTime) * BPM / 60 * LPB);
    }

    private IEnumerator End()
    {

        yield return new WaitForSeconds(5.0f);
        isEnd = true;
        score = Zone.gameObject.GetComponent<Zone>().score;
        TrasitionToResultScene();
    }

    void NotesIns()
    {
        //Debug.Log(beatNum + ", " + nowTime);

        GetScoreTime();

        if (beatCount < scoreNum.Length)
        {
            isBeat = (scoreNum[beatCount] == beatNum);
        }

        if (isBeat)
        {
            float v3 = 0f;
            if (scoreDirection[beatCount] == 0)
            {
                v3 = 5f;
            }

            if (scoreDirection[beatCount] == 1)
            {
                v3 = 0.6f;
            }

            if (scoreDirection[beatCount] == 2)
            {
                v3 = -5f;
            }


            if (scoreBlock[beatCount] == 0)
            {
                //夏空
                if (songUuid== "0f1b605e-53e1-45ca-92a8-8dc97a63071e")
                {
                    sources[0].Play();
                }

                //calmest
                if (songUuid == "5676c60e-3274-4111-86c9-47b7af6ba8f7")
                {
                    sources[2].Play();
                }
            }

            if (scoreBlock[beatCount] == 1)
            {
                Instantiate(NotesA, new Vector3(10f, v3, 0f), Quaternion.identity);
            }

            if (scoreBlock[beatCount] == 2)
            {
                Instantiate(NotesB, new Vector3(10f, v3, 0f), Quaternion.identity);
            }

            if (scoreBlock[beatCount] == 4)
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
        if (isEnd == false)
        {
            NotesIns();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GameOver() //ゲームオーバー時に呼ぶ
    {

        sources[0].Stop();
        isEnd = true;

        gameOverPanelObject.SetActive(true); //ToDo これの前に音声停止処理を呼ぶ
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
        var gameManager = GameObject.FindWithTag("GameController").GetComponent<ScoreGetter>();
        gameManager.SetScore(score);
        SceneManager.sceneLoaded -= SendScoreToResult;
    }
}
