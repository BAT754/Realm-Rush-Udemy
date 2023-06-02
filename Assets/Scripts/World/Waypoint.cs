using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] MeshRenderer placementMesh;
    [SerializeField] MeshRenderer rangeMesh;
    [SerializeField] Material validPlacement;
    [SerializeField] Material invalidPlacement;
    

    Shop shop;
    

    private void Start() {
        shop = FindObjectOfType<Shop>();

        if (shop == null)
            Debug.Log("Didn't find the shop");

        placementMesh.enabled = false;
    }
    
    private void Update() 
    {
        
    }

    private void OnMouseDown() {
        Debug.Log("I was clicked!");

        if (!shop.ItemIsSelected)   // No item selected, ignore click
            return;

        if (!isPlaceable)           // Can't place anthing here, ignore
            return;

        ShopItem toBuy = shop.SelectedItem;

        bool towerBought = toBuy.PurchaseItem(transform.position);      // Get back true if we bought the tower, false if we didn't
        isPlaceable = !towerBought;             // If we bought the tower (True), set isPlaceable to (~True)

        
    }

    private void OnMouseExit() {
        placementMesh.enabled = false;
        rangeMesh.enabled = false;
    }

    private void OnMouseEnter() {
        if (!shop.ItemIsSelected)   // We're not trying to buy anything, so don't show if we can place anything
            return;

        Material toUse;
        toUse = invalidPlacement;
        
        if (isPlaceable)
        {
            toUse = validPlacement;

            ShopItem item = shop.SelectedItem;
            float range = item.TowerInfo.range;
            rangeMesh.transform.localScale = new Vector3(range, 0.1f, range);
            rangeMesh.enabled = true;
        }

        placementMesh.material = toUse;
        placementMesh.enabled = true;
    }

    Material DecideOnMaterial()
    {
        if (isPlaceable)
            return validPlacement;
        else
            return invalidPlacement;
    }
}
