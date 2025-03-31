using System.Collections;
using UnityEngine;

public class MoldObject : MonoBehaviour
{
    [SerializeField] GameObject lava;
    [SerializeField] GameObject head;
    Rigidbody rb;

    [HideInInspector] public bool molding;

    int hits;
    float time;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.GetAccumulatedForce().magnitude >= 1f)
        {
            hits++;
            if (hits >= 3)
            {
                head.SetActive(true);
                head.transform.parent = null;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            StartCoroutine(Mold());
        }
    }

    private void Update()
    {
        if (molding)
        {
            time += Time.deltaTime;
            lava.GetComponent<MeshRenderer>().material.SetFloat("_Fill", Mathf.Clamp01(time / 2f));
        }
    }

    private IEnumerator Mold()
    {
        molding = true;
        yield return new WaitForSeconds(2f);
        molding = false;
    }

}
