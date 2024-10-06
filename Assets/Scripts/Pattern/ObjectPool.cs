using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    private List<Queue<GameObject>> poolList = new List<Queue<GameObject>>();

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
        GameObject obj = Instantiate(gameObject, this.transform);
        obj.SetActive(false);
        return obj;
    }

    public void ReturnToPool(GameObject obj, PoolTypeEnums poolType)
    {
        obj.SetActive(false);
        poolList[(int)poolType].Enqueue(obj);
    }
}