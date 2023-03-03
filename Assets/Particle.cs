using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float lifeTime = 1f;

    private float _timer = 0f;
    private Color _color;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = new Color(1f, 1f, 1f, 1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer >= lifeTime)
        {
            Destroy(gameObject);
        }

        _color.a = 1 - (_timer / lifeTime);
        _spriteRenderer.color = _color;
        _timer += Time.deltaTime;
    }
}
