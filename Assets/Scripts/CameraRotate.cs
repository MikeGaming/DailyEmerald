using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] Transform mainCam;
    Transform portalCam;

    private void Start()
    {
        portalCam = transform;
    }

    private void Update()
    {
        Vector3 vector3 = portalCam.position - mainCam.position;
        portalCam.rotation = Quaternion.LookRotation(vector3, Vector3.up);
    }
}
