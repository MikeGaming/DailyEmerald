using UnityEngine;

public class HammerParticles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<ParticleSystem>().Play();
    }
}
