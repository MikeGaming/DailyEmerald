using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

[System.Serializable]
public class Customer : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasObj;
    [SerializeField]
    private TextMeshProUGUI textObj;

    public Transform startPoint;
    public Transform orderPoint;

    private NavMeshAgent navMeshAgent;
    public bool orderPending;

    public CustomerOrder order;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(orderPoint.position);
    }

    private void Update()
    {
        if(!navMeshAgent.pathPending && Mathf.Abs(Vector3.Distance(transform.position, orderPoint.position)) < 1f)
        {
            if(!orderPending) Order();
        }
    }

    private void Order()
    {
        textObj.SetText(order.customerOrderText);
        canvasObj.SetActive(true);
        orderPending = true;
    }

    public void ServeCustomer(float satisfaction)
    {   
        //TO-DO:
        //add small wait time after being served
        //update text based on satisfaction
        if(satisfaction != 1)
        {
            Debug.Log("Served Unhappy!");
            canvasObj.SetActive(false);
            //walk away
            navMeshAgent.SetDestination(startPoint.transform.position);
            Destroy(this.gameObject, 5f);
            return;
        }

        Debug.Log("Served Happy!");
        canvasObj.SetActive(false);
        //walk away
        navMeshAgent.SetDestination(startPoint.transform.position);
        Destroy(this.gameObject, 5f);
    }
}