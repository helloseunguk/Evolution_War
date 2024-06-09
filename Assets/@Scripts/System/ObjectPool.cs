using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T prefab;
    private readonly Queue<T> objects = new Queue<T>();
    private readonly Transform parentTransform;

    public ObjectPool(T prefab, int initialSize, Transform parentTransform = null)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;

        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject()
    {
        var newObj = Object.Instantiate(prefab, parentTransform);
        newObj.gameObject.SetActive(false);
        objects.Enqueue(newObj);
        return newObj;
    }

    public T Get()
    {
        if (objects.Count == 0)
        {
            CreateObject();
        }

        var obj = objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }
}
