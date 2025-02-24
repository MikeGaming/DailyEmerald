using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SwordBlade : MonoBehaviour
{
    [SerializeField]
    private Transform hitPoint;
    [SerializeField]
    private GameObject swordFull;

    private int hitCount;

    private List<GameObject> handleList = new List<GameObject>();

    private GameObject realHandle;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hammer")
        {
            Collider[] cols = Physics.OverlapSphere(hitPoint.position, 2f);
            if(cols.Length > 0f)
            {
                Debug.Log("pears"); 

                //collect all handles in a list
                foreach (Collider col in cols)
                {
                    if(col.tag == "Handle")
                    {
                        handleList.Add(col.transform.parent.gameObject);
                    }
                }

                //sorts entries by distance to the blade's hitpoint
                handleList.Sort((x, y) => { return (hitPoint.position - x.transform.position).sqrMagnitude.CompareTo((hitPoint.position - y.transform.position).sqrMagnitude); });

                if(handleList.Count > 0)
                {
                    if(handleList[0].gameObject == realHandle)
                    {
                        hitCount++;
                    }
                    else
                    {
                        realHandle = handleList[0].gameObject;
                        hitCount = 1;
                    }
                }

                //clear the handeList in case some messy things with multiple blades happens
                handleList.Clear();

                if (hitCount >= 3)
                {
                    Instantiate(swordFull, transform.position + new Vector3(0, 0.25f, 0), Quaternion.Euler(0, 0, 0));
                    Destroy(realHandle);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
