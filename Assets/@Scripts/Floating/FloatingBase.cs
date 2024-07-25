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

    public virtual void Init(Transform _target, int damage = -1)
    {
        target = _target;
        transform.position = Vector3.zero;
        text = GetComponent<TMP_Text>();
        var color = text.color;
        color.a = 1f;
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
        transform.position = target.position + offset;

        // 초기 scale을 0.02로 설정
        transform.localScale = Vector3.one * 0.02f;

        // 애니메이션 설정
        Sequence sequence = DOTween.Sequence();

        // 나타날 때 0.04로 커졌다가 0.02로 작아지는 효과
        sequence.Append(transform.DOScale(Vector3.one * 0.04f, 0.2f))
                .Append(transform.DOScale(Vector3.one * 0.02f, 0.2f));

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
        transform.position = target.position + offset;
    }
}
