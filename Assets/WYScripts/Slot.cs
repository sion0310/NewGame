using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    //public delegate void UseItemPro();
    //public UseItemPro useItem_pro = null;

    public bool isEmpty;

    public Item item;
    public int amount;
    public Text amount_Text;

    private void Awake()
    {
        isEmpty = true;
        
    }

    public void setItem(GameObject _parent, Item _item, int _amount) { 
        item = Instantiate(_item.itemButton, this.transform, false).GetComponent<Item>();
        item.transform.SetSiblingIndex(0);
        item.GetComponent<RectTransform>().sizeDelta = _parent.GetComponent<GridLayoutGroup>().cellSize;

        amount_Text.gameObject.SetActive(true);
        amountFunc(_amount);
        isEmpty = false;

    }

    public void amountFunc(int _amount) {
        amount += _amount;
        
        amount_Text.text = amount.ToString();
    }

    public void useItem()
    {
        if (item == null) { Debug.Log("empty slot"); return; }
        if ((int)item.item_Type > 10)
        {

            StartCoroutine(item.item_Type.ToString(), item.itemValue);

            amountFunc(-1);
            if (amount <= 0)
            {
                Player.getInstance.gameObject.GetComponent<Inventory>().removeItem(this.item.itemId);
                Destroy(item.gameObject);

            }
        }
    }

    IEnumerator HealHp(int _value)
    {
        Player.getInstance.HpFunc(_value);

        yield return 0;
    }

    IEnumerator HealMp(int _value)
    {
        Player.getInstance.MpFunc(_value);
        yield return 0;

    }

    IEnumerator Quest(int _value)
    {
        //퀘스트 완료 조건 달성
        //useItem_pro?.Invoke();
        //나도 이러고싶진않았어... 
        GameObject quest = GameObject.Find("DialogueData");
        quest.GetComponent<QuestPro>().UseItemPro();
        yield return 0;

    }
}
