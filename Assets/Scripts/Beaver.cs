using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Beaver : MonoBehaviour
{

    [SerializeField] float carveTime = 3f;
    [SerializeField] GameObject[] handleTypePrefabs;
    [SerializeField] GameObject[] handleTypeUIs;
    int handleTypeIndex = 0;
    public bool isCarving;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WoodBit") && !isCarving)
        {
            StartCoroutine(WoodCarve(other));
        }
        if (other.CompareTag("Axe") && !isCarving)
        {
            Debug.Log("orange juice");
            handleTypeUIs[handleTypeIndex].SetActive(false);
            handleTypeIndex = (handleTypeIndex + 1) % handleTypePrefabs.Length;
            handleTypeUIs[handleTypeIndex].SetActive(true);
        }
    }

    private IEnumerator WoodCarve(Collider collider)
    {
        isCarving = true;
        Transform tempParent = collider.transform.parent;
        
        collider.GetComponent<XRGrabInteractable>().enabled = false;
        collider.GetComponent<Rigidbody>().isKinematic = true;
        collider.transform.SetParent(transform);
        collider.transform.localPosition = Vector3.zero;
        collider.transform.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(carveTime);
        Destroy(collider.gameObject);
        Instantiate(handleTypePrefabs[handleTypeIndex], transform.position, transform.rotation).transform.SetParent(tempParent);
        isCarving = false;
    }
}
