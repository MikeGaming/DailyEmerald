using UnityEngine;
using System.Linq;

public class Seeder : MonoBehaviour
{
    [SerializeField] private Transform checkLoc;
    [SerializeField] private LayerMask hitLayers;

    public void SetSeed()
    {
        Collider[] objs = Physics.OverlapSphere(checkLoc.position, 0.5f, hitLayers);
        objs = objs.OrderBy((x) => (x.transform.position - checkLoc.position).sqrMagnitude).ToArray();

        int seed = Random.Range(0, 2048);
        if (objs.Length > 0)
        {
            seed = 0;
            for (int i = 0; i < objs[0].transform.root.name.Length; i++)
            {
                byte temp = (byte)objs[0].transform.root.name[i];
                seed += temp;
            }
        }
        Debug.Log(seed);
        Random.InitState(seed);
    }
}
