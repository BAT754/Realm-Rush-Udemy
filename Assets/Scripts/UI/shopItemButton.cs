using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class shopItemButton : MonoBehaviour, IDeselectHandler, ISelectHandler
{
    [SerializeField] ShopItem shopItem;
    
    public void OnDeselect(BaseEventData data)
    {
        ClearSelectedItem();    // Pause for a second before clearing the item to allow for a purchase to go through
    }

    public void OnSelect(BaseEventData data) {
        shopItem.ItemSelected();
    }
    
    public void ClearSelectedItem()
    {
        shopItem.ItemDeselected();
    }
    
}
