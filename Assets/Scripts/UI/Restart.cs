using System.Collections;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField]    private DongleCenter dongleCenter;

    public void OnClickRestart()
    {
        dongleCenter.ResetDongles();
        dongleCenter.InitializeCenter();
        // StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.2f);
        dongleCenter.ResetDongles();
        yield return new WaitForSeconds(0.2f);
        dongleCenter.InitializeCenter();
    }
}
