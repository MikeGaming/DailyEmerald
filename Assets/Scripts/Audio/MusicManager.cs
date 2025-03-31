using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private int intensity = 0;
    FMOD.Studio.EventInstance music;
    [SerializeField] Timer timer;

    private void Start()
    {
        music = GetComponent<FMODUnity.StudioEventEmitter>().EventInstance;
        music.start();
    }

    private void Update()
    {
        music.setParameterByName("Intensity", intensity);
    }

    public void SetIntensity(int intensity)
    {
        this.intensity = intensity;
    }
}
