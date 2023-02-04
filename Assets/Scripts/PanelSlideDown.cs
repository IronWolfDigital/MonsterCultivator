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
        // Get the starting and end positions of the panel
        startPos = panel.anchoredPosition;

        // Add an OnClick listener to the button
        imageButton.onClick.AddListener(ToggleSlide);
    }

    private void ToggleSlide()
    {
        if (isSlidedDown)
        {
            // Slide the panel back up
            panel.DOAnchorPos(startPos, slideDuration).SetEase(Ease.OutQuint);
            // Hide the panel by fading out the canvas group
            canvasGroup.DOFade(0, slideDuration).SetEase(Ease.OutQuint);
            isSlidedDown = false;
        }
        else
        {
            // Show the panel by fading in the canvas group
            canvasGroup.DOFade(1, slideDuration).SetEase(Ease.OutQuint);
            // Slide the panel down
            panel.DOAnchorPos(endPos, slideDuration).SetEase(Ease.OutQuint);
            isSlidedDown = true;
        }
    }
}