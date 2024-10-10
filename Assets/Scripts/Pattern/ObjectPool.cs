using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    private List<Queue<GameObject>> poolList = new List<Queue<GameObject>>();

    private void Awake() 
    {
        // Enum을 순회하며 poolList에 각각의 타입에 대한 Queue 추가
        foreach (PoolTypeEnums type in Enum.GetValues(typeof(PoolTypeEnums)))
        {
            poolList.Add(new Queue<GameObject>());
        }    
    }

    public void InitializePool(int poolSize, GameObject gameObject, PoolTypeEnums poolType)
    {
        for(int i = 0; i < poolSize; i++)
        {
            poolList[(int)poolType].Enqueue(CreatePool(gameObject));
        }
    }

    public GameObject GetFromPool(GameObject gameObject, PoolTypeEnums poolType)
    {
        if (poolList[(int)poolType].Count > 0)
        {
            GameObject obj = poolList[(int)poolType].Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = CreatePool(gameObject);
            obj.SetActive(true);
            return obj;
        }
    }

    public GameObject CreatePool(GameObject gameObject)
    {
        GameObject obj = Instantiate(gameObject);
        obj.SetActive(false);
        return obj;
    }

    public void ReturnToPool(GameObject obj, PoolTypeEnums poolType)
    {
        obj.SetActive(false);
        poolList[(int)poolType].Enqueue(obj);
    }
}