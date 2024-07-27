using DG.Tweening;
using UniRx;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    [Header("���̵� ����")]
    public float fadeDuration = 1f;

    [Header("���̵� ����")]
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
                rectTransform.anchoredPosition = new Vector2(startPos.x, startPos.y - 100); // �Ʒ����� ����
                break;
            case Define.FadeArrowType.DownArrow:
                rectTransform.anchoredPosition = new Vector2(startPos.x, startPos.y + 100); // ������ �Ʒ���
                break;
            case Define.FadeArrowType.LeftArrow:
                rectTransform.anchoredPosition = new Vector2(startPos.x + 100, startPos.y); // �����ʿ��� ��������
                break;
            case Define.FadeArrowType.RightArrow:
                rectTransform.anchoredPosition = new Vector2(startPos.x - 100, startPos.y); // ���ʿ��� ����������
                break;
        }

        // ��ġ �̵� �� ���̵� �� �ִϸ��̼�
        rectTransform.DOAnchorPos(startPos, fadeDuration);
        canvasGroup.DOFade(1, fadeDuration).From(0);
    }

    public void FadeOutAni(Define.FadeArrowType fadeOutType)
    {
        Vector3 endPos = rectTransform.anchoredPosition;
        switch (fadeOutType)
        {
            case Define.FadeArrowType.UpArrow:
                endPos = new Vector2(endPos.x, endPos.y + 100); // ���� �̵�
                break;
            case Define.FadeArrowType.DownArrow:
                endPos = new Vector2(endPos.x, endPos.y - 100); // �Ʒ��� �̵�
                break;
            case Define.FadeArrowType.LeftArrow:
                endPos = new Vector2(endPos.x - 100, endPos.y); // �������� �̵�
                break;
            case Define.FadeArrowType.RightArrow:
                endPos = new Vector2(endPos.x + 100, endPos.y); // ���������� �̵�
                break;
        }

        // ��ġ �̵� �� ���̵� �ƿ� �ִϸ��̼�
        canvasGroup.DOFade(0, fadeDuration).OnComplete(() => gameObject.SetActive(false));
        rectTransform.DOAnchorPos(endPos, fadeDuration);
    }
}
