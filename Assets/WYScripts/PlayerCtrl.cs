using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    public GameObject itemPrefab;
    void Update()
    {
        createItem();
        playerMove();

        
    }
    private void createItem()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject box = Instantiate(itemPrefab);
            box.transform.position = this.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

        }

    }

    private void playerMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += Vector3.forward * Time.deltaTime * 5f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += Vector3.back * Time.deltaTime * 5f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += Vector3.left * Time.deltaTime * 5f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += Vector3.right * Time.deltaTime * 5f;
        }
    }



}
