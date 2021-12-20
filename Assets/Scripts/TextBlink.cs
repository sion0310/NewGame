using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextBlink : MonoBehaviour
{
    public GameObject blink_img;
    void Start () 
    { 
        StartCoroutine (BlinkText()); 
    }
    
    public IEnumerator BlinkText()
    { 
        while (true) 
        {
            blink_img.SetActive(true);
            yield return new WaitForSeconds (1f); 
            blink_img.SetActive(false);
            yield return new WaitForSeconds (0.8f); 
        } 
    } 
}

