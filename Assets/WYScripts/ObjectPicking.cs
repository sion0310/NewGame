using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicking : MonoBehaviour {
    

    [SerializeField]private GameObject targetObj;
    [SerializeField]private GameObject dropBoxPanel;

    private void Awake()
    {
        dropBoxPanel = GameObject.Find("DropBoxPanel");
        //이거 좀 이상할거 같은데 수정\좀 해야될듯
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();

            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "Box")
                {
                    targetObj = hitInfo.transform.gameObject;
                    dropBoxPanel = targetObj.GetComponent<ItemBox>().dropItemPanel;
                    dropBoxPanel.transform.position = Input.mousePosition;

                    dropBoxPanel.SetActive(true);
                }

                else {
                    if (dropBoxPanel)
                    {
                        dropBoxPanel.SetActive(false);
                        dropBoxPanel = null;
                        targetObj = null;
                    }
                }


            }

        }
    }
}
