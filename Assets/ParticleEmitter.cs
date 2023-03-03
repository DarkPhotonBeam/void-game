using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    public List<Transform> particleOrigins;
    public GameObject particlePrefab;
    public float spawnRate = 1f;
    public int maxParticles = 100;

    private GameObject[] _particles;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _particles = new GameObject[maxParticles];
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= (1f / spawnRate))
        {
            timer = 0f;
            foreach (Transform particleOrigin in particleOrigins)
            {
                Instantiate(particlePrefab, particleOrigin.position, Quaternion.identity);
            }
        }
        
        timer += Time.deltaTime;
    }
}
