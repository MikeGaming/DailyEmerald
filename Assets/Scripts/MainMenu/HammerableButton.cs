using UnityEngine;

public class HammerableButton : MonoBehaviour
{
    // Script that will be attached to the button that will be hammered
    // It will be hammerable by always trying to lerp to it's origin point, and also have a boolean trigger when it is hit with the hammer.

    public bool isHammered = false;
    Rigidbody rb;
    [SerializeField] Vector3 originPosition;
    [SerializeField] float hammerForce = 5f;

    private void Start()
    {
        // Set the origin position of the button
        rb = GetComponent<Rigidbody>();
        originPosition = transform.position;
    }

    private void Update()
    {
        // If the button is hammered, then move the button down
        if (!isHammered)
        {
            rb.Move(originPosition, transform.rotation);
        }
    }

    public void HammerButton()
    {
        // Set the button to be hammered
        isHammered = !isHammered;
        rb.AddForce(-Vector3.up * hammerForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the button is hit with the hammer, then hammer the button
        if (collision.gameObject.CompareTag("Hammer"))
        {
            HammerButton();
        }
    }

}
