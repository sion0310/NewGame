using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public List<Item> items;
    public Item gold;
    public GameObject dropItemPanel;
    public GameObject slotPrefab;

    public List<Item> dropItems;
    public List<Slot> slots;

    private void Awake() {
        dropItems = new List<Item>();
        slots = new List<Slot>();

        selectDropItems();
        createSlot();

    }

    private void selectDropItems() {
        for (int i = 0; i < items.Count; i++) {
            int rand = Random.Range(0, 2);
            if (rand == 1) {
                dropItems.Add(items[i]);
            }
        }
        dropItems.Add(gold);
    }

    private void createSlot() {

        for (int i = 0; i < dropItems.Count - 1; i++) {
            slots.Add(Instantiate(slotPrefab, dropItemPanel.transform).GetComponent<Slot>());
            slots[i].setItem(dropItemPanel, dropItems[i], Random.Range(1, 4));
        }

        slots.Add(Instantiate(slotPrefab, dropItemPanel.transform).GetComponent<Slot>());
        slots[dropItems.Count - 1].setItem(dropItemPanel, dropItems[dropItems.Count - 1], Random.Range(30, 70));
        
    }

    
}
