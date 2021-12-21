using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour { 
    [SerializeField]
    private GameObject mainUI;
    [SerializeField]
    private GameObject mainSystem;
    [SerializeField]
    private GameObject status;
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject quest;
    [SerializeField]
    private GameObject skill;

    [SerializeField]
    private GameObject miniMap;
    
    

    private void Awake()
    {
    }

    private void Update() {
        
        UIKeySetting();

    }

    private void UIKeySetting()
    {
        

        //mainUI
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            UiOnOffFunc(mainUI.GetComponent<Canvas>());
        }

        //mainSystem
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UiOnOffFunc(mainSystem.GetComponent<Canvas>());
        }

        //status
        if (Input.GetKeyDown(KeyCode.C))
        {
            UiOnOffFunc(status.GetComponent<Canvas>());
        }

        //inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            UiOnOffFunc(inventory.GetComponent<Canvas>());
        }

        //quest
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UiOnOffFunc(quest.GetComponent<Canvas>());
        }

        //skill
        if (Input.GetKeyDown(KeyCode.K))
        {
            UiOnOffFunc(skill.GetComponent<Canvas>());
        }


        //minimap
        if (Input.GetKeyDown(KeyCode.M))
        {
            UiOnOffFunc(miniMap.GetComponent<Canvas>());
        }
    }

    public void UiOnOffFunc(Canvas cvs) {
        if (cvs.enabled)
        {
            cvs.enabled = false;
        }
        else if (!cvs.enabled)
        {
            cvs.enabled = true;
        }

    }


}
