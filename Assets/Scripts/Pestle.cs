using UnityEngine;

public class Pestle : MonoBehaviour
{

    [SerializeField] private LayerMask mortarLayer;

    private void Update()
    {
        // check collision between the pestle and the mortar using sphere
        Collider[] mortars = Physics.OverlapSphere(transform.position, 0.5f, mortarLayer);

        if (mortars.Length > 0)
        {
            // toggle the collision of the pestle
            mortars[0].GetComponent<MortarSystem>().TogglePestleCollision(true);
        }
        else
        {
            mortars[0].GetComponent<MortarSystem>().TogglePestleCollision(false);
        }
    }
}
