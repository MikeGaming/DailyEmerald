using UnityEngine;

public class Meltable : MonoBehaviour
{
    [SerializeField]
    public Enums.MaterialType oreType;

    //quantity system?

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Cauldron>(out Cauldron caul))
        {
            caul.meltingObjs.Add(this.gameObject);
            caul.TryMelt();
        }
    }
}