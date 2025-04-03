using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HammerParticles : MonoBehaviour
{
    [SerializeField] XRGrabInteractable hammer;
    FMODUnity.StudioEventEmitter emitter;
    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hammer.isGrabbed)
        {
            particle.Emit(50);
            emitter.Play();
            Debug.Log("pomegranate");
        }

        switch (other.tag)
        {
            case "Metal":
            case "Blade":
            case "Head":
                emitter.SetParameter("HammeredMaterial", 0);
                break;

            case "Wood":
            case "Handle":
                emitter.SetParameter("HammeredMaterial", 1);
                break;

            case "Stone":
            case "Oven":
            case "Pestle":
                emitter.SetParameter("HammeredMaterial", 2);
                break;

            case "Player":
                emitter.SetParameter("HammeredMaterial", 3);
                break;

            case "CustomerSound":
                emitter.SetParameter("HammeredMaterial", 4);
                break;

            case "LightningCrystal":
            case "LightningCrystalBit":
                emitter.SetParameter("HammeredMaterial", 5);
                break;

            case "FlameCrystal":
            case "FlameCrystalBit":
                emitter.SetParameter("HammeredMaterial", 6);
                break;

            case "FrostCrystal":
            case "FrostCrystalBit":
                emitter.SetParameter("HammeredMaterial", 7);
                break;

            case "Money":
                emitter.SetParameter("HammeredMaterial", 8);
                break;
        }
    }
}
