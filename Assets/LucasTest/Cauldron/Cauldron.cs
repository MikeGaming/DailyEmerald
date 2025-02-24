using UnityEngine;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;

    private bool heated = false;

    public List<GameObject> meltingObjs = new List<GameObject>();

    public void TryMelt()
    {
        //never nester moment
        if (!heated)
        {
            return;
        }

        //how do we aggregate melted items
        foreach(GameObject obj in meltingObjs)
        {
            meltingObjs.Remove(obj);
            Destroy(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Furnance")
        {
            heated = true;
        }
    }
}
