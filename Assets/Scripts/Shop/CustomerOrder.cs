using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CustomerOrder", menuName = "Scriptable Objects/CustomerOrder")]
public class CustomerOrder : ScriptableObject
{
    [TextArea(4, 8)]
    public string customerOrderText;
    public CustomerOrderItemDict acceptedItems;
    public CustomerOrderMagicDict acceptedMagics;
    public CustomerOrderMaterialDict acceptedMaterials;

    public int maxCoin;

    private void OnValidate()
    {
        //when scriptable object is updated it adds the changes to each dictionary.
        acceptedItems.DictionarizeSelf();
        acceptedMagics.DictionarizeSelf();
        acceptedMaterials.DictionarizeSelf();
        Debug.Log("Updated: " + this.name);
    }
}

[System.Serializable]
public class CustomerOrderItemDictItem
{
    public CustomerOrderItemDictItem(Enums.ItemType itemCon, float costCon)
    {
        item = itemCon;
        costRatio = costCon;
    }

    public Enums.ItemType item;
    [Range(0.0f, 1f)]
    public float costRatio;
}

[System.Serializable]
public class CustomerOrderItemDict
{
    public List<CustomerOrderItemDictItem> orderItems = new List<CustomerOrderItemDictItem>();
    public Dictionary<Enums.ItemType, float> dict = new Dictionary<Enums.ItemType, float>();

    public void DictionarizeSelf()
    {
        //clear dict to avoid duplicate key error
        dict.Clear();
        foreach (CustomerOrderItemDictItem orderItem in orderItems) try { dict.Add(orderItem.item, orderItem.costRatio); } catch (System.ArgumentException) { };
    }
}


[System.Serializable]
public class CustomerOrderMagicDictItem
{
    public CustomerOrderMagicDictItem(Enums.MagicType itemCon, float costCon)
    {
        magic = itemCon;
        costRatio = costCon;
    }

    public Enums.MagicType magic;
    [Range(0.0f, 1f)]
    public float costRatio;
}

[System.Serializable]
public class CustomerOrderMagicDict
{
    public List<CustomerOrderMagicDictItem> orderItems = new List<CustomerOrderMagicDictItem>();
    public Dictionary<Enums.MagicType, float> dict = new Dictionary<Enums.MagicType, float>();

    public void DictionarizeSelf()
    {
        //clear dict to avoid duplicate key error
        dict.Clear();
        foreach (CustomerOrderMagicDictItem orderItem in orderItems) try{ dict.Add(orderItem.magic, orderItem.costRatio); } catch (System.ArgumentException) { };
    }
}

[System.Serializable]
public class CustomerOrderMaterialDictItem
{
    public CustomerOrderMaterialDictItem(Enums.MaterialType itemCon, float costCon)
    {
        material = itemCon;
        costRatio = costCon;
    }

    public Enums.MaterialType material;
    [Range(0.0f, 1f)]
    public float costRatio;
}

[System.Serializable]
public class CustomerOrderMaterialDict
{
    public List<CustomerOrderMaterialDictItem> orderItems = new List<CustomerOrderMaterialDictItem>();
    public Dictionary<Enums.MaterialType, float> dict = new Dictionary<Enums.MaterialType, float>();

    public void DictionarizeSelf()
    {
        //clear dict to avoid duplicate key error
        dict.Clear();
        foreach (CustomerOrderMaterialDictItem orderItem in orderItems) try{ dict.Add(orderItem.material, orderItem.costRatio); } catch (System.ArgumentException) { };
    }
}