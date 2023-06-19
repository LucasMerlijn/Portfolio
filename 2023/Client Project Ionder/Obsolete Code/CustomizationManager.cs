using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationManager : MonoBehaviour
{
    [SerializeField] private Transform room;
    [SerializeField]
    private List<RoomItem> allRoomItems = new List<RoomItem>();
    private Sprite sprite1, sprite2;
    public Sprite Sprite1
    {
        get { return sprite1; }
    }
    public Sprite Sprite2
    {
        get { return sprite2; }
    }


    private void Start()
    {
        StartCustomization();
    }

    public void SpawnObject(RoomItem item, string name, Sprite spr, Sprite spr2)
    {
        //GameObject holder = item.GetComponent<GameObject>();
        RoomItem CurrentSpawnedObject = Instantiate(item, Vector2.zero, Quaternion.identity, room);
        CurrentSpawnedObject.gameObject.name = name;
        sprite1 = spr;
        sprite2 = spr2;

        allRoomItems.Add(CurrentSpawnedObject);
    }

    /// <summary>
    /// Change for the event call on the game state change towards customization.
    /// </summary>
    public void StartCustomization()
    {
        allRoomItems.Clear();
        allRoomItems = new List<RoomItem>();
        for (int i = 0; i < FindObjectsOfType<RoomItem>().Length; i++)
        {
            allRoomItems.Add(FindObjectsOfType<RoomItem>()[i]);
            allRoomItems[i].Selected = false;
        }
    }

    /// <summary>
    /// Delete Selected Room Item(s)
    /// </summary>
    public void Deletion()
    {
        for (int i = 0; i < allRoomItems.Count; i++)
        {
            if (allRoomItems[i].Selected)
            {
                Destroy(allRoomItems[i].gameObject);
                allRoomItems.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Deselect All Room Items
    /// </summary>
    public void DeselectRoomItems()
    {
        for (int i = 0; i < allRoomItems.Count; i++)
        {
            if (allRoomItems[i].Selected)
            {
                allRoomItems[i].Selected = false;
                allRoomItems[i].GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
        }
    }

    public void RotateSelectedRoomItem()
    {
        for (int i = 0; i < allRoomItems.Count; i++)
        {
            if (allRoomItems[i].Selected)
            {
                allRoomItems[i].RotateObject();
            }
        }
    }

}