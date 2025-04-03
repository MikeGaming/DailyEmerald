using UnityEngine;

public class LavaKiller : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            other.GetComponent<LavaBall>().KillLava();
        }
    }
}
