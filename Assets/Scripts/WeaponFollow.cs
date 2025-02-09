using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    //Script that linearly interpolates the weapon's position to the player's hand's position. So that when in VR, the weapon has weight to it.

    public Transform playerHand;
    public float followSpeed = 10f;


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerHand.position, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerHand.rotation, followSpeed * Time.deltaTime);

    }
}
