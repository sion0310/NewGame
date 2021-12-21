using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    private Item item;
    private List<Slot> slots;
    private void Awake() {

        //item = this.gameObject.GetComponent<Item>();
        slots = this.gameObject.GetComponent<ItemBox>().slots;
    }

    private void OnTriggerEnter(Collider other) { 
        if (other.transform.tag == "Player") {
           // other.GetComponent<Inventory>().addItem(item.itemButton, item.amount);

            for(int i = 0; i < slots.Count; i++) {
                other.GetComponent<Inventory>().addItem(slots[i].item,slots[i].amount);
                
            }

            if (this.GetComponent<ItemBox>().dropItemPanel.activeSelf) {
                this.GetComponent<ItemBox>().dropItemPanel.SetActive(false);
            }
            Destroy(this.gameObject);
        }
    }
}
