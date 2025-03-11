using UnityEngine;

public class DeathOverTime : MonoBehaviour
{
    [SerializeField] private GameObject objectToDestroy;
    private void Update()
    {
    }

    public void StartDeath(float secondsToDeath)
    {
        Destroy(objectToDestroy, secondsToDeath);

    }
}
