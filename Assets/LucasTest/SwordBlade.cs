using UnityEngine;

[System.Serializable]
public class SwordBlade : MonoBehaviour
{
    [SerializeField]
    private Transform hitPoint;
    [SerializeField]
    private GameObject swordFull;

    private int hitCount;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hammer")
        {
            Collider[] cols = Physics.OverlapSphere(hitPoint.position, 5f);
            if(cols.Length > 0f)
            {
                Debug.Log("pears"); 
                foreach (Collider col in cols)
                {
                    if(col.tag == "Handle")
                    {
                        Instantiate(swordFull, transform.position + new Vector3(0, 0.25f, 0), Quaternion.Euler(0, 0, 0));
                        //would be neat to sort by distance and destroy the closest
                        //I have a custom sorter for distance already I can port over
                        Destroy(col.transform.parent.gameObject);
                        Destroy(this.gameObject);

                        return;
                    }
                }
            }
        }
    }
}
