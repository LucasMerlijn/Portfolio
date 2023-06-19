using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Background : MonoBehaviour
{
    private CustomizationManager CM;
    private void Start()
    {
        CM = FindObjectOfType<CustomizationManager>();
    }

    public void Deselection(BaseEventData data)
    {
        CM.DeselectRoomItems();
    }
}
