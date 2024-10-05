using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{    
    [Header("초기 풀 사이즈")]
    public int poolSize = 20; 
    
    [SerializeField]
    private GameObject objectPrefab;

    private Queue<GameObject> poolQueue = new Queue<GameObject>(); // 리스트 보다 Queue가 더 효율적

    private void Awake() 
    { 
        if(objectPrefab == null) 
        {
            Debug.LogError("ObjectPrefab이 할당되지 않았습니다");
        }

        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = CreatePool();
            poolQueue.Enqueue(obj);
        }   
    }

    public GameObject GetFromPool()
    {
        if(poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.gameObject.SetActive(true);
            // obj.transform.SetParent(null); (필요 시) 부모 해제
            return obj;
        }
        else
        {
            // 필요시 오브젝트 생성
            GameObject obj = CreatePool();
            obj.SetActive(true);
            return obj;
        }
    }

    public GameObject CreatePool()
    {
        GameObject obj = Instantiate(objectPrefab, this.transform);
        obj.SetActive(false);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}
