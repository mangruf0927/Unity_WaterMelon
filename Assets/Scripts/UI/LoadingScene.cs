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
        StartCoroutine(ShowLoadingText());
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
