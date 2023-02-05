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

    private static PanelSlideDown openPanel;

    private void Start()
    {
        startPos = panel.anchoredPosition;

        imageButton.onClick.AddListener(ToggleSlide);
    }

    private void ToggleSlide()
    {
        if (openPanel != null)
        {
            Debug.Log(openPanel.gameObject.name);
        }
        
        if (openPanel != null && openPanel.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Debug.Log("cALLED");
            openPanel.ClosePanel();
        }

        if (isSlidedDown)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
    }

    private void OpenPanel()
    {
        canvasGroup.DOFade(1, slideDuration).SetEase(Ease.OutQuint);
        panel.DOAnchorPos(endPos, slideDuration).SetEase(Ease.OutQuint);
        openPanel = this;
        isSlidedDown = true;
    }

    private void ClosePanel()
    {
        panel.DOAnchorPos(startPos, slideDuration).SetEase(Ease.OutQuint);
        canvasGroup.DOFade(0, slideDuration).SetEase(Ease.OutQuint);
        openPanel = null;
        isSlidedDown = false;
    }
    private void ClosePanelWithoutEase()
    {
        panel.DOAnchorPos(startPos, slideDuration).SetEase(Ease.OutQuint);
        canvasGroup.alpha = 0;
        openPanel = null;
        isSlidedDown = false;
    }
}