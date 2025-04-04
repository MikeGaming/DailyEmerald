using UnityEngine;
using System.Linq;

public class Seeder : MonoBehaviour
{
    [SerializeField] private Transform checkLoc;
    [SerializeField] private LayerMask ignoreLayer;

    public void SetSeed()
    {
        Collider[] objs = Physics.OverlapSphere(checkLoc.position, 0.5f, Physics.AllLayers & ~(1 << ignoreLayer));
        objs = objs.OrderBy((x) => (x.transform.position - checkLoc.position).sqrMagnitude).ToArray();

        int seed = Mathf.FloorToInt(Time.realtimeSinceStartup);
        if (objs.Length > 0)
        {
            seed = 0;
            for (int i = 0; i < objs[0].name.Length; i++)
            {
                byte temp = (byte)objs[0].name[i];
                seed += temp;
            }
        }
        Random.InitState(seed);
    }
}
