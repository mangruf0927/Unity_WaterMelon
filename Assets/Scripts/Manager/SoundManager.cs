using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    [Serializable]
    public class Sound
    {
        public string soundName;
        public AudioClip audioClip; 
    }

    [Header("BGM Settings")]
    public Sound[] bgmList;
    private AudioSource bgmSource;
    
    [Header("SFX Settings")]
    public Sound[] sfxList;
    public int poolSize;  // 다량의 효과음을 낼 수 있도록 채널 개수 설정
    public GameObject sfxPrefab;

    private float sfxVolume;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        InitializeSound();
    }

    private void InitializeSound()
    {
        // BGM 플레이어 초기화
        GameObject bgmObject = new GameObject("BGM Player");
        bgmObject.transform.parent = transform;
        bgmSource = bgmObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.volume = 0.4f;

        // SFX 플레이어 초기화
        ObjectPool.Instance.InitializePool(poolSize, sfxPrefab, PoolTypeEnums.SFXSOUND);

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopBGM();

        foreach (var bgm in bgmList)
        {
            if (scene.name == bgm.soundName)
            {
                PlayBGM(bgm.soundName);
                break;
            }
        }
    }

    public void PlayBGM(string name)
    {
        Sound bgmSound = Array.Find(bgmList, x => x.soundName == name);

        if (bgmSound != null)
        {
            bgmSource.clip = bgmSound.audioClip;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning(name + " 이름의 BGM을 찾을 수 없습니다.");
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null)
            bgmSource.Stop();
    }

    public void PlaySFX(string name)
    {
        Sound sfxSound = Array.Find(sfxList, x => x.soundName == name);

        if (sfxSound == null)
        {
            Debug.LogWarning(name + " 이름의 SFX를 찾을 수 없습니다.");
        }
        else
        {
            // ObjectPool에서 AudioSource 프리팹을 가져옴
            GameObject sfxObject = ObjectPool.Instance.GetFromPool(sfxPrefab, PoolTypeEnums.SFXSOUND);
            AudioSource audioSource = sfxObject.GetComponent<AudioSource>();

            // SFX 재생
            audioSource.volume = sfxVolume;
            audioSource.clip = sfxSound.audioClip;
            audioSource.Play();

            // 재생이 끝나면 오브젝트 풀로 반환
            StartCoroutine(ReturnToPool(sfxObject, sfxSound.audioClip.length));
        }
    }

    private IEnumerator ReturnToPool(GameObject sfxObject, float delay)
    {
        // SFX 재생이 끝날 때까지 대기
        yield return new WaitForSeconds(delay);
        
        // 오브젝트 풀에 반환
        ObjectPool.Instance.ReturnToPool(sfxObject, PoolTypeEnums.SFXSOUND);
    }

    public void StopAllSFX()
    {
        /// ObjectPool에서 관리하는 모든 SFX 오브젝트를 비활성화
        foreach (AudioSource sfxSource in FindAllSFXSources())
        {
            sfxSource.Stop();
            ObjectPool.Instance.ReturnToPool(sfxSource.gameObject, PoolTypeEnums.SFXSOUND);
        }
    }

    public void ChangeBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        // Debug.Log("BGM : " + volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxVolume = volume;

        var allSFXSources = FindAllSFXSources();
        foreach (AudioSource sfxSource in allSFXSources)
        {
            sfxSource.volume = volume;
        }
    }

    private AudioSource[] FindAllSFXSources()
    {
        return FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }
}