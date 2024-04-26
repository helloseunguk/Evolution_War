using Cysharp.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;
using UnityEditor.VersionControl;
using UnityEditor;
using Cysharp.Text;
using Sirenix.Utilities;
using System.Linq;

public partial class ObjectPool
{
    [Serializable]
    public class Task
    {
        public string key;
        public List<object> callbacks;
    }

    [Serializable]
    public class ObjectItem
    {
        public string key;
        public string bundleName;
        public Object obj;
        public int lifeCount;
    }

    private ConcurrentDictionary<string, ObjectItem> items = new ConcurrentDictionary<string, ObjectItem>();
    private ConcurrentDictionary<string, Task> tasks = new ConcurrentDictionary<string, Task>();

    public T GetLoadedObject<T>(string _path, string _bundle) where T : UnityEngine.Object
    {
        //string key = "";
        //key = ZString.Concat(_path, NAssetPath.Slash, _bundle);
        //if (items.TryGetValue(key, out ObjectItem ret))
        //{
        //    return ret.obj as T;
        //}


        //var bundle = $"{_path}/root";
        //var assetBundle = EAsset.GetLoadedAssetBundle(bundle);
        //if (assetBundle == null)
        //    return null;
        //var asset = assetBundle.LoadAsset<T>(_bundle);

        //items.TryAdd(key, new ObjectItem { key = key, bundleName = bundle, obj = asset, lifeCount = 1, });

        //return asset;
        return null;
    }

    public async UniTask Load<T>(string bundleName, string assetName, Action<T> callback, int lifeCount) where T : UnityEngine.Object
    {
//        string key = ZString.Concat(bundleName, NMAssetPath.Slash, assetName);
//        if (items.TryGetValue(key, out ObjectItem ret))
//        {
//            ret.lifeCount = lifeCount;
//            callback(ret.obj as T);
//            return;
//        }


//        if (tasks.TryGetValue(key, out Task task))
//        {
//            task.callbacks.Add(callback);
//            return;
//        }

//        Task newTask = new Task()
//        {
//            key = key,
//            callbacks = new List<object>() { callback },
//        };
//        tasks.TryAdd(key, newTask);
//        var bundle = await NMAsset.LoadAssetBundle(bundleName);
//        if (bundle != null)
//        {
//            T newObj = bundle.LoadAsset<T>(assetName);
//#if TEST_DOWNLOAD && UNITY_EDITOR
//            if (newObj is GameObject)
//            {
//                NMAsset.SetNewShader(newObj as GameObject);
//            }
//#endif

//            if (lifeCount > 0)
//            {
//                items.TryAdd(key,
//                    new ObjectItem { key = key, bundleName = bundleName, obj = newObj, lifeCount = lifeCount, });
//            }

//            for (var index = 0; index < newTask.callbacks.Count; index++)
//            {
//                var newTaskCallback = newTask.callbacks[index] as Action<T>;
//                newTaskCallback?.Invoke(newObj);
//            }
//        }
//        else
//        {
//            for (var index = 0; index < newTask.callbacks.Count; index++)
//            {
//                var newTaskCallback = newTask.callbacks[index] as Action<T>;
//                newTaskCallback?.Invoke(null);
//            }
//        }

//        tasks.TryRemove(key, out var delete);
    }

#if UNITY_EDITOR
    public T LoadOnEditor<T>(string dirName, string assetName, int lifeCount) where T : UnityEngine.Object
    {
        //string key = ZString.Concat(dirName, NMAssetPath.Slash, assetName);
        //if (items.TryGetValue(key, out ObjectItem ret))
        //{
        //    ret.lifeCount = lifeCount;
        //    return ret.obj as T;
        //}

        //var assetPath = ZString.Concat(NMAssetPath.assetPath, key);
        //var newObj = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        //if (newObj != null)
        //{
        //    var path2 = AssetDatabase.GetAssetPath(newObj);
        //    if (Path.GetFileName(assetPath).CompareTo(Path.GetFileName(path2)) != 0)
        //    {
        //        Debug.LogError($"파일명의 대소문자를 확인해주세요! {assetPath}");
        //    }

        //    if (lifeCount > 0)
        //    {
        //        items.TryAdd(key, new ObjectItem { key = key, obj = newObj, lifeCount = lifeCount, });
        //    }
        //}

        //return newObj;
        return null;
    }

    public T LoadDefaultAssetOnEditor<T>(string dirName, string assetName, int lifeCount) where T : UnityEngine.Object
    {
        //string key = ZString.Concat(dirName, NMAssetPath.Slash, assetName);
        //if (items.TryGetValue(key, out ObjectItem ret))
        //{
        //    ret.lifeCount = lifeCount;
        //    return ret.obj as T;
        //}


        //var newObj = AssetDatabase.LoadAssetAtPath<T>(ZString.Concat(NMAssetPath.assetPath, dirName, NMAssetPath.Slash, assetName));
        //if (newObj != null)
        //{
        //    if (lifeCount > 0)
        //    {
        //        items.TryAdd(key, new ObjectItem { key = key, obj = newObj, lifeCount = lifeCount, });
        //    }
        //}

        //  return newObj;
        return null;
    }

#endif
    public void ReleaseReference()
    {
        items.Values.ForEach(x => x.lifeCount -= 1);

        var keys = items.Values.Where(x => x.lifeCount <= 0).Select(y => y.key).ToList();
        keys.ForEach(x => items.TryRemove(x, out var delete));
    }

    public void RemoveReferences(string bundleName)
    {
        var keys = items.Values.Where(x => x.bundleName == bundleName).Select(y => y.key).ToList();
        keys.ForEach(x => items.TryRemove(x, out var delete));
    }

    public void Clear()
    {
        items.Clear();
        tasks.Clear();
    }
}
