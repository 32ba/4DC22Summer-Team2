using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject NotesA;
    [SerializeField] GameObject NotesB;
    void Start() 
    {
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
