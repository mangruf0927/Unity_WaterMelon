using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]    private GameObject[] textArray;
    [SerializeField]    private GameObject[] dotArray;
    
    [SerializeField]    private float textDuration;
    [SerializeField]    private float dotDuration;
    [SerializeField]    private int nextScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;

        StartCoroutine(ShowLoadingText());

        // 로딩이 완료될 때까지 대기
        while (!operation.isDone)
        {
            // 로딩이 거의 완료되면 씬 활성화
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1.5f); // 2초 대기 후
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    private IEnumerator ShowLoadingText()
    {
        for(int i = 0; i < textArray.Length; i++)
        {
            textArray[i].SetActive(true);
            yield return new WaitForSeconds(textDuration);
        }

        StartCoroutine(ShowDotImage());
    }

    private IEnumerator ShowDotImage()
    {
        while(true)
        {
            foreach(GameObject dot in dotArray)
            {
                dot.SetActive(false);
            }

            for(int i = 0; i < dotArray.Length; i++)
            {
                dotArray[i].SetActive(true);
                yield return new WaitForSeconds(dotDuration);
            }
        }
    }
}
