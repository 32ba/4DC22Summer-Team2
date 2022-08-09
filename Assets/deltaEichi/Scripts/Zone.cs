using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zone : MonoBehaviour
{
    
    [SerializeField] GameObject AinA;
    [SerializeField] GameObject AinB;
    [SerializeField] GameObject BinA;
    [SerializeField] GameObject BinB;
    [SerializeField] GameObject Miss;

    public bool boolIsNotesAStay = false;
    public bool boolIsNotesBStay = false;

    public int intAinA = 0;
    public int intAinB = 0;
    public int intBinA = 0;
    public int intBinB = 0;

    public int miss = 0;

    public Collider2D col;

    void UIUpdate()
    {
        AinA.GetComponent<TextMeshProUGUI>().text = "AinA: " + intAinA;
        AinB.GetComponent<TextMeshProUGUI>().text = "AinB: " + intAinB;
        BinA.GetComponent<TextMeshProUGUI>().text = "BinA: " + intBinA;
        BinB.GetComponent<TextMeshProUGUI>().text = "BinB: " + intBinB;
        Miss.GetComponent<TextMeshProUGUI>().text = "Miss: " + miss;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        //Aキーが入力されたとき
        if (Input.GetKeyDown(KeyCode.A))
        {
            //ノーツAが判定ゾーンにあれば
            if (boolIsNotesAStay)
            {
                boolIsNotesAStay = false;
                Debug.Log("A!");
                /*
                Debug.Log(this.transform.position);
                Debug.Log(col.transform.position);
                */
                Debug.Log(Vector3.Distance(this.transform.position, col.transform.position));
                Destroy(col.gameObject);

                intAinA++;
                UIUpdate();
            }

            //ノーツBが判定ゾーンにあれば
            if (boolIsNotesBStay)
            {
                boolIsNotesAStay = false;
                Debug.Log("NotA!");
                /*
                Debug.Log(this.transform.position);
                Debug.Log(col.transform.position);
                */
                Debug.Log(Vector3.Distance(this.transform.position, col.transform.position));
                Destroy(col.gameObject);

                intAinB++;
                UIUpdate();
            }
        }

        //Bキーが入力されたとき
        if (Input.GetKeyDown(KeyCode.B))
        {
            //ノーツBが判定ゾーンにあれば
            if (boolIsNotesBStay)
            {
                boolIsNotesBStay = false;
                Debug.Log("B!");
                /*
                Debug.Log(this.transform.position);
                Debug.Log(col.transform.position);
                */
                Debug.Log(Vector3.Distance(this.transform.position, col.transform.position));
                Destroy(col.gameObject);

                intBinB++;
                UIUpdate();
            }

            //ノーツAが判定ゾーンにあれば
            if (boolIsNotesAStay)
            {
                boolIsNotesBStay = false;
                Debug.Log("NotB!");
                /*
                Debug.Log(this.transform.position);
                Debug.Log(col.transform.position);
                */
                Debug.Log(Vector3.Distance(this.transform.position, col.transform.position));
                Destroy(col.gameObject);

                intBinA++;
                UIUpdate();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NotesA"))
        {
            boolIsNotesAStay = true;
            col = other;
        }

        if (other.gameObject.CompareTag("NotesB"))
        {
            boolIsNotesBStay = true;
            col = other;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NotesA"))
        {
            boolIsNotesAStay = false;
        }

        if (other.gameObject.CompareTag("NotesB"))
        {
            boolIsNotesBStay = false;
        }

        miss++;
        UIUpdate();
    }

}
