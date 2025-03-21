using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandColliderController : MonoBehaviour
{
    MeshCollider handCollider;
    [SerializeField] NearFarInteractor nearFarInteractor;
    bool triggered;

    void Start()
    {
        handCollider = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if(nearFarInteractor.hasSelection)
        {
            handCollider.enabled = false;
            triggered = true;
        }
        
        if (!nearFarInteractor.hasSelection && triggered)
        {
            triggered = false;
            StartCoroutine(ReenableCollision());
        }
    }

    private IEnumerator ReenableCollision()
    {
        yield return new WaitForSeconds(0.5f);
        handCollider.enabled = true;
    }
}
