using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public GameObject itemButton;

    public int itemId;

    public enum ITEMTYPE {
        NONE = -1,
        UNUSABLE = 0,
        Coins,
        Material,
        USABLE = 10,
        Equipment,
        HealHp,
        HealMp,
        Quest
    }

    public ITEMTYPE item_Type;
    public int itemValue;



}
