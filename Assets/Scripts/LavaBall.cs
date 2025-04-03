using UnityEngine;

public class LavaBall : MonoBehaviour
{
    public Enums.MaterialType materialType;

    public void KillLava()
    {
        Destroy(gameObject, 60f);
    }

}
