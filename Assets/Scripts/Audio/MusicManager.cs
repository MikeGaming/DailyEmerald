using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField, Range(0, 4)] private int intensity = 0;
    FMOD.Studio.EventInstance music;

    private void Start()
    {
        music = GetComponent<FMODUnity.StudioEventEmitter>().EventInstance;
        music.start();
    }

    private void Update()
    {
        music.setParameterByName("Intensity", intensity / 2);
    }

    public void SetIntensity(int intensity)
    {
        this.intensity = intensity;
    }
}
