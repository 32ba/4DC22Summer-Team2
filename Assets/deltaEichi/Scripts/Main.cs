using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject NotesA;
    [SerializeField] GameObject NotesB;


    [Serializable] public class InputJson
    {
        public int BPM;
        public int LPB;
        public string music;
        public Notes[] chart;
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

    private int[] scoreNum;
    private int[] scoreBlock;
    private int[] scoreDirection;

    void Start() 
    {
    }

    void Awake()
    {
        string path = Application.dataPath + "/deltaEichi/jsons/test.json";
        using(var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            string result = fs.ReadToEnd();
            Debug.Log(result);
            InputJson inputJson = JsonUtility.FromJson<InputJson>(result);
            Debug.Log(inputJson.BPM);
            BPM = inputJson.BPM;
            LPB = inputJson.LPB;
            scoreNum = new int[inputJson.chart.Length];
            scoreBlock = new int[inputJson.chart.Length];
            scoreDirection = new int[inputJson.chart.Length];
            for (int i = 0; i < inputJson.chart.Length; i++)
            {
                scoreNum[i] = inputJson.chart[i].time;
                scoreBlock[i] = inputJson.chart[i].type;
            }
        }

	//string inputString = Resources.Load<TextAsset>("").ToString();
        //InvokeRepeating("NotesIns", 0.0f, moveSpan);
    }

    void GetScoreTime()
    {
        nowTime += moveSpan;

        if(beatCount>scoreNum.Length) return;

	beatNum = (int)(nowTime * BPM / 60 * LPB);
    }

    void NotesIns(){
        //Debug.Log(beatNum + ", " + nowTime);

        GetScoreTime();

        if(beatCount < scoreNum.Length){
            isBeat = (scoreNum[beatCount]==beatNum);
        }
	
        if(isBeat){
            if(scoreBlock[beatCount]==0){
            }

            if(scoreBlock[beatCount]==1){
                Instantiate(NotesA, new Vector3(10.0f, 0.0f, 0.0f), Quaternion.identity);
            }

            if(scoreBlock[beatCount]==2){ 
                Instantiate(NotesB, new Vector3(10.0f, 5.0f, 0.0f), Quaternion.identity);
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
        NotesIns();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(NotesA, new Vector3(6.0f, 0.0f, 0.0f), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Instantiate(NotesB, new Vector3(6.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }
}
