using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class FloatingBase : MonoBehaviour
{
    Transform mainCam;
    Transform target;
    Transform worldSpaceCanvas;

    public Vector3 offset;
    TMP_Text text;

    public virtual void Init(Transform _target, int damage, bool isCritical)
    {
        target = _target;
        text = GetComponent<TMP_Text>();
        var color = text.color;
        color.a = 1f;

        // 크리티컬 여부에 따른 텍스트 색상 변경
        if (isCritical)
        {
            color = Color.yellow; // 크리티컬일 경우 노란색
        }
        text.color = color;

        var collider = target.GetComponent<Collider>();
        if (damage != -1)
        {
            text.text = damage.ToString();
        }

        mainCam = Camera.main.transform;
        offset.y = collider.bounds.max.y + 0.5f;
        worldSpaceCanvas = Managers.Floating.GetFloatingCanvas().transform;

        transform.SetParent(worldSpaceCanvas);

        // 초기 위치 설정
        transform.position = target.position + offset;

        // 크리티컬 여부에 따른 초기 scale 설정
        float initialScale = isCritical ? 0.03f : 0.02f; // 크리티컬일 경우 더 크게
        transform.localScale = Vector3.one * initialScale;

        // 애니메이션 설정
        Sequence sequence = DOTween.Sequence();

        // 크리티컬 여부에 따른 애니메이션 scale 설정
        float enlargedScale = isCritical ? 0.06f : 0.04f; // 크리티컬일 경우 더 크게
        float finalScale = initialScale;

        // 나타날 때 더 커졌다가 원래 크기로 돌아오는 효과
        sequence.Append(transform.DOScale(Vector3.one * enlargedScale, 0.2f))
                .Append(transform.DOScale(Vector3.one * finalScale, 0.2f));

        // 위로 올라가면서 투명해지는 효과
        sequence.Append(transform.DOMoveY(transform.position.y + 1, 1.0f).SetEase(Ease.OutQuad));
        sequence.Join(text.DOFade(0, 1.0f).SetEase(Ease.OutQuad));

        // 애니메이션이 끝난 후 오브젝트 반환
        sequence.OnComplete(() => Managers.Floating.ReturnFloatingText(this));
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (target == null) return;

        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
    }
}
