using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private float time = 0;
    FMOD.Studio.EventInstance music;

    private void Start()
    {
        music = GetComponent<FMODUnity.StudioEventEmitter>().EventInstance;
        music.start();
    }

    private void Update()
    {
        if (time < 2.75)
        {
            music.setParameterByName("Intensity", 0);
        }
        else if (time < 3.4f)
        {
            music.setParameterByName("Intensity", 1);
        }
        else if (time < 4.3f)
        {
            music.setParameterByName("Intensity", 2);
        }
        else if (time < 8.5f)
        {
            music.setParameterByName("Intensity", 3);
        }
        else if (time < 9.7f)
        {
            music.setParameterByName("Intensity", 4);
        }
    }

    public void SetIntensity(float intensity)
    {
        this.time = intensity;
    }
}
