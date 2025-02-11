using UnityEngine;
using UnityEngine.UI;

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

    private int orderIndex;
    private string wantedMagic;

    private void Start()
    {
        Order();
    }

    private void Order()
    {
        Debug.Log(itemList.Length);
        orderIndex = Random.Range(0, itemList.Length);
        imageObj.sprite = imageList[orderIndex];
        canvasObj.SetActive(true);
        wantedMagic = magicList[Random.Range(0, magicList.Length)];
    }

    public string[] GetOrder()
    {
        return new string[] {itemList[orderIndex], wantedMagic};
    }

    public void ServeCustomer()
    {
        Debug.Log("Served!");
        canvasObj.setActive(false);
        //walk away
        Destroy(this.gameObject);
    }
}