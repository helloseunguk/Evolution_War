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
        // 어드레서블 에셋을 비동기 로드
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(address);

        // 로드 완료 대기
        await handle.Task;

        // 로드에 실패한 경우 예외 처리
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load asset at address: {address}");
            return null;
        }

        // 인스턴스화
        GameObject instantiatedObject = UnityEngine.Object.Instantiate(handle.Result, parent);

        // 인스턴스화한 오브젝트 반환
        return instantiatedObject;
    }
    public async UniTask<T> LoadAssetAsync<T>(string key) where T : class
    {
        var handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            Debug.LogError($"에셋 로드 실패 : {key}");
            return null;
        }
    }
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        UnityEngine.Object.Destroy(go);
    }
}
