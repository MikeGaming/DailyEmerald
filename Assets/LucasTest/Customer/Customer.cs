using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

[System.Serializable]
public class Customer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Ensure this array's length is the same as the length of Enums.ItemType")]
    private Sprite[] imageList;

    [SerializeField]
    private GameObject canvasObj;
    [SerializeField]
    private Image imageObj;

    public Transform startPoint;
    public Transform orderPoint;

    private int orderIndex;
    private int magicIndex;

    private NavMeshAgent navMeshAgent;

    private bool orderPending;

    public struct CustOrder
    {
        public Enums.ItemType orderItem;
        public Enums.MagicType orderMagic;

        public CustOrder(Enums.ItemType item, Enums.MagicType magic)
        {
            orderItem = item;
            orderMagic = magic;
        }
    }

    private CustOrder order;

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
        orderIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(Enums.ItemType)).Length);
        magicIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(Enums.MagicType)).Length);
        imageObj.sprite = imageList[orderIndex];
        canvasObj.SetActive(true);
        order = new CustOrder((Enums.ItemType)orderIndex, (Enums.MagicType)magicIndex);
        orderPending = true;
    }

    public CustOrder GetOrder()
    {
        return order;
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