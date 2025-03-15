#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class JSONImporter : EditorWindow
{
    static string currentFileName;

    [MenuItem("JSONImporter/Load JSON to Scriptable Objects From Folder")]
    static void ApplyFolder()
    {
        string path = EditorUtility.OpenFolderPanel("JSON Containing File", "", "");
        string[] files = Directory.GetFiles(path);
        
        foreach(string file in files)
        {
            if (file.EndsWith(".json"))
            {
                Debug.Log(file.Substring(0, file.Length - 5) + ".asset");

                //read file and parse as JSON
                string jsonString = File.ReadAllText(file);
                currentFileName = Path.GetFileName(file);
                Debug.Log(jsonString);
                JSONImport.Order currentOrder = JsonUtility.FromJson<JSONImport.Order>(jsonString);
                Debug.Log(currentOrder.OrderWeapons.Item1.CostRatio);
                Debug.Log(GetItemEnumFromName(currentOrder.OrderWeapons.Item1.Item));
                //Debug.Log(currentOrder.Skibidi);
                //Debug.Log(currentOrder.Toilet);
                //Debug.Log(currentOrder.Test2);

                //create sctipable object instance and fill with data
                CustomerOrder orderScriptableObject = ScriptableObject.CreateInstance<CustomerOrder>();

                //text
                orderScriptableObject.customerOrderText = currentOrder.CustomerOrderText;

                //weapons
                //List<CustomerOrderItemDictItem> dict = new();
                //dict.Add(new CustomerOrderItemDictItem(GetItemEnumFromName(currentOrder.OrderWeapons.Item1.Item), currentOrder.OrderWeapons.Item1.CostRatio));

                orderScriptableObject.acceptedItems.orderItems = new();

                //orderScriptableObject.acceptedItems.orderItems.Add(new CustomerOrderItemDictItem(GetItemEnumFromName(currentOrder.OrderWeapons.Item1.Item), currentOrder.OrderWeapons.Item1.CostRatio));
                orderScriptableObject.acceptedItems.orderItems.Add(new CustomerOrderItemDictItem(GetItemEnumFromName(currentOrder.OrderWeapons.Item2.Item), currentOrder.OrderWeapons.Item2.CostRatio));
                orderScriptableObject.acceptedItems.orderItems.Add(new CustomerOrderItemDictItem(GetItemEnumFromName(currentOrder.OrderWeapons.Item3.Item), currentOrder.OrderWeapons.Item3.CostRatio));
                orderScriptableObject.acceptedItems.orderItems.Add(new CustomerOrderItemDictItem(GetItemEnumFromName(currentOrder.OrderWeapons.Item4.Item), currentOrder.OrderWeapons.Item4.CostRatio));

                orderScriptableObject.acceptedItems.DictionarizeSelf();

                //magics
                orderScriptableObject.acceptedMagics.orderItems.Add(new CustomerOrderMagicDictItem(GetMagicEnumFromName(currentOrder.OrderEnchantments.Item1.Item), currentOrder.OrderEnchantments.Item1.CostRatio));
                orderScriptableObject.acceptedMagics.orderItems.Add(new CustomerOrderMagicDictItem(GetMagicEnumFromName(currentOrder.OrderEnchantments.Item2.Item), currentOrder.OrderEnchantments.Item2.CostRatio));
                orderScriptableObject.acceptedMagics.orderItems.Add(new CustomerOrderMagicDictItem(GetMagicEnumFromName(currentOrder.OrderEnchantments.Item3.Item), currentOrder.OrderEnchantments.Item3.CostRatio));
                orderScriptableObject.acceptedMagics.orderItems.Add(new CustomerOrderMagicDictItem(GetMagicEnumFromName(currentOrder.OrderEnchantments.Item4.Item), currentOrder.OrderEnchantments.Item4.CostRatio));

                orderScriptableObject.acceptedMagics.DictionarizeSelf();

                //materials
                orderScriptableObject.acceptedMaterials.orderItems.Add(new CustomerOrderMaterialDictItem(GetMaterialEnumFromName(currentOrder.OrderMaterials.Item1.Item), currentOrder.OrderMaterials.Item1.CostRatio));
                orderScriptableObject.acceptedMaterials.orderItems.Add(new CustomerOrderMaterialDictItem(GetMaterialEnumFromName(currentOrder.OrderMaterials.Item2.Item), currentOrder.OrderMaterials.Item2.CostRatio));
                orderScriptableObject.acceptedMaterials.orderItems.Add(new CustomerOrderMaterialDictItem(GetMaterialEnumFromName(currentOrder.OrderMaterials.Item3.Item), currentOrder.OrderMaterials.Item3.CostRatio));

                orderScriptableObject.acceptedMaterials.DictionarizeSelf();

                //create scriptable object in proper folder
                AssetDatabase.CreateAsset(orderScriptableObject, "Assets/Scripts/Shop/OrderScriptableObjects/" + file.Substring(0, file.Length - 5) + ".asset");
                AssetDatabase.SaveAssets();
            }
        }
    }
    static Enums.ItemType GetItemEnumFromName(string name)
    {
        switch(name.ToLower()){
            case "sword":
                return Enums.ItemType.SWORD;
            case "axe":
                return Enums.ItemType.AXE;
            case "mace":
                return Enums.ItemType.MACE;
            case "spear":
                return Enums.ItemType.SPEAR;
            default:
                Debug.LogError("JSON IMPORTER ERROR: OrderWeapons has Invalid Entry in " + currentFileName);
                return Enums.ItemType.SWORD;
        }
    }

    static Enums.MagicType GetMagicEnumFromName(string name)
    {
        switch (name.ToLower())
        {
            case "flame":
                return Enums.MagicType.FLAME;
            case "frost":
                return Enums.MagicType.FROST;
            case "lightning":
                return Enums.MagicType.LIGHTNING;
            case "none":
                return Enums.MagicType.NONE;
            default:
                Debug.LogError("JSON IMPORTER ERROR: OrderEnchantments has Invalid Entry in " + currentFileName);
                return Enums.MagicType.NONE;
        }
    }

    static Enums.MaterialType GetMaterialEnumFromName(string name)
    {
        switch (name.ToLower())
        {
            case "flame":
                return Enums.MaterialType.IRON;
            case "frost":
                return Enums.MaterialType.GOLD;
            case "lightning":
                return Enums.MaterialType.SILVER;
            default:
                Debug.LogError("JSON IMPORTER ERROR: OrderMaterials has Invalid Entry in " + currentFileName);
                return Enums.MaterialType.IRON;
        }
    }
}

//namespace is used to avoid bloating intellisense with garbage when working in other scripts
namespace JSONImport
{
    [System.Serializable]
    public class Item1
    {
        public string Item;
        public float CostRatio;
    }

    [System.Serializable]
    public class Item2
    {
        public string Item;
        public float CostRatio;
    }

    [System.Serializable]
    public class Item3
    {
        public string Item;
        public float CostRatio;
    }

    [System.Serializable]
    public class Item4
    {
        public string Item;
        public float CostRatio;
    }

    [System.Serializable]
    public class OrderEnchantments
    {
        public Item1 Item1;
        public Item2 Item2;
        public Item3 Item3;
        public Item4 Item4;
    }

    [System.Serializable]
    public class OrderMaterials
    {
        public Item1 Item1;
        public Item2 Item2;
        public Item3 Item3;
    }

    [System.Serializable]
    public class OrderWeapons
    {
        public Item1 Item1;
        public Item2 Item2;
        public Item3 Item3;
        public Item4 Item4;
    }

    public class Order
    {
        public string CustomerOrderText;
        public OrderWeapons OrderWeapons;
        public OrderEnchantments OrderEnchantments;
        public OrderMaterials OrderMaterials;
    }

    public class Test
    {
        public string Skibidi;
        public int Toilet;
        public string Test2;
    }
}
#endif