using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public float acceleration = 100f;
    public float speedLimit = 10f;
    public float torque = 10f;
    public float rotationLimit = 10f;
    public float cameraStiffness = 1f;
    public float cameraRange = 75f;
    public ParticleEmitter particleEmitter;

    public GameObject projectile;

    public List<Transform> weaponLocations;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _currentOffset;

    public float weaponRate = .1f;

    private float _weaponTimer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentOffset = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.magnitude <= speedLimit) // Thrust
        {
            Vector2 force = new Vector2(Mathf.Sin(_rigidbody.rotation * (Mathf.PI / 180)), -Mathf.Cos(_rigidbody.rotation * (Mathf.PI / 180))) * (Time.deltaTime * acceleration * Input.GetAxis("Throttle"));
            _rigidbody.AddForce(force);
        }
        
        if (_rigidbody.velocity.magnitude > speedLimit)
            _rigidbody.velocity = _rigidbody.velocity.normalized * speedLimit;

        if (Mathf.Abs(_rigidbody.angularVelocity) <= rotationLimit) // Rotation
        {
            //Debug.Log("BELOW");
            _rigidbody.AddTorque(-Input.GetAxis("Horizontal") * (Time.deltaTime * torque));
            
        }
        
        if (_rigidbody.angularVelocity > rotationLimit) _rigidbody.angularVelocity = rotationLimit;
        if (_rigidbody.angularVelocity < -rotationLimit) _rigidbody.angularVelocity = -rotationLimit;
        
        // Weapons

        if (Input.GetButton("Fire1") && _weaponTimer >= (1/weaponRate))
        {
            foreach (Transform weaponLocation in weaponLocations)
            {
                GameObject spawnedProjectile = Instantiate(projectile, weaponLocation.position, Quaternion.identity);
                spawnedProjectile.GetComponent<Rigidbody2D>().velocity = _rigidbody.velocity + new Vector2(Mathf.Sin(_rigidbody.rotation * (Mathf.PI / 180)), -Mathf.Cos(_rigidbody.rotation * (Mathf.PI / 180))) * -4f;
            }
            _weaponTimer = 0f;
        }
        

        // Camera
        var velocity = _rigidbody.velocity;
        Vector2 offsetPosition = -velocity.normalized * ((velocity.magnitude / speedLimit) * cameraRange);
        _currentOffset = Vector2.Lerp(_currentOffset, offsetPosition, cameraStiffness * Time.deltaTime);
        cameraTransform.position = _rigidbody.position + _currentOffset;

        particleEmitter.spawnRate = 1f + (Mathf.Abs(Input.GetAxis("Throttle")) * 19f);

        _weaponTimer += Time.deltaTime;
    }
}
