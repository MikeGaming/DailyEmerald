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

    private float itemRatio;
    private float magicRatio;
    private float materialRatio;

    private float satisfactionAmount;

    public void SetMagic(Enums.MagicType mag)
    {
        magic = mag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Customer")
        {
            if (!other.GetComponent<Customer>().orderPending) return;
            CustomerOrder custOrder = other.GetComponent<Customer>().order;
            SetCostRatios(custOrder);

            other.GetComponent<Customer>().ServeCustomer(satisfactionAmount);
            Destroy(this.gameObject);
        }
    }

    private void SetCostRatios(CustomerOrder custOrder)
    {
        itemRatio = 0;
        magicRatio = 0;
        materialRatio = 0;
        if (custOrder.acceptedItems.dict.ContainsKey(item))
        {
            itemRatio = custOrder.acceptedItems.dict[item];
        }

        if (custOrder.acceptedMagics.dict.ContainsKey(magic))
        {
            magicRatio = custOrder.acceptedMagics.dict[magic];
        }

        if (custOrder.acceptedMaterials.dict.ContainsKey(material))
        {
            materialRatio = custOrder.acceptedMaterials.dict[material];
        }

        satisfactionAmount = (itemRatio + magicRatio + materialRatio) / 3f;
        Debug.Log("Satisfaction Amount: " + satisfactionAmount);
    }
}
