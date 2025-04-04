using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

//when object enters trigger it gets stored
//each object is worth 1 metal
//all meltable objects are destroyed, with their ore type being counted
//highest ore count is the metal made, all objects not matching the majority are destroyed and wasted
//melt count sent to bucket for storage.
//if new metal reaches the bucket it replaces the current bucket metal

public class Oven : MonoBehaviour
{
    [SerializeField] private Transform doorTrans;
    [SerializeField] private LavaAnimation firstAnim;

    private List<GameObject> objsInsideOven = new List<GameObject>();

    //index for each ore is the int representation of Enums.MaterialType
    private int[] oreCounts = new int[Enum.GetNames(typeof(Enums.MaterialType)).Length];

    public Enums.MaterialType meltMaterial;

    //add meltable objects to list
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<Meltable>(out Meltable meltComp)) objsInsideOven.Add(other.transform.parent.gameObject);
    }

    //remove meltable objects from list
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.TryGetComponent<Meltable>(out Meltable meltComp)) objsInsideOven.Remove(other.transform.parent.gameObject);
    }

    //melt objects
    public void Melt()
    {
        Debug.Log("MeltTest");
        if (doorTrans.position.y > 1.2f) return;
        Debug.Log("MeltTest2");
        CountMeltables();

        //one-liner for finding largest value in array (thanks stack overflow)
        (int number, int index) = oreCounts.Select((n, i) => (n, i)).Max();

        Debug.Log((Enums.MaterialType) index);

        //TO-DO:
        //play sound
        firstAnim.canStart = true;
    }

    private void CountMeltables()
    {
        oreCounts[0] = 0;
        oreCounts[1] = 0;
        oreCounts[2] = 0;

        foreach (GameObject obj in objsInsideOven)
        {
            Meltable meltComp = obj.GetComponentInParent<Meltable>();

            switch (meltComp.oreType)
            {
                case Enums.MaterialType.IRON:
                    oreCounts[(int)Enums.MaterialType.IRON]++;
                    break;
                case Enums.MaterialType.SILVER:
                    oreCounts[(int)Enums.MaterialType.SILVER]++;
                    break;
                case Enums.MaterialType.GOLD:
                    oreCounts[(int)Enums.MaterialType.GOLD]++;
                    break;
            }

            Destroy(obj);
        }

        objsInsideOven.Clear();
    }
}
