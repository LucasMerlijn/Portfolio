using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HoverFunctionality : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{
    EmailGeneratorTesting EGT;

    public Texture2D MouseSpriteNormal, MouseSpriteHover;
    private void Start()
    {
        EGT = FindObjectOfType<EmailGeneratorTesting>();
    }

    public void OnPointerMove(PointerEventData pointerEventData)
    {
        TMP_Text text = GetComponent<TextMeshProUGUI>();
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

        //pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<SpriteRenderer>().sprite = highlightSprite;

        if (linkIndex > -1)
        {
            EGT.SetHyperLink(true);
            // Hovering over the link
            Cursor.SetCursor(MouseSpriteHover, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            EGT.SetHyperLink(false);
            Cursor.SetCursor(MouseSpriteNormal, Vector2.zero, CursorMode.Auto);
            // No longer hovering over the link
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //eventData.pointerCurrentRaycast.gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
    }
}