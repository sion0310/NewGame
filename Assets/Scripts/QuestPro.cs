using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPro : MonoBehaviour
{
    public delegate void QuestAchieve(bool isAchieved);
    public QuestAchieve questAchieve = null;

    public bool isAchieved;

    public Inventory inven;
    public Slot slot;
    public Item item;

    public int enemyCount = 0;

    public string condition;

    private void Start()
    {
        slot.useItem_pro = UseItemPro;
    }

    private void Update()
    {



        switch (condition)
        {
            case "item":
                ItemCheck();
                break;
            case "enemy":
                EnemyCount();
                break;
            case "boss":

                break;
            default:
                break;
        }


        if (isAchieved)
        {
            questAchieve?.Invoke(isAchieved);
        }
    }
    public void AchievedQuest()
    {
        isAchieved = true;
    }

    public void EnemyCount()
    {
        if (enemyCount >= 3)
        {
            isAchieved = true;
            enemyCount = 0;
        }
    }

    public void ItemCheck()
    {
        if (inven.inventory.ContainsKey(100))
        {
            isAchieved = true;
        }
    }

    public void aa()
    {
        inven.addItem(item, 1);
    }

    void UseItemPro()
    {
        isAchieved = true;
        Debug.Log("aa");
    }

}
