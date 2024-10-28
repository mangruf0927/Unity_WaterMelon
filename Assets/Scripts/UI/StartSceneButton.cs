using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
{
    [SerializeField] private GameObject scoreUI;

    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickScore()
    {
        scoreUI.SetActive(true);
    }
}
