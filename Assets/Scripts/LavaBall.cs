using UnityEngine;

public class LavaBall : MonoBehaviour
{
    public Enums.MaterialType materialType;
    public LavaAnimation anim;

    public void KillLava()
    {
        Destroy(gameObject, 30f);
    }

}
