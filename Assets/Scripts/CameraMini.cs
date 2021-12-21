using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMini : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y = 30f;
        this.transform.position = pos;
        
    }
}
