using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    private CustomizationManager CM;

    [SerializeField]
    private List<Transform> childrenRotations = new List<Transform>();
    private int rotationValue;

    [SerializeField]
    private bool selected;

    [SerializeField]
    private int offset;
    public bool Selected
    {
        get { return selected; }
        set { selected = value; }
    }
    [SerializeField]
    private RoomItemType itemType;
    public RoomItemType ItemType
    {
        set { itemType = value; }
    }
    int itemTypeSortLayer;



    private void Start()
    {
        CM = FindObjectOfType<CustomizationManager>();

        foreach (Transform child in transform)
        {
            childrenRotations.Add(child);
        }

        for (int i = 0; i < childrenRotations.Count; i++)
        {
            if (i != rotationValue)
            {
                //Debug.Log(childrenRotations[i]);
                childrenRotations[i].gameObject.SetActive(false);
            }
        }

        childrenRotations[0].GetComponent<SpriteRenderer>().sprite = CM.Sprite1;
        childrenRotations[1].GetComponent<SpriteRenderer>().sprite = CM.Sprite2;
    }

    public void Setup()
    {
        CM = FindObjectOfType<CustomizationManager>();

        foreach (Transform child in transform)
        {
            childrenRotations.Add(child);
        }

        for (int i = 0; i < childrenRotations.Count; i++)
        {
            if (i != rotationValue)
            {
                Debug.Log(childrenRotations[i]);
                childrenRotations[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (childrenRotations[rotationValue].GetComponent<SpriteRenderer>().sortingOrder != (int)transform.position.y)
        {
            switch (itemType)
            {
                case RoomItemType.FloorItem:
                    itemTypeSortLayer = -1000;
                    break;

                case RoomItemType.StandingItem:
                    itemTypeSortLayer = 0;
                    break;

                case RoomItemType.CeilingItem:
                    itemTypeSortLayer = 1000;
                    break;
            }

            int sortLayer = -(int)(transform.position.y * 100) + itemTypeSortLayer + offset;
            childrenRotations[rotationValue].GetComponent<SpriteRenderer>().sortingOrder = sortLayer;
        }
    }

    public void SetSprites(Sprite sprite1, Sprite sprite2)
    {
        childrenRotations[0].GetComponent<SpriteRenderer>().sprite = sprite1;
        childrenRotations[1].GetComponent<SpriteRenderer>().sprite = sprite2;
    }

    public void Select(BaseEventData data)
    {
        if (GameManager.Instance.gameState != GameState.CustomizeGameplayState)
            return;

        CM.DeselectRoomItems();
        selected = true;
        for (int i = 0; i < childrenRotations.Count; i++)
        {
            childrenRotations[i].GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public void DragObject(BaseEventData data)
    {
        if (selected)
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0;

            transform.position = vec;
        }
    }

    public void RotateObject()
    {
        rotationValue++;
        if (rotationValue > childrenRotations.Count - 1)
            rotationValue = 0;

        for (int i = 0; i < childrenRotations.Count; i++)
        {
            childrenRotations[i].gameObject.SetActive(false);

            if (rotationValue == i)
                childrenRotations[i].gameObject.SetActive(true);
        }
    }
}

public enum RoomItemType
{
    FloorItem,
    StandingItem,
    CeilingItem
}