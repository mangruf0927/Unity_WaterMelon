using UnityEngine;

public class PoolTest : MonoBehaviour
{
    public GameObject donglePrefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject obj = ObjectPool.Instance.GetFromPool(donglePrefab, PoolTypeEnums.DONGLE);
            obj.transform.position = spawnPos;
        }

        if(Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                ObjectPool.Instance.ReturnToPool(hitObject, PoolTypeEnums.DONGLE);
                Debug.Log("PoolTestManager: 클릭한 오브젝트를 풀에 반환했습니다.");
            }
        }
    }
}
