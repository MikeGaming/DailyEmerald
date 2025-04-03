using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using System.Collections;

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

    public FlipClockManager scoreManager;

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

    string[,] satisfactionText = new string[4, 3];

    private void Awake()
    {
        // 3 different bad satisfaction texts
        satisfactionText[0, 0] = "This is not what I wanted at all!";
        satisfactionText[0, 1] = "What am I supposed to do with this??";
        satisfactionText[0, 2] = "Absolutely useless item but I'll take it anyway";

        // 3 different ok satisfaction texts
        satisfactionText[1, 0] = "Not exactly what I wanted, but I'll take it.";
        satisfactionText[1, 1] = "Not bad, but not great either.";
        satisfactionText[1, 2] = "Could be better, but it's okay I guess.";

        // 3 different good satisfaction texts
        satisfactionText[2, 0] = "Decent work, thanks!";
        satisfactionText[2, 1] = "Good enough, I suppose.";
        satisfactionText[2, 2] = "Not bad, I like it!";

        // 3 different perfect satisfaction texts
        satisfactionText[3, 0] = "Wow, this is exactly what I wanted!";
        satisfactionText[3, 1] = "This is perfect, thank you!";
        satisfactionText[3, 2] = "This is exactly what I wanted, thank you!";
    }
    public IEnumerator ServeCustomer(float satisfaction)
    {
        //TO-DO:
        //make goon hold weapon
        //apply to global score counter

            // display text with delay if happy or unhappy
        if (satisfaction == 0)
        {
            textObj.SetText(satisfactionText[0, Random.Range(0, 3)]);
        }
        else if (satisfaction < 0.5f)
        {
            textObj.SetText(satisfactionText[1, Random.Range(0, 3)]);
        }
        else if (satisfaction < 0.75f)
        {
            textObj.SetText(satisfactionText[2, Random.Range(0, 3)]);
        }
        else if (satisfaction == 1)
        {
            textObj.SetText(satisfactionText[3, Random.Range(0, 3)]);
        }

        yield return new WaitForSeconds(2f);

        scoreManager.AddScore(Mathf.FloorToInt(order.maxCoin * satisfaction));

        canvasObj.SetActive(false);
        //walk away
        navMeshAgent.SetDestination(startPoint.transform.position);
        Destroy(this.gameObject, 5f);
    }
}