using System.Collections;
using UnityEngine;

public class OOBManager : MonoBehaviour
{

    //bool isOutOfBounds;

    [SerializeField] Transform landingPoint;

    private void OnTriggerExit(Collider other)
    {
        // Reset velocity of object and send it along a curve towards the landing point
        if (other.attachedRigidbody && !other.CompareTag("WoodBit") && !other.CompareTag("Head") && !other.CompareTag("ShelfObj"))
        {
            StartCoroutine(MoveObject(other.attachedRigidbody, landingPoint.position, 1f, 2f));
        }
    }

    private IEnumerator MoveObject(Rigidbody objectToMove, Vector3 toPosition, float duration, float moveDelay)
    {
        Vector3 initialScale = objectToMove.transform.localScale;
        yield return new WaitForSeconds(moveDelay);
        Vector3 startPos = objectToMove.position;
        // scale object size down to zero over time before moving it
        for(float counter = 0; counter < 2f; counter += Time.deltaTime)
        {
            objectToMove.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, counter / 2f);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        objectToMove.linearVelocity = Vector3.zero;
        objectToMove.angularVelocity = Vector3.zero;
        for (float counter = 0; counter < duration; counter+= Time.deltaTime)
        {
            objectToMove.MovePosition(toPosition);
            objectToMove.transform.localScale = initialScale;
            yield return null;
        }
    }
}
