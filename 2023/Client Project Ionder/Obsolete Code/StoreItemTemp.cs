using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemTemp : MonoBehaviour
{
    public RawImage image;
    public Sprite sprite;
    public Button button;
    public TMP_Text nameText;
    public TMP_Text costText;
    public RoomItem roomItem;

    public void SetShopButton(ShopItem item, Shop shop)
    {
        button.onClick.AddListener(() => shop.OnButtonClic(item));
    }
    public void SetInventoryButton(ShopItem item, Inventory inventory)
    {
        button.onClick.AddListener(() => inventory.OnButtonClic(item));
    }
}
