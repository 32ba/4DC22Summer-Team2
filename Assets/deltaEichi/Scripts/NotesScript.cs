using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesScript : MonoBehaviour
{
    float time = 1;
    private GameObject zone;
    private GameObject main;
    private Main mainScript;

    public bool boolActive;
    private Vector3 notesVelocity;


    void OnEnable()
    {
        zone = GameObject.Find("Zone");
        notesVelocity = (zone.transform.position - transform.position) / time;
        boolActive = true;
        /*
        main = GameObject.Find("Main");
        mainScript = main.GetComponent<Main>();
        Debug.Log(mainScript.BPM);
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += notesVelocity * Time.deltaTime;
    }


}

