using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Text text;
 
    public void TextAppare()
    {
        text.text = "Not playable in this version";
    }
}
