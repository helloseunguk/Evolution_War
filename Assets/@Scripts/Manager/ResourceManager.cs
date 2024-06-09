using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

[Serializable]
public class ScenePreLoadAssetBudle 
{
    public string sceneName;
    public List<string> bundleList;
}
[Serializable]
public class ScenePreLoadAssetBundleList 
{
    public ScenePreLoadAssetBudle[] loadList;
}
public class ResourceManager
{
    public enum ESIZE 
    {
        S,
        M,
        L,
    }
    public static readonly int LIFE_COUNT_TEXT_ASSEST = 1;
    public static readonly int LIFE_COUNT_INGAME_OBJECT = 2;
    public static readonly int LIFE_COUNT_SOUND_CLIP = 2;
    public static readonly int LIFE_COUNT_SCENARIO_WEBTOON = 1;
    public static readonly int LIFE_COUNT_SCENARIO_CHARACTER = 1;

    public const int InitialLifeCount = 1;

    private static Subject<string> loadTextAsset = new Subject<string>();

    public static IObservable<string> OnLoadTextAsset
    {
        get { return loadTextAsset.AsObservable(); }
    }
    //public async UniTask<T> LoadAsync<T>(string bundleName, string assetName, int lifeCount = InitialLifeCount,CancellationToken cancellationToken = default(CancellationToken)) where T: UnityEngine.Object
    //{
    //    var asset = await EAsset.LoadAsset<T>(bundleName, assetName, lifeCount, cancellationToken);
    //    return asset;
    //}


}
