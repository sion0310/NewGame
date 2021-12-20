using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    [SerializeField] GameObject MousePoint_ = null;
    void Start()
    {
        MousePoint_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            if (Input.GetMouseButtonDown(1))
            {
                MousePoint_.SetActive(false);
                MousePoint_.transform.position = hit.point;
                MousePoint_.SetActive(true);
            }
        }

    }
}
