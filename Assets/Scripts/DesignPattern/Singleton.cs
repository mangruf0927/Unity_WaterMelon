using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static T _instance; // 정적 멤버 변수에 인스턴스 저장

    private static object _lock = new object(); // 다중 스레드 환경에서의 데이터 경합 방지

    public static bool destroyOnLoad = false;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock) // 리소스를 많이 잡아먹어서 비싼 연산. 따라서 우선 _instance가 존재하는지를 확인한 후 lock문
                {
                    // instance가 존재하지 않을 경우 singleton 클래스의 instance를 찾음
                    _instance = (T)FindAnyObjectByType(typeof(T));
                    
                    if (_instance == null)
                    {
                        //
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        if (!Singleton<T>.destroyOnLoad)
                        {
                            DontDestroyOnLoad(singleton); // 특정 객체가 씬 전환 시에도 파괴되지 않도록 설정하는 메서드
                        }
                    }
                }
            }
            return _instance;
        }
    }


    protected virtual void OnDestroy()
    {
        if (!Application.isPlaying && Instance != null && Equals(Instance, this))
        {
            _instance = null;
        }
    }
}