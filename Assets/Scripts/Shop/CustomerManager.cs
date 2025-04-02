using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startPoint;
    [SerializeField]
    private GameObject endPoint;
    [SerializeField]
    private GameObject custObj;
    [SerializeField]
    private CustomerOrder[] orderList;

    [SerializeField]
    private FlipClockManager scoreManager;

    [Header("Tutorial")]
    [SerializeField] private bool isTutorial;
    [SerializeField] private CustomerOrder tutorialOrder;

    private Customer currentCust;

    private void Update()
    {
        if (currentCust == null && !isTutorial)
        {
            currentCust = Instantiate(custObj, startPoint.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Customer>();
            currentCust.startPoint = startPoint.transform;
            currentCust.orderPoint = endPoint.transform;
            currentCust.order = orderList[Random.Range(0, orderList.Length)];
            currentCust.scoreManager = scoreManager;
        }
    }

    private void Start()
    {
        if (isTutorial)
        {
            currentCust = Instantiate(custObj, startPoint.transform.position, Quaternion.identity).GetComponent<Customer>();
            currentCust.startPoint = startPoint.transform;
            currentCust.orderPoint = endPoint.transform;
            currentCust.order = tutorialOrder;
            currentCust.scoreManager = scoreManager;
        }
    }
}
