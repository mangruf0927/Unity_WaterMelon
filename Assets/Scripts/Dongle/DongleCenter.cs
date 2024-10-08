using System.Collections;
using UnityEngine;

public class DongleCenter : MonoBehaviour
{
    [Header("동글 Prefab")]
    [SerializeField] private GameObject donglePrefab;

    private DongleFactory dongleFactory;
    private DongleController dongle;

    public delegate void DongleHandler(DongleController controller);
    public event DongleHandler OnGetController;

    private void Awake() 
    {
        dongleFactory = new DongleFactory(donglePrefab);
        dongleFactory.CreateDongle(Random.Range(0, 4));    
    }

    private void Start() 
    {
        CreateNextDongle();    
    }

    private void CreateNextDongle()
    {
        DongleController newDongle = dongleFactory.CreateDongle(Random.Range(0,4));
        dongle = newDongle;
        dongle.gameObject.SetActive(true);

        OnGetController.Invoke(dongle);

        StartCoroutine(WaitNext(2.5f));
    }

    private IEnumerator WaitNext(float waitTime)
    {
        while(dongle != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        CreateNextDongle();
    }

    public void DropDongle()
    {
        dongle = null;
    }
}
