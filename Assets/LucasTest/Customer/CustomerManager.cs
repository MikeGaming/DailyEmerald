using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startPoint;
    [SerializeField]
    private GameObject endPoint;
    [SerializeField]
    private GameObject custObj;

    private Customer currentCust;

    private void Update()
    {
        if (currentCust == null)
        {
            currentCust = Instantiate(custObj, startPoint.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Customer>();
            currentCust.startPoint = startPoint.transform;
            currentCust.orderPoint = endPoint.transform;

        }
    }
}
