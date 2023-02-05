using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeImageSpriteOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public Image image;
    public Sprite normalSprite;
    public Sprite hoverSprite;

    private void OnPointerEnter(PointerEventData eventData)
    {
        var spriteState = new SpriteState();
        spriteState.highlightedSprite = hoverSprite;
        button.spriteState = spriteState;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        var spriteState = new SpriteState();
        spriteState.highlightedSprite = hoverSprite;
        button.spriteState = spriteState;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        var spriteState = new SpriteState();
        spriteState.highlightedSprite = hoverSprite;
        button.spriteState = spriteState;
    }

    private void OnPointerExit(PointerEventData eventData)
    {
        var spriteState = new SpriteState();
        spriteState.highlightedSprite = normalSprite;
        button.spriteState = spriteState;
    }
}