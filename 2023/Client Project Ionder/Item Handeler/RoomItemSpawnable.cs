using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItemSpawnable : MonoBehaviour
{
    [SerializeField] private int ID; // the set ID
    public int getID
    {
        get { return ID; }
    }

    [SerializeField] private bool bought; // if item is bought
    public bool Bought
    {
        get { return bought; }
        set { bought = value; }
    }

    public int price; // Buying price of the item

    public bool isHighlighted;
    public Sprite highLightedSprite;
    public Sprite UIimage;

    [SerializeField] private List<Sprite> ItemRotationImages = new List<Sprite>(); // Images for the rotation 
    [SerializeField] private List<Sprite> ItemColourImagesRot1 = new List<Sprite>(); // Images for different colours rotation 1 
    [SerializeField] private List<Sprite> ItemColourImagesRot2 = new List<Sprite>(); // Images for different colours rotation 2

    public ItemType itemType;
    public ItemType MyItemType
    {
        get { return itemType; }
    }

    private SpriteRenderer SR;
    private int layerOffset;
    public void Setup(ParticleSystem placementParticleSystem)
    {
        GetComponentInChildren<PickUp>().PlacementParticles = placementParticleSystem;
    }

    public void Highlight(bool active)
    {
        if (active)
        {
            isHighlighted = true;
            GetComponentInChildren<SpriteRenderer>().sprite = highLightedSprite;
        }
        else
        {
            isHighlighted = false;
            GetComponentInChildren<SpriteRenderer>().sprite = UIimage;
        }
    }

    public void CalculateLayer(Transform pos, PickUp pickup)
    {
        if (SR == null)
        {
            SR = GetComponentInChildren<SpriteRenderer>();
            pickup.MyItemType = MyItemType;
            CalculateLayerOffset(pickup);
        }

        float sortLayer = (pos.position.y * -1000) + layerOffset;
        SR.sortingOrder = (int)sortLayer;
    }

    private void CalculateLayerOffset(PickUp pickup)
    {
        switch (itemType)
        {
            case ItemType.Low_Ground_Item:
                layerOffset = -10000;
                break;

            case ItemType.Ground_Item:
                layerOffset = 10000;
                break;

            case ItemType.Wall_Item:
                layerOffset = -2500;
                break;
        }
    }
}