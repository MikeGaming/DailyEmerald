using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MoldObject : MonoBehaviour
{
    [SerializeField] GameObject lava;
    [SerializeField] GameObject head;
    [SerializeField] Transform headSpawnPosition;

    [SerializeField] Material[] mats;
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
            if (collision.impulse.magnitude >= 100f)
            {
                hits++;
                if (hits >= 3)
                {
                    temp.transform.parent = null;
                    temp.GetComponent<Rigidbody>().isKinematic = false;
                    temp.GetComponent<Rigidbody>().useGravity = true;
                    temp.GetComponent<XRGrabInteractable>().enabled = true;
                    temp.GetComponentInChildren<MeshCollider>().enabled = true;
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
                switch (collision.gameObject.GetComponent<LavaBall>().materialType)
                {
                    case Enums.MaterialType.IRON:
                        temp.GetComponentInChildren<MeshRenderer>().material = mats[0];
                        break;

                    case Enums.MaterialType.GOLD:
                        temp.GetComponentInChildren<MeshRenderer>().material = mats[1];
                        break;

                    case Enums.MaterialType.SILVER:
                        temp.GetComponentInChildren<MeshRenderer>().material = mats[2];
                        break;

                    default:
                        temp.GetComponentInChildren<MeshRenderer>().material = mats[0];
                        break;
                }
                temp.GetComponent<SwordBlade>().materialType = collision.gameObject.GetComponent<LavaBall>().materialType;
                temp.transform.parent = gameObject.transform;
                temp.GetComponent<Rigidbody>().isKinematic = true;
                temp.GetComponent<Rigidbody>().useGravity = false;
                temp.GetComponentInChildren<MeshCollider>().enabled = false;
                temp.GetComponentInChildren<MeshRenderer>().material.SetFloat("_Fill", 0);
                temp.GetComponent<XRGrabInteractable>().enabled = false;
            }
            lava.GetComponent<MeshRenderer>().material.SetFloat("_Fill", Mathf.Clamp01(fillAmount));

        }
    }

}
