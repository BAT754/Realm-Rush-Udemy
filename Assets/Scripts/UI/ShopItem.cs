using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Tower tower;
    public Tower Tower { get { return tower; }}

    [SerializeField] TowerStats towerInfo;
    public TowerStats TowerInfo { get { return towerInfo; }}

    [Header("Children objects")]
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] Button button;

    int itemPrice = 100;
    public int ItemPrice { get { return itemPrice; }}
    string itemName = "Name";

    Shop shop = null;


    // Start is called before the first frame update
    void Start()
    {
        itemPrice = towerInfo.price;
        itemName = towerInfo.towerName;

        updateTitles(itemName, itemPrice);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void updateTitles(string name, int price)
    {
        title.text = name;
        cost.text = "$" + price.ToString();
    }

    public void CheckItemPrice(int balance)
    {
        if (itemPrice > balance && button.interactable)             // We can't afford the item, but the button is still active
            CantAffordItem();
        else if (itemPrice <= balance && !button.interactable)      // We can afford the item, but the button isn't active
            CanAffordItem();
        else                                                        // No need to change the button status, as it's already what it should be
            return;
    }

    public bool PurchaseItem(Vector3 position)
    {   
        // Check if we can buy the item - 
        //      We should know that we can due to the logic already implemented with the button,
        //      but this servers as a good double check
        if (shop.CanBuyItem(itemPrice))
        {
            Instantiate(tower, position, Quaternion.identity);    // Create the object
            return true;
        }

        return false;
    }

    public void CantAffordItem()
    {
        cost.color = Color.red;
        title.color = Color.gray;
        button.interactable = false;
    }

    public void CanAffordItem()
    {
        cost.color = Color.white;
        title.color = Color.white;
        button.interactable = true;
    }

    public void ItemSelected()
    {
        shop.ShopItemSelected(this);
    }

    public void ItemDeselected()
    {
        shop.ShopItemDeselected(this);
    }

    public void SetShop(Shop value)
    {
        shop = value;
    }
    
}
