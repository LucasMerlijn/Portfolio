using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightHover : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{
    private PickUp pickup;
    private RoomItemSpawnable ris;

    public void OnPointerMove(PointerEventData eventData)
    {
        //if (GameManager.Instance.gameState == GameState.PlacementState)
        //{
        //    if (pickup != null)
        //    {
        //        if (!pickup.GetComponentInParent<RoomItemSpawnable>().isHighlighted)
        //        {
        //            pickup.GetComponentInParent<RoomItemSpawnable>().Highlight(true);
        //        }
        //    }
        //    else if (ris != null)
        //    {
        //        if (!ris.isHighlighted)
        //        {
        //            ris.Highlight(true);
        //        }
        //    }
        //    else
        //    {
        //        if (eventData.pointerCurrentRaycast.gameObject != null)
        //        {
        //            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<PickUp>() != null)
        //            {
        //                if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<RoomItemSpawnable>() != null)
        //                {
        //                    RoomItemSpawnable obj = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<RoomItemSpawnable>();
        //                    if (!obj.isHighlighted)
        //                    {
        //                        pickup = eventData.pointerCurrentRaycast.gameObject.GetComponent<PickUp>();
        //                        obj.Highlight(true);
        //                    }
        //                }
        //            }
        //            else if (eventData.pointerCurrentRaycast.gameObject.GetComponent<RoomItemSpawnable>() != null)
        //            {
        //                RoomItemSpawnable obj = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<RoomItemSpawnable>();
        //                if (obj.isHighlighted == false)
        //                {
        //                    ris = eventData.pointerCurrentRaycast.gameObject.GetComponent<RoomItemSpawnable>();
        //                    obj.Highlight(true);
        //                }
        //            }
        //            else
        //            {
        //                Debug.Log("Hover issue");
        //            }
        //        }
        //    }
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (pickup != null)
        //{
        //    if (pickup.GetComponentInParent<RoomItemSpawnable>().isHighlighted)
        //    {
        //        pickup.GetComponentInParent<RoomItemSpawnable>().Highlight(false);
        //    }
        //}
        //else if (ris != null)
        //{
        //    if (ris.isHighlighted)
        //    {
        //        ris.Highlight(false);
        //    }
        //}
    }
}
