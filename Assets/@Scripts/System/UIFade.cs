using DG.Tweening;
using UniRx;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    [Header("페이드 세팅")]
    public float fadeDuration = 1f;

    [Header("페이드 방향")]
    public Define.FadeArrowType fadeType = Define.FadeArrowType.None;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("UIFade requires a RectTransform component.");
        }
    }

    private void OnEnable()
    {
        FadeInAni(fadeType);
    }

    private void OnDisable()
    {
        FadeOutAni(fadeType);
    }

    public void FadeInAni(Define.FadeArrowType fadeInType)
    {
        Vector3 startPos = rectTransform.anchoredPosition;
        switch (fadeInType)
        {
            case Define.FadeArrowType.UpArrow:
                rectTransform.anchoredPosition = new Vector2(startPos.x, startPos.y - 100); // 아래에서 위로
                break;
            case Define.FadeArrowType.DownArrow:
                rectTransform.anchoredPosition = new Vector2(startPos.x, startPos.y + 100); // 위에서 아래로
                break;
            case Define.FadeArrowType.LeftArrow:
                rectTransform.anchoredPosition = new Vector2(startPos.x + 100, startPos.y); // 오른쪽에서 왼쪽으로
                break;
            case Define.FadeArrowType.RightArrow:
                rectTransform.anchoredPosition = new Vector2(startPos.x - 100, startPos.y); // 왼쪽에서 오른쪽으로
                break;
        }

        // 위치 이동 및 페이드 인 애니메이션
        rectTransform.DOAnchorPos(startPos, fadeDuration);
        canvasGroup.DOFade(1, fadeDuration).From(0);
    }

    public void FadeOutAni(Define.FadeArrowType fadeOutType)
    {
        Vector3 endPos = rectTransform.anchoredPosition;
        switch (fadeOutType)
        {
            case Define.FadeArrowType.UpArrow:
                endPos = new Vector2(endPos.x, endPos.y + 100); // 위로 이동
                break;
            case Define.FadeArrowType.DownArrow:
                endPos = new Vector2(endPos.x, endPos.y - 100); // 아래로 이동
                break;
            case Define.FadeArrowType.LeftArrow:
                endPos = new Vector2(endPos.x - 100, endPos.y); // 왼쪽으로 이동
                break;
            case Define.FadeArrowType.RightArrow:
                endPos = new Vector2(endPos.x + 100, endPos.y); // 오른쪽으로 이동
                break;
        }

        // 위치 이동 및 페이드 아웃 애니메이션
        canvasGroup.DOFade(0, fadeDuration).OnComplete(() => gameObject.SetActive(false));
        rectTransform.DOAnchorPos(endPos, fadeDuration);
    }
}
