using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//when object enters trigger it gets stored
//each object is worth 1 metal
//all meltable objects are destroyed, with their ore type being counted
//highest ore count is the metal made, all objects not matching the majority are destroyed and wasted
//melt count sent to bucket for storage.
//if new metal reaches the bucket it replaces the current bucket metal

public class Oven : MonoBehaviour
{
    [SerializeField] private Transform doorTrans;

    private List<GameObject> objsInsideOven = new List<GameObject>();

    //index for each ore is the int representation of Enums.MaterialType
    private int[] oreCounts;

    private Enums.MaterialType meltMaterial;

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
        if (doorTrans.position.y! < 1.2f) return;
        CountMeltables();

        //one-liner for finding largest value in array (thanks stack overflow)
        (int number, int index) = oreCounts.Select((n, i) => (n, i)).Max();

        meltMaterial = (Enums.MaterialType) index;

        //TO-DO:
        //send data to bucket
        //trigger animation through pipes
        //play sound
    }

    private void CountMeltables()
    {
        foreach(GameObject obj in objsInsideOven)
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
