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
    [SerializeField]
    private GameObject[] magicParticles; //0 = flame, 1 = frost, 2 = lightning

    private float itemRatio;
    private float magicRatio;
    private float materialRatio;
    private bool fullOfMagic;

    private Vector3 magicAmount;

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

    private void Update()
    {
        if((magicAmount.x >= 40 || magicAmount.y >= 40 || magicAmount.z >= 40) && !fullOfMagic)
        {
            // Whichever magic type is the highest, that's the one that will be used
            if (magicAmount.x > magicAmount.y && magicAmount.x > magicAmount.z)
            {
                SetMagic(Enums.MagicType.FLAME);
                magicParticles[0].SetActive(true);
            }
            else if (magicAmount.y > magicAmount.x && magicAmount.y > magicAmount.z)
            {
                SetMagic(Enums.MagicType.FROST);
                magicParticles[1].SetActive(true);

            }
            else if (magicAmount.z > magicAmount.x && magicAmount.z > magicAmount.y)
            {
                SetMagic(Enums.MagicType.LIGHTNING);
                magicParticles[2].SetActive(true);
            }
            fullOfMagic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CrystalBit" && !fullOfMagic)
        {
            switch (collision.gameObject.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)","")) {
                case "Flame":
                    magicAmount += Vector3.right;
                    break;
                case "Frost":
                    magicAmount += Vector3.up;
                    break;
                case "Lightning":
                    magicAmount += Vector3.forward;
                    break;
            }
            Destroy(collision.gameObject);
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
