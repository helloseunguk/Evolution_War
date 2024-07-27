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

        // ũ��Ƽ�� ���ο� ���� �ؽ�Ʈ ���� ����
        if (isCritical)
        {
            color = Color.yellow; // ũ��Ƽ���� ��� �����
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

        // �ʱ� ��ġ ����
        transform.position = target.position + offset;

        // ũ��Ƽ�� ���ο� ���� �ʱ� scale ����
        float initialScale = isCritical ? 0.03f : 0.02f; // ũ��Ƽ���� ��� �� ũ��
        transform.localScale = Vector3.one * initialScale;

        // �ִϸ��̼� ����
        Sequence sequence = DOTween.Sequence();

        // ũ��Ƽ�� ���ο� ���� �ִϸ��̼� scale ����
        float enlargedScale = isCritical ? 0.06f : 0.04f; // ũ��Ƽ���� ��� �� ũ��
        float finalScale = initialScale;

        // ��Ÿ�� �� �� Ŀ���ٰ� ���� ũ��� ���ƿ��� ȿ��
        sequence.Append(transform.DOScale(Vector3.one * enlargedScale, 0.2f))
                .Append(transform.DOScale(Vector3.one * finalScale, 0.2f));

        // ���� �ö󰡸鼭 ���������� ȿ��
        sequence.Append(transform.DOMoveY(transform.position.y + 1, 1.0f).SetEase(Ease.OutQuad));
        sequence.Join(text.DOFade(0, 1.0f).SetEase(Ease.OutQuad));

        // �ִϸ��̼��� ���� �� ������Ʈ ��ȯ
        sequence.OnComplete(() => Managers.Floating.ReturnFloatingText(this));
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (target == null) return;

        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
    }
}
