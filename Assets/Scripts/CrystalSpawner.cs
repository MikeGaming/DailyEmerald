using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] crystalPrefabs;
    [SerializeField] Transform spawnPointListParent;

    //int[] crystalCounts = {0, 0, 0};
    public List<GameObject> crystals1 = new List<GameObject>();
    public List<GameObject> crystals2 = new List<GameObject>();
    public List<GameObject> crystals3 = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCrystal), 0, 2.5f);
    }

    void SpawnCrystal()
    {
        crystals1 = crystals1.Where(c => c != null).ToList();
        crystals2 = crystals2.Where(c => c != null).ToList();
        crystals3 = crystals3.Where(c => c != null).ToList();
        if (crystals1.Count < 3)
        {
            // prune missing/deleted/null crystals
            crystals1.Add(Instantiate(crystalPrefabs[0], spawnPointListParent.GetChild(Random.Range(0, spawnPointListParent.childCount)).position, Quaternion.identity));
        }
        if (crystals2.Count < 3)
        {
            crystals2.Add(Instantiate(crystalPrefabs[1], spawnPointListParent.GetChild(Random.Range(0, spawnPointListParent.childCount)).position, Quaternion.identity));
        }
        if (crystals3.Count < 3)
        {
            crystals3.Add(Instantiate(crystalPrefabs[2], spawnPointListParent.GetChild(Random.Range(0, spawnPointListParent.childCount)).position, Quaternion.identity));
        }
    }
}
