using UnityEngine;

public class DeathOverTime : MonoBehaviour
{
    [SerializeField] private GameObject objectToDestroy;

    float timeToDeath;

    bool die;
    float t;
    private void Update()
    {
        t += Time.deltaTime;
        //if (t >= timeToDeath) die = true;
        if ((die && t >= timeToDeath) || die)
        {
            Destroy(objectToDestroy);
            gameObject.SetActive(false);
        }
    }

    public void StartDeath(float secondsToDeath)
    {
        die = true;
        timeToDeath = secondsToDeath;
    }
}
