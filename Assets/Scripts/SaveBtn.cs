using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBtn : MonoBehaviour
{
    public DialogueManager dialMg = null;
    public GameObject player = null;
    

    public void SaveDataBtn()
    {
        DataCtrl.Instance._gameData._questIndex = dialMg.questIndex;

        DataCtrl.Instance._gameData.player_x = player.transform.position.x;
        DataCtrl.Instance._gameData.player_y = player.transform.position.y;
        DataCtrl.Instance._gameData.player_z = player.transform.position.z;

        DataCtrl.Instance._gameData.hp = player.GetComponent<Player>().hp;
        DataCtrl.Instance._gameData.curhp = player.GetComponent<Player>().curhp;
        DataCtrl.Instance._gameData.mp = player.GetComponent<Player>().mp;
        DataCtrl.Instance._gameData.curmp = player.GetComponent<Player>().curmp;
        DataCtrl.Instance._gameData.ex = player.GetComponent<Player>().ex;
        DataCtrl.Instance._gameData.curex = player.GetComponent<Player>().curex;
        DataCtrl.Instance._gameData.level = player.GetComponent<Player>().level;

        DataCtrl.Instance.SaveGameData();
    }
    public void ResetBtn()
    {
        DataCtrl.Instance._gameData._questIndex = 0;

        DataCtrl.Instance._gameData.player_x = -13.31f;
        DataCtrl.Instance._gameData.player_y = 0.044267f;
        DataCtrl.Instance._gameData.player_z = -17.83143f;

        DataCtrl.Instance._gameData.hp = 100;
        DataCtrl.Instance._gameData.curhp = 100;
        DataCtrl.Instance._gameData.mp = 100;
        DataCtrl.Instance._gameData.curmp = 100;
        DataCtrl.Instance._gameData.ex = 100;
        DataCtrl.Instance._gameData.curex = 0;
        DataCtrl.Instance._gameData.level = 1;

        DataCtrl.Instance.SaveGameData();
    }
}
