using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Off : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("OffObject");
    }

    IEnumerator OffObject()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
