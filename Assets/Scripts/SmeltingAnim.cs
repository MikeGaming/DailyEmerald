using System.Collections;
using UnityEngine;

public class SmeltingAnim : MonoBehaviour
{
    [SerializeField] Transform ripcord;
    [SerializeField] Transform bucket;
    [SerializeField] float pullStrength = 4;

    private void Update()
    {
        bucket.localRotation = Quaternion.Euler(Mathf.Lerp(-180, 0, ripcord.GetComponent<SpringJoint>().currentForce.y/(60*pullStrength)+0.5f), -90, -90);
    }

}
