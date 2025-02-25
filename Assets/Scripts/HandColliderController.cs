using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandColliderController : MonoBehaviour
{
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
        if(other.transform.parent)
        {
            other.transform.parent.TryGetComponent<XRGrabInteractable>(out XRGrabInteractable grabInteractable);
            if(grabInteractable)
            {
                if (grabInteractable.isGrabbed)
                {
                    handCollider.enabled = false;
                    Debug.Log(other.name);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

            handCollider.enabled = true;
    }


}
