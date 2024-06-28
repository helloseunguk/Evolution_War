using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class ResourceManager
{
    public async UniTask<GameObject> Instantiate(string address, Transform parent = null)
    {
        // ��巹���� ������ �񵿱� �ε�
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(address);

        // �ε� �Ϸ� ���
        await handle.Task;

        // �ε忡 ������ ��� ���� ó��
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load asset at address: {address}");
            return null;
        }

        // �ν��Ͻ�ȭ
        GameObject instantiatedObject = UnityEngine.Object.Instantiate(handle.Result, parent);

        // �ν��Ͻ�ȭ�� ������Ʈ ��ȯ
        return instantiatedObject;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        UnityEngine.Object.Destroy(go);
    }
}
