using System.Collections;
using UnityEngine;

public class MergeEffect : MonoBehaviour
{
    [SerializeField]    private ParticleSystem particle;

    // StartSize와 Shape Radius 값을 배열로 정의
    private readonly float[] startSizeArray = { 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f, 1.1f, 1.2f };
    private readonly float[] shapeRadiusArray = { 0.05f, 0.1f, 0.2f, 0.4f, 0.6f, 0.7f, 0.9f, 1.0f, 1.2f, 1.4f };

    public void SetProperties(int dongleLevel)
    {
        if (dongleLevel < 2 || dongleLevel > 11)
        {
            Debug.Log("존재하지 않는 레벨");
            return;
        }

        // 파티클 시스템의 메인 모듈과 쉐이프 모듈 가져오기
        ParticleSystem.MainModule mainModule = particle.main;
        ParticleSystem.ShapeModule shapeModule = particle.shape;

        // 배열에서 값 가져와서 설정 (dongleLevel - 1 인덱스 사용)
        mainModule.startSize = startSizeArray[dongleLevel - 2];
        shapeModule.radius = shapeRadiusArray[dongleLevel - 2];

        particle.Play();

        StartCoroutine(ReturnParticleToPool());
    }

    private IEnumerator ReturnParticleToPool()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.Instance.ReturnToPool(gameObject , PoolTypeEnums.PARTICLE);
    }
}
