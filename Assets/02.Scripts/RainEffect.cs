using System.Collections.Generic;
using UnityEngine;

public class RainEffect : MonoBehaviour
{
    [SerializeField] private GameObject splashPrefab;
    [SerializeField] private float splashScale = 0.5f;

    void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> events = new List<ParticleCollisionEvent>();
        int count = GetComponent<ParticleSystem>().GetCollisionEvents(other, events);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = events[i].intersection;
            GameObject splash = Instantiate(splashPrefab, pos, Quaternion.identity);
            splash.transform.localScale = Vector3.one * splashScale;
            Destroy(splash, 1f);
        }
    }
}
