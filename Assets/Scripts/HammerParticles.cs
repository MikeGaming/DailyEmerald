using UnityEngine;

public class HammerParticles : MonoBehaviour
{

    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        particle.Emit(50);
        Debug.Log("pomegranate");
    }
}
