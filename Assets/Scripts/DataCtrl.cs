using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataCtrl : MonoBehaviour {


    static GameObject _container;
    static GameObject Container
    {
        get 
        { 
            return _container; 
        }
    }

    static DataCtrl _instance;
    public static DataCtrl Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataCtrl)) as DataCtrl;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    private string GameDataFileName = "/Data/.json";    // 저장경로


    public Data _gameData;
    public Data gameData {
        get
        {
            if(_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Awake()
    {
        LoadGameData();
    }

    public void LoadGameData()
    {
        string filePath = Application.dataPath + GameDataFileName;
        if (File.Exists(filePath))
        {
            Debug.Log("Load Success");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<Data>(FromJsonData);
        }

        else
        {
            Debug.Log("create new datafile");
            _gameData = new Data();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        Debug.Log(GameDataFileName);
        string filePath = Application.dataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log(filePath);
        Debug.Log("Save Success");

    }

    //private void OnApplicationQuit()
    //{
    //    SaveGameData();
    //}
}
