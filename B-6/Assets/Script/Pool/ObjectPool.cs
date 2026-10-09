using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T : APooledObject
{
    private Stack<APooledObject> _pool;
    private IPooledObjectFactory _factory;

    /// @brief オブジェクトプールを生成してファクトリーを保持します
    public ObjectPool(IPooledObjectFactory factory)
    {
        _pool = new Stack<APooledObject>();
        _factory = factory;
    }

    /// @brief プールのセットアップを行う関数
    public void PoolSetUp(uint initPoolSize)
    {
        if (_factory == null) return;

        Stack<APooledObject> objectPool = _pool;

        for (int i = 0; i < initPoolSize; i++)
        {
            T instance = ObjectInstantiate();
            if (instance == null) return;

            instance.gameObject.SetActive(false);
            objectPool.Push(instance);
        }
    }

    public APooledObject GetFromPool()
    {
        if (_factory == null) return null;

        Stack<APooledObject> objectPool = _pool;

        if (objectPool.Count < 1)
        {
            T newInstance = ObjectInstantiate();
            if (newInstance == null) return null;

            return newInstance;
        }

        T nextInstance = objectPool.Pop() as T;
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    private T ObjectInstantiate()
    {
        T instance = _factory.Create() as T;
        if (instance == null) return null;

        instance.SetPoolAction(ReturnToPool);
        return instance;
    }

    public void ReturnToPool(APooledObject pooledObject)
    {
        Stack<APooledObject> objectPool = _pool;
        objectPool.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}

