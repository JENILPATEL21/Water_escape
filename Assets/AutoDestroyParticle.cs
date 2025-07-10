using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour
{
    void Start()
    {
        float duration = GetComponent<ParticleSystem>().main.duration;
        Destroy(gameObject, duration);
    }
}
