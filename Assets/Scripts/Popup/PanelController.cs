using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    // 팝업창 RectTransform
    [SerializeField] private RectTransform panelRectTransform;
    
    private CanvasGroup _backgroundCanvasGroup;
    
    // Panel이 Hide 될 때 해야 할 동작
    public delegate void PanelControllerHideDelegate();

    private void Awake()
    {
        _backgroundCanvasGroup  = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Panel 표시
    /// </summary>
    public virtual void Show()
    {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        _backgroundCanvasGroup.DOFade(1, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    /// <summary>
    /// Panel 숨기기
    /// </summary>
    public void Hide(PanelControllerHideDelegate hideDelegate = null)
    {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        _backgroundCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                hideDelegate?.Invoke();
                Destroy(gameObject);
            });
    }

    protected void Shake()
    {
        panelRectTransform.DOShakeAnchorPos(0.3f);
    }
}
