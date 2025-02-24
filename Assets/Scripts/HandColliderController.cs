using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandColliderController : MonoBehaviour
{

    XRGrabInteractable grabInteractable;
    MeshCollider handCollider;

    void Start()
    {
        handCollider = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        other.transform.parent.TryGetComponent<XRGrabInteractable>(out grabInteractable);
        if (grabInteractable.isGrabbed)
        {
            handCollider.enabled = false;
            Debug.Log(other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {

            handCollider.enabled = true;
            Debug.Log("skibidi " + other.name);
    }


}
