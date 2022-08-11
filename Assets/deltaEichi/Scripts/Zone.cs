using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Zone : MonoBehaviour
{
    
    [SerializeField] GameObject AinA;
    [SerializeField] GameObject AinB;
    [SerializeField] GameObject BinA;
    [SerializeField] GameObject BinB;
    [SerializeField] GameObject Miss;
    public Slider slider;

    private AudioSource[] sources;

    public bool boolIsNotesAStay = false;
    public bool boolIsNotesBStay = false;
    public bool boolIsNotesEXStay = false;

    public int notesPerfect = 0;
    public int notesGreat = 0;
    public int notesGood = 0;

    public int intAinA = 0;
    public int intAinB = 0;
    public int intBinA = 0;
    public int intBinB = 0;
    private float hitPoint = 100;

    public int miss = 0;

    public int intCountEX = 0;

    public Collider2D col;


    private int magni = 2;
    private int perfectScore = 4;
    private int greatScore = 2;
    private int goodScore = 1;

    public int score = 0;

    void UIUpdate()
    {
        AinA.GetComponent<TextMeshProUGUI>().text = "AinA: " + intAinA;
        AinB.GetComponent<TextMeshProUGUI>().text = "AinB: " + intAinB;
        BinA.GetComponent<TextMeshProUGUI>().text = "BinA: " + intBinA;
        BinB.GetComponent<TextMeshProUGUI>().text = "BinB: " + intBinB;
        Miss.GetComponent<TextMeshProUGUI>().text = "Miss: " + miss;
    }
    void Awake()
    {
        sources = gameObject.GetComponents<AudioSource>();
        Debug.Log(slider.GetComponent<Slider>().value);
        slider.GetComponent<Slider>().value = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIUpdate();
    }

    // Update is called once per frame
    void Update()
    {

        //A�L�[�����͂��ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (boolIsNotesEXStay)
            {
                intCountEX++;
            }
            else
            {
                //�m�[�cA������]�[���ɂ����
                if (boolIsNotesAStay)
                {
                    float floatDist = Vector3.Distance(this.transform.position, col.transform.position);
                    int s;
                    if (floatDist < 0.1f)
                    {
                        notesPerfect++;
                        Debug.Log("Perfect!");
                        s = perfectScore;
                    }
                    else if(floatDist < 0.3f)
                    {
                        notesGreat++;
                        Debug.Log("Great!");
                        s = greatScore;
                    }
                    else
                    {
                        notesGood++;
                        Debug.Log("Good!");
                        s = goodScore;
                    }
                    score += s * magni;
                    sources[0].Play();
                    boolIsNotesAStay = false;
                    NotesScript script = col.gameObject.GetComponent<NotesScript>();
                    script.boolActive = false;
                    Destroy(col.gameObject);


                    intAinA++;
                    UIUpdate();
                }

                //�m�[�cB������]�[���ɂ����
                if (boolIsNotesBStay)
                {
                    float floatDist = Vector3.Distance(this.transform.position, col.transform.position);
                    int s;
                    if (floatDist < 0.1)
                    {
                        notesPerfect++;
                        Debug.Log("Perfect!");
                        s = perfectScore;
                    }
                    else if (floatDist < 0.2)
                    {
                        notesGreat++;
                        Debug.Log("Great!");
                        s = greatScore;
                    }
                    else
                    {
                        notesGood++;
                        Debug.Log("Good!");
                        s = goodScore;
                    }

                    score += s;

                    sources[1].Play();
                    boolIsNotesAStay = false;
                    Debug.Log("NotA!");
                    /*
                    Debug.Log(this.transform.position);
                    Debug.Log(col.transform.position);
                    */
                    NotesScript script = col.gameObject.GetComponent<NotesScript>();
                    script.boolActive = false;
                    Destroy(col.gameObject);

                    intAinB++;
                    UIUpdate();
                }

            }
        }

        //B�L�[�����͂��ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (boolIsNotesEXStay)
            {
                intCountEX++;
            }
            else
            {
                //�m�[�cB������]�[���ɂ����
                if (boolIsNotesBStay)
                {
                    float floatDist = Vector3.Distance(this.transform.position, col.transform.position);
                    int s;
                    if (floatDist < 0.1)
                    {
                        notesPerfect++;
                        Debug.Log("Perfect!");
                        s = perfectScore;
                    }
                    else if (floatDist < 0.2)
                    {
                        notesGreat++;
                        Debug.Log("Great!");
                        s = greatScore;
                    }
                    else
                    {
                        notesGood++;
                        Debug.Log("Good!");
                        s = goodScore;
                    }

                    score += s * magni;
                    sources[1].Play();
                    boolIsNotesBStay = false;
                    Debug.Log("B!");
                    /*
                    Debug.Log(this.transform.position);
                    Debug.Log(col.transform.position);
                    */
                    NotesScript script = col.gameObject.GetComponent<NotesScript>();
                    script.boolActive = false;
                    Destroy(col.gameObject);

                    intBinB++;
                    UIUpdate();
                }

                //�m�[�cA������]�[���ɂ����
                if (boolIsNotesAStay)
                {
                    float floatDist = Vector3.Distance(this.transform.position, col.transform.position);
                    int s;
                    if (floatDist < 0.1)
                    {
                        notesPerfect++;
                        Debug.Log("Perfect!");
                        s = perfectScore;
                    }
                    else if (floatDist < 0.2)
                    {
                        notesGreat++;
                        Debug.Log("Great!");
                        s = greatScore;
                    }
                    else
                    {
                        notesGood++;
                        Debug.Log("Good!");
                        s = goodScore;
                    }

                    score += s;
                    sources[0].Play();
                    boolIsNotesBStay = false;
                    Debug.Log("NotB!");
                    /*
                    Debug.Log(this.transform.position);
                    Debug.Log(col.transform.position);
                    */
                    NotesScript script = col.gameObject.GetComponent<NotesScript>();
                    script.boolActive = false;
                    Destroy(col.gameObject);

                    intBinA++;
                    UIUpdate();
                }
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

        if (other.gameObject.CompareTag("NotesEX"))
        {
            boolIsNotesEXStay = true;
            col = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        NotesScript script = other.GetComponent<NotesScript>();
        if (other.gameObject.CompareTag("NotesA"))
        {
            boolIsNotesAStay = false;
            if (script.boolActive)
            {
                Debug.Log("miss");
                hitPoint -= 10;
                miss++;
                slider.GetComponent<Slider>().value = hitPoint / 100f;
                Debug.Log(hitPoint + "/100");
            }
            else
            {
                Debug.Log("Notmiss");
            }
        }

        if (other.gameObject.CompareTag("NotesB"))
        {
            boolIsNotesBStay = false;
            if (script.boolActive)
            {
                Debug.Log("miss");
                hitPoint -= 10;
                miss++;
                Debug.Log(hitPoint/100);
                slider.GetComponent<Slider>().value = hitPoint / 100f;
            }
            else
            {
                Debug.Log("Notmiss");
            }
        }

        UIUpdate();
    }

}