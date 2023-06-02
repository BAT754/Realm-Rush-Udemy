using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject listings;
    List<ShopItem> shopItems = new List<ShopItem>();

    bool itemIsSelected = false;
    public bool ItemIsSelected { get { return itemIsSelected; }}

    ShopItem selectedItem = null;
    public ShopItem SelectedItem { get { return selectedItem; }}

    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        // Get all the current listings
        foreach (Transform child in listings.transform)
        {
            shopItems.Add(child.GetComponent<ShopItem>());
        }

        foreach (ShopItem item in shopItems)
        {
            item.SetShop(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckBankBalance(int balance)
    {
        foreach (ShopItem item in shopItems)
        {
            item.CheckItemPrice(balance);
        }
    }

    public void ShopItemSelected(ShopItem item)
    {
        itemIsSelected = true;
        selectedItem = item;

        Debug.Log("Item has been selected");
    }

    public void ShopItemDeselected(ShopItem item)
    {
        if (item != selectedItem || selectedItem == null)   // If we've already changed the selected item, then ignore the rest of this function
            return;

        itemIsSelected = false;
        selectedItem = null;

        Debug.Log("Item is no longer selected");
    }       

    public bool CanBuyItem(int price)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
            return false;   // We don't have a connection to the bank

        // Shouldn't need to check this, but we will anyways
        if (price > bank.CurrentBalance)
            return false;   // Item is too expenseive, can't buy it

        bank.Withdraw(price);
        return true;
    }
}
