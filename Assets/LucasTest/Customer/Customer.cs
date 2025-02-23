using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

[System.Serializable]
public class Customer : MonoBehaviour
{
    [SerializeField]
    private string[] itemList;
    [SerializeField]
    private string[] magicList;
    [SerializeField]
    private Sprite[] imageList;

    [SerializeField]
    private GameObject canvasObj;
    [SerializeField]
    private Image imageObj;

    public Transform startPoint;
    public Transform orderPoint;

    private int orderIndex;
    private string wantedMagic;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(orderPoint.position);
    }

    private void Update()
    {
        if(!navMeshAgent.pathPending && Mathf.Abs(Vector3.Distance(transform.position, orderPoint.position)) < 1f)
        {
            Order();
        }
    }

    private void Order()
    {
        orderIndex = Random.Range(0, itemList.Length);
        imageObj.sprite = imageList[orderIndex];
        canvasObj.SetActive(true);
        wantedMagic = magicList[Random.Range(0, magicList.Length)];
        Debug.Log("Going to Order");
    }

    public string[] GetOrder()
    {
        return new string[] {itemList[orderIndex], wantedMagic};
    }

    public void ServeCustomer()
    {   
        Debug.Log("Served!");
        canvasObj.SetActive(false);
        //walk away
        navMeshAgent.SetDestination(startPoint.transform.position);
        Destroy(this.gameObject, 5f);
    }
}