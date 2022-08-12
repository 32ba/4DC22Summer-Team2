using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesEXScript : MonoBehaviour
{

    [SerializeField] GameObject Effect;
    private int intHP = 10;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(intHP);
        if (other.gameObject.CompareTag("NotesA"))
        {
            Destroy(other.gameObject);
            intHP--;
        }

        if (other.gameObject.CompareTag("NotesB"))
        {
            Destroy(other.gameObject);
            intHP--;
        }
        if(intHP<=0){
            Instantiate(Effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(intHP < 4)
        {
            transform.Find("2").gameObject.SetActive(false);
            transform.Find("3").gameObject.SetActive(true);
        }
        else if(intHP < 6)
        {
            transform.Find("1").gameObject.SetActive(false);
            transform.Find("2").gameObject.SetActive(true);
        }
    }
}
