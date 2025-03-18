using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] crystalPrefabs;
    [SerializeField] Transform spawnPointListParent;

    int[] crystalCounts = {0, 0, 0};

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCrystal), 0, 2.5f);
    }

    void SpawnCrystal()
    {
        if (crystalCounts[0] < 3)
        {
            Instantiate(crystalPrefabs[0], spawnPointListParent.GetChild(Random.Range(0, spawnPointListParent.childCount)).position, Quaternion.identity);
            crystalCounts[0]++;
        }
        if (crystalCounts[1] < 3)
        {
            Instantiate(crystalPrefabs[1], spawnPointListParent.GetChild(Random.Range(0, spawnPointListParent.childCount)).position, Quaternion.identity);
            crystalCounts[1]++;
        }
        if (crystalCounts[2] < 3)
        {
            Instantiate(crystalPrefabs[2], spawnPointListParent.GetChild(Random.Range(0, spawnPointListParent.childCount)).position, Quaternion.identity);
            crystalCounts[2]++;
        }
    }
}
