using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesScript : MonoBehaviour
{
    [SerializeField] GameObject Effect;

    float time = 1;
    private GameObject zone;
    private Main main;
    

    public bool boolActive;
    private Vector3 notesVelocity;


    void OnEnable()
    {
        zone = GameObject.Find("Zone");
        main = GameObject.Find("Main").GetComponent<Main>();
        if (main.songUuid == "0f1b605e-53e1-45ca-92a8-8dc97a63071e")
        {

        }
        if (main.songUuid == "5676c60e-3274-4111-86c9-47b7af6ba8f7")
        {
            time = (main.BPM / 60 * main.LPB);
        }
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


    void OnDestroy()
    {
        Instantiate(Effect, this.transform.position, Quaternion.identity);
    }

}

