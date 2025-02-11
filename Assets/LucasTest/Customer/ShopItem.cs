using UnityEngine;

[System.Serializable]
public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private string objName;
    [SerializeField]
    private string magic;

    public void SetMagic(string mag)
    {
        magic = mag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Customer")
        {
            string[] custOrder = other.GetComponent<Customer>().GetOrder();
            if(objName == custOrder[0] && magic == custOrder[1])
            {
                other.GetComponent<Customer>().ServeCustomer();
                Destroy(this.gameObject);
            }
        }
    }
}
