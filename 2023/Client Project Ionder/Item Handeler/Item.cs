using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Item : MonoBehaviour
{
    public ItemHandeler IH;

    public RoomItemSpawnable RIS;
    public bool bought = false;

    public string price;
    public string buttonText;
    public Sprite img;

    [SerializeField] private Image _img;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private TMP_Text _price;
    public Button buyButton, placeButton;

    public Transform spawnParent;

    public ParticleSystem ps;

    /*
    Make boolean for items that are not grabbed to be ignored by the collision
    
    When boolean is true, check for collisions

    When leaving the boolean should be false (even while holding)
     
     */


    public void SetUI()
    {
        _img.sprite = img;
        _buttonText.text = buttonText;
        _price.text = price;

        if (!RIS.Bought)
            buyButton.onClick.AddListener(BuyButton);
    }

    public void BuyButton()
    {
        bought = true;
        RIS.Bought = true;

        price = "You own this.";
        buttonText = "Bought!";
        _buttonText.text = buttonText;
        _price.text = price;

        buyButton.onClick.RemoveListener(BuyButton);
        IH.boughtItem(this);
    }

    public void InventoryIni()
    {
        price = "";
        buttonText = "Place";
        _buttonText.text = buttonText;
        _price.text = price;
        _img.sprite = img;
    }

    public void PlaceButton(Transform parent, ParticleSystem particleSystem)
    {
        ps = particleSystem;
        spawnParent = parent;
        placeButton.onClick.AddListener(SpawnObject);
    }

    public void SpawnObject()
    {
        RoomItemSpawnable obj = Instantiate(RIS, spawnParent);
        obj.Setup(ps);
        GameManager.Instance.UI_Manager.PlaceObject();
    }
}