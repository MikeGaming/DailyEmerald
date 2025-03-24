using UnityEngine;

public class DeathOverTime : MonoBehaviour
{
    [SerializeField] private GameObject objectToDestroy;

    public void StartDeath(float secondsToDeath)
    {
        Destroy(objectToDestroy, secondsToDeath);
    }
}
