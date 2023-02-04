using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeButtonSpriteOnHover : MonoBehaviour
{
    public Button button;
    public Image image;
    public Sprite normalSprite;
    public Sprite hoverSprite;
    PointerEventData cachedEventData;

    private void OnPointerEnter(PointerEventData eventData)
    {
        var spriteState = new SpriteState();
        spriteState.highlightedSprite = hoverSprite;
        button.spriteState = spriteState;
        cachedEventData = eventData;
    }

    private void OnPointerExit(PointerEventData eventData)
    {
        var spriteState = new SpriteState();
        spriteState.highlightedSprite = normalSprite;
        button.spriteState = spriteState;
        cachedEventData = eventData;
    }

    private void Update()
    {
       // var btnState = cachedEventData 
    }
}