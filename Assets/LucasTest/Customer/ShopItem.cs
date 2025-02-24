using UnityEngine;

[System.Serializable]
public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Enums.ItemType item;
    [SerializeField]
    private Enums.MaterialType material;
    [SerializeField]
    private Enums.MagicType magic;

    public void SetMagic(Enums.MagicType mag)
    {
        magic = mag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Customer")
        {
            Customer.CustOrder custOrder = other.GetComponent<Customer>().GetOrder();
            if(item == custOrder.orderItem && magic == custOrder.orderMagic)
            {
                other.GetComponent<Customer>().ServeCustomer();
                Destroy(this.gameObject);
            }
        }
    }
}
