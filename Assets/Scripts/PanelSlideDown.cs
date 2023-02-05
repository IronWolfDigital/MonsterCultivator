using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelSlideDown : MonoBehaviour
{
    public RectTransform panel;
    public float slideDuration = 1f;
    public Button imageButton;
    public CanvasGroup canvasGroup;

    private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    private bool isSlidedDown = false;

    private void Start()
    {
        startPos = panel.anchoredPosition;

        imageButton.onClick.AddListener(ToggleSlide);
    }

    private void ToggleSlide()
    {
        if (isSlidedDown)
        {
            panel.DOAnchorPos(startPos, slideDuration).SetEase(Ease.OutQuint);
            canvasGroup.DOFade(0, slideDuration).SetEase(Ease.OutQuint);
            isSlidedDown = false;
        }
        else
        {
            canvasGroup.DOFade(1, slideDuration).SetEase(Ease.OutQuint);
            panel.DOAnchorPos(endPos, slideDuration).SetEase(Ease.OutQuint);
            isSlidedDown = true;
        }
    }
}