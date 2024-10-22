using System.Collections;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public GameObject particlePrefab;
    public float[] testSizes;

    public float delayBetweenTests = 1.0f;

    private void Start()
    {
        // 크기 테스트를 순차적으로 실행
        StartCoroutine(TestParticleEffectsSequentially());
    }


    private IEnumerator TestParticleEffectsSequentially()
    {
        foreach (float size in testSizes)
        {
            TestParticleEffect(size);
            yield return new WaitForSeconds(delayBetweenTests);
        }
    }

    private void TestParticleEffect(float dongleSize)
    {
        // 파티클 Prefab을 인스턴스화
        GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        // 파티클 시스템 컴포넌트를 가져옴
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
        var mainModule = particleSystem.main;

        // Dongle의 크기에 따라 Start Size 조절 (퍼지는 정도를 변경)
        mainModule.startSize = new ParticleSystem.MinMaxCurve(dongleSize * 0.5f, dongleSize);

        // Shape Module의 반경도 크기에 따라 조절 (Shape Module을 Sphere로 설정하여 사방으로 퍼지도록 변경)
        var shapeModule = particleSystem.shape;
        shapeModule.shapeType = ParticleSystemShapeType.Sphere;
        shapeModule.scale = new Vector3(dongleSize * 0.5f, dongleSize * 0.5f, 0f);

        // 파티클 재생 후 일정 시간 뒤 파괴
        particleSystem.Play();
        Destroy(particle, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
    }
}
