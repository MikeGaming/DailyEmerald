using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HammerParticles : MonoBehaviour
{
    [SerializeField] XRGrabInteractable hammer;
    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hammer.isGrabbed)
        {
            particle.Emit(50);
            Debug.Log("pomegranate");
        }
    }
}
