using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Inventory : MonoBehaviour {

    public GameObject slotPrefab;
    public GameObject inventoryPanel;

    public List<Slot> itemSlots;
    public Dictionary<int, Slot> inventory;
    // <ItemId , 

    private void Awake() { 
        
        itemSlots = new List<Slot>();
        inventory = new Dictionary<int, Slot>();

        for (int i = 0; i < 16; i++) {
            GameObject slot = Instantiate(slotPrefab,inventoryPanel.transform);
            itemSlots.Add(slot.GetComponent<Slot>());

        }
        
    }

    public void addItem(Item _item,int _amount) {
        addItemDict(_item,_amount);
    }

    public void removeItem(int _itemId) {
        itemSlots[inventory[_itemId].transform.GetSiblingIndex()].isEmpty = true;
        inventory.Remove(_itemId);
    }

    private void addItemDict(Item _item,int _amount) {
        if (isItemExistInInventory(_item)) {
            inventory[_item.itemId].amountFunc(_amount);
            return;
        }

        for (int i = 0; i < itemSlots.Count; i++) {
            if (itemSlots[i].isEmpty) {
                itemSlots[i].setItem(inventoryPanel,_item, _amount);
                inventory.Add(_item.itemId, itemSlots[i]);
                break;
            }
        }
    }

    private bool isItemExistInInventory(Item _item) {
        return inventory.ContainsKey(_item.itemId);
    }

    //인벤정리
    private void SortInven() {
        for(int i = 0; i < inventory.Count; i++) {
            
        }
    }

    private void SwapObj(GameObject _first, GameObject _second) {
        GameObject _tmp = _first;
        _first = _second;
        _second = _tmp;

    }
    
}
