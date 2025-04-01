using System.Collections;
using UnityEngine;

public class MoldObject : MonoBehaviour
{
    [SerializeField] GameObject lava;
    [SerializeField] GameObject head;
    [SerializeField] Transform headSpawnPosition;
    Rigidbody rb;

    GameObject temp;

    bool molded;

    int hits;
    float fillAmount;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(molded)
        {
            if (collision.impulse.magnitude >= 1f)
            {
                hits++;
                if (hits >= 3)
                {
                    temp.transform.parent = null;
                    temp.GetComponent<Rigidbody>().isKinematic = false;
                    temp.GetComponent<Rigidbody>().useGravity = true;
                    //Destroy(gameObject);
                    molded = false;
                    hits = 0;
                    fillAmount = 0;
                }
            }
        }
        if (collision.gameObject.CompareTag("Lava") && !molded)
        {
            Destroy(collision.gameObject);
            fillAmount += 1 / 25f;
            if(fillAmount >= 1f)
            {
                fillAmount = 0;
                molded = true;
                temp = Instantiate(head, headSpawnPosition.position, headSpawnPosition.rotation);
                temp.GetComponent<Rigidbody>().isKinematic = true;
                temp.GetComponent<Rigidbody>().useGravity = false;
                temp.GetComponent<MeshRenderer>().material.SetFloat("_Fill", 0);
            }
            lava.GetComponent<MeshRenderer>().material.SetFloat("_Fill", Mathf.Clamp01(fillAmount));

        }
    }

}
