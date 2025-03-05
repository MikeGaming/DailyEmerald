using System.Collections;
using UnityEngine;

public class OOBManager : MonoBehaviour
{

    //bool isOutOfBounds;

    [SerializeField] Transform landingPoint;

    private void OnTriggerExit(Collider other)
    {
        // Reset velocity of object and send it along a curve towards the landing point
        if (other.transform.GetComponentInParent<Rigidbody>())
        {
            Rigidbody rb = other.transform.GetComponentInParent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            StartCoroutine(MoveObject(rb.transform, landingPoint.position, 1f));
        }
    }

    private IEnumerator MoveObject(Transform objectToMove, Vector3 toPosition, float duration)
    {
        float counter = 0;
        Vector3 startPos = objectToMove.position;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            objectToMove.position = Vector3.Lerp(startPos, toPosition, counter / duration);
            yield return null;
        }
    }
}
