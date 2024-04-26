using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EAsset 
{
    private static ObjectPool objectPool = new ObjectPool();
    public static async UniTask<T> LoadAsset<T>(string bundleName, string assetName, int lifeCount, CancellationToken cancellationToken = default(CancellationToken)) where T : Object
    {
#if TEST_DOWNLOAD || !UNITY_EDITOR
        T retObj = null;
        bool isRet = false;
        try
        {
            objectPool.Load<T>(bundleName, assetName, (ret) =>
            {
                isRet = true;
                retObj = ret;
            }, lifeCount).TimeoutWithoutException(TimeSpan.FromSeconds(5));
            await UniTask.WaitWhile(() => !isRet, PlayerLoopTiming.Update, cancellationToken);
            return retObj;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
#elif UNITY_EDITOR
        await UniTask.DelayFrame(SettingScriptableObject.Instance.editorResourceLoadDelayFrame, PlayerLoopTiming.Update, cancellationToken);
        return objectPool.LoadOnEditor<T>(bundleName, assetName, lifeCount);
#endif
    }
        public static T LoadDefaultAsset<T>(string path, string bundle) where T : Object
    {
#if UNITY_EDITOR
        //if (EditorPrefs.GetBool("ASSET_BUILD"))
        //{
        //    return objectPool.LoadDefaultAssetOnEditor<T>(path, bundle, 1);
        //}
#if TEST_DOWNLOAD
        return objectPool.GetLoadedObject<T>(path, bundle);
#else
        return objectPool.LoadDefaultAssetOnEditor<T>(path, bundle, 1);
#endif
#else
    return objectPool.GetLoadedObject<T>(path, bundle);
#endif
    }
}
