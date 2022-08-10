using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject NotesA;
    [SerializeField] GameObject NotesB;

    private float moveSpan = 0.01f;
    private float nowTime = 0.0f;
    private int beatNum;
    private int beatCount = 0;
    private bool isBeat = false;


    private int BPM = 60;
    private int LPB = 4;

    private int[] scoreNum = {32, 34, 36, 40, 42, 44, 48, 50, 52, 54, 56, 58, 60};
    private int[] scoreBlock = {1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 2};

    void Start() 
    {
    }

    void Awake()
    {
	//string inputString = Resources.Load<TextAsset>("").ToString();
        InvokeRepeating("NotesIns", 0.0f, moveSpan);
    }

    void GetScoreTime()
    {
        nowTime += moveSpan;

        if(beatCount>scoreNum.Length) return;

	beatNum = (int)(nowTime * BPM / 60 * LPB);
    }

    void NotesIns(){

        GetScoreTime();

        if(beatCount < scoreNum.Length){
            isBeat = (scoreNum[beatCount]==beatNum);
        }
	
        if(isBeat){
            if(scoreBlock[beatCount]==0){
            }

            if(scoreBlock[beatCount]==1){
                Instantiate(NotesA, new Vector3(6.0f, 0.0f, 0.0f), Quaternion.identity);
            }

            if(scoreBlock[beatCount]==2){ 
                Instantiate(NotesB, new Vector3(6.0f, 0.0f, 0.0f), Quaternion.identity);
            }

            beatCount++;
            isBeat = false;
	}

        GetScoreTime();
        Debug.Log("nowTime" + nowTime);
        Debug.Log("beatNum" + beatNum);

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
