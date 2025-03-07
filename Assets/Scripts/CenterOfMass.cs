using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CenterOfMass : MonoBehaviour
{
    [SerializeField] Vector3 centerOfMass;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerOfMass;
    }

    private void Update()
    {
        #if UNITY_EDITOR
        _rb.centerOfMass = centerOfMass;
        _rb.WakeUp();
        #endif
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.rotation * centerOfMass, 0.05f);

        #if UNITY_EDITOR
        centerOfMass = Handles.PositionHandle(centerOfMass, Quaternion.identity);
        #endif
    }
}
